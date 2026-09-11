using System;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Students
{
    public partial class StudentEditorForm : Form
    {
        private StudentService _service;
        private Student _student;

        public Student SavedStudent { get; private set; }

        public StudentEditorForm()
        {
            InitializeComponent();
        }

        public StudentEditorForm(Student student)
        {
            InitializeComponent();
            _service = new StudentService();
            _student = student == null ? new Student { IsActive = true, YearLevel = 1 } : Clone(student);
            Text = student == null ? "Add Student" : "Edit Student";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(660, 650);
            MinimumSize = new Size(620, 610);
            BackColor = UiTheme.Background;
            _cancelButton.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            LoadStudent();
        }

        private void LoadStudent()
        {
            _studentNumber.Text = _student.StudentNumber ?? string.Empty;
            _firstName.Text = _student.FirstName ?? string.Empty;
            _lastName.Text = _student.LastName ?? string.Empty;
            _birthDate.Checked = _student.BirthDate.HasValue;
            if (_student.BirthDate.HasValue) _birthDate.Value = _student.BirthDate.Value;
            _gender.SelectedItem = _student.Gender ?? string.Empty;
            if (_gender.SelectedIndex < 0) _gender.SelectedIndex = 0;
            _email.Text = _student.Email ?? string.Empty;
            _course.Text = _student.Course ?? string.Empty;
            _year.Value = Math.Max(_year.Minimum, Math.Min(_year.Maximum, _student.YearLevel));
            _section.Text = _student.Section ?? string.Empty;
            _rfidTag.Text = _student.RfidTag ?? string.Empty;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            _error.Text = string.Empty;
            try
            {
                _student.StudentNumber = _studentNumber.Text;
                _student.FirstName = _firstName.Text;
                _student.LastName = _lastName.Text;
                _student.BirthDate = _birthDate.Checked ? (DateTime?)_birthDate.Value.Date : null;
                _student.Gender = _gender.Text;
                _student.Email = _email.Text;
                _student.Course = _course.Text;
                _student.YearLevel = (int)_year.Value;
                _student.Section = _section.Text;
                _student.RfidTag = _rfidTag.Text;
                SavedStudent = _service.Save(_student);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Saving student");
                _error.Text = ex.Message;
            }
        }

        private static Student Clone(Student source)
        {
            return new Student
            {
                Id = source.Id,
                StudentNumber = source.StudentNumber,
                FirstName = source.FirstName,
                LastName = source.LastName,
                BirthDate = source.BirthDate,
                Gender = source.Gender,
                Email = source.Email,
                Course = source.Course,
                YearLevel = source.YearLevel,
                Section = source.Section,
                RfidTag = source.RfidTag,
                IsActive = source.IsActive,
                CreatedAtUtc = source.CreatedAtUtc,
                UpdatedAtUtc = source.UpdatedAtUtc
            };
        }
    }
}
