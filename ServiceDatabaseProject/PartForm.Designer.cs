namespace ServiceDatabaseProject
{
    partial class PartForm
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
            ComponentComboBox = new ComboBox();
            labelPartNumber = new Label();
            PartNumberTextBox = new TextBox();
            labelPartName = new Label();
            PartNameTextBox = new TextBox();
            labelComponentId = new Label();
            labelPrice = new Label();
            PriceTextBox = new TextBox();
            labelQuantity = new Label();
            QuantityTextBox = new TextBox();
            AddPartButton = new Button();
            DeletePartButton = new Button();
            ClearButton = new Button();
            CloseButton = new Button();
            labelManufacturer = new Label();
            UseExistingManufacturerCheckBox = new CheckBox();
            ManufacturerComboBox = new ComboBox();
            labelCustomManufacturer = new Label();
            CustomManufacturerTextBox = new TextBox();
            labelPartStatus = new Label();
            PartStatusComboBox = new ComboBox();
            groupBoxRight = new GroupBox();
            PartsDGV = new DataGridView();
            groupBox1 = new GroupBox();
            SearchButton = new Button();
            label2 = new Label();
            StockCheckbox = new CheckBox();
            CriteriaComboBox = new ComboBox();
            SearchTextBox = new TextBox();
            UndoButton = new Button();
            groupBoxLeft.SuspendLayout();
            groupBoxRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PartsDGV).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLeft
            // 
            groupBoxLeft.Controls.Add(label1);
            groupBoxLeft.Controls.Add(ComponentComboBox);
            groupBoxLeft.Controls.Add(labelPartNumber);
            groupBoxLeft.Controls.Add(PartNumberTextBox);
            groupBoxLeft.Controls.Add(labelPartName);
            groupBoxLeft.Controls.Add(PartNameTextBox);
            groupBoxLeft.Controls.Add(labelComponentId);
            groupBoxLeft.Controls.Add(labelPrice);
            groupBoxLeft.Controls.Add(PriceTextBox);
            groupBoxLeft.Controls.Add(labelQuantity);
            groupBoxLeft.Controls.Add(QuantityTextBox);
            groupBoxLeft.Controls.Add(AddPartButton);
            groupBoxLeft.Controls.Add(DeletePartButton);
            groupBoxLeft.Controls.Add(ClearButton);
            groupBoxLeft.Controls.Add(CloseButton);
            groupBoxLeft.Controls.Add(labelManufacturer);
            groupBoxLeft.Controls.Add(UseExistingManufacturerCheckBox);
            groupBoxLeft.Controls.Add(ManufacturerComboBox);
            groupBoxLeft.Controls.Add(labelCustomManufacturer);
            groupBoxLeft.Controls.Add(CustomManufacturerTextBox);
            groupBoxLeft.Controls.Add(labelPartStatus);
            groupBoxLeft.Controls.Add(PartStatusComboBox);
            groupBoxLeft.Location = new Point(12, 216);
            groupBoxLeft.Name = "groupBoxLeft";
            groupBoxLeft.Size = new Size(380, 750);
            groupBoxLeft.TabIndex = 0;
            groupBoxLeft.TabStop = false;
            groupBoxLeft.Text = "Part details";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(88, 726);
            label1.Name = "label1";
            label1.Size = new Size(286, 21);
            label1.TabIndex = 21;
            label1.Text = "Note: clear the fields to add a new entry";
            // 
            // ComponentComboBox
            // 
            ComponentComboBox.FormattingEnabled = true;
            ComponentComboBox.Location = new Point(12, 65);
            ComponentComboBox.Name = "ComponentComboBox";
            ComponentComboBox.Size = new Size(356, 33);
            ComponentComboBox.TabIndex = 20;
            // 
            // labelPartNumber
            // 
            labelPartNumber.AutoSize = true;
            labelPartNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPartNumber.Location = new Point(12, 101);
            labelPartNumber.Name = "labelPartNumber";
            labelPartNumber.Size = new Size(119, 25);
            labelPartNumber.TabIndex = 0;
            labelPartNumber.Text = "Part number";
            // 
            // PartNumberTextBox
            // 
            PartNumberTextBox.Location = new Point(12, 129);
            PartNumberTextBox.Name = "PartNumberTextBox";
            PartNumberTextBox.Size = new Size(356, 31);
            PartNumberTextBox.TabIndex = 1;
            // 
            // labelPartName
            // 
            labelPartName.AutoSize = true;
            labelPartName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPartName.Location = new Point(12, 173);
            labelPartName.Name = "labelPartName";
            labelPartName.Size = new Size(100, 25);
            labelPartName.TabIndex = 2;
            labelPartName.Text = "Part name";
            // 
            // PartNameTextBox
            // 
            PartNameTextBox.Location = new Point(12, 201);
            PartNameTextBox.Name = "PartNameTextBox";
            PartNameTextBox.Size = new Size(356, 31);
            PartNameTextBox.TabIndex = 3;
            // 
            // labelComponentId
            // 
            labelComponentId.AutoSize = true;
            labelComponentId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelComponentId.Location = new Point(12, 30);
            labelComponentId.Name = "labelComponentId";
            labelComponentId.Size = new Size(154, 25);
            labelComponentId.TabIndex = 4;
            labelComponentId.Text = "Component type";
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPrice.Location = new Point(12, 235);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(54, 25);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Price";
            // 
            // PriceTextBox
            // 
            PriceTextBox.Location = new Point(12, 263);
            PriceTextBox.Name = "PriceTextBox";
            PriceTextBox.Size = new Size(356, 31);
            PriceTextBox.TabIndex = 7;
            // 
            // labelQuantity
            // 
            labelQuantity.AutoSize = true;
            labelQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelQuantity.Location = new Point(12, 307);
            labelQuantity.Name = "labelQuantity";
            labelQuantity.Size = new Size(87, 25);
            labelQuantity.TabIndex = 8;
            labelQuantity.Text = "Quantity";
            // 
            // QuantityTextBox
            // 
            QuantityTextBox.Location = new Point(12, 335);
            QuantityTextBox.Name = "QuantityTextBox";
            QuantityTextBox.Size = new Size(356, 31);
            QuantityTextBox.TabIndex = 9;
            // 
            // AddPartButton
            // 
            AddPartButton.Image = Properties.Resources.updateadd;
            AddPartButton.ImageAlign = ContentAlignment.MiddleRight;
            AddPartButton.Location = new Point(12, 633);
            AddPartButton.Name = "AddPartButton";
            AddPartButton.Size = new Size(180, 34);
            AddPartButton.TabIndex = 16;
            AddPartButton.Text = "Add / Update";
            AddPartButton.TextAlign = ContentAlignment.MiddleLeft;
            AddPartButton.UseVisualStyleBackColor = true;
            AddPartButton.Click += AddPartButton_Click;
            // 
            // DeletePartButton
            // 
            DeletePartButton.Image = Properties.Resources.delete;
            DeletePartButton.ImageAlign = ContentAlignment.MiddleRight;
            DeletePartButton.Location = new Point(198, 633);
            DeletePartButton.Name = "DeletePartButton";
            DeletePartButton.Size = new Size(170, 34);
            DeletePartButton.TabIndex = 17;
            DeletePartButton.Text = "Delete";
            DeletePartButton.TextAlign = ContentAlignment.MiddleLeft;
            DeletePartButton.UseVisualStyleBackColor = true;
            DeletePartButton.Click += DeletePartButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Image = Properties.Resources.clear;
            ClearButton.ImageAlign = ContentAlignment.MiddleRight;
            ClearButton.Location = new Point(12, 673);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(180, 34);
            ClearButton.TabIndex = 18;
            ClearButton.Text = "Clear";
            ClearButton.TextAlign = ContentAlignment.MiddleLeft;
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Image = Properties.Resources.smallclose;
            CloseButton.ImageAlign = ContentAlignment.MiddleRight;
            CloseButton.Location = new Point(198, 673);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(170, 34);
            CloseButton.TabIndex = 19;
            CloseButton.Text = "Close";
            CloseButton.TextAlign = ContentAlignment.MiddleLeft;
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // labelManufacturer
            // 
            labelManufacturer.AutoSize = true;
            labelManufacturer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelManufacturer.Location = new Point(12, 379);
            labelManufacturer.Name = "labelManufacturer";
            labelManufacturer.Size = new Size(129, 25);
            labelManufacturer.TabIndex = 10;
            labelManufacturer.Text = "Manufacturer";
            // 
            // UseExistingManufacturerCheckBox
            // 
            UseExistingManufacturerCheckBox.AutoSize = true;
            UseExistingManufacturerCheckBox.Checked = true;
            UseExistingManufacturerCheckBox.CheckState = CheckState.Checked;
            UseExistingManufacturerCheckBox.Location = new Point(12, 407);
            UseExistingManufacturerCheckBox.Name = "UseExistingManufacturerCheckBox";
            UseExistingManufacturerCheckBox.Size = new Size(242, 29);
            UseExistingManufacturerCheckBox.TabIndex = 11;
            UseExistingManufacturerCheckBox.Text = "Use existing manufacturer";
            UseExistingManufacturerCheckBox.UseVisualStyleBackColor = true;
            UseExistingManufacturerCheckBox.CheckedChanged += UseExistingManufacturerCheckBox_CheckedChanged;
            // 
            // ManufacturerComboBox
            // 
            ManufacturerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ManufacturerComboBox.FormattingEnabled = true;
            ManufacturerComboBox.Location = new Point(12, 442);
            ManufacturerComboBox.Name = "ManufacturerComboBox";
            ManufacturerComboBox.Size = new Size(356, 33);
            ManufacturerComboBox.TabIndex = 12;
            // 
            // labelCustomManufacturer
            // 
            labelCustomManufacturer.AutoSize = true;
            labelCustomManufacturer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelCustomManufacturer.Location = new Point(12, 488);
            labelCustomManufacturer.Name = "labelCustomManufacturer";
            labelCustomManufacturer.Size = new Size(197, 25);
            labelCustomManufacturer.TabIndex = 13;
            labelCustomManufacturer.Text = "Custom manufacturer";
            // 
            // CustomManufacturerTextBox
            // 
            CustomManufacturerTextBox.Enabled = false;
            CustomManufacturerTextBox.Location = new Point(12, 516);
            CustomManufacturerTextBox.Name = "CustomManufacturerTextBox";
            CustomManufacturerTextBox.Size = new Size(356, 31);
            CustomManufacturerTextBox.TabIndex = 14;
            // 
            // labelPartStatus
            // 
            labelPartStatus.AutoSize = true;
            labelPartStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPartStatus.Location = new Point(12, 559);
            labelPartStatus.Name = "labelPartStatus";
            labelPartStatus.Size = new Size(104, 25);
            labelPartStatus.TabIndex = 15;
            labelPartStatus.Text = "Part status";
            // 
            // PartStatusComboBox
            // 
            PartStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PartStatusComboBox.FormattingEnabled = true;
            PartStatusComboBox.Location = new Point(12, 587);
            PartStatusComboBox.Name = "PartStatusComboBox";
            PartStatusComboBox.Size = new Size(356, 33);
            PartStatusComboBox.TabIndex = 16;
            // 
            // groupBoxRight
            // 
            groupBoxRight.Controls.Add(UndoButton);
            groupBoxRight.Controls.Add(PartsDGV);
            groupBoxRight.Location = new Point(398, 12);
            groupBoxRight.Name = "groupBoxRight";
            groupBoxRight.Size = new Size(982, 954);
            groupBoxRight.TabIndex = 1;
            groupBoxRight.TabStop = false;
            groupBoxRight.Text = "Part list";
            // 
            // PartsDGV
            // 
            PartsDGV.AllowUserToAddRows = false;
            PartsDGV.AllowUserToDeleteRows = false;
            PartsDGV.AllowUserToResizeRows = false;
            PartsDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PartsDGV.Location = new Point(6, 60);
            PartsDGV.MultiSelect = false;
            PartsDGV.Name = "PartsDGV";
            PartsDGV.ReadOnly = true;
            PartsDGV.RowHeadersWidth = 62;
            PartsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PartsDGV.Size = new Size(969, 888);
            PartsDGV.TabIndex = 0;
            PartsDGV.CellDoubleClick += PartsDGV_CellDoubleClick;
            PartsDGV.SelectionChanged += PartsDGV_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SearchButton);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(StockCheckbox);
            groupBox1.Controls.Add(CriteriaComboBox);
            groupBox1.Controls.Add(SearchTextBox);
            groupBox1.Font = new Font("Segoe UI", 9F);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(380, 198);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search";
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(6, 73);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 3;
            label2.Text = "Search criteria";
            // 
            // StockCheckbox
            // 
            StockCheckbox.AutoSize = true;
            StockCheckbox.Location = new Point(6, 154);
            StockCheckbox.Name = "StockCheckbox";
            StockCheckbox.Size = new Size(101, 29);
            StockCheckbox.TabIndex = 2;
            StockCheckbox.Text = "In Stock";
            StockCheckbox.UseVisualStyleBackColor = true;
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
            // UndoButton
            // 
            UndoButton.Image = Properties.Resources.undo;
            UndoButton.Location = new Point(940, 20);
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(35, 34);
            UndoButton.TabIndex = 3;
            UndoButton.UseVisualStyleBackColor = true;
            UndoButton.Click += UndoButton_Click;
            // 
            // PartForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1387, 975);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxRight);
            Controls.Add(groupBoxLeft);
            Name = "PartForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Part management";
            groupBoxLeft.ResumeLayout(false);
            groupBoxLeft.PerformLayout();
            groupBoxRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PartsDGV).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }


        #endregion

        private GroupBox groupBoxLeft;
        private GroupBox groupBoxRight;

        private DataGridView PartsDGV;

        private Label labelPartNumber;
        private Label labelPartName;
        private Label labelComponentId;
        private Label labelPrice;
        private Label labelQuantity;

        private TextBox PartNumberTextBox;
        private TextBox PartNameTextBox;
        private TextBox PriceTextBox;
        private TextBox QuantityTextBox;

        private Button AddPartButton;
        private Button DeletePartButton;
        private Button ClearButton;
        private Button CloseButton;
        private ComboBox PartStatusComboBox;
        private Label labelPartStatus;

        private CheckBox UseExistingManufacturerCheckBox;
        private ComboBox ManufacturerComboBox;
        private Label labelManufacturer;
        private Label labelCustomManufacturer;
        private TextBox CustomManufacturerTextBox;
        private ComboBox ComponentComboBox;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox SearchTextBox;
        private Button SearchButton;
        private Label label2;
        private CheckBox StockCheckbox;
        private ComboBox CriteriaComboBox;
        private Button UndoButton;
    }
}