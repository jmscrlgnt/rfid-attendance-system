using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Records
{
    public partial class AttendanceCorrectionForm : Form
    {
        private AttendanceService _attendance;
        private AttendanceView _record;

        public AttendanceCorrectionForm()
        {
            InitializeComponent();
        }

        public AttendanceCorrectionForm(AttendanceView record)
        {
            InitializeComponent();
            _attendance = new AttendanceService();
            _record = record ?? throw new ArgumentNullException(nameof(record));
            _studentLabel.Text = _record.StudentName + " · " + _record.StudentNumber;
            _timeIn.Value = _record.TimeInUtc.ToLocalTime();
            _timeOut.Checked = _record.TimeOutUtc.HasValue;
            _timeOut.Value = _record.TimeOutUtc.HasValue ? _record.TimeOutUtc.Value.ToLocalTime() : DateTime.Now;
            _notes.Text = _record.Notes ?? string.Empty;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            try
            {
                _attendance.CorrectAttendance(_record.AttendanceId, _timeIn.Value, _timeOut.Checked ? (DateTime?)_timeOut.Value : null, _notes.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Correcting attendance");
                _error.Text = ex.Message;
            }
        }
    }
}
