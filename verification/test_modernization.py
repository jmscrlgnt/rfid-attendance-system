import re
import unittest
import xml.etree.ElementTree as ET
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / "JCGAttendanceSystem"


class ModernizationTests(unittest.TestCase):
    def read(self, rel):
        return (ROOT / rel).read_text(encoding="utf-8-sig")

    def test_modern_project_exists(self):
        self.assertTrue((ROOT / "JCGAttendanceSystem.sln").exists())
        csproj = PROJECT / "JCGAttendanceSystem.csproj"
        self.assertTrue(csproj.exists())
        text = csproj.read_text(encoding="utf-8-sig")
        self.assertIn("<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>", text)
        self.assertIn("System.Data.SQLite.Core", text)
        ET.parse(csproj)

    def test_database_architecture(self):
        app_paths = self.read("JCGAttendanceSystem/Utilities/AppPaths.cs")
        database = self.read("JCGAttendanceSystem/Data/Database.cs")
        initializer = self.read("JCGAttendanceSystem/Data/DatabaseInitializer.cs")
        self.assertIn("Environment.SpecialFolder.LocalApplicationData", app_paths)
        self.assertIn("JCGAttendanceSystem", app_paths)
        self.assertIn("jcg_attendance.db", app_paths)
        self.assertIn("PRAGMA foreign_keys = ON", database)
        for table in ["Users", "Students", "AttendanceRecords", "AuditLogs", "AppSettings"]:
            self.assertIn("CREATE TABLE IF NOT EXISTS " + table, initializer)
        self.assertIn("UNIQUE(StudentId, AttendanceDate)", initializer)
        self.assertIn("FOREIGN KEY(StudentId) REFERENCES Students(Id)", initializer)
        self.assertIn("yyyy-MM-dd", initializer)
        for model in ["User.cs", "Student.cs", "AttendanceRecord.cs", "AuditLogEntry.cs", "DashboardSummary.cs", "AttendanceView.cs"]:
            self.assertTrue((PROJECT / "Models" / model).exists(), model)

    def test_security_architecture(self):
        hasher = self.read("JCGAttendanceSystem/Security/PasswordHasher.cs")
        session = self.read("JCGAttendanceSystem/Security/SessionContext.cs")
        users = self.read("JCGAttendanceSystem/Data/Repositories/UserRepository.cs")
        auth = self.read("JCGAttendanceSystem/Services/AuthService.cs")
        self.assertIn("Rfc2898DeriveBytes", hasher)
        self.assertIn("HashAlgorithmName.SHA256", hasher)
        self.assertRegex(hasher, r"Iterations\s*=\s*(?:1[0-9]{5,}|[2-9][0-9]{5,})")
        self.assertIn("RandomNumberGenerator", hasher)
        self.assertIn("FixedTimeEquals", hasher)
        self.assertIn("CurrentUser", session)
        self.assertIn("SessionUser", session)
        self.assertIn("IsAdministrator", session)
        self.assertNotIn("PasswordHash", session)
        self.assertNotIn("PasswordSalt", session)
        self.assertIn("_users.GetById(SessionContext.CurrentUser.Id)", auth)
        self.assertIn("CreateInitialAdministrator", auth)
        self.assertIn("Authenticate", auth)
        self.assertIn("last active administrator", auth.lower())
        self.assertNotIn("smtp.gmail.com", (PROJECT / "Services" / "AuthService.cs").read_text(encoding="utf-8-sig").lower())
        self.assertIn("@Username", users)
        self.assertNotIn("Password TEXT", users)

    def test_student_management_architecture(self):
        repo = self.read("JCGAttendanceSystem/Data/Repositories/StudentRepository.cs")
        service = self.read("JCGAttendanceSystem/Services/StudentService.cs")
        validation = self.read("JCGAttendanceSystem/Utilities/Validation.cs")
        for term in ["StudentNumber", "FirstName", "LastName", "Course", "YearLevel", "Section", "RfidTag", "IsActive"]:
            self.assertIn(term, service + repo)
        self.assertIn("YearLevel < 1 || student.YearLevel > 4", service)
        self.assertIn("IsValidEmail", validation)
        self.assertIn("already uses that student number", service.lower())
        self.assertIn("already uses that email", service.lower())
        self.assertIn("already uses that rfid tag", service.lower())
        self.assertIn("SET IsActive = @IsActive", repo)
        self.assertNotIn("DELETE FROM Students", repo)
        self.assertIn("@Query", repo)

    def test_attendance_architecture(self):
        repo = self.read("JCGAttendanceSystem/Data/Repositories/AttendanceRepository.cs")
        service = self.read("JCGAttendanceSystem/Services/AttendanceService.cs")
        rfid = self.read("JCGAttendanceSystem/RFID/IRfidReader.cs")
        result = self.read("JCGAttendanceSystem/Models/AttendanceActionResult.cs")
        self.assertIn("BeginTransaction", service)
        self.assertIn("TimeIn", service)
        self.assertIn("TimeOut", service)
        self.assertIn("inactive student", service.lower())
        self.assertIn("already timed in", service.lower())
        self.assertIn("already completed", service.lower())
        self.assertIn("AttendanceSources.Manual", service)
        self.assertIn("AttendanceSources.Rfid", service)
        self.assertIn("CorrectAttendance", service)
        self.assertIn("IsAdministrator", service)
        self.assertIn("TagScanned", rfid)
        self.assertIn("SQLiteTransaction", repo)
        self.assertIn("Success", result)

    def test_reporting_architecture(self):
        dashboard = self.read("JCGAttendanceSystem/Services/DashboardService.cs")
        repo = self.read("JCGAttendanceSystem/Data/Repositories/AttendanceRepository.cs")
        csvw = self.read("JCGAttendanceSystem/Utilities/CsvWriter.cs")
        export = self.read("JCGAttendanceSystem/Services/RecordExportService.cs")
        logger = self.read("JCGAttendanceSystem/Utilities/AppLogger.cs")
        for term in ["TotalActiveStudents", "PresentToday", "CurrentlyTimedIn", "CompletedToday"]:
            self.assertIn(term, dashboard + repo)
        self.assertIn("SearchRecords", repo)
        self.assertIn("AttendanceFilter", repo)
        self.assertNotIn("SELECT *", repo.upper())
        self.assertIn('value.Replace', csvw)
        self.assertIn('IndexOfAny', csvw)
        self.assertIn("ExportCsv", export)
        self.assertIn("LogsDirectory", logger)
        self.assertIn("GetRecentActivity", dashboard)

    def test_backup_and_legacy_architecture(self):
        backup = self.read("JCGAttendanceSystem/Services/BackupService.cs")
        legacy = self.read("JCGAttendanceSystem/Data/Legacy/LegacyImportService.cs")
        summary = self.read("JCGAttendanceSystem/Models/LegacyImportSummary.cs")
        self.assertIn("IsAdministrator", backup)
        self.assertIn("ValidateDatabase", backup)
        self.assertIn("safety", backup.lower())
        self.assertIn("CreateBackup", backup)
        self.assertIn("Restore", backup)
        self.assertIn("OleDbConnection", legacy)
        self.assertIn("Data1", legacy)
        self.assertIn("Time", legacy)
        self.assertNotIn("SELECT * FROM Login", legacy)
        self.assertNotIn("Password", legacy)
        self.assertIn("ImportedStudents", summary)
        self.assertTrue((PROJECT / "Legacy" / "Data.mdb").exists())

    def test_ui_boundary_and_shell(self):
        login = self.read("JCGAttendanceSystem/Forms/Authentication/LoginForm.cs")
        setup = self.read("JCGAttendanceSystem/Forms/Authentication/SetupAdminForm.cs")
        shell = self.read("JCGAttendanceSystem/Forms/Main/MainShellForm.cs")
        program = self.read("JCGAttendanceSystem/Program.cs")
        theme = self.read("JCGAttendanceSystem/Forms/UiTheme.cs")
        forms_text = "\n".join(p.read_text(encoding="utf-8-sig") for p in (PROJECT / "Forms").rglob("*.cs"))
        for forbidden in ["SQLiteConnection", "SQLiteCommand", "OleDbConnection", "Rfc2898DeriveBytes", "SELECT ", "INSERT INTO", "UPDATE "]:
            self.assertNotIn(forbidden, forms_text)
        self.assertIn("AuthService", login)
        self.assertIn("CreateInitialAdministrator", setup)
        self.assertIn("Navigate", shell)
        self.assertIn("SessionContext.CurrentUser", shell)
        self.assertIn("DatabaseInitializer.Initialize", program)
        self.assertIn("NeedsInitialAdminSetup", program)
        self.assertIn("Segoe UI", theme)
        self.assertNotIn("progressBar", program.lower())

    def test_core_feature_screens(self):
        dashboard = self.read("JCGAttendanceSystem/Forms/Dashboard/DashboardForm.cs") + self.read("JCGAttendanceSystem/Forms/Dashboard/DashboardForm.Designer.cs")
        attendance = self.read("JCGAttendanceSystem/Forms/Attendance/AttendanceForm.cs") + self.read("JCGAttendanceSystem/Forms/Attendance/AttendanceForm.Designer.cs")
        students = self.read("JCGAttendanceSystem/Forms/Students/StudentsForm.cs") + self.read("JCGAttendanceSystem/Forms/Students/StudentsForm.Designer.cs")
        editor = self.read("JCGAttendanceSystem/Forms/Students/StudentEditorForm.cs") + self.read("JCGAttendanceSystem/Forms/Students/StudentEditorForm.Designer.cs")
        records = self.read("JCGAttendanceSystem/Forms/Records/RecordsForm.cs") + self.read("JCGAttendanceSystem/Forms/Records/RecordsForm.Designer.cs")
        correction = self.read("JCGAttendanceSystem/Forms/Records/AttendanceCorrectionForm.cs") + self.read("JCGAttendanceSystem/Forms/Records/AttendanceCorrectionForm.Designer.cs")
        for term in ["Total Active Students", "Present Today", "Currently Timed In", "Completed Today"]:
            self.assertIn(term, dashboard)
        self.assertIn("GetRecentActivity", dashboard)
        self.assertIn("AttendanceSources.Manual", attendance)
        self.assertIn("AttendanceSources.Rfid", attendance)
        self.assertIn("IRfidReader", attendance)
        self.assertIn("Time In", attendance)
        self.assertIn("Time Out", attendance)
        self.assertIn("includeInactive", students)
        self.assertIn("StudentEditorForm", students)
        self.assertIn("RfidTag", editor)
        self.assertIn("AttendanceFilter", records)
        self.assertIn("ExportCsv", records)
        self.assertIn("IsAdministrator", records)
        self.assertIn("CorrectAttendance", correction)
        for src in [dashboard, attendance, students, records]:
            self.assertIn("DataGridViewTextBoxColumn", src)

    def test_settings_and_admin_screens(self):
        settings = self.read("JCGAttendanceSystem/Forms/Settings/SettingsForm.cs") + self.read("JCGAttendanceSystem/Forms/Settings/SettingsForm.Designer.cs")
        users = self.read("JCGAttendanceSystem/Forms/Settings/UserManagementForm.cs") + self.read("JCGAttendanceSystem/Forms/Settings/UserManagementForm.Designer.cs")
        editor = self.read("JCGAttendanceSystem/Forms/Settings/UserEditorForm.cs") + self.read("JCGAttendanceSystem/Forms/Settings/UserEditorForm.Designer.cs")
        audit = self.read("JCGAttendanceSystem/Forms/Settings/AuditLogForm.cs") + self.read("JCGAttendanceSystem/Forms/Settings/AuditLogForm.Designer.cs")
        password = self.read("JCGAttendanceSystem/Forms/Settings/ChangePasswordForm.cs") + self.read("JCGAttendanceSystem/Forms/Settings/ChangePasswordForm.Designer.cs")
        auth = self.read("JCGAttendanceSystem/Services/AuthService.cs")
        for term in ["Change Password", "Backup", "Restore", "Legacy", "User Management", "Audit"]:
            self.assertIn(term, settings)
        self.assertIn("SessionContext.IsAdministrator", settings)
        self.assertIn("GetUsersForAdministrator", users)
        self.assertIn("SetUserActive", users)
        self.assertIn("CreateStaff", editor)
        self.assertIn("GetRecentForAdministrator", audit)
        self.assertIn("ChangeOwnPassword", password)
        self.assertIn("ResetUserPassword", auth)
        self.assertIn("DataGridViewTextBoxColumn", users)
        self.assertIn("DataGridViewTextBoxColumn", audit)

    def test_final_cleanliness_and_docs(self):
        source_files = list(PROJECT.rglob("*.cs")) + list(PROJECT.rglob("*.config"))
        all_text = "\n".join(p.read_text(encoding="utf-8-sig", errors="ignore") for p in source_files)
        self.assertNotRegex(all_text, r"[A-Za-z]:\\Users\\")
        self.assertNotIn("E:\\Latest", all_text)
        self.assertNotIn("smtp.gmail.com", all_text.lower())
        nonlegacy = "\n".join(p.read_text(encoding="utf-8-sig", errors="ignore") for p in source_files if "Data/Legacy" not in p.as_posix())
        self.assertNotIn("OleDbConnection", nonlegacy)
        self.assertNotIn("Jet.OLEDB", nonlegacy)
        self.assertNotIn("ACE.OLEDB", nonlegacy)
        for old_name in ["class Form3", "class List", "class MainForm", "class Email"]:
            self.assertNotIn(old_name, all_text)
        for doc in ["README.md", "docs/TESTING.md", "docs/ARCHITECTURE.md"]:
            self.assertTrue((ROOT / doc).exists(), doc)
        readme = self.read("README.md")
        self.assertIn("Visual Studio", readme)
        self.assertIn("SQLite", readme)
        self.assertIn("first-run", readme.lower())
        self.assertIn("RFID", readme)


if __name__ == "__main__":
    unittest.main()
