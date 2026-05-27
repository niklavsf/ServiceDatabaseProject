namespace ServiceDatabaseProject
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            MainScreenDGV = new DataGridView();
            DeleteMainWindow = new Button();
            groupBox1 = new GroupBox();
            AddNewServiceButton = new Button();
            AdvancedAddButton = new Button();
            groupBox2 = new GroupBox();
            SearchBox = new TextBox();
            label1 = new Label();
            SearchButton = new Button();
            SearchCriteriaCombo = new ComboBox();
            ExportToCSVButton = new Button();
            NotificationButton = new Button();
            ManageCustomerButton = new Button();
            PartViewButton = new Button();
            groupBox3 = new GroupBox();
            ManageDevicesButton = new Button();
            UndoButton = new Button();
            groupBox4 = new GroupBox();
            AdminButton = new Button();
            ((System.ComponentModel.ISupportInitialize)MainScreenDGV).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // MainScreenDGV
            // 
            MainScreenDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MainScreenDGV.Location = new Point(6, 60);
            MainScreenDGV.Name = "MainScreenDGV";
            MainScreenDGV.RowHeadersWidth = 62;
            MainScreenDGV.Size = new Size(1226, 551);
            MainScreenDGV.TabIndex = 0;
            MainScreenDGV.CellContentDoubleClick += MainScreenDGV_CellContentDoubleClick;
            // 
            // DeleteMainWindow
            // 
            DeleteMainWindow.Image = Properties.Resources.delete;
            DeleteMainWindow.ImageAlign = ContentAlignment.MiddleRight;
            DeleteMainWindow.Location = new Point(17, 110);
            DeleteMainWindow.Name = "DeleteMainWindow";
            DeleteMainWindow.Size = new Size(163, 34);
            DeleteMainWindow.TabIndex = 1;
            DeleteMainWindow.Text = "Delete";
            DeleteMainWindow.TextAlign = ContentAlignment.MiddleLeft;
            DeleteMainWindow.UseVisualStyleBackColor = true;
            DeleteMainWindow.Click += DeleteMainWindow_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(AddNewServiceButton);
            groupBox1.Controls.Add(AdvancedAddButton);
            groupBox1.Controls.Add(DeleteMainWindow);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(196, 162);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Add";
            // 
            // AddNewServiceButton
            // 
            AddNewServiceButton.Image = Properties.Resources.add;
            AddNewServiceButton.ImageAlign = ContentAlignment.MiddleRight;
            AddNewServiceButton.Location = new Point(17, 70);
            AddNewServiceButton.Name = "AddNewServiceButton";
            AddNewServiceButton.Size = new Size(163, 34);
            AddNewServiceButton.TabIndex = 5;
            AddNewServiceButton.Text = "Add new";
            AddNewServiceButton.TextAlign = ContentAlignment.MiddleLeft;
            AddNewServiceButton.UseVisualStyleBackColor = true;
            AddNewServiceButton.Click += AddNewServiceButton_Click;
            // 
            // AdvancedAddButton
            // 
            AdvancedAddButton.Image = Properties.Resources.open;
            AdvancedAddButton.ImageAlign = ContentAlignment.MiddleRight;
            AdvancedAddButton.Location = new Point(17, 30);
            AdvancedAddButton.Name = "AdvancedAddButton";
            AdvancedAddButton.Size = new Size(163, 34);
            AdvancedAddButton.TabIndex = 4;
            AdvancedAddButton.Text = "Open service";
            AdvancedAddButton.TextAlign = ContentAlignment.MiddleLeft;
            AdvancedAddButton.UseVisualStyleBackColor = true;
            AdvancedAddButton.Click += AdvancedAddButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(SearchBox);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(SearchButton);
            groupBox2.Controls.Add(SearchCriteriaCombo);
            groupBox2.Location = new Point(12, 224);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(196, 209);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Search";
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(9, 41);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(181, 31);
            SearchBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(9, 85);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 2;
            label1.Text = "Search by:";
            // 
            // SearchButton
            // 
            SearchButton.Image = Properties.Resources.search;
            SearchButton.ImageAlign = ContentAlignment.MiddleRight;
            SearchButton.Location = new Point(17, 169);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(163, 34);
            SearchButton.TabIndex = 1;
            SearchButton.Text = "Search";
            SearchButton.TextAlign = ContentAlignment.MiddleLeft;
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // SearchCriteriaCombo
            // 
            SearchCriteriaCombo.FormattingEnabled = true;
            SearchCriteriaCombo.Location = new Point(8, 113);
            SearchCriteriaCombo.Name = "SearchCriteriaCombo";
            SearchCriteriaCombo.Size = new Size(182, 33);
            SearchCriteriaCombo.TabIndex = 0;
            // 
            // ExportToCSVButton
            // 
            ExportToCSVButton.Image = Properties.Resources.csv;
            ExportToCSVButton.ImageAlign = ContentAlignment.MiddleRight;
            ExportToCSVButton.Location = new Point(17, 150);
            ExportToCSVButton.Name = "ExportToCSVButton";
            ExportToCSVButton.Size = new Size(163, 34);
            ExportToCSVButton.TabIndex = 6;
            ExportToCSVButton.Text = "Export to CSV";
            ExportToCSVButton.TextAlign = ContentAlignment.MiddleLeft;
            ExportToCSVButton.UseVisualStyleBackColor = true;
            ExportToCSVButton.Click += ExportToCSVButton_Click;
            // 
            // NotificationButton
            // 
            NotificationButton.AutoSize = true;
            NotificationButton.BackgroundImageLayout = ImageLayout.None;
            NotificationButton.Image = (Image)resources.GetObject("NotificationButton.Image");
            NotificationButton.Location = new Point(1194, 20);
            NotificationButton.Name = "NotificationButton";
            NotificationButton.Size = new Size(34, 34);
            NotificationButton.TabIndex = 7;
            NotificationButton.UseVisualStyleBackColor = true;
            NotificationButton.Click += NotificationButton_Click;
            // 
            // ManageCustomerButton
            // 
            ManageCustomerButton.Image = Properties.Resources.customers;
            ManageCustomerButton.ImageAlign = ContentAlignment.MiddleRight;
            ManageCustomerButton.Location = new Point(17, 70);
            ManageCustomerButton.Name = "ManageCustomerButton";
            ManageCustomerButton.Size = new Size(163, 34);
            ManageCustomerButton.TabIndex = 8;
            ManageCustomerButton.Text = "Customers";
            ManageCustomerButton.TextAlign = ContentAlignment.MiddleLeft;
            ManageCustomerButton.UseVisualStyleBackColor = true;
            ManageCustomerButton.Click += ManageCustomerButton_Click;
            // 
            // PartViewButton
            // 
            PartViewButton.Image = Properties.Resources.part;
            PartViewButton.ImageAlign = ContentAlignment.MiddleRight;
            PartViewButton.Location = new Point(17, 30);
            PartViewButton.Name = "PartViewButton";
            PartViewButton.Size = new Size(163, 34);
            PartViewButton.TabIndex = 9;
            PartViewButton.Text = "Parts";
            PartViewButton.TextAlign = ContentAlignment.MiddleLeft;
            PartViewButton.UseVisualStyleBackColor = true;
            PartViewButton.Click += PartViewButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ManageDevicesButton);
            groupBox3.Controls.Add(PartViewButton);
            groupBox3.Controls.Add(ExportToCSVButton);
            groupBox3.Controls.Add(ManageCustomerButton);
            groupBox3.Location = new Point(12, 439);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(196, 196);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Registry";
            // 
            // ManageDevicesButton
            // 
            ManageDevicesButton.Image = Properties.Resources.devices;
            ManageDevicesButton.ImageAlign = ContentAlignment.MiddleRight;
            ManageDevicesButton.Location = new Point(17, 110);
            ManageDevicesButton.Name = "ManageDevicesButton";
            ManageDevicesButton.Size = new Size(163, 34);
            ManageDevicesButton.TabIndex = 10;
            ManageDevicesButton.Text = "Devices";
            ManageDevicesButton.TextAlign = ContentAlignment.MiddleLeft;
            ManageDevicesButton.UseVisualStyleBackColor = true;
            ManageDevicesButton.Click += ManageDevicesButton_Click;
            // 
            // UndoButton
            // 
            UndoButton.Image = Properties.Resources.undo;
            UndoButton.Location = new Point(1154, 20);
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(34, 34);
            UndoButton.TabIndex = 11;
            UndoButton.UseVisualStyleBackColor = true;
            UndoButton.Click += UndoButton_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(AdminButton);
            groupBox4.Controls.Add(MainScreenDGV);
            groupBox4.Controls.Add(UndoButton);
            groupBox4.Controls.Add(NotificationButton);
            groupBox4.Location = new Point(214, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1238, 623);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Services";
            // 
            // AdminButton
            // 
            AdminButton.Image = Properties.Resources.admin;
            AdminButton.Location = new Point(955, 20);
            AdminButton.Name = "AdminButton";
            AdminButton.Size = new Size(34, 34);
            AdminButton.TabIndex = 12;
            AdminButton.UseVisualStyleBackColor = true;
            AdminButton.Click += AdminButton_Click;
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 644);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "HomeScreen";
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)MainScreenDGV).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView MainScreenDGV;
        private Button DeleteMainWindow;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ComboBox SearchCriteriaCombo;
        private TextBox SearchBox;
        private Label label1;
        private Button SearchButton;
        private Button AdvancedAddButton;
        private Button ExportToCSVButton;
        private Button NotificationButton;
        private Button AddNewServiceButton;
        private Button ManageCustomerButton;
        private Button PartViewButton;
        private GroupBox groupBox3;
        private Button ManageDevicesButton;
        private Button UndoButton;
        private GroupBox groupBox4;
        private Button AdminButton;
    }
}
