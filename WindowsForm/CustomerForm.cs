using System;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class CustomerForm : Form
    {
        public string CustomerName { get { return txtName.Text; } }
        public string CustomerType { get { return cboType.Text; } }
        
        public int LastMonthReading 
        { 
            get 
            { 
                int.TryParse(txtLastMonth.Text, out int val); 
                return val; 
            } 
        }

        public int ThisMonthReading
        {
            get
            {
                int.TryParse(txtThisMonth.Text, out int val);
                return val;
            }
        }

        public CustomerForm()
        {
            InitializeComponent();
        }

        // Constructor for Editing mode
        public CustomerForm(Customer existingCustomer) : this()
        {
            if (existingCustomer != null)
            {
                txtName.Text = existingCustomer.Name;
                cboType.Text = existingCustomer.CustomerType;
                txtLastMonth.Text = existingCustomer.LastMonthReading.ToString();
                txtThisMonth.Text = existingCustomer.ThisMonthReading.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Basic validation inside the form before closing
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(cboType.Text))
            {
                MessageBox.Show("Name and Type are required.");
                this.DialogResult = DialogResult.None; // Prevent closing
                return;
            }

            if (!int.TryParse(txtLastMonth.Text, out int last) || !int.TryParse(txtThisMonth.Text, out int current))
            {
                MessageBox.Show("Readings must be valid numbers.");
                this.DialogResult = DialogResult.None;
                return;
            }

            if (current < last)
            {
                MessageBox.Show("This Month cannot be less than Last Month.");
                this.DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
