namespace WindowsForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbElectricityApplication = new Label();
            btnSubmit = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnExport = new Button();
            lvCustomer = new ListView();
            CustomerName = new ColumnHeader();
            CustomerType = new ColumnHeader();
            LastMonthElectricity = new ColumnHeader();
            ThisMonthElectricity = new ColumnHeader();
            UsageAmount = new ColumnHeader();
            ElectricityBill = new ColumnHeader();
            SuspendLayout();
            // 
            // tbElectricityApplication
            // 
            tbElectricityApplication.AutoSize = true;
            tbElectricityApplication.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            tbElectricityApplication.Location = new Point(30, 20);
            tbElectricityApplication.Name = "tbElectricityApplication";
            tbElectricityApplication.Size = new Size(311, 38);
            tbElectricityApplication.TabIndex = 12;
            tbElectricityApplication.Text = "Electricity Application";
            // 
            // btnSubmit
            // 
            btnSubmit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSubmit.Location = new Point(30, 409);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(160, 29);
            btnSubmit.TabIndex = 14;
            btnSubmit.Text = "Add New Customer";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += button1_Click;
            //
            // btnDelete
            //
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(200, 409);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 29);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnEdit
            //
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.Location = new Point(330, 409);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 29);
            btnEdit.TabIndex = 16;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            //
            // btnExport
            //
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(460, 409);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 29);
            btnExport.TabIndex = 19;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // lvCustomer
            // 
            lvCustomer.Columns.AddRange(new ColumnHeader[] { CustomerName, CustomerType, LastMonthElectricity, ThisMonthElectricity, UsageAmount, ElectricityBill });
            lvCustomer.GridLines = true;
            lvCustomer.LabelWrap = false;
            lvCustomer.Location = new Point(30, 80);
            lvCustomer.Name = "lvCustomer";
            lvCustomer.Size = new Size(900, 300);
            lvCustomer.TabIndex = 17;
            lvCustomer.UseCompatibleStateImageBehavior = false;
            lvCustomer.View = View.Details;
            // 
            // CustomerName
            // 
            CustomerName.Text = "Customer Name";
            CustomerName.Width = 150;
            // 
            // CustomerType
            // 
            CustomerType.Text = "Customer Type";
            CustomerType.Width = 150;
            // 
            // LastMonthElectricity
            // 
            LastMonthElectricity.Text = "Last Month";
            LastMonthElectricity.Width = 100;
            // 
            // ThisMonthElectricity
            // 
            ThisMonthElectricity.Text = "This Month";
            ThisMonthElectricity.Width = 100;
            // 
            // UsageAmount
            // 
            UsageAmount.Text = "Usage";
            UsageAmount.Width = 100;
            // 
            // ElectricityBill
            // 
            ElectricityBill.Text = "Bill (VND)";
            ElectricityBill.Width = 150;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.HotPink;
            ClientSize = new Size(960, 460);
            Controls.Add(lvCustomer);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnSubmit);
            Controls.Add(btnExport);
            Controls.Add(tbElectricityApplication);
            Name = "Form1";
            Text = "Electricity Application - Dashboard";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tbElectricityApplication;
        private Button btnSubmit;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnExport;
        private ListView lvCustomer;
        private ColumnHeader CustomerName;
        private ColumnHeader CustomerType;
        private ColumnHeader LastMonthElectricity;
        private ColumnHeader ThisMonthElectricity;
        private ColumnHeader UsageAmount;
        private ColumnHeader ElectricityBill;
    }
}
