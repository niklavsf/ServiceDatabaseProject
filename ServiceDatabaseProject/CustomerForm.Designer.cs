namespace ServiceDatabaseProject
{
    partial class CustomerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxLeft = new GroupBox();
            label1 = new Label();
            CustomerCloseButton = new Button();
            labelName = new Label();
            NameTextBox = new TextBox();
            labelSurname = new Label();
            SurnameTextBox = new TextBox();
            labelPhone = new Label();
            PhoneTextBox = new TextBox();
            labelEmail = new Label();
            EmailTextBox = new TextBox();
            AddCustomerButton = new Button();
            DeleteCustomerButton = new Button();
            ClearButton = new Button();
            groupBoxRight = new GroupBox();
            UndoButton = new Button();
            CustomersDGV = new DataGridView();
            SearchButton = new Button();
            CriteriaComboBox = new ComboBox();
            SearchTextBox = new TextBox();
            groupBox1 = new GroupBox();
            label2 = new Label();
            groupBoxLeft.SuspendLayout();
            groupBoxRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CustomersDGV).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLeft
            // 
            groupBoxLeft.Controls.Add(label1);
            groupBoxLeft.Controls.Add(CustomerCloseButton);
            groupBoxLeft.Controls.Add(labelName);
            groupBoxLeft.Controls.Add(NameTextBox);
            groupBoxLeft.Controls.Add(labelSurname);
            groupBoxLeft.Controls.Add(SurnameTextBox);
            groupBoxLeft.Controls.Add(labelPhone);
            groupBoxLeft.Controls.Add(PhoneTextBox);
            groupBoxLeft.Controls.Add(labelEmail);
            groupBoxLeft.Controls.Add(EmailTextBox);
            groupBoxLeft.Controls.Add(AddCustomerButton);
            groupBoxLeft.Controls.Add(DeleteCustomerButton);
            groupBoxLeft.Controls.Add(ClearButton);
            groupBoxLeft.Location = new Point(12, 216);
            groupBoxLeft.Name = "groupBoxLeft";
            groupBoxLeft.Size = new Size(380, 463);
            groupBoxLeft.TabIndex = 0;
            groupBoxLeft.TabStop = false;
            groupBoxLeft.Text = "Customer details";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(88, 439);
            label1.Name = "label1";
            label1.Size = new Size(286, 21);
            label1.TabIndex = 22;
            label1.Text = "Note: clear the fields to add a new entry";
            // 
            // CustomerCloseButton
            // 
            CustomerCloseButton.Image = Properties.Resources.smallclose;
            CustomerCloseButton.ImageAlign = ContentAlignment.MiddleRight;
            CustomerCloseButton.Location = new Point(194, 390);
            CustomerCloseButton.Name = "CustomerCloseButton";
            CustomerCloseButton.Size = new Size(180, 34);
            CustomerCloseButton.TabIndex = 12;
            CustomerCloseButton.Text = "Close";
            CustomerCloseButton.TextAlign = ContentAlignment.MiddleLeft;
            CustomerCloseButton.UseVisualStyleBackColor = true;
            CustomerCloseButton.Click += CustomerCloseButton_Click;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelName.Location = new Point(12, 38);
            labelName.Name = "labelName";
            labelName.Size = new Size(62, 25);
            labelName.TabIndex = 0;
            labelName.Text = "Name";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(12, 66);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(356, 31);
            NameTextBox.TabIndex = 1;
            // 
            // labelSurname
            // 
            labelSurname.AutoSize = true;
            labelSurname.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelSurname.Location = new Point(12, 110);
            labelSurname.Name = "labelSurname";
            labelSurname.Size = new Size(87, 25);
            labelSurname.TabIndex = 2;
            labelSurname.Text = "Surname";
            // 
            // SurnameTextBox
            // 
            SurnameTextBox.Location = new Point(12, 138);
            SurnameTextBox.Name = "SurnameTextBox";
            SurnameTextBox.Size = new Size(356, 31);
            SurnameTextBox.TabIndex = 3;
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPhone.Location = new Point(12, 182);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(137, 25);
            labelPhone.TabIndex = 4;
            labelPhone.Text = "Phone number";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.Location = new Point(12, 210);
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(356, 31);
            PhoneTextBox.TabIndex = 5;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelEmail.Location = new Point(12, 254);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(65, 25);
            labelEmail.TabIndex = 6;
            labelEmail.Text = "E-mail";
            // 
            // EmailTextBox
            // 
            EmailTextBox.Location = new Point(12, 282);
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(356, 31);
            EmailTextBox.TabIndex = 7;
            // 
            // AddCustomerButton
            // 
            AddCustomerButton.Image = Properties.Resources.updateadd;
            AddCustomerButton.ImageAlign = ContentAlignment.MiddleRight;
            AddCustomerButton.Location = new Point(6, 350);
            AddCustomerButton.Name = "AddCustomerButton";
            AddCustomerButton.Size = new Size(180, 34);
            AddCustomerButton.TabIndex = 8;
            AddCustomerButton.Text = "Add / Update";
            AddCustomerButton.TextAlign = ContentAlignment.MiddleLeft;
            AddCustomerButton.UseVisualStyleBackColor = true;
            AddCustomerButton.Click += AddCustomerButton_Click;
            // 
            // DeleteCustomerButton
            // 
            DeleteCustomerButton.Image = Properties.Resources.delete;
            DeleteCustomerButton.ImageAlign = ContentAlignment.MiddleRight;
            DeleteCustomerButton.Location = new Point(194, 350);
            DeleteCustomerButton.Name = "DeleteCustomerButton";
            DeleteCustomerButton.Size = new Size(180, 34);
            DeleteCustomerButton.TabIndex = 10;
            DeleteCustomerButton.Text = "Delete";
            DeleteCustomerButton.TextAlign = ContentAlignment.MiddleLeft;
            DeleteCustomerButton.UseVisualStyleBackColor = true;
            DeleteCustomerButton.Click += DeleteCustomerButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Image = Properties.Resources.clear;
            ClearButton.ImageAlign = ContentAlignment.MiddleRight;
            ClearButton.Location = new Point(6, 390);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(180, 34);
            ClearButton.TabIndex = 11;
            ClearButton.Text = "Clear Fields";
            ClearButton.TextAlign = ContentAlignment.MiddleLeft;
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // groupBoxRight
            // 
            groupBoxRight.Controls.Add(UndoButton);
            groupBoxRight.Controls.Add(CustomersDGV);
            groupBoxRight.Location = new Point(398, 12);
            groupBoxRight.Name = "groupBoxRight";
            groupBoxRight.Size = new Size(725, 667);
            groupBoxRight.TabIndex = 1;
            groupBoxRight.TabStop = false;
            groupBoxRight.Text = "Customer list";
            // 
            // UndoButton
            // 
            UndoButton.Image = Properties.Resources.undo;
            UndoButton.Location = new Point(680, 17);
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(35, 34);
            UndoButton.TabIndex = 1;
            UndoButton.UseVisualStyleBackColor = true;
            UndoButton.Click += UndoButton_Click;
            // 
            // CustomersDGV
            // 
            CustomersDGV.AllowUserToAddRows = false;
            CustomersDGV.AllowUserToDeleteRows = false;
            CustomersDGV.AllowUserToResizeRows = false;
            CustomersDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustomersDGV.Location = new Point(6, 57);
            CustomersDGV.MultiSelect = false;
            CustomersDGV.Name = "CustomersDGV";
            CustomersDGV.ReadOnly = true;
            CustomersDGV.RowHeadersWidth = 62;
            CustomersDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomersDGV.Size = new Size(709, 604);
            CustomersDGV.TabIndex = 0;
            CustomersDGV.CellDoubleClick += CustomersDGV_CellDoubleClick;
            CustomersDGV.SelectionChanged += CustomersDGV_SelectionChanged;
            // 
            // SearchButton
            // 
            SearchButton.Image = Properties.Resources.search;
            SearchButton.ImageAlign = ContentAlignment.MiddleRight;
            SearchButton.Location = new Point(262, 149);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(112, 34);
            SearchButton.TabIndex = 4;
            SearchButton.Text = "Search";
            SearchButton.TextAlign = ContentAlignment.MiddleLeft;
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // CriteriaComboBox
            // 
            CriteriaComboBox.FormattingEnabled = true;
            CriteriaComboBox.Location = new Point(6, 101);
            CriteriaComboBox.Name = "CriteriaComboBox";
            CriteriaComboBox.Size = new Size(368, 33);
            CriteriaComboBox.TabIndex = 1;
            // 
            // SearchTextBox
            // 
            SearchTextBox.Location = new Point(6, 30);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.Size = new Size(368, 31);
            SearchTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SearchButton);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(CriteriaComboBox);
            groupBox1.Controls.Add(SearchTextBox);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(380, 198);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 73);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 3;
            label2.Text = "Search criteria";
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 687);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxRight);
            Controls.Add(groupBoxLeft);
            Name = "CustomerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Customer management";
            groupBoxLeft.ResumeLayout(false);
            groupBoxLeft.PerformLayout();
            groupBoxRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CustomersDGV).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }


        #endregion
        private GroupBox groupBoxLeft;
        private Label labelName;
        private Label labelSurname;
        private Label labelPhone;
        private Label labelEmail;
        private TextBox NameTextBox;
        private TextBox SurnameTextBox;
        private TextBox PhoneTextBox;
        private TextBox EmailTextBox;

        private Button AddCustomerButton;
        private Button DeleteCustomerButton;
        private Button ClearButton;

        private GroupBox groupBoxRight;
        private DataGridView CustomersDGV;
        private Button CustomerCloseButton;
        private Label label1;
        private Button SearchButton;
        private ComboBox CriteriaComboBox;
        private TextBox SearchTextBox;
        private GroupBox groupBox1;
        private Label label2;
        private Button UndoButton;
    }
}