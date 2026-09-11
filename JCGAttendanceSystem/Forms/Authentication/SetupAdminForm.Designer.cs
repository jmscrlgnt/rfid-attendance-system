using System.Drawing;
using System.Windows.Forms;

namespace JCGAttendanceSystem.Forms.Authentication
{
    partial class SetupAdminForm
    {
        private Label _headingLabel; private Label _subtitleLabel; private Label _usernameLabel; private TextBox _username; private Label _passwordLabel; private TextBox _password; private Label _confirmLabel; private TextBox _confirm; private Label _error; private Button _createButton;
        private void InitializeComponent()
        {
            _headingLabel=new Label(); _subtitleLabel=new Label(); _usernameLabel=new Label(); _username=new TextBox(); _passwordLabel=new Label(); _password=new TextBox(); _confirmLabel=new Label(); _confirm=new TextBox(); _error=new Label(); _createButton=new Button(); SuspendLayout();
            BackColor=Color.White; ClientSize=new Size(560,560); FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; MinimizeBox=false; StartPosition=FormStartPosition.CenterScreen; Text="First-run administrator setup"; Font=new Font("Segoe UI",9F);
            _headingLabel.AutoSize=true; _headingLabel.Font=new Font("Segoe UI",22F,FontStyle.Bold); _headingLabel.ForeColor=Color.FromArgb(31,41,55); _headingLabel.Location=new Point(50,48); _headingLabel.Text="JCG Attendance System";
            _subtitleLabel.Font=new Font("Segoe UI",10F); _subtitleLabel.ForeColor=Color.FromArgb(107,114,128); _subtitleLabel.Location=new Point(53,98); _subtitleLabel.Size=new Size(445,54); _subtitleLabel.Text="Create the first administrator. There is no default administrator password.";
            _usernameLabel.AutoSize=true; _usernameLabel.Font=new Font("Segoe UI",9.5F,FontStyle.Bold); _usernameLabel.Location=new Point(53,173); _usernameLabel.Text="Administrator username"; _username.Font=new Font("Segoe UI",11F); _username.Location=new Point(56,198); _username.Size=new Size(442,32);
            _passwordLabel.AutoSize=true; _passwordLabel.Font=new Font("Segoe UI",9.5F,FontStyle.Bold); _passwordLabel.Location=new Point(53,253); _passwordLabel.Text="Password (minimum 8 characters)"; _password.Font=new Font("Segoe UI",11F); _password.Location=new Point(56,278); _password.Size=new Size(442,32); _password.UseSystemPasswordChar=true;
            _confirmLabel.AutoSize=true; _confirmLabel.Font=new Font("Segoe UI",9.5F,FontStyle.Bold); _confirmLabel.Location=new Point(53,333); _confirmLabel.Text="Confirm password"; _confirm.Font=new Font("Segoe UI",11F); _confirm.Location=new Point(56,358); _confirm.Size=new Size(442,32); _confirm.UseSystemPasswordChar=true;
            _error.Font=new Font("Segoe UI",9F); _error.ForeColor=Color.FromArgb(198,40,40); _error.Location=new Point(56,403); _error.Size=new Size(442,44);
            _createButton.BackColor=Color.FromArgb(30,136,229); _createButton.FlatStyle=FlatStyle.Flat; _createButton.FlatAppearance.BorderSize=0; _createButton.ForeColor=Color.White; _createButton.Font=new Font("Segoe UI",10F,FontStyle.Bold); _createButton.Location=new Point(56,462); _createButton.Size=new Size(442,48); _createButton.Text="Create Administrator"; _createButton.Click+=Create_Click;
            Controls.AddRange(new Control[]{_headingLabel,_subtitleLabel,_usernameLabel,_username,_passwordLabel,_password,_confirmLabel,_confirm,_error,_createButton}); AcceptButton=_createButton; ResumeLayout(false); PerformLayout();
        }
    }
}
