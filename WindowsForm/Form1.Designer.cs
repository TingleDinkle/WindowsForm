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
            txtName = new TextBox();
            txtLastMonth = new TextBox();
            txtThisMonth = new TextBox();
            btnUsage = new Button();
            btnBill = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            tbElectricityApplication = new Label();
            cboType = new ComboBox();
            btnSubmit = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            lvCustomer = new ListView();
            CustomerName = new ColumnHeader();
            CustomerType = new ColumnHeader();
            LastMonthElectricity = new ColumnHeader();
            ThisMonthElectricity = new ColumnHeader();
            UsageAmount = new ColumnHeader();
            ElectricityBill = new ColumnHeader();
            SuspendLayout();
            //
            // txtName
            //
            txtName.Location = new Point(168, 68);
            txtName.Name = "txtName";
            txtName.Size = new Size(261, 29);
            txtName.TabIndex = 0;
            //
            // txtLastMonth
            //
            txtLastMonth.Location = new Point(252, 188);
            txtLastMonth.Name = "txtLastMonth";
            txtLastMonth.Size = new Size(185, 29);
            txtLastMonth.TabIndex = 1;
            //
            // txtThisMonth
            //
            txtThisMonth.Location = new Point(252, 250);
            txtThisMonth.Name = "txtThisMonth";
            txtThisMonth.Size = new Size(185, 29);
            txtThisMonth.TabIndex = 3;
            // 
            // btnUsage
            // 
            btnUsage.Location = new Point(244, 304);
            btnUsage.Name = "btnUsage";
            btnUsage.Size = new Size(185, 29);
            btnUsage.TabIndex = 4;
            btnUsage.UseVisualStyleBackColor = true;
            // 
            // btnBill
            // 
            btnBill.Location = new Point(168, 360);
            btnBill.Name = "btnBill";
            btnBill.Size = new Size(261, 29);
            btnBill.TabIndex = 5;
            btnBill.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AccessibleRole = AccessibleRole.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 72);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 6;
            label1.Text = "Customer Name:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(27, 364);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 7;
            label2.Text = "Electricity Bill:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 308);
            label3.Name = "label3";
            label3.Size = new Size(190, 20);
            label3.TabIndex = 8;
            label3.Text = "Usage Electricity Amount:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(27, 254);
            label4.Name = "label4";
            label4.Size = new Size(228, 20);
            label4.TabIndex = 9;
            label4.Text = "This Month Electricity Number:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(27, 192);
            label5.Name = "label5";
            label5.Size = new Size(228, 20);
            label5.TabIndex = 10;
            label5.Text = "Last Month Electricity Number:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(27, 135);
            label6.Name = "label6";
            label6.Size = new Size(118, 20);
            label6.TabIndex = 11;
            label6.Text = "Customer Type:";
            // 
            // tbElectricityApplication
            // 
            tbElectricityApplication.AutoSize = true;
            tbElectricityApplication.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            tbElectricityApplication.Location = new Point(302, 9);
            tbElectricityApplication.Name = "tbElectricityApplication";
            tbElectricityApplication.Size = new Size(311, 38);
            tbElectricityApplication.TabIndex = 12;
            tbElectricityApplication.Text = "Electricity Application";
            tbElectricityApplication.Click += label7_Click;
            // 
            // cboType
            // 
            cboType.FormattingEnabled = true;
            cboType.Items.AddRange(new object[] { "HouseHold", "Public Services", "Production Units", "Business Services" });
            cboType.Location = new Point(168, 127);
            cboType.Name = "cboType";
            cboType.Size = new Size(261, 28);
            cboType.TabIndex = 13;
            // 
            // btnSubmit
            // 
            btnSubmit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSubmit.Location = new Point(272, 409);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(275, 29);
            btnSubmit.TabIndex = 14;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += button1_Click;
            //
            // btnDelete
            //
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(553, 409);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(280, 29);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnEdit
            //
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.Location = new Point(839, 409);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(282, 29);
            btnEdit.TabIndex = 16;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // lvCustomer
            // 
            lvCustomer.Columns.AddRange(new ColumnHeader[] { CustomerName, CustomerType, LastMonthElectricity, ThisMonthElectricity, UsageAmount, ElectricityBill });
            lvCustomer.GridLines = true;
            lvCustomer.LabelWrap = false;
            lvCustomer.Location = new Point(450, 68);
            lvCustomer.Name = "lvCustomer";
            lvCustomer.Size = new Size(671, 330);
            lvCustomer.TabIndex = 17;
            lvCustomer.UseCompatibleStateImageBehavior = false;
            lvCustomer.View = View.Details;
            // 
            // CustomerName
            // 
            CustomerName.Text = "Customer Name";
            CustomerName.Width = 120;
            // 
            // CustomerType
            // 
            CustomerType.Text = "Customy Type";
            CustomerType.Width = 120;
            // 
            // LastMonthElectricity
            // 
            LastMonthElectricity.Text = "Last Month Electricity";
            LastMonthElectricity.Width = 100;
            // 
            // ThisMonthElectricity
            // 
            ThisMonthElectricity.Text = "This Month Electricity";
            ThisMonthElectricity.Width = 100;
            // 
            // UsageAmount
            // 
            UsageAmount.Text = "Usage Amount";
            UsageAmount.Width = 120;
            // 
            // ElectricityBill
            // 
            ElectricityBill.Text = "Electricity Bill";
            ElectricityBill.Width = 120;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.HotPink;
            ClientSize = new Size(1144, 450);
            Controls.Add(lvCustomer);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnSubmit);
            Controls.Add(cboType);
            Controls.Add(tbElectricityApplication);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBill);
            Controls.Add(btnUsage);
            Controls.Add(txtThisMonth);
            Controls.Add(txtLastMonth);
            Controls.Add(txtName);
            Name = "Form1";
            Text = "Electricity Application";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtLastMonth;
        private TextBox txtThisMonth;
        private Button btnUsage;
        private Button btnBill;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label tbElectricityApplication;
        private ComboBox cboType;
        private Button btnSubmit;
        private Button btnDelete;
        private Button btnEdit;
        private ListView lvCustomer;
        private ColumnHeader CustomerName;
        private ColumnHeader CustomerType;
        private ColumnHeader LastMonthElectricity;
        private ColumnHeader ThisMonthElectricity;
        private ColumnHeader UsageAmount;
        private ColumnHeader ElectricityBill;
    }
}
