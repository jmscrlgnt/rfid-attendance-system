from pathlib import Path

ROOT = Path(__file__).resolve().parents[1] / "JCGAttendanceSystem"


def text(rel):
    return (ROOT / rel).read_text(encoding="utf-8")


def require(cond, msg):
    if not cond:
        raise AssertionError(msg)


login = text("Forms/Authentication/LoginForm.Designer.cs")
require("private TableLayoutPanel _loginHost;" in login, "login needs a dedicated centered host layout")
require("_loginHost.ColumnCount = 3" in login, "login host needs 3-column centering")
require("new ColumnStyle(SizeType.Absolute, 460F)" in login, "login card center column must have stable width")
require("_loginCard.Dock = DockStyle.Fill" in login, "login card should fill the protected center cell")
require("MinimumSize = new Size(1000, 640)" in login, "login minimum size should protect its two-column layout")

records = text("Forms/Records/RecordsForm.Designer.cs")
require("new RowStyle(SizeType.Absolute, 258F)" in records, "records filter area needs enough vertical space")
require("new RowStyle(SizeType.Absolute, 62F)" in records, "records action row must have fixed button-safe height")
for label in ("Apply Filters", "Clear", "Export CSV", "Correct Selected"):
    require(label in records, f"records action missing: {label}")
require("AlternatingRowsDefaultCellStyle" in records, "records grid should use alternating rows for readability")

attendance = text("Forms/Attendance/AttendanceForm.Designer.cs")
require("new RowStyle(SizeType.Absolute, 220F)" in attendance, "attendance results area should be compact and predictable")

dashboard = text("Forms/Dashboard/DashboardForm.Designer.cs")
for field in ("_totalAccent", "_presentAccent", "_timedInAccent", "_completedAccent"):
    require(field in dashboard, f"dashboard metric accent missing: {field}")
require("AlternatingRowsDefaultCellStyle" in dashboard, "dashboard grid should use alternating rows")

students = text("Forms/Students/StudentsForm.Designer.cs")
require("AlternatingRowsDefaultCellStyle" in students, "students grid should use alternating rows")

print("UI polish checks: PASS")
