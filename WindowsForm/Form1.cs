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

        public Form1()
        {
            InitializeComponent();
            // Composition Root
            ICustomerRepository repository = new JsonCustomerRepository();
            _customerManager = new CustomerManager(repository);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _customerManager.LoadData();
            RefreshListView();
        }

        // "Add Customer" Button (previously btnSubmit)
        private void button1_Click(object sender, EventArgs e)
        {
            // Open the CustomerForm as a dialog for ADDING
            using (var form = new CustomerForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool success = _customerManager.AddCustomer(
                        form.CustomerName, 
                        form.CustomerType, 
                        form.LastMonthReading, 
                        form.ThisMonthReading
                    );

                    if (success)
                    {
                        RefreshListView();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add customer. Please check logs or inputs.");
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit");
                return;
            }
            
            int index = lvCustomer.SelectedItems[0].Index;
            Customer existing = _customerManager.GetCustomer(index);

            // Open the CustomerForm as a dialog for EDITING
            using (var form = new CustomerForm(existing))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool success = _customerManager.UpdateCustomer(
                        index,
                        form.CustomerName,
                        form.CustomerType,
                        form.LastMonthReading,
                        form.ThisMonthReading
                    );

                    if (success)
                    {
                        RefreshListView();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer.");
                    }
                }
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
    }
}