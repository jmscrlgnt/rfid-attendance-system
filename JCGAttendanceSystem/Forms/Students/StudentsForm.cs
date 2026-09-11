using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Students
{
    public partial class StudentsForm : Form
    {
        private StudentService _service;

        public StudentsForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _service = new StudentService();
            Text = "Students";
            BackColor = UiTheme.Background;
            _searchButton.Click += (s, e) => LoadStudents();
            _search.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LoadStudents(); } };
            _includeInactive.CheckedChanged += (s, e) => LoadStudents();
            _addButton.Click += (s, e) => EditStudent(null);
            _editButton.Click += (s, e) => EditSelected();
            _grid.SelectionChanged += (s, e) => UpdateToggleButton();
            _grid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            Load += (s, e) => LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                var includeInactive = _includeInactive.Checked;
                var students = _service.Search(_search.Text, includeInactive);
                _grid.DataSource = new List<Student>(students);
                _empty.Visible = students.Count == 0;
                UpdateToggleButton();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Loading students");
                MessageBox.Show(this, "Students could not be loaded.", "Students", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Student SelectedStudent => _grid.SelectedRows.Count == 0 ? null : _grid.SelectedRows[0].DataBoundItem as Student;

        private void EditSelected()
        {
            var student = SelectedStudent;
            if (student != null) EditStudent(student);
        }

        private void EditStudent(Student student)
        {
            using (var editor = new StudentEditorForm(student))
            {
                if (editor.ShowDialog(this) == DialogResult.OK) LoadStudents();
            }
        }

        private void ToggleActive_Click(object sender, EventArgs e)
        {
            var student = SelectedStudent;
            if (student == null) return;
            var action = student.IsActive ? "deactivate" : "reactivate";
            if (MessageBox.Show(this, "Are you sure you want to " + action + " " + student.FullName + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                _service.SetActive(student.Id, !student.IsActive);
                LoadStudents();
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Changing student active state");
                MessageBox.Show(this, ex.Message, "Students", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateToggleButton()
        {
            var student = SelectedStudent;
            _toggleActive.Enabled = student != null;
            if (student == null) return;
            _toggleActive.Text = student.IsActive ? "Deactivate" : "Reactivate";
            _toggleActive.BackColor = student.IsActive ? UiTheme.Danger : UiTheme.Success;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_grid.Columns[e.ColumnIndex].Name != "Status") return;
            var student = _grid.Rows[e.RowIndex].DataBoundItem as Student;
            if (student == null) return;
            e.Value = student.IsActive ? "Active" : "Inactive";
            e.CellStyle.ForeColor = student.IsActive ? UiTheme.Success : UiTheme.Danger;
            e.CellStyle.Font = UiTheme.Font(9f, FontStyle.Bold);
        }
    }
}
