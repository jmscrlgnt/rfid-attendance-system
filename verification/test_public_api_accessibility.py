from pathlib import Path
import re

root = Path(__file__).resolve().parents[1] / 'JCGAttendanceSystem'
model_dir = root / 'Models'
form_dir = root / 'Forms'

internal_models = set()
for p in model_dir.glob('*.cs'):
    text = p.read_text(encoding='utf-8')
    m = re.search(r'\binternal\s+(?:sealed\s+)?class\s+(\w+)', text)
    if m:
        internal_models.add(m.group(1))

failures = []
for p in form_dir.rglob('*.cs'):
    if p.name.endswith('.Designer.cs'):
        continue
    text = p.read_text(encoding='utf-8')
    if not re.search(r'\bpublic\s+partial\s+class\s+\w+\s*:\s*Form', text):
        continue
    for model in sorted(internal_models):
        patterns = [
            rf'\bpublic\s+\w+\s*\([^)]*\b{model}\b[^)]*\)',
            rf'\bpublic\s+{model}\b\s+\w+\s*\{{',
        ]
        if any(re.search(pattern, text, re.S) for pattern in patterns):
            failures.append(f'{p.relative_to(root)} exposes internal model {model}')

if failures:
    print('FAIL')
    for f in failures:
        print(' -', f)
    raise SystemExit(1)

print('PASS: public form APIs do not expose internal model types')
