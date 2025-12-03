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

        public int PeopleCount
        {
            get
            {
                int.TryParse(txtPeopleCount.Text, out int val);
                return val;
            }
        }

        public CustomerForm()
        {
            InitializeComponent();
            cboType.SelectedIndex = 0; 
            UpdatePeopleFieldVisibility();
        }

        public CustomerForm(Customer existingCustomer) : this()
        {
            if (existingCustomer != null)
            {
                txtName.Text = existingCustomer.Name;
                SelectTypeInCombo(existingCustomer.CustomerType);
                
                txtLastMonth.Text = existingCustomer.LastMonthReading.ToString();
                txtThisMonth.Text = existingCustomer.ThisMonthReading.ToString();
                
                if (existingCustomer is HouseholdCustomer hh)
                {
                    txtPeopleCount.Text = hh.PeopleCount.ToString();
                }
            }
        }
        
        private void SelectTypeInCombo(string type)
        {
             // Default fallback
             cboType.SelectedIndex = 0;

             for(int i=0; i<cboType.Items.Count; i++)
             {
                 string? item = cboType.Items[i]?.ToString();
                 if (item != null && (type.Contains(item) || item.Contains(type)))
                 {
                     cboType.SelectedIndex = i;
                     return;
                 }
             }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePeopleFieldVisibility();
        }

        private void UpdatePeopleFieldVisibility()
        {
            string selected = cboType.Text;
            // Check for "Household" robustly
            if (selected.Contains("Household"))
            {
                txtPeopleCount.Enabled = true;
                txtPeopleCount.Visible = true;
                labelPeople.Visible = true;
            }
            else
            {
                txtPeopleCount.Enabled = false;
                txtPeopleCount.Visible = false;
                labelPeople.Visible = false;
                txtPeopleCount.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(cboType.Text))
            {
                MessageBox.Show("Name and Type are required.");
                this.DialogResult = DialogResult.None; 
                return;
            }

            if (!int.TryParse(txtLastMonth.Text, out int last) || !int.TryParse(txtThisMonth.Text, out int current))
            {
                MessageBox.Show("Readings must be valid numbers.");
                this.DialogResult = DialogResult.None;
                return;
            }
            
            // Logic from WaterCalc: if (WaterThisMonthAmount < WaterLastMonthAmount) ... Invalid
            if (current < last)
            {
                MessageBox.Show("This Month reading cannot be less than Last Month reading.");
                this.DialogResult = DialogResult.None;
                return;
            }
            
            if (cboType.Text.Contains("Household"))
            {
                 if (!int.TryParse(txtPeopleCount.Text, out int people) || people <= 0)
                 {
                      MessageBox.Show("Household must have at least 1 person.");
                      this.DialogResult = DialogResult.None;
                      return;
                 }
            }
        }
    }
}
