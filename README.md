# JCG Attendance System v3.1.4 — Accessibility + Path-Safe Designer Build

This maintenance release keeps the v3.1 UI and backend behavior, and fixes two Visual Studio/WinForms Designer issues found during live testing.



## v3.1.4 C# accessibility fix

This build fixes the four Visual Studio compiler errors `CS0051` / `CS0053` introduced when the WinForms classes were made public for Designer stability. The model types `AttendanceView`, `User`, and `Student` are now public as well, so public form constructors/properties no longer expose less-accessible types. No database schema or attendance behavior changed.

## v3.1.3 Windows extraction/path update

This build shortens legacy image/resource filenames and is packaged under a short `JCG-v3.1.3` root folder. The longest project-relative file path is now capped by an automated Windows path-budget check, preventing the Explorer `0x80010135: Path too long` error seen when extracting under Desktop/OneDrive paths. Resource keys exposed to the application remain unchanged; only the backing filenames were shortened.

## v3.1.2 Designer Stability Update

This build hardens Visual Studio WinForms Designer support. All root forms now use conventional `public partial class ... : Form` declarations. Runtime service/database objects are created only after `InitializeComponent()` and are skipped when Visual Studio is in design mode. Editor dialogs such as Attendance Correction, Student Editor, and User Editor keep a pure parameterless constructor for the Designer while their runtime overloads retain normal behavior.

If Visual Studio previously displayed `The base class System.Void cannot be designed`, close all Designer tabs, clean/rebuild the solution, then reopen the form with **View Designer**. If Visual Studio cached an older failure, close Visual Studio and delete the solution's `.vs`, `bin`, and `obj` folders before reopening.

## v3.1.1 fixes

- Fixed the Attendance **Find a student** TextBox/Search button being vertically clipped.
- Reworked DataGridView columns into named Designer components instead of inline object initializers on Dashboard, Attendance, Students, Records, Audit Log, and User Management.
- Added explicit design-time project metadata for `Properties\Resources.Designer.cs` to resolve the `ResXFileCodeGenerator` warning.
- Preserves the same SQLite database path, so existing users/students/attendance data remain available.

> After extracting, use **Build > Clean Solution**, **Build > Rebuild Solution**, then close and reopen any Designer tabs before choosing **View Designer** again.

---

# JCG Attendance System v3.1 — Designer UI Polish Edition

JCG Attendance System v3.1 is a modernized C# WinForms attendance application built from the original student RFID attendance project. It keeps the SQLite/service/repository backend introduced in v2.2 and upgrades the presentation layer so the forms can be opened and edited with the standard **Visual Studio Windows Forms Designer**.


## What Changed in v3.1

- Fixed the Login screen clipping issue by placing the sign-in card inside a protected responsive center layout.
- Added a **Show password** option without changing authentication behavior.
- Fixed the Records filter action row so **Apply Filters, Clear, Export CSV, and Correct Selected** remain fully visible.
- Added subtle dashboard metric accent bars and clearer status colors.
- Reduced unused Attendance-page space and turned the RFID/manual-mode notice into a clearer status banner.
- Added alternating grid rows and softer selection colors across Dashboard, Attendance, Students, Records, Audit Log, and User Management.
- Added status-aware text colors for student/user states, attendance status, audit login outcomes, and recent attendance events.
- Tightened the main shell spacing while preserving Designer editability and all existing SQLite/service behavior.

## What Changed in v3

- All 14 user-facing forms now use the conventional WinForms file structure:

  ```text
  FormName.cs
  FormName.Designer.cs
  FormName.resx
  ```

- Forms can be opened with **View Designer** in Visual Studio.
- Layout code is separated from event/business orchestration.
- The Records filters were reorganized so date and filter controls have usable space.
- Student editor and administration dialogs use more balanced spacing.
- The existing SQLite database, attendance rules, authentication, audit logging, backup/restore, and RFID-ready service architecture are preserved.

## Technology

- C# / Windows Forms
- .NET Framework 4.8
- SQLite through `System.Data.SQLite.Core`
- PBKDF2-HMAC-SHA256 password hashing
- Visual Studio 2019 or 2022

## Main Features

- First-run Administrator setup with no default password
- Administrator and Staff roles
- Secure password hashing and local session handling
- Student CRUD, activation/deactivation, search, and RFID tag assignment
- Manual Time In / Time Out workflow
- RFID-ready `IRfidReader` integration boundary
- One attendance record per student per day
- Dashboard statistics and recent attendance activity
- Attendance filtering, CSV export, and administrator corrections
- Audit logging
- SQLite backup and restore
- Optional best-effort import from the original Microsoft Access `Data.mdb`
- Local diagnostic logs
- Designer-editable WinForms UI

## Requirements

Install Visual Studio 2019 or 2022 with:

1. **.NET desktop development** workload
2. **.NET Framework 4.8 Developer Pack**
3. NuGet package restore enabled

The solution uses `System.Data.SQLite.Core` from NuGet. Visual Studio should restore the package automatically when the solution is opened or built.

## Run the Program

1. Extract the project to a normal local folder such as `C:\JCG-v3.1.4`.
2. Open **`JCGAttendanceSystem.sln`**. Do not open only the `.csproj` file.
3. In Visual Studio, choose **Build > Restore NuGet Packages** if restore does not start automatically.
4. Choose **Build > Clean Solution**.
5. Choose **Build > Rebuild Solution**.
6. Confirm the build reports `0 errors`.
7. Press **F5** or click **Start**.

