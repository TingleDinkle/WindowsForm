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
            cboType.SelectedIndex = 0; // Default to Household
            UpdatePeopleFieldVisibility();
        }

        public CustomerForm(Customer existingCustomer) : this()
        {
            if (existingCustomer != null)
            {
                txtName.Text = existingCustomer.Name;
                
                // Map complex type names to simple Combo box items if needed, or ensure exact match
                // The Manager NormalizeType helps, but UI needs to match "Household", "Administrative Agency" etc.
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
             for(int i=0; i<cboType.Items.Count; i++)
             {
                 string item = cboType.Items[i].ToString();
                 // Check for partial matches because "Administrative Agency" vs "Administrative Agency / Public Service"
                 if (type.Contains(item) || item.Contains(type))
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
            if (selected == "Household")
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

            if (current < last)
            {
                MessageBox.Show("This Month cannot be less than Last Month.");
                this.DialogResult = DialogResult.None;
                return;
            }
            
            if (cboType.Text == "Household")
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