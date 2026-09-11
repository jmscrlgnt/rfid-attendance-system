# JCG Attendance System v3 Architecture

## Overview

The project separates the Visual Studio Designer-editable WinForms presentation layer from business logic and data access.

```text
WinForms UI (.cs + .Designer.cs + .resx)
    |
    v
Services / Business Rules
    |
    v
Repositories
    |
    v
SQLite Database
```

Cross-cutting helpers provide password security, session state, logging, CSV export, application paths, backup/restore, and RFID abstraction.

## WinForms Presentation Structure

Every user-facing form uses the conventional WinForms partial-class pattern:

```text
Forms/<Area>/ExampleForm.cs
Forms/<Area>/ExampleForm.Designer.cs
Forms/<Area>/ExampleForm.resx
```

Responsibilities are intentionally split:

- `ExampleForm.cs` contains event handlers, service calls, state, data binding, and screen behavior.
- `ExampleForm.Designer.cs` contains `InitializeComponent()`, control declarations, and visual/layout properties managed by Visual Studio Designer.
- `ExampleForm.resx` contains Designer-managed resources.

The `.csproj` explicitly associates `Designer.cs` and `.resx` files with their parent form through `DependentUpon`, and parent forms use `<SubType>Form</SubType>`. This lets Visual Studio provide **View Designer** and nest the related files in Solution Explorer.

### Designer rule

Designer files must remain presentation-only. Do not place SQL, authentication, password hashing, attendance rules, or database connection logic in `*.Designer.cs`. Visual Studio is allowed to regenerate Designer code, so durable behavior belongs in the form's main `.cs` file or, preferably, the service layer.

## Folder Responsibilities

### `Forms/`

Presentation only. Forms collect input, display data, call services, and show user-friendly errors. Forms must not contain SQL, password derivation, or database connection strings.

Main areas:

- `Forms/Authentication/` — first administrator setup and sign-in
- `Forms/Main/` — application shell/navigation
- `Forms/Dashboard/` — metrics and recent activity
- `Forms/Attendance/` — manual/RFID-ready attendance workflow
- `Forms/Students/` — student list/editor
- `Forms/Records/` — filters, export, administrator correction
- `Forms/Settings/` — account settings and administrator tools

`Forms/UiTheme.cs` provides shared colors/fonts and small presentation helpers; it does not contain business rules.

### `Models/`

Plain data objects such as `User`, `Student`, `AttendanceRecord`, `AttendanceView`, and filters/results.

### `Data/`

`Database` creates SQLite connections. `DatabaseInitializer` creates the schema. `Repositories/` contains parameterized SQL and row mapping.

`Data/Legacy/LegacyImportService.cs` is the only part of the application allowed to use `OleDb` because it is isolated migration code for the old Access file.

### `Services/`

Business rules and orchestration:

- `AuthService` — authentication, role-aware user management, password changes
- `StudentService` — student validation and lifecycle
- `AttendanceService` — Time In, Time Out, corrections
- `DashboardService` — current-day metrics/activity
- `RecordsService` — filtered attendance retrieval
- `RecordExportService` — CSV export
- `BackupService` — backup/restore
- `AuditService` — audit events
- `SettingsService` — non-secret application settings

### `Security/`

`PasswordHasher` derives password hashes with PBKDF2-HMAC-SHA256. `SessionContext` stores only the currently authenticated system user identity/role.

### `RFID/`

`IRfidReader` isolates hardware. `NullRfidReader` is the safe default. A future hardware reader emits tags through `TagScanned`; the existing `AttendanceService` remains the only place that decides whether a Time In/Time Out is valid.

### `Utilities/`

Reusable local helpers such as `AppPaths`, `Validation`, `CsvWriter`, `AppLogger`, and UI-independent formatting/support.

## SQLite Schema

Core tables:

- `Users`
- `Students`
- `AttendanceRecords`
- `AuditLogs`
- `AppSettings`

`AttendanceRecords` has a database-level unique constraint on `(StudentId, AttendanceDate)`. This protects duplicate daily attendance even if a UI bug attempts a second insert.

Date/time approach:

- `AttendanceDate`: local attendance day in `yyyy-MM-dd`
- Time In / Time Out / audit timestamps: UTC ISO-8601 strings
- UI converts UTC timestamps back to local time for display

## Authentication Flow

```text
Program startup
  -> AppPaths.EnsureDirectories()
  -> DatabaseInitializer.Initialize()
  -> no users? SetupAdminForm
  -> LoginForm
  -> AuthService.Authenticate()
  -> SessionContext
  -> MainShellForm
```

There is no default Administrator password.

## Attendance Flow

```text
Manual search -----------\
                         > Student -> AttendanceService -> AttendanceRepository -> SQLite
IRfidReader.TagScanned --/
```

The same service rules apply to both sources.

## Role Model

### Staff

- dashboard
- attendance
- student management
- records/filter/export
- own password change

### Administrator

Includes all Staff capabilities plus:

- system-user management
- attendance correction
- audit history
- backup/restore
- legacy import
- high-impact settings

Service-level checks protect administrator operations even if a UI control is accidentally exposed.

## Runtime/Upgrade Boundary

The live database is outside the solution under `%LOCALAPPDATA%\JCGAttendanceSystem`. UI/source upgrades therefore do not replace the user's live data. v3 intentionally uses the same path as v2.2.

## Deployment Boundary

The program is currently a single-PC desktop application. SQLite is local to that Windows user. Multiple computers do not synchronize automatically.

If a future deployment requires multiple kiosks sharing one attendance database, do not place the SQLite file on a shared network folder. Introduce a central API/server and keep the WinForms app as a client.
