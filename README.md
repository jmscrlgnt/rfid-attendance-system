# JCG Attendance System — Setup & Usage Instructions

## Quick Start

If Visual Studio and .NET Framework 4.8 are already installed:

1. Clone or download this repository.
2. Open `JCGAttendanceSystem.sln` in Visual Studio.
3. Allow NuGet packages to restore.
4. Select **Build > Clean Solution**.
5. Select **Build > Rebuild Solution**.
6. Confirm the project builds with **0 errors**.
7. Press **F5** to run the application.
8. On first launch, create the Administrator account when prompted.

> The application stores its SQLite database under `%LOCALAPPDATA%\JCGAttendanceSystem`, so the runtime data is separate from the GitHub source folder.

---

## Requirements

Before running the project, install:

1. **Visual Studio 2019 or 2022**
2. **.NET desktop development** workload
3. **.NET Framework 4.8 Developer Pack**
4. NuGet package restore enabled

The project uses `System.Data.SQLite.Core`. Visual Studio should restore the required NuGet packages automatically when the solution is opened or built.

---

## How to Run the Program

1. Clone or download this repository.

2. Place the project in a short local path when possible, for example:

```text
C:\JCGAttendanceSystem
```

3. Open:

```text
JCGAttendanceSystem.sln
```

> Open the `.sln` file instead of opening only the `.csproj` file.

4. In Visual Studio, restore NuGet packages if necessary:

```text
Build > Restore NuGet Packages
```

5. Clean the solution:

```text
Build > Clean Solution
```

6. Rebuild the solution:

```text
Build > Rebuild Solution
```

7. Confirm that Visual Studio reports:

```text
0 errors
```

8. Press **F5** or click **Start**.

---

## First-Time Setup

On a fresh installation, the application automatically creates its local folders and SQLite database.

The **Administrator Setup** screen will appear.

Create your own Administrator username and password.

> There are no default Administrator credentials.

---

## Editing Forms With Visual Studio Designer

The application uses standard Windows Forms Designer files.

Example:

```text
StudentsForm.cs
StudentsForm.Designer.cs
StudentsForm.resx
```

To edit a form:

1. Open **Solution Explorer**.
2. Locate the form you want to edit.
3. Right-click the main `.cs` file.
4. Select **View Designer**.

You can also press:

```text
Shift + F7
```

File responsibilities:

```text
StudentsForm.cs
```

Contains event handlers, service calls, validation, loading, and screen behavior.

```text
StudentsForm.Designer.cs
```

Contains controls, layout, sizes, positions, fonts, colors, docking, and other Designer-managed properties.

```text
StudentsForm.resx
```

Contains Windows Forms resources.

Whenever possible, modify the UI using:

```text
View Designer + Properties
```

instead of manually editing `.Designer.cs`.

Database logic, security code, password handling, and business rules should remain inside the appropriate services, repositories, and security classes.

---

## If the Designer Does Not Open

If Visual Studio displays:

```text
The base class System.Void cannot be designed
```

try the following:

1. Close all Designer tabs.
2. Run **Build > Clean Solution**.
3. Run **Build > Rebuild Solution**.
4. Reopen the form using **View Designer**.

If the problem remains:

1. Close Visual Studio.
2. Delete these generated folders:

```text
.vs
bin
obj
```

3. Reopen the solution.
4. Rebuild the project.
5. Open the Designer again.

---

## Application Data Location

The live SQLite database is stored outside the source-code folder:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Data\jcg_attendance.db
```

Database backups are stored in:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Backups\
```

Application logs are stored in:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Logs\
```

Because the database is outside the GitHub/project folder, recloning or replacing the source code does not automatically delete existing users, students, attendance records, settings, or audit history.

---

## Reset the Application for Testing

To return the system to a completely fresh installation:

1. Close JCG Attendance System.

2. Press:

```text
Win + R
```

3. Open:

```text
%LOCALAPPDATA%\JCGAttendanceSystem
```

4. Back up anything important.

5. Delete the `JCGAttendanceSystem` folder.

6. Start the program again.

The first-run Administrator setup will appear again.

> For normal usage, do not reset the application just to remove an administrator. Use **Settings > User Management**.

---

## Manual Attendance

To record attendance manually:

1. Open **Attendance**.
2. Search for a student.
3. Select the student.
4. Choose **Time In** or **Time Out**.

The system applies the following attendance rules:

- A student can Time In once per day.
- A student can Time Out only after Time In.
- A completed attendance day cannot be submitted again.
- Inactive students cannot record attendance.

---

## RFID Integration

The application is currently **RFID-ready**.

RFID hardware integration is handled through:

```text
IRfidReader
```

The included:

```text
NullRfidReader
```

allows the application to continue working in manual mode when no physical RFID reader is connected.

To add a real RFID reader:

1. Create a new class that implements `IRfidReader`.
2. Read the RFID tag from the hardware.
3. Pass the scanned tag value to the existing attendance workflow.
4. Keep attendance rules inside the service layer.

Do not duplicate attendance rules inside the RFID hardware implementation.

---

## Administrator Tools

Administrators can access **Settings** to:

- Create Staff accounts
- Activate or deactivate users
- Reset user passwords
- View the Audit Log
- Create database backups
- Restore validated SQLite backups
- Import supported legacy data
- Change the organization display name

Database restore automatically creates a safety backup before restoring.

Restart the application after completing a database restore.

---

## Legacy Database Import

The original Microsoft Access database is stored at:

```text
JCGAttendanceSystem\Legacy\Data.mdb
```

The normal application does not connect to this database.

It is only used for optional legacy-data migration.

The importer attempts to migrate supported:

```text
Students
Attendance Records
```

Old login passwords are intentionally not migrated.

Importing `.mdb` data may require the Microsoft Access Database Engine or a compatible OLE DB provider.

The normal SQLite-based application works even if the Access provider is not installed.

---

## Troubleshooting

### SQLite or NuGet Dependency Error

Restore packages:

```text
Build > Restore NuGet Packages
```

Then run:

```text
Build > Clean Solution
Build > Rebuild Solution
```

---

### Windows Path Too Long

If Windows displays:

```text
0x80010135: Path too long
```

move the repository to a shorter location, for example:

```text
C:\JCGAttendanceSystem
```

Then reopen the solution.

---

### Existing Data Is Missing

Check whether the application's database still exists at:

```text
%LOCALAPPDATA%\JCGAttendanceSystem\Data\jcg_attendance.db
```

Remember that the source-code repository and the runtime database are stored separately.

---

## Additional Documentation

More technical information is available in:

```text
docs/ARCHITECTURE.md
docs/TESTING.md
```
