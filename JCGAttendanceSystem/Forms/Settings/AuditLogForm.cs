using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JCGAttendanceSystem.Models;
using JCGAttendanceSystem.Services;
using JCGAttendanceSystem.Utilities;

namespace JCGAttendanceSystem.Forms.Settings
{
    public partial class AuditLogForm : Form
    {
        private AuditService _audit;

        public AuditLogForm()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;

            _audit = new AuditService();
            Text = "Audit Log"; StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(980, 600); MinimumSize = new Size(800, 500); BackColor = UiTheme.Background;
            _refreshButton.Click += (s, e) => LoadAudit(); Load += (s, e) => LoadAudit();
        }

        private void LoadAudit()
        {
            try { _grid.DataSource = new List<AuditLogEntry>(_audit.GetRecentForAdministrator(500)); }
            catch (Exception ex) { AppLogger.LogException(ex, "Loading audit log"); MessageBox.Show(this, ex.Message, "Audit Log", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var item = _grid.Rows[e.RowIndex].DataBoundItem as AuditLogEntry; if (item == null) return;
            var columnName = _grid.Columns[e.ColumnIndex].Name;
            if (columnName == "Time") e.Value = item.CreatedAtUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            if (columnName == "Entity") e.Value = string.IsNullOrWhiteSpace(item.EntityType) ? "—" : item.EntityType + (string.IsNullOrWhiteSpace(item.EntityId) ? "" : " #" + item.EntityId);
            if (_grid.Columns[e.ColumnIndex].DataPropertyName == "Action")
            {
                if ((item.Action ?? string.Empty).IndexOf("failed", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = UiTheme.Danger;
                else if ((item.Action ?? string.Empty).IndexOf("successful", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = UiTheme.Success;
            }
        }
    }
}
