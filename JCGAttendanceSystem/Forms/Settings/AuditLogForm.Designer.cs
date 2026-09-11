using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Settings
{
    partial class AuditLogForm
    {
        private TableLayoutPanel _rootLayout;
        private Panel _toolbar;
        private Label _titleLabel;
        private Button _refreshButton;
        private Panel _gridCard;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _timeColumn;
        private DataGridViewTextBoxColumn _userColumn;
        private DataGridViewTextBoxColumn _actionColumn;
        private DataGridViewTextBoxColumn _entityColumn;
        private DataGridViewTextBoxColumn _detailsColumn;

        private void InitializeComponent()
        {
            this._rootLayout = new TableLayoutPanel();
            this._toolbar = new Panel();
            this._titleLabel = new Label();
            this._refreshButton = new Button();
            this._gridCard = new Panel();
            this._grid = new DataGridView();
            this._timeColumn = new DataGridViewTextBoxColumn();
            this._userColumn = new DataGridViewTextBoxColumn();
            this._actionColumn = new DataGridViewTextBoxColumn();
            this._entityColumn = new DataGridViewTextBoxColumn();
            this._detailsColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            this._rootLayout.Dock = DockStyle.Fill;
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            this._rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._toolbar.BackColor = Color.White;
            this._toolbar.BorderStyle = BorderStyle.FixedSingle;
            this._toolbar.Dock = DockStyle.Fill;
            this._toolbar.Margin = new Padding(0, 0, 0, 14);
            this._toolbar.Size = new Size(1010, 78);
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this._titleLabel.Location = new Point(20, 20);
            this._titleLabel.Text = "Audit activity";
            this._refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._refreshButton.BackColor = Color.White;
            this._refreshButton.FlatAppearance.BorderColor = Color.FromArgb(210, 218, 226);
            this._refreshButton.FlatStyle = FlatStyle.Flat;
            this._refreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._refreshButton.Location = new Point(895, 16);
            this._refreshButton.Size = new Size(95, 38);
            this._refreshButton.Text = "Refresh";
            this._toolbar.Controls.Add(this._refreshButton);
            this._toolbar.Controls.Add(this._titleLabel);
            this._rootLayout.Controls.Add(this._toolbar, 0, 0);
            this._gridCard.BackColor = Color.White;
            this._gridCard.BorderStyle = BorderStyle.FixedSingle;
            this._gridCard.Dock = DockStyle.Fill;
            this._gridCard.Padding = new Padding(18);
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoGenerateColumns = false;
            this._grid.BackgroundColor = Color.White;
            this._grid.BorderStyle = BorderStyle.None;
            this._grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 248);
            this._grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._grid.ColumnHeadersHeight = 36;
            this._grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            this._grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(222, 235, 247);
            this._grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            this._grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            this._grid.Dock = DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.MultiSelect = false;
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowTemplate.Height = 34;
            this._grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this._timeColumn.Name = "Time";
            this._timeColumn.HeaderText = "Time";
            this._timeColumn.Width = 165;
            this._userColumn.Name = "UserColumn";
            this._userColumn.HeaderText = "User";
            this._userColumn.DataPropertyName = "Username";
            this._userColumn.Width = 140;
            this._actionColumn.Name = "ActionColumn";
            this._actionColumn.HeaderText = "Action";
            this._actionColumn.DataPropertyName = "Action";
            this._actionColumn.Width = 210;
            this._entityColumn.Name = "Entity";
            this._entityColumn.HeaderText = "Entity";
            this._entityColumn.Width = 160;
            this._detailsColumn.Name = "DetailsColumn";
            this._detailsColumn.HeaderText = "Details";
            this._detailsColumn.DataPropertyName = "Details";
            this._detailsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this._grid.Columns.AddRange(new DataGridViewColumn[] { this._timeColumn, this._userColumn, this._actionColumn, this._entityColumn, this._detailsColumn });





            this._grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            this._gridCard.Controls.Add(this._grid);
            this._rootLayout.Controls.Add(this._gridCard, 0, 1);
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1040, 640);
            this.Controls.Add(this._rootLayout);
            this.MinimumSize = new Size(800, 500);
            this.Name = "AuditLogForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Audit Log";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