On a completely fresh Windows profile, the program creates its local folders and SQLite database, then opens the **first-run Administrator setup** screen. Create your own Administrator username and password. No default Administrator credentials are shipped.

## Editing the UI with Visual Studio Designer

The v3 forms are intended to be maintained through the normal WinForms Designer workflow.

For example:

1. In **Solution Explorer**, expand `Forms > Students`.
2. Right-click `StudentsForm.cs`.
3. Choose **View Designer** or press **Shift+F7**.
4. Expand the form node if you want to see its nested files:

   ```text
   StudentsForm.cs
     StudentsForm.Designer.cs
     StudentsForm.resx
   ```

Use the files for these responsibilities:

- **`StudentsForm.cs`** — event handlers, service calls, data loading, validation responses, and screen behavior.
- **`StudentsForm.Designer.cs`** — controls, sizes, positions, docking, fonts, colors, and other Designer-managed layout properties.
- **`StudentsForm.resx`** — WinForms resources used by the Designer.

When changing the interface, prefer using **View Designer + Properties** rather than manually editing `Designer.cs`. Visual Studio may regenerate Designer code. Keep SQL, password/security code, and business rules out of form files; those belong in repositories/services/security classes.

## Runtime Data Location

The live database is stored outside the source folder:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Data\jcg_attendance.db
```

Other generated data is stored under:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Backups\
%LOCALAPPDATA%\JCGAttendanceSystem\Logs\
```

### Important when upgrading from v2.2

v3 intentionally uses the **same runtime data location** as v2.2. If you run v3 under the same Windows user account, your existing Administrator, students, attendance records, settings, and audit history should remain available because they are stored outside the source ZIP.

If you want a completely fresh test instead, back up the database first and then remove `%LOCALAPPDATA%\JCGAttendanceSystem` while the application is closed.

## Reset the Program During Development

To return to a completely fresh installation during testing:

1. Close JCG Attendance System.
2. Press `Win + R`.
3. Open `%LOCALAPPDATA%\JCGAttendanceSystem`.
4. Back up anything important first.
5. Delete the folder.
6. Start the program again.

The first-run Administrator setup will appear again.

For a real deployment, do **not** reset the folder to remove an administrator. Use **Settings > User Management** for account management. The system prevents deactivation of the last active Administrator.

## Attendance Workflow

### Manual mode

Open **Attendance**, search for a student, select the student, then use **Time In** or **Time Out**. The service enforces these rules:

- A student can Time In once per day.
- A student can Time Out only after Time In.
- A completed attendance day cannot be submitted again.
- Inactive students cannot record new attendance.

### RFID mode

The current release is **RFID-ready**, not tied to a specific physical reader. `IRfidReader` is the hardware boundary. The included `NullRfidReader` keeps manual mode working until a real reader model/protocol is known.

When hardware is available, implement another `IRfidReader` class (for example a serial-port reader) and pass scanned tag values into the same attendance service. Do not duplicate attendance rules inside the hardware class.

## Administrator Tools

Administrators can open **Settings** to:

- create Staff accounts;
- activate/deactivate users;
- reset a system-user password;
- view the Audit Log;
- create a database Backup;
- Restore a validated JCG SQLite backup;
- Import Legacy Data from the old Access project;
- change the organization display name.

Restore creates an automatic pre-restore safety backup and requires an application restart afterward.

## Legacy Access Data

The original `Data.mdb` is included under `JCGAttendanceSystem\Legacy\Data.mdb` only as a migration/reference file. Normal application operation never connects to it.

Legacy import is best-effort because the old database contains inconsistent field formats. It imports students and attendance when possible and reports rows it cannot safely normalize. Old login passwords are intentionally **not** migrated.

Importing `.mdb` data requires a compatible Microsoft Access Database Engine / OLE DB provider on Windows. If it is not installed, the SQLite application still works normally.

## Security Note About the Original Project

The uploaded original source contained a mail application credential directly in code. The modernized project does not contain that credential or SMTP functionality. Any previously exposed mail/app password from the old source should be revoked or rotated in the relevant account before the old code is shared publicly.

## Documentation

- [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md) — layer responsibilities and UI/data flow.
- [`docs/TESTING.md`](docs/TESTING.md) — Visual Studio build, Designer, and functional test checklist.

## Version Notes

### v3.1 UI Polish Edition
- Responsive Login center-host layout prevents the credential card from collapsing/clipping.
- Records filter action row has fixed button-safe height.
- Dashboard, grids, Attendance mode banner, and status styling were polished.
- All v3 Designer-form structure and v2.2 backend behavior are retained.

### v3 Designer Edition
- Converted all 14 user-facing forms to `.cs + .Designer.cs + .resx`.
- Added explicit project metadata so Designer and resource files nest under each form in Solution Explorer.
- Preserved v2.2 backend behavior and runtime database location.
- Improved Records filter spacing and several dialog layouts.

### v2.2 Hotfix carried forward
- Fixed first-run administrator creation failing with `SQL logic error: 6 values for 7 columns`.
- Added regression validation for SQL INSERT column/value counts.
