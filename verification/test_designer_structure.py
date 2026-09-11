from pathlib import Path
import re
import unittest
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / "JCGAttendanceSystem"
FORMS = [
    "Forms/Authentication/LoginForm",
    "Forms/Authentication/SetupAdminForm",
    "Forms/Main/MainShellForm",
    "Forms/Dashboard/DashboardForm",
    "Forms/Attendance/AttendanceForm",
    "Forms/Students/StudentsForm",
    "Forms/Students/StudentEditorForm",
    "Forms/Records/RecordsForm",
    "Forms/Records/AttendanceCorrectionForm",
    "Forms/Settings/SettingsForm",
    "Forms/Settings/AuditLogForm",
    "Forms/Settings/ChangePasswordForm",
    "Forms/Settings/UserManagementForm",
    "Forms/Settings/UserEditorForm",
]

class DesignerStructureTests(unittest.TestCase):
    def test_every_form_has_designer_triple(self):
        for stem in FORMS:
            behavior = PROJECT / f"{stem}.cs"
            designer = PROJECT / f"{stem}.Designer.cs"
            resx = PROJECT / f"{stem}.resx"
            self.assertTrue(behavior.exists(), stem)
            self.assertTrue(designer.exists(), f"Missing designer for {stem}")
            self.assertTrue(resx.exists(), f"Missing resx for {stem}")
            ET.parse(resx)
            text = behavior.read_text(encoding="utf-8-sig")
            self.assertRegex(text, r"partial\s+class\s+" + re.escape(Path(stem).name))
            self.assertIn("InitializeComponent();", text)
            self.assertNotIn("BuildUi()", text)
            dtext = designer.read_text(encoding="utf-8-sig")
            self.assertIn("void InitializeComponent()", dtext)
            self.assertIn("partial class " + Path(stem).name, dtext)
            # Keep Designer files conventional so Visual Studio can safely round-trip them.
            self.assertNotIn("=>", dtext, f"Lambda found in designer for {stem}")
            self.assertNotRegex(dtext, r"\b(?:for|foreach)\s*\(", f"Loop found in designer for {stem}")
            self.assertNotRegex(dtext, r"\bvar\s+", f"Implicit local found in designer for {stem}")
            self.assertNotRegex(dtext, r"private\s+static\s+", f"Helper method found in designer for {stem}")

    def test_every_form_has_parameterless_constructor_for_designer(self):
        for stem in FORMS:
            behavior = PROJECT / f"{stem}.cs"
            name = Path(stem).name
            text = behavior.read_text(encoding="utf-8-sig")
            self.assertRegex(
                text,
                r"(?:public|internal|private)\s+" + re.escape(name) + r"\s*\(\s*\)",
                f"{name} needs a true zero-argument constructor for reliable WinForms Designer loading",
            )

    def test_project_nests_designer_files(self):
        text = (PROJECT / "JCGAttendanceSystem.csproj").read_text(encoding="utf-8-sig")
        for stem in FORMS:
            rel = stem.replace('/', '\\')
            name = Path(stem).name
            self.assertIn(f'<Compile Include="{rel}.cs">', text)
            self.assertIn('<SubType>Form</SubType>', text)
            self.assertIn(f'<Compile Include="{rel}.Designer.cs">', text)
            self.assertIn(f'<DependentUpon>{name}.cs</DependentUpon>', text)
            self.assertIn(f'<EmbeddedResource Include="{rel}.resx">', text)

if __name__ == "__main__":
    unittest.main()
