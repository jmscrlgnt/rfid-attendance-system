using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Attendance
{
    partial class AttendanceForm
    {
        private TableLayoutPanel _rootLayout;
        private Panel _searchCard;
        private Label _searchTitle;
        private Label _searchHint;
        private TextBox _search;
        private Button _searchButton;
        private Panel _resultsCard;
        private Label _resultsTitle;
        private DataGridView _matches;
        private Panel _detailsCard;
        private TableLayoutPanel _detailsLayout;
        private Panel _namePanel;
        private Label _nameCaption;
        private Label _name;
        private Panel _studentNoPanel;
        private Label _studentNoCaption;
        private Label _studentNo;
        private Panel _coursePanel;
        private Label _courseCaption;
        private Label _course;
        private Panel _statusPanel;
        private Label _statusCaption;
        private Label _status;
        private Label _rfidStatus;
        private FlowLayoutPanel _attendanceActions;
        private Button _timeIn;
        private Button _timeOut;
        private DataGridViewTextBoxColumn _studentNumberColumn;
        private DataGridViewTextBoxColumn _studentNameColumn;
        private DataGridViewTextBoxColumn _courseColumn;
        private DataGridViewTextBoxColumn _yearColumn;
        private DataGridViewTextBoxColumn _sectionColumn;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._searchCard = new System.Windows.Forms.Panel();
            this._searchButton = new System.Windows.Forms.Button();
            this._search = new System.Windows.Forms.TextBox();
            this._searchHint = new System.Windows.Forms.Label();
            this._searchTitle = new System.Windows.Forms.Label();
            this._resultsCard = new System.Windows.Forms.Panel();
            this._matches = new System.Windows.Forms.DataGridView();
            this._resultsTitle = new System.Windows.Forms.Label();
            this._detailsCard = new System.Windows.Forms.Panel();
            this._detailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._namePanel = new System.Windows.Forms.Panel();
            this._name = new System.Windows.Forms.Label();
            this._nameCaption = new System.Windows.Forms.Label();
            this._studentNoPanel = new System.Windows.Forms.Panel();
            this._studentNo = new System.Windows.Forms.Label();
            this._studentNoCaption = new System.Windows.Forms.Label();
            this._coursePanel = new System.Windows.Forms.Panel();
            this._course = new System.Windows.Forms.Label();
            this._courseCaption = new System.Windows.Forms.Label();
            this._statusPanel = new System.Windows.Forms.Panel();
            this._status = new System.Windows.Forms.Label();
            this._statusCaption = new System.Windows.Forms.Label();
            this._rfidStatus = new System.Windows.Forms.Label();
            this._attendanceActions = new System.Windows.Forms.FlowLayoutPanel();
            this._timeIn = new System.Windows.Forms.Button();
            this._timeOut = new System.Windows.Forms.Button();
            this._rootLayout.SuspendLayout();
            this._searchCard.SuspendLayout();
            this._resultsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._matches)).BeginInit();
            this._detailsCard.SuspendLayout();
            this._detailsLayout.SuspendLayout();
            this._namePanel.SuspendLayout();
            this._studentNoPanel.SuspendLayout();
            this._coursePanel.SuspendLayout();
            this._statusPanel.SuspendLayout();
            this._attendanceActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._rootLayout.Controls.Add(this._searchCard, 0, 0);
            this._rootLayout.Controls.Add(this._resultsCard, 0, 1);
            this._rootLayout.Controls.Add(this._detailsCard, 0, 2);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 3;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1051, 687);
            this._rootLayout.TabIndex = 0;
            // 
            // _searchCard
            // 
            this._searchCard.BackColor = System.Drawing.Color.White;
            this._searchCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchCard.Controls.Add(this._searchButton);
            this._searchCard.Controls.Add(this._search);
            this._searchCard.Controls.Add(this._searchHint);
            this._searchCard.Controls.Add(this._searchTitle);
            this._searchCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchCard.Location = new System.Drawing.Point(0, 0);
            this._searchCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._searchCard.Name = "_searchCard";
            this._searchCard.Size = new System.Drawing.Size(1051, 102);
            this._searchCard.TabIndex = 0;
            // 
            // _searchButton
            // 
            this._searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this._searchButton.FlatAppearance.BorderSize = 0;
            this._searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._searchButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._searchButton.ForeColor = System.Drawing.Color.White;
            this._searchButton.Location = new System.Drawing.Point(401, 62);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(94, 33);
            this._searchButton.TabIndex = 0;
            this._searchButton.Text = "Search";
            this._searchButton.UseVisualStyleBackColor = false;
            // 
            // _search
            // 
            this._search.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._search.Location = new System.Drawing.Point(19, 65);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(369, 25);
            this._search.TabIndex = 1;
            // 
            // _searchHint
            // 
            this._searchHint.AutoSize = true;
            this._searchHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._searchHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._searchHint.Location = new System.Drawing.Point(18, 38);
            this._searchHint.Name = "_searchHint";
            this._searchHint.Size = new System.Drawing.Size(347, 15);
            this._searchHint.TabIndex = 2;
            this._searchHint.Text = "Search by student number, name, email, course, year, or section.";
            // 
            // _searchTitle
            // 
            this._searchTitle.AutoSize = true;
            this._searchTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._searchTitle.Location = new System.Drawing.Point(17, 14);
            this._searchTitle.Name = "_searchTitle";
            this._searchTitle.Size = new System.Drawing.Size(118, 21);
            this._searchTitle.TabIndex = 3;
            this._searchTitle.Text = "Find a student";
            // 
            // _resultsCard
            // 
            this._resultsCard.BackColor = System.Drawing.Color.White;
            this._resultsCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._resultsCard.Controls.Add(this._matches);
            this._resultsCard.Controls.Add(this._resultsTitle);
            this._resultsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultsCard.Location = new System.Drawing.Point(0, 114);
            this._resultsCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._resultsCard.Name = "_resultsCard";
            this._resultsCard.Padding = new System.Windows.Forms.Padding(15, 42, 15, 12);
            this._resultsCard.Size = new System.Drawing.Size(1051, 179);
            this._resultsCard.TabIndex = 1;
            // 
            // _matches
            // 
            this._matches.AllowUserToAddRows = false;
            this._matches.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._matches.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._matches.BackgroundColor = System.Drawing.Color.White;
            this._matches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._matches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._matches.ColumnHeadersHeight = 34;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._matches.DefaultCellStyle = dataGridViewCellStyle3;
            this._matches.Dock = System.Windows.Forms.DockStyle.Fill;
            this._matches.EnableHeadersVisualStyles = false;
            this._matches.Location = new System.Drawing.Point(15, 42);
            this._matches.MultiSelect = false;
            this._matches.Name = "_matches";
            this._matches.ReadOnly = true;
            this._matches.RowHeadersVisible = false;
            this._matches.RowTemplate.Height = 34;
            this._matches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._matches.Size = new System.Drawing.Size(1019, 123);
            this._matches.TabIndex = 0;
            this._matches.SelectionChanged += new System.EventHandler(this.Matches_SelectionChanged);
            // 
            // _resultsTitle
            // 
            this._resultsTitle.AutoSize = true;
            this._resultsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._resultsTitle.Location = new System.Drawing.Point(15, 14);
            this._resultsTitle.Name = "_resultsTitle";
            this._resultsTitle.Size = new System.Drawing.Size(106, 20);
            this._resultsTitle.TabIndex = 1;
            this._resultsTitle.Text = "Search results";
            // 
            // _detailsCard
            // 
            this._detailsCard.BackColor = System.Drawing.Color.White;
            this._detailsCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._detailsCard.Controls.Add(this._detailsLayout);
            this._detailsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsCard.Location = new System.Drawing.Point(3, 308);
            this._detailsCard.Name = "_detailsCard";
            this._detailsCard.Padding = new System.Windows.Forms.Padding(17, 17, 17, 17);
            this._detailsCard.Size = new System.Drawing.Size(1045, 376);
            this._detailsCard.TabIndex = 2;
            // 
            // _detailsLayout
            // 
            this._detailsLayout.ColumnCount = 2;
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this._detailsLayout.Controls.Add(this._namePanel, 0, 0);
            this._detailsLayout.Controls.Add(this._studentNoPanel, 1, 0);
            this._detailsLayout.Controls.Add(this._coursePanel, 0, 1);
            this._detailsLayout.Controls.Add(this._statusPanel, 1, 1);
            this._detailsLayout.Controls.Add(this._rfidStatus, 0, 2);
            this._detailsLayout.Controls.Add(this._attendanceActions, 0, 3);
            this._detailsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsLayout.Location = new System.Drawing.Point(17, 17);
            this._detailsLayout.Name = "_detailsLayout";
            this._detailsLayout.RowCount = 4;
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._detailsLayout.Size = new System.Drawing.Size(1009, 340);
            this._detailsLayout.TabIndex = 0;
            // 
            // _namePanel
            // 
            this._namePanel.Controls.Add(this._name);
            this._namePanel.Controls.Add(this._nameCaption);
            this._namePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._namePanel.Location = new System.Drawing.Point(3, 3);
            this._namePanel.Name = "_namePanel";
            this._namePanel.Size = new System.Drawing.Size(548, 56);
            this._namePanel.TabIndex = 0;
            // 
            // _name
            // 
            this._name.Dock = System.Windows.Forms.DockStyle.Fill;
            this._name.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._name.Location = new System.Drawing.Point(0, 21);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(548, 35);
            this._name.TabIndex = 0;
            this._name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameCaption
            // 
            this._nameCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._nameCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._nameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._nameCaption.Location = new System.Drawing.Point(0, 0);
            this._nameCaption.Name = "_nameCaption";
            this._nameCaption.Size = new System.Drawing.Size(548, 21);
            this._nameCaption.TabIndex = 1;
            this._nameCaption.Text = "Student";
            // 
            // _studentNoPanel
            // 
            this._studentNoPanel.Controls.Add(this._studentNo);
            this._studentNoPanel.Controls.Add(this._studentNoCaption);
            this._studentNoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._studentNoPanel.Location = new System.Drawing.Point(557, 3);
            this._studentNoPanel.Name = "_studentNoPanel";
            this._studentNoPanel.Size = new System.Drawing.Size(449, 56);
            this._studentNoPanel.TabIndex = 1;
            // 
            // _studentNo
            // 
            this._studentNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._studentNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._studentNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._studentNo.Location = new System.Drawing.Point(0, 21);
            this._studentNo.Name = "_studentNo";
            this._studentNo.Size = new System.Drawing.Size(449, 35);
            this._studentNo.TabIndex = 0;
            this._studentNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _studentNoCaption
            // 
            this._studentNoCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._studentNoCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._studentNoCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._studentNoCaption.Location = new System.Drawing.Point(0, 0);
            this._studentNoCaption.Name = "_studentNoCaption";
            this._studentNoCaption.Size = new System.Drawing.Size(449, 21);
            this._studentNoCaption.TabIndex = 1;
            this._studentNoCaption.Text = "Student Number";
            // 
            // _coursePanel
            // 
            this._coursePanel.Controls.Add(this._course);
            this._coursePanel.Controls.Add(this._courseCaption);
            this._coursePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._coursePanel.Location = new System.Drawing.Point(3, 65);
            this._coursePanel.Name = "_coursePanel";
            this._coursePanel.Size = new System.Drawing.Size(548, 56);
            this._coursePanel.TabIndex = 2;
            // 
            // _course
            // 
            this._course.Dock = System.Windows.Forms.DockStyle.Fill;
            this._course.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._course.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._course.Location = new System.Drawing.Point(0, 21);
            this._course.Name = "_course";
            this._course.Size = new System.Drawing.Size(548, 35);
            this._course.TabIndex = 0;
            this._course.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _courseCaption
            // 
            this._courseCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._courseCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._courseCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._courseCaption.Location = new System.Drawing.Point(0, 0);
            this._courseCaption.Name = "_courseCaption";
            this._courseCaption.Size = new System.Drawing.Size(548, 21);
            this._courseCaption.TabIndex = 1;
            this._courseCaption.Text = "Course / Year / Section";
            // 
            // _statusPanel
            // 
            this._statusPanel.Controls.Add(this._status);
            this._statusPanel.Controls.Add(this._statusCaption);
            this._statusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusPanel.Location = new System.Drawing.Point(557, 65);
            this._statusPanel.Name = "_statusPanel";
            this._statusPanel.Size = new System.Drawing.Size(449, 56);
            this._statusPanel.TabIndex = 3;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._status.Location = new System.Drawing.Point(0, 21);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(449, 35);
            this._status.TabIndex = 0;
            this._status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _statusCaption
            // 
            this._statusCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._statusCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._statusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._statusCaption.Location = new System.Drawing.Point(0, 0);
            this._statusCaption.Name = "_statusCaption";
            this._statusCaption.Size = new System.Drawing.Size(449, 21);
            this._statusCaption.TabIndex = 1;
            this._statusCaption.Text = "Today\'s Attendance Status";
            // 
            // _rfidStatus
            // 
            this._rfidStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._detailsLayout.SetColumnSpan(this._rfidStatus, 2);
            this._rfidStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rfidStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._rfidStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this._rfidStatus.Location = new System.Drawing.Point(0, 128);
            this._rfidStatus.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this._rfidStatus.Name = "_rfidStatus";
            this._rfidStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._rfidStatus.Size = new System.Drawing.Size(1009, 32);
            this._rfidStatus.TabIndex = 4;
            this._rfidStatus.Text = "Manual attendance mode · RFID reader not configured";
            this._rfidStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _attendanceActions
            // 
            this._detailsLayout.SetColumnSpan(this._attendanceActions, 2);
            this._attendanceActions.Controls.Add(this._timeIn);
            this._attendanceActions.Controls.Add(this._timeOut);
            this._attendanceActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._attendanceActions.Location = new System.Drawing.Point(3, 167);
            this._attendanceActions.Name = "_attendanceActions";
            this._attendanceActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this._attendanceActions.Size = new System.Drawing.Size(1003, 170);
            this._attendanceActions.TabIndex = 5;
            this._attendanceActions.WrapContents = false;
            // 
            // _timeIn
            // 
            this._timeIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this._timeIn.FlatAppearance.BorderSize = 0;
            this._timeIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._timeIn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._timeIn.ForeColor = System.Drawing.Color.White;
            this._timeIn.Location = new System.Drawing.Point(0, 10);
            this._timeIn.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this._timeIn.Name = "_timeIn";
            this._timeIn.Size = new System.Drawing.Size(129, 36);
            this._timeIn.TabIndex = 0;
            this._timeIn.Text = "Time In";
            this._timeIn.UseVisualStyleBackColor = false;
            // 
            // _timeOut
            // 
            this._timeOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this._timeOut.FlatAppearance.BorderSize = 0;
            this._timeOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._timeOut.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._timeOut.ForeColor = System.Drawing.Color.White;
            this._timeOut.Location = new System.Drawing.Point(141, 13);
            this._timeOut.Name = "_timeOut";
            this._timeOut.Size = new System.Drawing.Size(129, 36);
            this._timeOut.TabIndex = 1;
            this._timeOut.Text = "Time Out";
            this._timeOut.UseVisualStyleBackColor = false;
            // 
            // AttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1051, 687);
            this.Controls.Add(this._rootLayout);
            this.Name = "AttendanceForm";
            this.Text = "Attendance";
            this._rootLayout.ResumeLayout(false);
            this._searchCard.ResumeLayout(false);
            this._searchCard.PerformLayout();
            this._resultsCard.ResumeLayout(false);
            this._resultsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._matches)).EndInit();
            this._detailsCard.ResumeLayout(false);
            this._detailsLayout.ResumeLayout(false);
            this._namePanel.ResumeLayout(false);
            this._studentNoPanel.ResumeLayout(false);
            this._coursePanel.ResumeLayout(false);
            this._statusPanel.ResumeLayout(false);
            this._attendanceActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
