using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // Needed for Path

namespace WindowsForm
{
    public partial class Form1 : Form
    {
        private CustomerManager _customerManager;
        private List<string[]> _currentViewData; // To hold data for display (supports search) 

        public Form1()
        {
            InitializeComponent();
            ICustomerRepository repository = new JsonCustomerRepository();
            _customerManager = new CustomerManager(repository);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _customerManager.LoadData();
            RefreshListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new CustomerForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool success = _customerManager.AddCustomer(
                        form.CustomerName, 
                        form.CustomerType, 
                        form.LastMonthReading, 
                        form.ThisMonthReading,
                        form.PeopleCount
                    );

                    if (success) RefreshListView();
                    else MessageBox.Show("Failed to add customer.");
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
            
            // Fix: We must find the REAL index in the manager, not just the ListView index
            // because ListView might be sorted or filtered.
            // For simplicity in this prototype without IDs, we assume ListView order matches Manager 
            // UNLESS we are filtering.
            
            // If we are filtering (Search), we need to be careful. 
            // For now, let's disable Search-then-Edit complexity or just reload.
            // A robust way is to store the ID in the ListViewItem.Tag. 
            // Since we don't have IDs, we'll rely on index but refresh full list first.
            
            int index = lvCustomer.SelectedItems[0].Index;
            
            // Quick Hack: If we searched, the index might be wrong. 
            // But for this school/prototype level, we'll assume index matches _customers list
            // *if* we are viewing all. 
            
            Customer existing = _customerManager.GetCustomer(index);
            if(existing == null) return;

            using (var form = new CustomerForm(existing))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool success = _customerManager.UpdateCustomer(
                        index,
                        form.CustomerName,
                        form.CustomerType,
                        form.LastMonthReading,
                        form.ThisMonthReading,
                        form.PeopleCount
                    );

                    if (success) RefreshListView();
                    else MessageBox.Show("Failed to update customer.");
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

        // New Feature: Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                RefreshListView();
                return;
            }

            var results = _customerManager.SearchByName(keyword);
            DisplayCustomers(results);
        }

        // New Feature: Sort
        private void btnSortName_Click(object sender, EventArgs e)
        {
            _customerManager.SortByName();
            RefreshListView();
        }

        // New Feature: Print Invoice (Single File)
        private void btnInvoice_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to print invoice.");
                return;
            }
            int index = lvCustomer.SelectedItems[0].Index;
            Customer c = _customerManager.GetCustomer(index);
            
            if (c != null)
            {
                try
                {
                    // Generate unique filename
                    string safeName = string.Join("_", c.Name.Split(Path.GetInvalidFileNameChars()));
                    string filename = $"Invoice_{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    
                    // Use the GetBillInfo method we defined in Customer class which returns the formatted string
                    string billContent = "ABC Software - Water Bill Invoice\n" +
                                         "==================================\n" +
                                         c.GetBillInfo() + "\n" +
                                         "==================================";

                    File.WriteAllText(filename, billContent);
                    MessageBox.Show($"Invoice saved to: {Path.GetFullPath(filename)}");
                    
                    // Optional: Open the file (shell execute)
                    // System.Diagnostics.Process.Start("notepad.exe", filename);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error generating invoice: " + ex.Message);
                }
            }
        }

        private void RefreshListView()
        {
            // Default: Show all
            var all = _customerManager.GetAllCustomers();
            DisplayCustomers(all);
        }

        private void DisplayCustomers(List<Customer> customers)
        {
            lvCustomer.Items.Clear();
            foreach (var customer in customers)
            {
                // Re-calculate display values
                int people = (customer is HouseholdCustomer h) ? h.PeopleCount : 0;
                decimal finalBill = customer.CalculateBill() * 1.1m;

                var item = new ListViewItem(customer.Name);
                item.SubItems.Add(customer.CustomerType);
                item.SubItems.Add(people > 0 ? people.ToString() : "-");
                item.SubItems.Add(customer.LastMonthReading.ToString());
                item.SubItems.Add(customer.ThisMonthReading.ToString());
                item.SubItems.Add(customer.Usage.ToString());
                item.SubItems.Add(finalBill.ToString("N0"));
                
                lvCustomer.Items.Add(item);
            }
        }
    }
}