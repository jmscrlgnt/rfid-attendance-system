using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Settings
{
    partial class ChangePasswordForm
    {
        private Label _headingLabel; private Label _subtitleLabel; private Label _currentLabel; private TextBox _current; private Label _newLabel; private TextBox _newPassword; private Label _confirmLabel; private TextBox _confirm; private Label _error; private Button _saveButton;
        private void InitializeComponent(){_headingLabel=new Label();_subtitleLabel=new Label();_currentLabel=new Label();_current=new TextBox();_newLabel=new Label();_newPassword=new TextBox();_confirmLabel=new Label();_confirm=new TextBox();_error=new Label();_saveButton=new Button();SuspendLayout();BackColor=Color.White;ClientSize=new Size(500,470);StartPosition=FormStartPosition.CenterParent;Text="Change Password";Font=new Font("Segoe UI",9F);
            _headingLabel.AutoSize=true;_headingLabel.Font=new Font("Segoe UI",20F,FontStyle.Bold);_headingLabel.Location=new Point(38,34);_headingLabel.Text="Change Password";_subtitleLabel.AutoSize=true;_subtitleLabel.ForeColor=Color.FromArgb(107,114,128);_subtitleLabel.Location=new Point(41,82);_subtitleLabel.Text="Use at least 8 characters for the new password.";
            _currentLabel.AutoSize=true;_currentLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_currentLabel.Location=new Point(41,127);_currentLabel.Text="Current password";_current.Location=new Point(44,151);_current.Size=new Size(408,30);_current.UseSystemPasswordChar=true;_current.Font=new Font("Segoe UI",10F);
            _newLabel.AutoSize=true;_newLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_newLabel.Location=new Point(41,203);_newLabel.Text="New password";_newPassword.Location=new Point(44,227);_newPassword.Size=new Size(408,30);_newPassword.UseSystemPasswordChar=true;_newPassword.Font=new Font("Segoe UI",10F);
            _confirmLabel.AutoSize=true;_confirmLabel.Font=new Font("Segoe UI",9F,FontStyle.Bold);_confirmLabel.Location=new Point(41,279);_confirmLabel.Text="Confirm new password";_confirm.Location=new Point(44,303);_confirm.Size=new Size(408,30);_confirm.UseSystemPasswordChar=true;_confirm.Font=new Font("Segoe UI",10F);
            _error.ForeColor=Color.FromArgb(198,40,40);_error.Location=new Point(44,346);_error.Size=new Size(408,42);_saveButton.BackColor=Color.FromArgb(30,136,229);_saveButton.ForeColor=Color.White;_saveButton.FlatStyle=FlatStyle.Flat;_saveButton.FlatAppearance.BorderSize=0;_saveButton.Font=new Font("Segoe UI",9.5F,FontStyle.Bold);_saveButton.Location=new Point(282,402);_saveButton.Size=new Size(170,42);_saveButton.Text="Update Password";_saveButton.Click+=Save_Click;Controls.AddRange(new Control[]{_headingLabel,_subtitleLabel,_currentLabel,_current,_newLabel,_newPassword,_confirmLabel,_confirm,_error,_saveButton});AcceptButton=_saveButton;ResumeLayout(false);PerformLayout();}
    }
}
