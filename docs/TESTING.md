# JCG Attendance System v3.1 Testing Checklist

## 1. Visual Studio / Designer Verification

1. Open **`JCGAttendanceSystem.sln`** in Visual Studio 2019/2022.
2. In Solution Explorer, expand `Forms > Students`.
3. Confirm `StudentsForm.cs` has nested `StudentsForm.Designer.cs` and `StudentsForm.resx` files.
4. Right-click `StudentsForm.cs` and choose **View Designer** (or press **Shift+F7**).
5. Confirm the Students screen opens visually rather than as source code.
6. Repeat for at least:
   - `LoginForm.cs`
   - `MainShellForm.cs`
   - `DashboardForm.cs`
   - `AttendanceForm.cs`
   - `RecordsForm.cs`
   - `SettingsForm.cs`
7. Select a harmless label/control in Designer and confirm the Properties window can edit visual properties such as `Text`, `Font`, `Size`, or `Location`.
8. Undo the test change if you do not want to keep it.

If **View Designer** is missing, make sure you opened the `.sln`, not an individual loose source file, and let Visual Studio finish loading/restoring the project.


## 1A. v3.1 Responsive UI Checks

1. Open Login at the default window size and confirm the entire login card is visible.
2. Resize Login down to its minimum size and confirm username, password, Show password, Sign In, and the storage note remain inside the card.
3. Toggle **Show password** and confirm only password masking changes.
4. Open Records and confirm all four action buttons are fully visible: **Apply Filters**, **Clear**, **Export CSV**, and **Correct Selected** (Administrator only).
5. Confirm Dashboard metric cards show their thin colored accent strips.
6. Confirm Dashboard, Attendance, Students, Records, Audit Log, and User Management grids use alternating row shading and a soft blue selection state.
7. Open Attendance and confirm the manual/RFID status banner is visible and the search/results/details sections fit without unnecessary clipping.

## 2. Build Verification

On Windows with Visual Studio 2019/2022:

1. Restore NuGet packages.
2. Build > Clean Solution.
3. Build > Rebuild Solution.
4. Confirm the Output window reports the project was built, not skipped.
5. Confirm there are **0 build errors**.

The development container used to prepare this source does not contain the Windows .NET Framework/MSBuild toolchain, so this Visual Studio build is the authoritative compile check.

## 3. Existing v2.2 Data Upgrade Test

If v2.2 was already used under the same Windows user:

1. Back up `%LOCALAPPDATA%\JCGAttendanceSystem\Data\jcg_attendance.db`.
2. Run v3 without deleting `%LOCALAPPDATA%\JCGAttendanceSystem`.
3. Confirm the existing Administrator can log in.
4. Confirm existing students remain visible.
5. Confirm previous attendance/audit entries remain visible.

This verifies that the Designer conversion changed the source/UI layer without resetting runtime data.

## 4. Fresh First Run

For a clean test, close the app and remove `%LOCALAPPDATA%\JCGAttendanceSystem` after backing up anything important.

1. Start the app.
2. Confirm first-run Administrator setup opens.
3. Try a username shorter than 3 characters: it should be rejected.
4. Try a password shorter than 8 characters: it should be rejected.
5. Enter mismatching password confirmation: it should be rejected.
6. Create the Administrator.
7. Sign in using the new account.

## 5. Authentication / Users

1. Sign in with a wrong password: login must fail.
2. Open Settings > User Management as Administrator.
3. Create a Staff account.
4. Logout and sign in as Staff.
5. Confirm User Management, Audit, Backup/Restore, Legacy Import, and attendance correction are not available to Staff.
6. Change the Staff account's own password.
7. Login with the old password: it must fail.
8. Login with the new password: it must succeed.
9. As Administrator, test Staff activation/deactivation.
10. Confirm the last active Administrator cannot be deactivated.

## 6. Students

