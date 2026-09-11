using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Security;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Records
{
    public partial class RecordsForm : Form
    {
        private RecordsService _records;
        private RecordExportService _export;
        private IList<AttendanceView> _current = new List<AttendanceView>();

        public RecordsForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _records = new RecordsService();
            _export = new RecordExportService();
            Text = "Records";
            BackColor = UiTheme.Background;
            _applyButton.Click += (s, e) => Search();
            _clearButton.Click += (s, e) => ClearFilters();
            _correctButton.Visible = SessionContext.IsAdministrator;
            Load += (s, e) => { _from.Value = DateTime.Today; _to.Value = DateTime.Today; Search(); };
        }

        private AttendanceFilter BuildFilter()
        {
            int year;
            return new AttendanceFilter
            {
                DateFrom = _from.Checked ? (DateTime?)_from.Value.Date : null,
                DateTo = _to.Checked ? (DateTime?)_to.Value.Date : null,
                StudentQuery = _student.Text,
                Course = _course.Text,
                YearLevel = int.TryParse(_year.Text, out year) ? (int?)year : null,
                Section = _section.Text,
                Status = _status.Text == "All" ? null : _status.Text
            };
        }

        private void Search()
        {
            try
            {
                _current = _records.Search(BuildFilter());
                _grid.DataSource = new List<AttendanceView>(_current);
                _empty.Visible = _current.Count == 0;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Searching attendance records");
                MessageBox.Show(this, "Attendance records could not be loaded.", "Records", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFilters()
        {
            _from.Checked = true; _from.Value = DateTime.Today;
            _to.Checked = true; _to.Value = DateTime.Today;
            _student.Clear(); _course.Clear(); _year.SelectedIndex = 0; _section.Clear(); _status.SelectedIndex = 0;
            Search();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (_current.Count == 0) { MessageBox.Show(this, "There are no filtered records to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            using (var dialog = new SaveFileDialog { Filter = "CSV file (*.csv)|*.csv", FileName = "attendance-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".csv" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try { _export.ExportCsv(dialog.FileName, _current); MessageBox.Show(this, "CSV exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                catch (Exception ex) { AppLogger.LogException(ex, "Exporting attendance CSV"); MessageBox.Show(this, "The CSV could not be exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void Correct_Click(object sender, EventArgs e)
        {
            if (!SessionContext.IsAdministrator || _grid.SelectedRows.Count == 0) return;
            var selected = _grid.SelectedRows[0].DataBoundItem as AttendanceView;
            if (selected == null) return;
            using (var form = new AttendanceCorrectionForm(selected))
                if (form.ShowDialog(this) == DialogResult.OK) Search();
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var item = _grid.Rows[e.RowIndex].DataBoundItem as AttendanceView;
            if (item == null) return;
            var name = _grid.Columns[e.ColumnIndex].Name;
            if (name == "Date") e.Value = item.AttendanceDate.ToString("yyyy-MM-dd");
            if (name == "YearSection") e.Value = item.YearLevel + "/" + item.Section;
            if (name == "TimeIn") e.Value = item.TimeInUtc.ToLocalTime().ToString("hh:mm tt");
            if (name == "TimeOut") e.Value = item.TimeOutUtc.HasValue ? item.TimeOutUtc.Value.ToLocalTime().ToString("hh:mm tt") : "—";
            if (name == "Status")
            {
                e.CellStyle.ForeColor = string.Equals(item.Status, "Completed", StringComparison.OrdinalIgnoreCase) ? UiTheme.Success : UiTheme.Warning;
                e.CellStyle.Font = UiTheme.Font(9f, FontStyle.Bold);
            }
            if (name == "Source")
            {
                e.CellStyle.ForeColor = string.Equals(item.Source, AttendanceSources.Rfid, StringComparison.OrdinalIgnoreCase) ? UiTheme.AccentDark : UiTheme.Muted;
            }
        }
    }
}
