namespace WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastMonth.Text) || string.IsNullOrEmpty(txtThisMonth.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            int last;
            if (!int.TryParse(txtLastMonth.Text, out last))
            {
                MessageBox.Show("Invalid last month number");
                return;
            }
            int thism;
            if (!int.TryParse(txtThisMonth.Text, out thism))
            {
                MessageBox.Show("Invalid this month number");
                return;
            }
            if (thism < last)
            {
                MessageBox.Show("This month cannot be less than last month");
                return;
            }
            string name = txtName.Text;
            string type = cboType.Text;
            Customer.AddCustomer(name, type, last, thism);
            RefreshListView();
            txtName.Text = "";
            txtLastMonth.Text = "";
            txtThisMonth.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void RefreshListView()
        {
            lvCustomer.Items.Clear();
            var customers = Customer.GetAllCustomers();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete");
                return;
            }
            int index = lvCustomer.SelectedItems[0].Index;
            Customer.DeleteCustomer(index);
            RefreshListView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvCustomer.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit");
                return;
            }
            int index = lvCustomer.SelectedItems[0].Index;
            // For now, just show bill info
            string bill = Customer.GetBillInfo(index);
            MessageBox.Show(bill, "Bill Information");
        }


    }
}
