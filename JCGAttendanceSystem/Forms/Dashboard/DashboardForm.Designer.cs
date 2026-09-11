using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Dashboard
{
    partial class DashboardForm
    {
        private TableLayoutPanel _rootLayout;
        private Panel _introPanel;
        private Label _todayTitle;
        private Label _dateLabel;
        private TableLayoutPanel _metricLayout;
        private Panel _totalCard;
        private Panel _presentCard;
        private Panel _timedInCard;
        private Panel _completedCard;
        private Panel _totalAccent;
        private Panel _presentAccent;
        private Panel _timedInAccent;
        private Panel _completedAccent;
        private Label _totalCaption;
        private Label _presentCaption;
        private Label _timedInCaption;
        private Label _completedCaption;
        private Label _total;
        private Label _present;
        private Label _timedIn;
        private Label _completed;
        private Panel _activityCard;
        private Label _activityTitle;
        private DataGridView _grid;
        private Label _empty;
        private Timer _refreshTimer;
        private DataGridViewTextBoxColumn _studentNumberColumn;
        private DataGridViewTextBoxColumn _studentColumn;
        private DataGridViewTextBoxColumn _eventColumn;
        private DataGridViewTextBoxColumn _courseColumn;
        private DataGridViewTextBoxColumn _yearSectionColumn;
        private DataGridViewTextBoxColumn _eventTimeColumn;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._introPanel = new System.Windows.Forms.Panel();
            this._dateLabel = new System.Windows.Forms.Label();
            this._todayTitle = new System.Windows.Forms.Label();
            this._metricLayout = new System.Windows.Forms.TableLayoutPanel();
            this._totalCard = new System.Windows.Forms.Panel();
            this._total = new System.Windows.Forms.Label();
            this._totalCaption = new System.Windows.Forms.Label();
            this._totalAccent = new System.Windows.Forms.Panel();
            this._presentCard = new System.Windows.Forms.Panel();
            this._present = new System.Windows.Forms.Label();
            this._presentCaption = new System.Windows.Forms.Label();
            this._presentAccent = new System.Windows.Forms.Panel();
            this._timedInCard = new System.Windows.Forms.Panel();
            this._timedIn = new System.Windows.Forms.Label();
            this._timedInCaption = new System.Windows.Forms.Label();
            this._timedInAccent = new System.Windows.Forms.Panel();
            this._completedCard = new System.Windows.Forms.Panel();
            this._completed = new System.Windows.Forms.Label();
            this._completedCaption = new System.Windows.Forms.Label();
            this._completedAccent = new System.Windows.Forms.Panel();
            this._activityCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.YearSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._empty = new System.Windows.Forms.Label();
            this._activityTitle = new System.Windows.Forms.Label();
            this._refreshTimer = new System.Windows.Forms.Timer(this.components);
            this._rootLayout.SuspendLayout();
            this._introPanel.SuspendLayout();
            this._metricLayout.SuspendLayout();
            this._totalCard.SuspendLayout();
            this._presentCard.SuspendLayout();
            this._timedInCard.SuspendLayout();
            this._completedCard.SuspendLayout();
            this._activityCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._rootLayout.Controls.Add(this._introPanel, 0, 0);
            this._rootLayout.Controls.Add(this._metricLayout, 0, 1);
            this._rootLayout.Controls.Add(this._activityCard, 0, 2);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 3;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1394, 702);
            this._rootLayout.TabIndex = 0;
            // 
            // _introPanel
            // 
            this._introPanel.Controls.Add(this._dateLabel);
            this._introPanel.Controls.Add(this._todayTitle);
            this._introPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._introPanel.Location = new System.Drawing.Point(3, 3);
            this._introPanel.Name = "_introPanel";
            this._introPanel.Size = new System.Drawing.Size(1388, 62);
            this._introPanel.TabIndex = 0;
            // 
            // _dateLabel
            // 
            this._dateLabel.AutoSize = true;
            this._dateLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._dateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._dateLabel.Location = new System.Drawing.Point(5, 45);
            this._dateLabel.Name = "_dateLabel";
            this._dateLabel.Size = new System.Drawing.Size(43, 17);
            this._dateLabel.TabIndex = 0;
            this._dateLabel.Text = "Today";
            // 
            // _todayTitle
            // 
            this._todayTitle.AutoSize = true;
            this._todayTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._todayTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this._todayTitle.Location = new System.Drawing.Point(3, 6);
            this._todayTitle.Name = "_todayTitle";
            this._todayTitle.Size = new System.Drawing.Size(241, 37);
            this._todayTitle.TabIndex = 1;
            this._todayTitle.Text = "Today at a glance";
            // 
            // _metricLayout
            // 
            this._metricLayout.ColumnCount = 4;
            this._metricLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._metricLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._metricLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._metricLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._metricLayout.Controls.Add(this._totalCard, 0, 0);
            this._metricLayout.Controls.Add(this._presentCard, 1, 0);
            this._metricLayout.Controls.Add(this._timedInCard, 2, 0);
            this._metricLayout.Controls.Add(this._completedCard, 3, 0);
            this._metricLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._metricLayout.Location = new System.Drawing.Point(3, 71);
            this._metricLayout.Name = "_metricLayout";
            this._metricLayout.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._metricLayout.RowCount = 1;
            this._metricLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._metricLayout.Size = new System.Drawing.Size(1388, 117);
            this._metricLayout.TabIndex = 1;
            // 
            // _totalCard
            // 
            this._totalCard.BackColor = System.Drawing.Color.White;
            this._totalCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._totalCard.Controls.Add(this._total);
            this._totalCard.Controls.Add(this._totalCaption);
            this._totalCard.Controls.Add(this._totalAccent);
            this._totalCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalCard.Location = new System.Drawing.Point(0, 0);
            this._totalCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._totalCard.Name = "_totalCard";
            this._totalCard.Size = new System.Drawing.Size(337, 105);
            this._totalCard.TabIndex = 0;
            // 
            // _total
            // 
            this._total.AutoSize = true;
            this._total.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this._total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this._total.Location = new System.Drawing.Point(17, 47);
            this._total.Name = "_total";
            this._total.Size = new System.Drawing.Size(40, 47);
            this._total.TabIndex = 0;
            this._total.Text = "0";
            // 
            // _totalCaption
            // 
            this._totalCaption.AutoSize = true;
            this._totalCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._totalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._totalCaption.Location = new System.Drawing.Point(17, 16);
            this._totalCaption.Name = "_totalCaption";
            this._totalCaption.Size = new System.Drawing.Size(139, 17);
            this._totalCaption.TabIndex = 1;
            this._totalCaption.Text = "Total Active Students";
            // 
            // _totalAccent
            // 
            this._totalAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this._totalAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this._totalAccent.Location = new System.Drawing.Point(0, 0);
            this._totalAccent.Name = "_totalAccent";
            this._totalAccent.Size = new System.Drawing.Size(335, 3);
            this._totalAccent.TabIndex = 2;
            // 
            // _presentCard
            // 
            this._presentCard.BackColor = System.Drawing.Color.White;
            this._presentCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._presentCard.Controls.Add(this._present);
            this._presentCard.Controls.Add(this._presentCaption);
            this._presentCard.Controls.Add(this._presentAccent);
            this._presentCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._presentCard.Location = new System.Drawing.Point(347, 0);
            this._presentCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._presentCard.Name = "_presentCard";
            this._presentCard.Size = new System.Drawing.Size(337, 105);
            this._presentCard.TabIndex = 1;
            // 
            // _present
            // 
            this._present.AutoSize = true;
            this._present.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this._present.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this._present.Location = new System.Drawing.Point(17, 47);
            this._present.Name = "_present";
            this._present.Size = new System.Drawing.Size(40, 47);
            this._present.TabIndex = 0;
            this._present.Text = "0";
            // 
            // _presentCaption
            // 
            this._presentCaption.AutoSize = true;
            this._presentCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._presentCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._presentCaption.Location = new System.Drawing.Point(17, 16);
            this._presentCaption.Name = "_presentCaption";
            this._presentCaption.Size = new System.Drawing.Size(95, 17);
            this._presentCaption.TabIndex = 1;
            this._presentCaption.Text = "Present Today";
            // 
            // _presentAccent
            // 
            this._presentAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this._presentAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this._presentAccent.Location = new System.Drawing.Point(0, 0);
            this._presentAccent.Name = "_presentAccent";
            this._presentAccent.Size = new System.Drawing.Size(335, 3);
            this._presentAccent.TabIndex = 2;
            // 
            // _timedInCard
            // 
            this._timedInCard.BackColor = System.Drawing.Color.White;
            this._timedInCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._timedInCard.Controls.Add(this._timedIn);
            this._timedInCard.Controls.Add(this._timedInCaption);
            this._timedInCard.Controls.Add(this._timedInAccent);
            this._timedInCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._timedInCard.Location = new System.Drawing.Point(694, 0);
            this._timedInCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._timedInCard.Name = "_timedInCard";
            this._timedInCard.Size = new System.Drawing.Size(337, 105);
            this._timedInCard.TabIndex = 2;
            // 
            // _timedIn
            // 
            this._timedIn.AutoSize = true;
            this._timedIn.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this._timedIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this._timedIn.Location = new System.Drawing.Point(17, 47);
            this._timedIn.Name = "_timedIn";
            this._timedIn.Size = new System.Drawing.Size(40, 47);
            this._timedIn.TabIndex = 0;
            this._timedIn.Text = "0";
            // 
            // _timedInCaption
            // 
            this._timedInCaption.AutoSize = true;
            this._timedInCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._timedInCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._timedInCaption.Location = new System.Drawing.Point(17, 16);
            this._timedInCaption.Name = "_timedInCaption";
            this._timedInCaption.Size = new System.Drawing.Size(124, 17);
            this._timedInCaption.TabIndex = 1;
            this._timedInCaption.Text = "Currently Timed In";
            // 
            // _timedInAccent
            // 
            this._timedInAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this._timedInAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this._timedInAccent.Location = new System.Drawing.Point(0, 0);
            this._timedInAccent.Name = "_timedInAccent";
            this._timedInAccent.Size = new System.Drawing.Size(335, 3);
            this._timedInAccent.TabIndex = 2;
            // 
            // _completedCard
            // 
            this._completedCard.BackColor = System.Drawing.Color.White;
            this._completedCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._completedCard.Controls.Add(this._completed);
            this._completedCard.Controls.Add(this._completedCaption);
            this._completedCard.Controls.Add(this._completedAccent);
            this._completedCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._completedCard.Location = new System.Drawing.Point(1041, 0);
            this._completedCard.Margin = new System.Windows.Forms.Padding(0);
            this._completedCard.Name = "_completedCard";
            this._completedCard.Size = new System.Drawing.Size(347, 105);
            this._completedCard.TabIndex = 3;
            // 
            // _completed
            // 
            this._completed.AutoSize = true;
            this._completed.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this._completed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(92)))));
            this._completed.Location = new System.Drawing.Point(17, 47);
            this._completed.Name = "_completed";
            this._completed.Size = new System.Drawing.Size(40, 47);
            this._completed.TabIndex = 0;
            this._completed.Text = "0";
            // 
            // _completedCaption
            // 
            this._completedCaption.AutoSize = true;
            this._completedCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._completedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._completedCaption.Location = new System.Drawing.Point(17, 16);
            this._completedCaption.Name = "_completedCaption";
            this._completedCaption.Size = new System.Drawing.Size(116, 17);
            this._completedCaption.TabIndex = 1;
            this._completedCaption.Text = "Completed Today";
            // 
            // _completedAccent
            // 
            this._completedAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(123)))));
            this._completedAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this._completedAccent.Location = new System.Drawing.Point(0, 0);
            this._completedAccent.Name = "_completedAccent";
            this._completedAccent.Size = new System.Drawing.Size(345, 3);
            this._completedAccent.TabIndex = 2;
            // 
            // _activityCard
            // 
            this._activityCard.BackColor = System.Drawing.Color.White;
            this._activityCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._activityCard.Controls.Add(this._grid);
            this._activityCard.Controls.Add(this._empty);
            this._activityCard.Controls.Add(this._activityTitle);
            this._activityCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._activityCard.Location = new System.Drawing.Point(3, 194);
            this._activityCard.Name = "_activityCard";
            this._activityCard.Padding = new System.Windows.Forms.Padding(15, 42, 15, 14);
            this._activityCard.Size = new System.Drawing.Size(1388, 505);
            this._activityCard.TabIndex = 2;
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
            this.YearSection,
            this.EventTime});
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
            this._grid.Location = new System.Drawing.Point(15, 42);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowTemplate.Height = 34;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1356, 421);
            this._grid.TabIndex = 0;
            this._grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            // 
            // YearSection
            // 
            this.YearSection.HeaderText = "Year / Section";
            this.YearSection.Name = "YearSection";
            this.YearSection.ReadOnly = true;
            this.YearSection.Width = 120;
            // 
            // EventTime
            // 
            this.EventTime.HeaderText = "Time";
            this.EventTime.Name = "EventTime";
            this.EventTime.ReadOnly = true;
            this.EventTime.Width = 150;
            // 
            // _empty
            // 
            this._empty.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._empty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._empty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this._empty.Location = new System.Drawing.Point(15, 463);
            this._empty.Name = "_empty";
            this._empty.Size = new System.Drawing.Size(1356, 26);
            this._empty.TabIndex = 1;
            this._empty.Text = "No attendance activity has been recorded yet.";
            this._empty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _activityTitle
            // 
            this._activityTitle.AutoSize = true;
            this._activityTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._activityTitle.Location = new System.Drawing.Point(15, 14);
            this._activityTitle.Name = "_activityTitle";
            this._activityTitle.Size = new System.Drawing.Size(213, 21);
            this._activityTitle.TabIndex = 2;
            this._activityTitle.Text = "Recent attendance activity";
            // 
            // _refreshTimer
            // 
            this._refreshTimer.Interval = 30000;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1394, 702);
            this.Controls.Add(this._rootLayout);
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this._rootLayout.ResumeLayout(false);
            this._introPanel.ResumeLayout(false);
            this._introPanel.PerformLayout();
            this._metricLayout.ResumeLayout(false);
            this._totalCard.ResumeLayout(false);
            this._totalCard.PerformLayout();
            this._presentCard.ResumeLayout(false);
            this._presentCard.PerformLayout();
            this._timedInCard.ResumeLayout(false);
            this._timedInCard.PerformLayout();
            this._completedCard.ResumeLayout(false);
            this._completedCard.PerformLayout();
            this._activityCard.ResumeLayout(false);
            this._activityCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn YearSection;
        private DataGridViewTextBoxColumn EventTime;
        private System.ComponentModel.IContainer components;
    }
}
