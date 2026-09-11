from pathlib import Path
import re
import sqlite3
import sys
import tempfile
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / "JCGAttendanceSystem"


def validate_xml():
    files = list(PROJECT.rglob("*.csproj")) + list(PROJECT.rglob("*.resx")) + list(PROJECT.rglob("*.config"))
    for path in files:
        ET.parse(path)
    return len(files)


def validate_resource_refs():
    resx = PROJECT / "Properties" / "Resources.resx"
    tree = ET.parse(resx)
    missing = []
    refs = 0
    for elem in tree.getroot().iter("value"):
        text = elem.text or ""
        if ";System." not in text:
            continue
        raw = text.split(";", 1)[0].strip().replace("\\", "/")
        candidate = (resx.parent / raw).resolve()
        refs += 1
        if not candidate.exists():
            missing.append(str(candidate))
    if missing:
        raise AssertionError("Missing resource files:\n" + "\n".join(missing))
    return refs


def strip_csharp(text):
    out = []
    i = 0
    n = len(text)
    state = "code"
    while i < n:
        c = text[i]
        nxt = text[i + 1] if i + 1 < n else ""
        if state == "code":
            if c == '/' and nxt == '/':
                state = "line_comment"; out.extend("  "); i += 2; continue
            if c == '/' and nxt == '*':
                state = "block_comment"; out.extend("  "); i += 2; continue
            if c == '@' and nxt == '"':
                state = "verbatim"; out.extend("  "); i += 2; continue
            if c == '"':
                state = "string"; out.append(' '); i += 1; continue
            if c == "'":
                state = "char"; out.append(' '); i += 1; continue
            out.append(c); i += 1; continue
        if state == "line_comment":
            if c == '\n': state = "code"; out.append('\n')
            else: out.append(' ')
            i += 1; continue
        if state == "block_comment":
            if c == '*' and nxt == '/': state = "code"; out.extend("  "); i += 2
            else: out.append('\n' if c == '\n' else ' '); i += 1
            continue
        if state == "string":
            if c == '\\': out.extend("  "); i += 2; continue
            if c == '"': state = "code"
            out.append(' '); i += 1; continue
        if state == "verbatim":
            if c == '"' and nxt == '"': out.extend("  "); i += 2; continue
            if c == '"': state = "code"
            out.append('\n' if c == '\n' else ' '); i += 1; continue
        if state == "char":
            if c == '\\': out.extend("  "); i += 2; continue
            if c == "'": state = "code"
            out.append(' '); i += 1; continue
    if state in {"string", "verbatim", "char", "block_comment"}:
        raise AssertionError("Unterminated C# lexical construct")
    return ''.join(out)


def validate_csharp_balance():
    pairs = {')': '(', ']': '[', '}': '{'}
    opens = set(pairs.values())
    checked = 0
    for path in PROJECT.rglob("*.cs"):
        cleaned = strip_csharp(path.read_text(encoding="utf-8-sig", errors="strict"))
        stack = []
        line = 1
        for ch in cleaned:
            if ch == '\n': line += 1
            if ch in opens: stack.append((ch, line))
            elif ch in pairs:
                if not stack or stack[-1][0] != pairs[ch]:
                    raise AssertionError(f"Delimiter mismatch in {path} near line {line}")
                stack.pop()
        if stack:
            raise AssertionError(f"Unclosed delimiter in {path}: {stack[-1]}")
        checked += 1
    return checked


def validate_forbidden_patterns():
    allowed_legacy = (PROJECT / "Data" / "Legacy").resolve()
    errors = []
    patterns = [
        (re.compile(r"[A-Za-z]:\\\\Users\\\\", re.I), "hardcoded user path"),
        (re.compile(r"E:\\\\", re.I), "hardcoded E drive path"),
        (re.compile(r"smtp\\.gmail\\.com", re.I), "embedded Gmail SMTP"),
    ]
    for path in list(PROJECT.rglob("*.cs")) + list(PROJECT.rglob("*.config")):
        text = path.read_text(encoding="utf-8-sig", errors="ignore")
        for rx, label in patterns:
            if rx.search(text): errors.append(f"{label}: {path}")
        if allowed_legacy not in path.resolve().parents:
            for token in ("OleDbConnection", "Jet.OLEDB", "ACE.OLEDB"):
                if token in text: errors.append(f"legacy provider outside importer ({token}): {path}")
    if errors:
        raise AssertionError("Forbidden legacy patterns found:\n" + "\n".join(errors))



def validate_schema_sql():
    source = (PROJECT / "Data" / "DatabaseInitializer.cs").read_text(encoding="utf-8-sig")
    statements = re.findall(r'Execute\(connection, transaction, @"(.*?)"\);', source, flags=re.S)
    statements += re.findall(r'Execute\(connection, transaction, "([^"\n]+)"\);', source)
    if len(statements) < 10:
        raise AssertionError("Could not locate the expected schema/index statements in DatabaseInitializer.cs")
    with tempfile.TemporaryDirectory() as td:
        db_path = Path(td) / "schema-test.db"
        connection = sqlite3.connect(str(db_path))
        try:
            connection.execute("PRAGMA foreign_keys = ON;")
            for statement in statements:
                connection.execute(statement.replace('""', '"'))
            connection.execute("INSERT INTO AppSettings(Key, Value, UpdatedAtUtc) VALUES('SchemaVersion','2','2026-09-06T00:00:00Z');")
            connection.commit()
            tables = {row[0] for row in connection.execute("SELECT name FROM sqlite_master WHERE type='table'")}
            expected = {"Users", "Students", "AttendanceRecords", "AuditLogs", "AppSettings"}
            if not expected.issubset(tables):
                raise AssertionError("Schema SQL did not create all required tables")
        finally:
            connection.close()
    return len(statements)

def validate_project_identity():
    sln = (ROOT / "JCGAttendanceSystem.sln").read_text(encoding="utf-8-sig")
    if "JCGAttendanceSystem\\JCGAttendanceSystem.csproj" not in sln:
        raise AssertionError("Solution does not reference modern project")
    csproj = (PROJECT / "JCGAttendanceSystem.csproj").read_text(encoding="utf-8-sig")
    for stale in ("PublishUrl", "ManifestKeyFile", "SignManifests", "Login with Database"):
        if stale in csproj:
            raise AssertionError("Stale project configuration remains: " + stale)


def main():
    xml_count = validate_xml()
    ref_count = validate_resource_refs()
    cs_count = validate_csharp_balance()
    sql_count = validate_schema_sql()
    validate_forbidden_patterns()
    validate_project_identity()
    print(f"Validated {xml_count} XML files, {ref_count} resource references, {cs_count} C# source files, and {sql_count} SQLite schema statements.")
    print("Project structural validation passed.")


if __name__ == "__main__":
    try:
        main()
    except Exception as exc:
        print("VALIDATION FAILED:", exc, file=sys.stderr)
        raise
