using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.RFID;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Attendance
{
    public partial class AttendanceForm : Form
    {
        private StudentService _students;
        private AttendanceService _attendance;
        private IRfidReader _rfidReader;
        private Student _selectedStudent;

        public AttendanceForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;
            InitializeRuntime(new NullRfidReader());
        }

        internal AttendanceForm(IRfidReader rfidReader)
        {
            InitializeComponent();
            InitializeRuntime(rfidReader);
        }

        private void InitializeRuntime(IRfidReader rfidReader)
        {
            _students = new StudentService();
            _attendance = new AttendanceService();
            _rfidReader = rfidReader ?? new NullRfidReader();
            Text = "Attendance";
            BackColor = UiTheme.Background;
            _searchButton.Click += (s, e) => SearchStudents();
            _search.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; SearchStudents(); } };
            _timeIn.Click += (s, e) => RecordAttendance(true, AttendanceSources.Manual);
            _timeOut.Click += (s, e) => RecordAttendance(false, AttendanceSources.Manual);
            ClearSelection();
            _rfidReader.TagScanned += OnRfidTagScanned;
            _rfidReader.Start();
        }

        private void SearchStudents()
        {
            try
            {
                var results = _students.Search(_search.Text, false);
                _matches.DataSource = new List<Student>(results);
                if (results.Count == 1)
                {
                    _matches.ClearSelection();
                    _matches.Rows[0].Selected = true;
                    SelectStudent(results[0]);
                }
                else if (results.Count == 0)
                {
                    ClearSelection();
                    MessageBox.Show(this, "No active students matched that search.", "No results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Searching students for attendance");
                MessageBox.Show(this, "Student search could not be completed.", "Search error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Matches_SelectionChanged(object sender, EventArgs e)
        {
            if (_matches.SelectedRows.Count == 0) return;
            var student = _matches.SelectedRows[0].DataBoundItem as Student;
            if (student != null) SelectStudent(student);
        }

        private void SelectStudent(Student student)
        {
            _selectedStudent = student;
            _name.Text = student.FullName;
            _studentNo.Text = student.StudentNumber;
            _course.Text = student.Course + " / Year " + student.YearLevel + " / " + student.Section;
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            if (_selectedStudent == null) { ClearSelection(); return; }
            var record = _attendance.GetTodayStatus(_selectedStudent.Id);
            if (record == null)
            {
                _status.Text = "Not timed in";
                _status.ForeColor = UiTheme.Muted;
                _timeIn.Enabled = true;
                _timeOut.Enabled = false;
            }
            else if (!record.TimeOutUtc.HasValue)
            {
                _status.Text = "Timed in at " + record.TimeInUtc.ToLocalTime().ToString("hh:mm tt");
                _status.ForeColor = UiTheme.Warning;
                _timeIn.Enabled = false;
                _timeOut.Enabled = true;
            }
            else
            {
                _status.Text = "Completed · " + record.TimeInUtc.ToLocalTime().ToString("hh:mm tt") + " - " + record.TimeOutUtc.Value.ToLocalTime().ToString("hh:mm tt");
                _status.ForeColor = UiTheme.Success;
                _timeIn.Enabled = false;
                _timeOut.Enabled = false;
            }
        }

        private void RecordAttendance(bool isTimeIn, string source)
        {
            if (_selectedStudent == null) return;
            try
            {
                var result = isTimeIn ? _attendance.TimeIn(_selectedStudent, source) : _attendance.TimeOut(_selectedStudent, source);
                MessageBox.Show(this, result.Message, result.Success ? "Attendance recorded" : "Attendance", MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                RefreshStatus();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Recording attendance");
                MessageBox.Show(this, "Attendance could not be recorded.", "Attendance error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnRfidTagScanned(object sender, RfidTagEventArgs e)
        {
            if (InvokeRequired) { BeginInvoke(new Action(() => OnRfidTagScanned(sender, e))); return; }
            try
            {
                var student = _students.GetByRfidTag(e.Tag);
                if (student == null || !student.IsActive)
                {
                    MessageBox.Show(this, "The scanned RFID tag is not assigned to an active student.", "Unknown RFID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SelectStudent(student);
                var current = _attendance.GetTodayStatus(student.Id);
                RecordAttendance(current == null, AttendanceSources.Rfid);
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Processing RFID scan");
                MessageBox.Show(this, "The RFID scan could not be processed.", "RFID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSelection()
        {
            _selectedStudent = null;
            _name.Text = "Select a student";
            _studentNo.Text = "—";
            _course.Text = "—";
            _status.Text = "—";
            _status.ForeColor = UiTheme.Muted;
            _timeIn.Enabled = false;
            _timeOut.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _rfidReader != null)
            {
                _rfidReader.TagScanned -= OnRfidTagScanned;
                _rfidReader.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
