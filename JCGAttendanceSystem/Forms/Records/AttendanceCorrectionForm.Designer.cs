using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Records
{
    partial class AttendanceCorrectionForm
    {
        private Label _studentLabel; private Label _subtitleLabel; private Label _timeInLabel; private DateTimePicker _timeIn; private Label _timeOutLabel; private DateTimePicker _timeOut; private Label _notesLabel; private TextBox _notes; private Label _error; private Button _saveButton;
        private void InitializeComponent()
        {
            _studentLabel=new Label();_subtitleLabel=new Label();_timeInLabel=new Label();_timeIn=new DateTimePicker();_timeOutLabel=new Label();_timeOut=new DateTimePicker();_notesLabel=new Label();_notes=new TextBox();_error=new Label();_saveButton=new Button();SuspendLayout();
            BackColor=Color.White;ClientSize=new Size(560,500);StartPosition=FormStartPosition.CenterParent;Text="Correct Attendance";Font=new Font("Segoe UI",9F);
            _studentLabel.AutoSize=true;_studentLabel.Font=new Font("Segoe UI",15F,FontStyle.Bold);_studentLabel.ForeColor=Color.FromArgb(31,41,55);_studentLabel.Location=new Point(38,34);
            _subtitleLabel.Font=new Font("Segoe UI",9F);_subtitleLabel.ForeColor=Color.FromArgb(107,114,128);_subtitleLabel.Location=new Point(41,74);_subtitleLabel.Size=new Size(470,42);_subtitleLabel.Text="Corrections are restricted to administrators and are written to the audit trail.";
            _timeInLabel.AutoSize=true;_timeInLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_timeInLabel.Location=new Point(41,132);_timeInLabel.Text="Time In";_timeIn.Format=DateTimePickerFormat.Custom;_timeIn.CustomFormat="yyyy-MM-dd hh:mm tt";_timeIn.Location=new Point(44,157);_timeIn.Size=new Size(470,30);
            _timeOutLabel.AutoSize=true;_timeOutLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_timeOutLabel.Location=new Point(41,206);_timeOutLabel.Text="Time Out (uncheck if still timed in)";_timeOut.Format=DateTimePickerFormat.Custom;_timeOut.CustomFormat="yyyy-MM-dd hh:mm tt";_timeOut.ShowCheckBox=true;_timeOut.Location=new Point(44,231);_timeOut.Size=new Size(470,30);
            _notesLabel.AutoSize=true;_notesLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_notesLabel.Location=new Point(41,280);_notesLabel.Text="Correction Notes";_notes.Location=new Point(44,305);_notes.Size=new Size(470,80);_notes.Multiline=true;_notes.ScrollBars=ScrollBars.Vertical;
            _error.Font=new Font("Segoe UI",9F);_error.ForeColor=Color.FromArgb(198,40,40);_error.Location=new Point(44,395);_error.Size=new Size(470,32);
            _saveButton.BackColor=Color.FromArgb(30,136,229);_saveButton.ForeColor=Color.White;_saveButton.FlatStyle=FlatStyle.Flat;_saveButton.FlatAppearance.BorderSize=0;_saveButton.Font=new Font("Segoe UI",9.5F,FontStyle.Bold);_saveButton.Location=new Point(344,438);_saveButton.Size=new Size(170,42);_saveButton.Text="Save Correction";_saveButton.Click+=Save_Click;
            Controls.AddRange(new Control[]{_studentLabel,_subtitleLabel,_timeInLabel,_timeIn,_timeOutLabel,_timeOut,_notesLabel,_notes,_error,_saveButton});AcceptButton=_saveButton;ResumeLayout(false);PerformLayout();
        }
    }
}
