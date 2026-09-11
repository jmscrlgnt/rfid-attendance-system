# Designer-Compatible WinForms UI Conversion Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Convert every user-facing JCG Attendance System form from runtime/code-built layouts to Visual Studio WinForms Designer-compatible partial forms while preserving the existing SQLite, service, repository, security, audit, backup, legacy import, and RFID-ready behavior.

**Architecture:** Business logic remains in the existing services/repositories. Each UI class is split into a behavior file (`FormName.cs`), a generated-style layout file (`FormName.Designer.cs`), and a resource file (`FormName.resx`). The project file explicitly marks form source files with `SubType=Form` and nests designer/resource files with `DependentUpon` so Visual Studio exposes **View Designer** correctly.

**Tech Stack:** C# 7.x-compatible syntax, Windows Forms, .NET Framework 4.8, System.Data.SQLite.Core 1.0.119.

**Spec:** Approved in-chat design: keep the v2.2 backend and convert the presentation layer to traditional Visual Studio Designer forms with improved spacing and polish.

## Global Constraints

- Keep .NET Framework 4.8 and the existing startup object `JCGAttendanceSystem.Program`.
- Preserve all current SQLite schema and runtime data paths.
- Do not change attendance rules, authentication rules, role checks, audit behavior, backup/restore semantics, or legacy import semantics unless required to preserve existing behavior.
- All user-facing forms must open in Visual Studio Designer without depending on runtime-created controls.
- Use standard WinForms controls and designer-safe property assignments in `InitializeComponent`.
- Preserve the existing navy/blue JCG visual identity while improving spacing, hierarchy, grids, empty states, and dialogs.

---

### Task 1: Designer project plumbing and regression checks

**Files:**
- Create: `verification/test_designer_structure.py`
- Modify: `JCGAttendanceSystem/JCGAttendanceSystem.csproj`

**Interfaces:**
- Produces: Visual Studio form nesting metadata and a structural test requiring all 14 forms to have `.Designer.cs` and `.resx` siblings.

- [ ] Write a failing structural test that checks every `Forms/**/*.cs` form class is `partial`, calls `InitializeComponent()`, has matching `.Designer.cs`/`.resx`, and has project nesting metadata.
- [ ] Run the test and confirm it fails against v2.2.
- [ ] Update the project item metadata to exclude Forms from the wildcard compile item and explicitly include behavior/designer/resource files.
- [ ] Re-run after converted forms are present.

### Task 2: Authentication and shell forms

**Files:**
- Modify/Create: `Forms/Authentication/LoginForm.cs`, `LoginForm.Designer.cs`, `LoginForm.resx`
- Modify/Create: `Forms/Authentication/SetupAdminForm.cs`, `SetupAdminForm.Designer.cs`, `SetupAdminForm.resx`
- Modify/Create: `Forms/Main/MainShellForm.cs`, `MainShellForm.Designer.cs`, `MainShellForm.resx`

**Interfaces:**
- Consumes: `AuthService`, `SessionContext`.
- Produces: Designer-editable login, first-run setup, and persistent sidebar shell.

- [ ] Move all control construction to `InitializeComponent` and preserve sign-in/setup/logout event handlers.
- [ ] Use explicit designer fields for username/password/error controls and navigation buttons.
- [ ] Keep child-page hosting logic in the behavior file.

### Task 3: Primary operational pages

**Files:**
- Convert: `DashboardForm`, `AttendanceForm`, `StudentsForm`, `StudentEditorForm`, `RecordsForm`, `AttendanceCorrectionForm` to `.cs/.Designer.cs/.resx` triples.

**Interfaces:**
- Consumes: existing dashboard, attendance, student, records, and export services.
- Produces: Designer-editable data pages with stable named controls.

- [ ] Preserve dashboard refresh/timer behavior and make metric cards/grid designer controls.
- [ ] Preserve manual/RFID attendance logic and make search/results/student detail controls designer fields.
- [ ] Preserve student CRUD/deactivation logic and make editor inputs designer fields.
- [ ] Preserve records filtering/export/correction logic and improve filter spacing.

### Task 4: Settings and administrator dialogs

**Files:**
- Convert: `SettingsForm`, `AuditLogForm`, `ChangePasswordForm`, `UserManagementForm`, `UserEditorForm` to `.cs/.Designer.cs/.resx` triples.

**Interfaces:**
- Consumes: `SettingsService`, `BackupService`, `AuditService`, `AuthService`, `LegacyImportService`.
- Produces: Designer-editable administration interface without behavior regressions.

- [ ] Preserve administrator visibility/enablement checks.
- [ ] Preserve backup/restore/import/user-management flows.
- [ ] Preserve audit loading and password-change validation.

### Task 5: Verification and delivery documentation

**Files:**
- Modify: `README.md`, `docs/ARCHITECTURE.md`, `docs/TESTING.md`
- Run: `verification/test_designer_structure.py`, `verification/test_modernization.py`, `verification/validate_project.py`, `tests/test_sql_insert_arity.py`

**Interfaces:**
- Produces: A clean delivery ZIP and instructions for opening each form with **View Designer**.

- [ ] Run all structural and modernization checks.
- [ ] Parse all `.resx`, `.csproj`, and `.sln` XML/text structures.
- [ ] Confirm no `BuildUi()` method remains in converted forms.
- [ ] Document Visual Studio Designer workflow and runtime database location.
- [ ] Package only source/resources/docs/tests; exclude `bin`, `obj`, `.vs`, and user-specific files.