1. Add a student with student number, name, course, year, and section.
2. Add optional email and RFID tag.
3. Attempt a duplicate student number: reject it.
4. Attempt duplicate non-empty email: reject it.
5. Attempt duplicate non-empty RFID tag: reject it.
6. Attempt year below 1 or above 4: reject it.
7. Search by number, name, email, course, year, and section.
8. Edit a student and confirm values persist after restarting the app.
9. Deactivate the student and enable Show inactive.
10. Reactivate the student.

## 7. Attendance

Use an active student.

1. Search/select the student.
2. Confirm status says not timed in.
3. Click Time In.
4. Confirm Time In becomes disabled and Time Out becomes enabled.
5. Attempt another Time In through any available path: it must not create a duplicate record.
6. Click Time Out.
7. Confirm both buttons become disabled for the completed day.
8. Attempt another Time Out: it must be rejected.
9. Deactivate a different student and verify new attendance is rejected.

## 8. Dashboard

After recording attendance:

1. Total Active Students matches active students.
2. Present Today increments once per student with a Time In.
3. Currently Timed In counts records without Time Out.
4. Completed Today counts records with Time Out.
5. Recent activity includes Time In/Time Out events with local display times.

## 9. Records / Export

1. Open Records.
2. Confirm the filter controls are fully visible and not compressed/overlapping.
3. Filter current day.
4. Filter by student query.
5. Filter by course/year/section.
6. Filter Timed In vs Completed.
7. Export the current filtered set to CSV.
8. Open the CSV in Excel and verify columns/commas/quotes render correctly.

As Administrator:

9. Select a record and open Correct Selected.
10. Change a time/notes and save.
11. Confirm the record changes.
12. Confirm Audit Log contains an attendance-correction entry.

## 10. UI Resize / Navigation Smoke Test

1. Navigate repeatedly among Dashboard, Attendance, Students, Records, and Settings.
2. Confirm only the selected page is displayed in the content area.
3. Confirm the active navigation button changes state correctly.
4. Resize/maximize the application and confirm major panels/grids remain usable.
5. Open and close Student Editor, Audit Log, User Management, Change Password, and Attendance Correction dialogs.
6. Confirm no controls visibly overlap or disappear at normal Windows display scaling.

## 11. Backup / Restore

As Administrator:

1. Create a backup from Settings.
2. Confirm the `.db` file exists.
3. Add or edit a harmless test record.
4. Restore the prior backup.
5. Confirm a pre-restore safety backup is created under `%LOCALAPPDATA%\JCGAttendanceSystem\Backups`.
6. Restart the application as instructed.
7. Confirm the restored data matches the backup.

Also test selecting a random/non-JCG `.db` file for restore; validation must reject it.

## 12. Legacy Import

This test requires a Windows Access/OLE DB provider.

1. Open Settings > Import Legacy Data.
2. Select the included `Legacy\Data.mdb`.
3. Confirm import returns a summary rather than silently failing rows.
4. Confirm imported students appear in Students.
5. Confirm resolvable historical attendance appears in Records.
6. Confirm no old Login password is imported as a system account.

If the Access provider is missing, confirm the error clearly explains that the app can continue using SQLite without legacy import.

## 13. Restart / Persistence

1. Close the program normally.
2. Reopen it.
3. Confirm users, students, attendance, settings, and audit logs persist.
4. Confirm the first-run Administrator setup does not reappear.

## 14. RFID Readiness

Without hardware, confirm Attendance states that the reader is not configured and manual attendance works.

When a real reader is added later, test the implementation against `IRfidReader` and verify it only emits tag values. Attendance business rules must remain in `AttendanceService`.

## Visual Studio Designer regression checks (v3.1.1)

1. Build > Clean Solution, then Build > Rebuild Solution.
2. Confirm the Error List no longer shows the `ResXFileCodeGenerator` missing-output warning.
3. Right-click `DashboardForm.cs` > **View Designer**.
4. Right-click `AttendanceForm.cs` > **View Designer**.
5. Repeat for `StudentsForm.cs`, `RecordsForm.cs`, `AuditLogForm.cs`, and `UserManagementForm.cs`.
6. Run the application and confirm the Attendance search TextBox and Search button are fully visible.

