using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Settings
{
    partial class UserManagementForm
    {
        private TableLayoutPanel _rootLayout;
        private Panel _toolbar;
        private Button _addButton;
        private Button _resetButton;
        private Button _toggle;
        private Panel _gridCard;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _usernameColumn;
        private DataGridViewTextBoxColumn _roleColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _createdColumn;

        private void InitializeComponent()
        {
            this._rootLayout = new TableLayoutPanel();
            this._toolbar = new Panel();
            this._addButton = new Button();
            this._resetButton = new Button();
            this._toggle = new Button();
            this._gridCard = new Panel();
            this._grid = new DataGridView();
            this._usernameColumn = new DataGridViewTextBoxColumn();
            this._roleColumn = new DataGridViewTextBoxColumn();
            this._statusColumn = new DataGridViewTextBoxColumn();
            this._createdColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            this._rootLayout.Dock = DockStyle.Fill;
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            this._rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._toolbar.BackColor = Color.White;
            this._toolbar.BorderStyle = BorderStyle.FixedSingle;
            this._toolbar.Dock = DockStyle.Fill;
            this._toolbar.Margin = new Padding(0, 0, 0, 14);
            this._addButton.BackColor = Color.FromArgb(30, 136, 229);
            this._addButton.FlatAppearance.BorderSize = 0;
            this._addButton.FlatStyle = FlatStyle.Flat;
            this._addButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._addButton.ForeColor = Color.White;
            this._addButton.Location = new Point(18, 20);
            this._addButton.Size = new Size(110, 40);
            this._addButton.Text = "Add Staff";
            this._resetButton.BackColor = Color.White;
            this._resetButton.FlatAppearance.BorderColor = Color.FromArgb(210, 218, 226);
            this._resetButton.FlatStyle = FlatStyle.Flat;
            this._resetButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._resetButton.Location = new Point(140, 20);
            this._resetButton.Size = new Size(135, 40);
            this._resetButton.Text = "Reset Password";
            this._toggle.BackColor = Color.FromArgb(198, 40, 40);
            this._toggle.FlatAppearance.BorderSize = 0;
            this._toggle.FlatStyle = FlatStyle.Flat;
            this._toggle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._toggle.ForeColor = Color.White;
            this._toggle.Location = new Point(287, 20);
            this._toggle.Size = new Size(115, 40);
            this._toggle.Text = "Deactivate";
            this._toggle.Click += new System.EventHandler(this.Toggle_Click);
            this._toolbar.Controls.Add(this._toggle);
            this._toolbar.Controls.Add(this._resetButton);
            this._toolbar.Controls.Add(this._addButton);
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
            this._usernameColumn.Name = "UsernameColumn";
            this._usernameColumn.HeaderText = "Username";
            this._usernameColumn.DataPropertyName = "Username";
            this._usernameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this._roleColumn.Name = "RoleColumn";
            this._roleColumn.HeaderText = "Role";
            this._roleColumn.DataPropertyName = "Role";
            this._roleColumn.Width = 140;
            this._statusColumn.Name = "Status";
            this._statusColumn.HeaderText = "Status";
            this._statusColumn.Width = 100;
            this._createdColumn.Name = "Created";
            this._createdColumn.HeaderText = "Created";
            this._createdColumn.Width = 165;
            this._grid.Columns.AddRange(new DataGridViewColumn[] { this._usernameColumn, this._roleColumn, this._statusColumn, this._createdColumn });




            this._grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            this._gridCard.Controls.Add(this._grid);
            this._rootLayout.Controls.Add(this._gridCard, 0, 1);
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(820, 560);
            this.Controls.Add(this._rootLayout);
            this.MinimumSize = new Size(700, 480);
            this.Name = "UserManagementForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "User Management";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
