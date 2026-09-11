using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Records
{
    partial class RecordsForm
    {
        private TableLayoutPanel _rootLayout;
        private Panel _filterCard;
        private TableLayoutPanel _filterLayout;
        private Panel _fromPanel;
        private Label _fromLabel;
        private DateTimePicker _from;
        private Panel _toPanel;
        private Label _toLabel;
        private DateTimePicker _to;
        private Panel _studentPanel;
        private Label _studentLabel;
        private TextBox _student;
        private Panel _coursePanel;
        private Label _courseLabel;
        private TextBox _course;
        private Panel _yearPanel;
        private Label _yearLabel;
        private ComboBox _year;
        private Panel _sectionPanel;
        private Label _sectionLabel;
        private TextBox _section;
        private Panel _statusPanel;
        private Label _statusLabel;
        private ComboBox _status;
        private FlowLayoutPanel _filterActions;
        private Button _applyButton;
        private Button _clearButton;
        private Button _exportButton;
        private Button _correctButton;
        private Panel _gridCard;
        private DataGridView _grid;
        private Label _empty;
        private DataGridViewTextBoxColumn _dateColumn;
        private DataGridViewTextBoxColumn _studentNumberColumn;
        private DataGridViewTextBoxColumn _studentColumn;
        private DataGridViewTextBoxColumn _courseColumn;
        private DataGridViewTextBoxColumn _yearSectionColumn;
        private DataGridViewTextBoxColumn _timeInColumn;
        private DataGridViewTextBoxColumn _timeOutColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _sourceColumn;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._filterCard = new System.Windows.Forms.Panel();
            this._filterLayout = new System.Windows.Forms.TableLayoutPanel();
            this._fromPanel = new System.Windows.Forms.Panel();
            this._from = new System.Windows.Forms.DateTimePicker();
            this._fromLabel = new System.Windows.Forms.Label();
            this._toPanel = new System.Windows.Forms.Panel();
            this._to = new System.Windows.Forms.DateTimePicker();
            this._toLabel = new System.Windows.Forms.Label();
            this._studentPanel = new System.Windows.Forms.Panel();
            this._student = new System.Windows.Forms.TextBox();
            this._studentLabel = new System.Windows.Forms.Label();
            this._coursePanel = new System.Windows.Forms.Panel();
            this._course = new System.Windows.Forms.TextBox();
            this._courseLabel = new System.Windows.Forms.Label();
            this._yearPanel = new System.Windows.Forms.Panel();
            this._year = new System.Windows.Forms.ComboBox();
            this._yearLabel = new System.Windows.Forms.Label();
            this._sectionPanel = new System.Windows.Forms.Panel();
            this._section = new System.Windows.Forms.TextBox();
            this._sectionLabel = new System.Windows.Forms.Label();
            this._statusPanel = new System.Windows.Forms.Panel();
            this._status = new System.Windows.Forms.ComboBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._filterActions = new System.Windows.Forms.FlowLayoutPanel();
            this._applyButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._exportButton = new System.Windows.Forms.Button();
            this._correctButton = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YearSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._empty = new System.Windows.Forms.Label();
            this._rootLayout.SuspendLayout();
            this._filterCard.SuspendLayout();
            this._filterLayout.SuspendLayout();
            this._fromPanel.SuspendLayout();
            this._toPanel.SuspendLayout();
            this._studentPanel.SuspendLayout();
            this._coursePanel.SuspendLayout();
            this._yearPanel.SuspendLayout();
            this._sectionPanel.SuspendLayout();
            this._statusPanel.SuspendLayout();
            this._filterActions.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._rootLayout.Controls.Add(this._filterCard, 0, 0);
            this._rootLayout.Controls.Add(this._gridCard, 0, 1);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 224F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1037, 713);
            this._rootLayout.TabIndex = 0;
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._filterLayout);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterCard.Location = new System.Drawing.Point(0, 0);
            this._filterCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this._filterCard.Size = new System.Drawing.Size(1037, 212);
            this._filterCard.TabIndex = 0;
            // 
            // _filterLayout
            // 
            this._filterLayout.ColumnCount = 4;
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._filterLayout.Controls.Add(this._fromPanel, 0, 0);
            this._filterLayout.Controls.Add(this._toPanel, 1, 0);
            this._filterLayout.Controls.Add(this._studentPanel, 2, 0);
            this._filterLayout.Controls.Add(this._coursePanel, 3, 0);
            this._filterLayout.Controls.Add(this._yearPanel, 0, 1);
            this._filterLayout.Controls.Add(this._sectionPanel, 1, 1);
            this._filterLayout.Controls.Add(this._statusPanel, 2, 1);
            this._filterLayout.Controls.Add(this._filterActions, 0, 2);
            this._filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterLayout.Location = new System.Drawing.Point(15, 16);
            this._filterLayout.Name = "_filterLayout";
            this._filterLayout.RowCount = 3;
            this._filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._filterLayout.Size = new System.Drawing.Size(1005, 178);
            this._filterLayout.TabIndex = 0;
            // 
            // _fromPanel
            // 
            this._fromPanel.Controls.Add(this._from);
            this._fromPanel.Controls.Add(this._fromLabel);
            this._fromPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fromPanel.Location = new System.Drawing.Point(5, 5);
            this._fromPanel.Margin = new System.Windows.Forms.Padding(5);
            this._fromPanel.Name = "_fromPanel";
            this._fromPanel.Size = new System.Drawing.Size(241, 52);
            this._fromPanel.TabIndex = 0;
            // 
            // _from
            // 
            this._from.Dock = System.Windows.Forms.DockStyle.Top;
            this._from.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._from.Location = new System.Drawing.Point(0, 22);
            this._from.Name = "_from";
            this._from.ShowCheckBox = true;
            this._from.Size = new System.Drawing.Size(241, 24);
            this._from.TabIndex = 0;
            // 
            // _fromLabel
            // 
            this._fromLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._fromLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._fromLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._fromLabel.Location = new System.Drawing.Point(0, 0);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Size = new System.Drawing.Size(241, 22);
            this._fromLabel.TabIndex = 1;
            this._fromLabel.Text = "From";
            // 
            // _toPanel
            // 
            this._toPanel.Controls.Add(this._to);
            this._toPanel.Controls.Add(this._toLabel);
            this._toPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toPanel.Location = new System.Drawing.Point(256, 5);
            this._toPanel.Margin = new System.Windows.Forms.Padding(5);
            this._toPanel.Name = "_toPanel";
            this._toPanel.Size = new System.Drawing.Size(241, 52);
            this._toPanel.TabIndex = 1;
            // 
            // _to
            // 
            this._to.Dock = System.Windows.Forms.DockStyle.Top;
            this._to.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._to.Location = new System.Drawing.Point(0, 22);
            this._to.Name = "_to";
            this._to.ShowCheckBox = true;
            this._to.Size = new System.Drawing.Size(241, 24);
            this._to.TabIndex = 0;
            // 
            // _toLabel
            // 
            this._toLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._toLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._toLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._toLabel.Location = new System.Drawing.Point(0, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(241, 22);
            this._toLabel.TabIndex = 1;
            this._toLabel.Text = "To";
            // 
            // _studentPanel
            // 
            this._studentPanel.Controls.Add(this._student);
            this._studentPanel.Controls.Add(this._studentLabel);
            this._studentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._studentPanel.Location = new System.Drawing.Point(507, 5);
            this._studentPanel.Margin = new System.Windows.Forms.Padding(5);
            this._studentPanel.Name = "_studentPanel";
            this._studentPanel.Size = new System.Drawing.Size(241, 52);
            this._studentPanel.TabIndex = 2;
            // 
            // _student
            // 
            this._student.Dock = System.Windows.Forms.DockStyle.Top;
            this._student.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._student.Location = new System.Drawing.Point(0, 22);
            this._student.Name = "_student";
            this._student.Size = new System.Drawing.Size(241, 24);
            this._student.TabIndex = 0;
            // 
            // _studentLabel
            // 
            this._studentLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._studentLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._studentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._studentLabel.Location = new System.Drawing.Point(0, 0);
            this._studentLabel.Name = "_studentLabel";
            this._studentLabel.Size = new System.Drawing.Size(241, 22);
            this._studentLabel.TabIndex = 1;
            this._studentLabel.Text = "Student";
            // 
            // _coursePanel
            // 
            this._coursePanel.Controls.Add(this._course);
            this._coursePanel.Controls.Add(this._courseLabel);
            this._coursePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._coursePanel.Location = new System.Drawing.Point(758, 5);
            this._coursePanel.Margin = new System.Windows.Forms.Padding(5);
            this._coursePanel.Name = "_coursePanel";
            this._coursePanel.Size = new System.Drawing.Size(242, 52);
            this._coursePanel.TabIndex = 3;
            // 
            // _course
            // 
            this._course.Dock = System.Windows.Forms.DockStyle.Top;
            this._course.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._course.Location = new System.Drawing.Point(0, 22);
            this._course.Name = "_course";
            this._course.Size = new System.Drawing.Size(242, 24);
            this._course.TabIndex = 0;
            // 
            // _courseLabel
            // 
            this._courseLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._courseLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._courseLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._courseLabel.Location = new System.Drawing.Point(0, 0);
            this._courseLabel.Name = "_courseLabel";
            this._courseLabel.Size = new System.Drawing.Size(242, 22);
            this._courseLabel.TabIndex = 1;
            this._courseLabel.Text = "Course";
            // 
            // _yearPanel
            // 
            this._yearPanel.Controls.Add(this._year);
            this._yearPanel.Controls.Add(this._yearLabel);
            this._yearPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._yearPanel.Location = new System.Drawing.Point(5, 67);
            this._yearPanel.Margin = new System.Windows.Forms.Padding(5);
            this._yearPanel.Name = "_yearPanel";
            this._yearPanel.Size = new System.Drawing.Size(241, 52);
            this._yearPanel.TabIndex = 4;
            // 
            // _year
            // 
            this._year.Dock = System.Windows.Forms.DockStyle.Top;
            this._year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._year.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._year.Items.AddRange(new object[] {
            "All",
            "1",
            "2",
            "3",
            "4"});
            this._year.Location = new System.Drawing.Point(0, 22);
            this._year.Name = "_year";
            this._year.Size = new System.Drawing.Size(241, 25);
            this._year.TabIndex = 0;
            // 
            // _yearLabel
            // 
            this._yearLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._yearLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._yearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._yearLabel.Location = new System.Drawing.Point(0, 0);
            this._yearLabel.Name = "_yearLabel";
            this._yearLabel.Size = new System.Drawing.Size(241, 22);
            this._yearLabel.TabIndex = 1;
            this._yearLabel.Text = "Year";
            // 
            // _sectionPanel
            // 
            this._sectionPanel.Controls.Add(this._section);
            this._sectionPanel.Controls.Add(this._sectionLabel);
            this._sectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sectionPanel.Location = new System.Drawing.Point(256, 67);
            this._sectionPanel.Margin = new System.Windows.Forms.Padding(5);
            this._sectionPanel.Name = "_sectionPanel";
            this._sectionPanel.Size = new System.Drawing.Size(241, 52);
            this._sectionPanel.TabIndex = 5;
            // 
            // _section
            // 
            this._section.Dock = System.Windows.Forms.DockStyle.Top;
            this._section.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._section.Location = new System.Drawing.Point(0, 22);
            this._section.Name = "_section";
            this._section.Size = new System.Drawing.Size(241, 24);
            this._section.TabIndex = 0;
            // 
            // _sectionLabel
            // 
            this._sectionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._sectionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._sectionLabel.Location = new System.Drawing.Point(0, 0);
            this._sectionLabel.Name = "_sectionLabel";
            this._sectionLabel.Size = new System.Drawing.Size(241, 22);
            this._sectionLabel.TabIndex = 1;
            this._sectionLabel.Text = "Section";
            // 
            // _statusPanel
            // 
            this._statusPanel.Controls.Add(this._status);
            this._statusPanel.Controls.Add(this._statusLabel);
            this._statusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusPanel.Location = new System.Drawing.Point(507, 67);
            this._statusPanel.Margin = new System.Windows.Forms.Padding(5);
            this._statusPanel.Name = "_statusPanel";
            this._statusPanel.Size = new System.Drawing.Size(241, 52);
            this._statusPanel.TabIndex = 6;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Top;
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._status.Items.AddRange(new object[] {
            "All",
            "Timed In",
            "Completed"});
            this._status.Location = new System.Drawing.Point(0, 22);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(241, 25);
            this._status.TabIndex = 0;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._statusLabel.Location = new System.Drawing.Point(0, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(241, 22);
            this._statusLabel.TabIndex = 1;
            this._statusLabel.Text = "Status";
            // 
            // _filterActions
            // 
            this._filterLayout.SetColumnSpan(this._filterActions, 4);
            this._filterActions.Controls.Add(this._applyButton);
            this._filterActions.Controls.Add(this._clearButton);
            this._filterActions.Controls.Add(this._exportButton);
            this._filterActions.Controls.Add(this._correctButton);
            this._filterActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterActions.Location = new System.Drawing.Point(3, 127);
            this._filterActions.Name = "_filterActions";
            this._filterActions.Padding = new System.Windows.Forms.Padding(5, 7, 0, 0);
            this._filterActions.Size = new System.Drawing.Size(999, 48);
            this._filterActions.TabIndex = 7;
            this._filterActions.WrapContents = false;
            // 
            // _applyButton
            // 
            this._applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this._applyButton.FlatAppearance.BorderSize = 0;
            this._applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._applyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._applyButton.ForeColor = System.Drawing.Color.White;
            this._applyButton.Location = new System.Drawing.Point(5, 7);
            this._applyButton.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this._applyButton.Name = "_applyButton";
            this._applyButton.Size = new System.Drawing.Size(96, 35);
            this._applyButton.TabIndex = 0;
            this._applyButton.Text = "Apply Filters";
            this._applyButton.UseVisualStyleBackColor = false;
            // 
            // _clearButton
            // 
            this._clearButton.BackColor = System.Drawing.Color.White;
            this._clearButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(226)))));
            this._clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._clearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._clearButton.Location = new System.Drawing.Point(110, 7);
            this._clearButton.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(73, 35);
            this._clearButton.TabIndex = 1;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = false;
            // 
            // _exportButton
            // 
            this._exportButton.BackColor = System.Drawing.Color.White;
            this._exportButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(226)))));
            this._exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._exportButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._exportButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._exportButton.Location = new System.Drawing.Point(192, 7);
            this._exportButton.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(94, 35);
            this._exportButton.TabIndex = 2;
            this._exportButton.Text = "Export CSV";
            this._exportButton.UseVisualStyleBackColor = false;
            this._exportButton.Click += new System.EventHandler(this.Export_Click);
            // 
            // _correctButton
            // 
            this._correctButton.BackColor = System.Drawing.Color.White;
            this._correctButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(226)))));
            this._correctButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._correctButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._correctButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._correctButton.Location = new System.Drawing.Point(298, 10);
            this._correctButton.Name = "_correctButton";
            this._correctButton.Size = new System.Drawing.Size(120, 35);
            this._correctButton.TabIndex = 3;
            this._correctButton.Text = "Correct Selected";
            this._correctButton.UseVisualStyleBackColor = false;
            this._correctButton.Click += new System.EventHandler(this.Correct_Click);
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Controls.Add(this._empty);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(3, 227);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this._gridCard.Size = new System.Drawing.Size(1031, 483);
            this._gridCard.TabIndex = 1;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.ColumnHeadersHeight = 36;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.YearSection,
            this.TimeIn,
            this.TimeOut});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle3;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.Location = new System.Drawing.Point(15, 16);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowTemplate.Height = 34;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(999, 421);
            this._grid.TabIndex = 0;
            this._grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 95;
            // 
            // YearSection
            // 
            this.YearSection.HeaderText = "Yr/Sec";
            this.YearSection.Name = "YearSection";
            this.YearSection.ReadOnly = true;
            this.YearSection.Width = 80;
            // 
            // TimeIn
            // 
            this.TimeIn.HeaderText = "Time In";
            this.TimeIn.Name = "TimeIn";
            this.TimeIn.ReadOnly = true;
            this.TimeIn.Width = 105;
            // 
            // TimeOut
            // 
            this.TimeOut.HeaderText = "Time Out";
            this.TimeOut.Name = "TimeOut";
            this.TimeOut.ReadOnly = true;
            this.TimeOut.Width = 105;
            // 
            // _empty
            // 
            this._empty.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._empty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._empty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._empty.Location = new System.Drawing.Point(15, 437);
            this._empty.Name = "_empty";
            this._empty.Size = new System.Drawing.Size(999, 28);
            this._empty.TabIndex = 1;
            this._empty.Text = "No attendance records match the current filters.";
            this._empty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1037, 713);
            this.Controls.Add(this._rootLayout);
            this.Name = "RecordsForm";
            this.Text = "Records";
            this._rootLayout.ResumeLayout(false);
            this._filterCard.ResumeLayout(false);
            this._filterLayout.ResumeLayout(false);
            this._fromPanel.ResumeLayout(false);
            this._toPanel.ResumeLayout(false);
            this._studentPanel.ResumeLayout(false);
            this._studentPanel.PerformLayout();
            this._coursePanel.ResumeLayout(false);
            this._coursePanel.PerformLayout();
            this._yearPanel.ResumeLayout(false);
            this._sectionPanel.ResumeLayout(false);
            this._sectionPanel.PerformLayout();
            this._statusPanel.ResumeLayout(false);
            this._filterActions.ResumeLayout(false);
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn YearSection;
        private DataGridViewTextBoxColumn TimeIn;
        private DataGridViewTextBoxColumn TimeOut;
    }
}
