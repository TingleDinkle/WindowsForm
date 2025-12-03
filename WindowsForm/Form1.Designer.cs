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
            this.tbElectricityApplication = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSortName = new System.Windows.Forms.Button();
            this.lvCustomer = new System.Windows.Forms.ListView();
            this.CustomerName = new System.Windows.Forms.ColumnHeader();
            this.CustomerType = new System.Windows.Forms.ColumnHeader();
            this.PeopleCount = new System.Windows.Forms.ColumnHeader();
            this.LastMonthElectricity = new System.Windows.Forms.ColumnHeader();
            this.ThisMonthElectricity = new System.Windows.Forms.ColumnHeader();
            this.UsageAmount = new System.Windows.Forms.ColumnHeader();
            this.ElectricityBill = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // tbElectricityApplication
            // 
            this.tbElectricityApplication.AutoSize = true;
            this.tbElectricityApplication.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbElectricityApplication.Location = new System.Drawing.Point(30, 20);
            this.tbElectricityApplication.Name = "tbElectricityApplication";
            this.tbElectricityApplication.Size = new System.Drawing.Size(311, 38);
            this.tbElectricityApplication.TabIndex = 12;
            this.tbElectricityApplication.Text = "Water Bill Management";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(30, 409);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(160, 29);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "Add New Customer";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(200, 409);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 29);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(330, 409);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 29);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(460, 409);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 29);
            this.btnExport.TabIndex = 19;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnInvoice
            // 
            this.btnInvoice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.Location = new System.Drawing.Point(590, 409);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(120, 29);
            this.btnInvoice.TabIndex = 20;
            this.btnInvoice.Text = "Print Invoice";
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(550, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 27);
            this.txtSearch.TabIndex = 21;
            this.txtSearch.PlaceholderText = "Search Name...";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(710, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 29);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSortName
            // 
            this.btnSortName.Location = new System.Drawing.Point(800, 29);
            this.btnSortName.Name = "btnSortName";
            this.btnSortName.Size = new System.Drawing.Size(130, 29);
            this.btnSortName.TabIndex = 23;
            this.btnSortName.Text = "Sort by Name";
            this.btnSortName.UseVisualStyleBackColor = true;
            this.btnSortName.Click += new System.EventHandler(this.btnSortName_Click);
            // 
            // lvCustomer
            // 
            this.lvCustomer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CustomerName,
            this.CustomerType,
            this.PeopleCount,
            this.LastMonthElectricity,
            this.ThisMonthElectricity,
            this.UsageAmount,
            this.ElectricityBill});
            this.lvCustomer.GridLines = true;
            this.lvCustomer.HideSelection = false;
            this.lvCustomer.Location = new System.Drawing.Point(30, 80);
            this.lvCustomer.Name = "lvCustomer";
            this.lvCustomer.Size = new System.Drawing.Size(900, 300);
            this.lvCustomer.TabIndex = 17;
            this.lvCustomer.UseCompatibleStateImageBehavior = false;
            this.lvCustomer.View = System.Windows.Forms.View.Details;
            // 
            // CustomerName
            // 
            this.CustomerName.Text = "Customer Name";
            this.CustomerName.Width = 150;
            // 
            // CustomerType
            // 
            this.CustomerType.Text = "Customer Type";
            this.CustomerType.Width = 150;
            // 
            // PeopleCount
            // 
            this.PeopleCount.Text = "People";
            this.PeopleCount.Width = 60;
            // 
            // LastMonthElectricity
            // 
            this.LastMonthElectricity.Text = "Last Month";
            this.LastMonthElectricity.Width = 90;
            // 
            // ThisMonthElectricity
            // 
            this.ThisMonthElectricity.Text = "This Month";
            this.ThisMonthElectricity.Width = 90;
            // 
            // UsageAmount
            // 
            this.UsageAmount.Text = "Usage";
            this.UsageAmount.Width = 80;
            // 
            // ElectricityBill
            // 
            this.ElectricityBill.Text = "Bill (VND)";
            this.ElectricityBill.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(960, 460);
            this.Controls.Add(this.lvCustomer);
            this.Controls.Add(this.btnSortName);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnInvoice);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbElectricityApplication);
            this.Name = "Form1";
            this.Text = "Water Bill Management - Dashboard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tbElectricityApplication;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSortName;
        private System.Windows.Forms.ListView lvCustomer;
        private System.Windows.Forms.ColumnHeader CustomerName;
        private System.Windows.Forms.ColumnHeader CustomerType;
        private System.Windows.Forms.ColumnHeader PeopleCount;
        private System.Windows.Forms.ColumnHeader LastMonthElectricity;
        private System.Windows.Forms.ColumnHeader ThisMonthElectricity;
        private System.Windows.Forms.ColumnHeader UsageAmount;
        private System.Windows.Forms.ColumnHeader ElectricityBill;
    }
}