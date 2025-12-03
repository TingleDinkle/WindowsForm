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

            // Show Login Form first
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // If login successful, run the main form
                Application.Run(new Form1());
            }
            else
            {
                // Exit application
                Application.Exit();
            }
        }
    }
}