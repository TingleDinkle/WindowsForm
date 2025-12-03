using System;
using System.Windows.Forms;

namespace WindowsForm
{
    // Simple Authentication Form.
    // Acts as a gatekeeper before the main application is loaded.
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Event Handler: Validates credentials.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;

            // Hardcoded credentials for demonstration purposes.
            // In a production environment, this should validate against a secure database or identity service.
            if (user == "admin" && pass == "admin")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event Handler: Closes the application if the user cancels.
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
