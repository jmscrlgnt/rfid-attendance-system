using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Dashboard
{
    public partial class DashboardForm : Form
    {
        private DashboardService _service;

        public DashboardForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _service = new DashboardService();
            Text = "Dashboard";
            BackColor = UiTheme.Background;
            Padding = new Padding(0);
            _dateLabel.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
            Load += (s, e) => RefreshDashboard();
            _refreshTimer.Tick += (s, e) => RefreshDashboard();
            _refreshTimer.Start();
        }

        private void RefreshDashboard()
        {
            try
            {
                var summary = _service.GetTodaySummary();
                _total.Text = summary.TotalActiveStudents.ToString();
                _present.Text = summary.PresentToday.ToString();
                _timedIn.Text = summary.CurrentlyTimedIn.ToString();
                _completed.Text = summary.CompletedToday.ToString();
                var recent = _service.GetRecentActivity(10);
                _grid.DataSource = new List<AttendanceView>(recent);
                _empty.Visible = recent.Count == 0;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Refreshing dashboard");
                _empty.Text = "Dashboard data could not be loaded.";
                _empty.Visible = true;
            }
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = _grid.Rows[e.RowIndex].DataBoundItem as AttendanceView;
            if (row == null) return;
            var columnName = _grid.Columns[e.ColumnIndex].Name;
            if (columnName == "YearSection") e.Value = row.YearLevel + " / " + row.Section;
            if (columnName == "EventTime" && row.EventTimeUtc.HasValue) e.Value = row.EventTimeUtc.Value.ToLocalTime().ToString("hh:mm:ss tt");
            if (_grid.Columns[e.ColumnIndex].DataPropertyName == "EventType")
            {
                var isTimeOut = string.Equals(row.EventType, "Time Out", StringComparison.OrdinalIgnoreCase);
                e.CellStyle.ForeColor = isTimeOut ? UiTheme.Success : UiTheme.AccentDark;
                e.CellStyle.Font = UiTheme.Font(9f, FontStyle.Bold);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _refreshTimer.Dispose();
            base.Dispose(disposing);
        }
    }
}
