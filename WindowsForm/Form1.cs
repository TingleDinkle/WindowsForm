using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class Form1 : Form
    {
        private CustomerManager _customerManager;
        private int _editingIndex = -1; // To track which item we are editing

        public Form1()
        {
            InitializeComponent();
            // Composition Root: Inject dependencies here
            ICustomerRepository repository = new JsonCustomerRepository();
            _customerManager = new CustomerManager(repository);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // "Add" Button Logic
            if (!ValidateInputs(out string name, out string type, out int last, out int thism)) return;

            bool success = _customerManager.AddCustomer(name, type, last, thism);
            
            if (!success)
            {
                 MessageBox.Show("Could not add customer. Check if 'This Month' is less than 'Last Month'.");
                 return;
            }

            ClearInputs();
            RefreshListView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // "Update" Button Logic
            if (_editingIndex == -1) return;

            if (!ValidateInputs(out string name, out string type, out int last, out int thism)) return;

            bool success = _customerManager.UpdateCustomer(_editingIndex, name, type, last, thism);
            if (success)
            {
                MessageBox.Show("Customer updated successfully.");
                ClearInputs();
                RefreshListView();
                
                // Reset UI state
                btnSubmit.Enabled = true;
                btnUpdate.Visible = false;
                _editingIndex = -1;
            }
            else
            {
                MessageBox.Show("Failed to update. Please check inputs.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit");
                return;
            }
            
            _editingIndex = lvCustomer.SelectedItems[0].Index;
            Customer c = _customerManager.GetCustomer(_editingIndex);
            
            if (c != null)
            {
                // Populate fields
                txtName.Text = c.Name;
                // Handle type selection safely
                if (cboType.Items.Contains(c.CustomerType))
                {
                    cboType.SelectedItem = c.CustomerType;
                }
                else
                {
                    cboType.Text = c.CustomerType; 
                }
                
                txtLastMonth.Text = c.LastMonthReading.ToString();
                txtThisMonth.Text = c.ThisMonthReading.ToString();

                // Switch UI to Edit Mode
                btnSubmit.Enabled = false;
                btnUpdate.Visible = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete");
                return;
            }
            int index = lvCustomer.SelectedItems[0].Index;
            
            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                 _customerManager.DeleteCustomer(index);
                 
                 // If we were editing this specific item, cancel edit mode
                 if (_editingIndex == index)
                 {
                     ClearInputs();
                     btnSubmit.Enabled = true;
                     btnUpdate.Visible = false;
                     _editingIndex = -1;
                 }
                 
                 RefreshListView();
            }
        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV File|*.csv|Text File|*.txt";
            saveFileDialog.Title = "Export Customer Data";
            saveFileDialog.FileName = "customers.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    _customerManager.ExportToCsv(saveFileDialog.FileName);
                    MessageBox.Show("Data exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _customerManager.LoadData();
            RefreshListView();
        }

        private void RefreshListView()
        {
            lvCustomer.Items.Clear();
            var customers = _customerManager.GetAllCustomersForView();
            foreach (var customer in customers)
            {
                var item = new ListViewItem(customer[0]);
                for (int i = 1; i < customer.Length; i++)
                {
                    item.SubItems.Add(customer[i]);
                }
                lvCustomer.Items.Add(item);
            }
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtLastMonth.Text = "";
            txtThisMonth.Text = "";
            cboType.SelectedIndex = -1;
        }

        private bool ValidateInputs(out string name, out string type, out int last, out int thism)
        {
            name = txtName.Text;
            type = cboType.Text;
            last = 0;
            thism = 0;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(txtLastMonth.Text) || string.IsNullOrEmpty(txtThisMonth.Text))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }
            
            if (!int.TryParse(txtLastMonth.Text, out last))
            {
                MessageBox.Show("Invalid last month number");
                return false;
            }
            
            if (!int.TryParse(txtThisMonth.Text, out thism))
            {
                MessageBox.Show("Invalid this month number");
                return false;
            }
            return true;
        }
    }
}
