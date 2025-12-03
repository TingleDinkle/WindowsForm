namespace WindowsForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Startup Sequence:
            // 1. Show the Login Form modally.
            // 2. Wait for the user to log in.
            // 3. If login is successful (DialogResult.OK), launch the main Form1.
            // 4. Otherwise, exit the application.
            
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // If login successful, run the main form
                Application.Run(new Form1());
            }
            else
            {
                // User cancelled or failed login -> Exit application
                Application.Exit();
            }
        }
    }
}