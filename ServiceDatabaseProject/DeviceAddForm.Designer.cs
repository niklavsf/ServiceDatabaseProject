namespace ServiceDatabaseProject
{
    partial class DeviceAddForm
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
            DeviceAddDGV = new DataGridView();
            DeviceComboBox = new ComboBox();
            label1 = new Label();
            AddNewTB = new TextBox();
            AcceptDeviceButton = new Button();
            DeviceTreeView = new TreeView();
            AddNewItemButton = new Button();
            groupBox1 = new GroupBox();
            DeleteButton = new Button();
            TextboxLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)DeviceAddDGV).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // DeviceAddDGV
            // 
            DeviceAddDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DeviceAddDGV.Location = new Point(301, 12);
            DeviceAddDGV.Name = "DeviceAddDGV";
            DeviceAddDGV.RowHeadersWidth = 62;
            DeviceAddDGV.Size = new Size(669, 373);
            DeviceAddDGV.TabIndex = 0;
            DeviceAddDGV.CellDoubleClick += DeviceAddDGV_CellDoubleClick;
            DeviceAddDGV.CellEndEdit += DeviceAddDGV_CellEndEdit;
            // 
            // DeviceComboBox
            // 
            DeviceComboBox.FormattingEnabled = true;
            DeviceComboBox.Location = new Point(6, 41);
            DeviceComboBox.Name = "DeviceComboBox";
            DeviceComboBox.Size = new Size(424, 33);
            DeviceComboBox.TabIndex = 1;
            DeviceComboBox.SelectedIndexChanged += DeviceComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(563, 402);
            label1.Name = "label1";
            label1.Size = new Size(87, 25);
            label1.TabIndex = 2;
            label1.Text = "Add new:";
            // 
            // AddNewTB
            // 
            AddNewTB.Location = new Point(528, 43);
            AddNewTB.Name = "AddNewTB";
            AddNewTB.Size = new Size(407, 31);
            AddNewTB.TabIndex = 4;
            // 
            // AcceptDeviceButton
            // 
            AcceptDeviceButton.Image = Properties.Resources.check;
            AcceptDeviceButton.ImageAlign = ContentAlignment.MiddleRight;
            AcceptDeviceButton.Location = new Point(823, 80);
            AcceptDeviceButton.Name = "AcceptDeviceButton";
            AcceptDeviceButton.Size = new Size(112, 34);
            AcceptDeviceButton.TabIndex = 5;
            AcceptDeviceButton.Text = "Accept";
            AcceptDeviceButton.TextAlign = ContentAlignment.MiddleLeft;
            AcceptDeviceButton.UseVisualStyleBackColor = true;
            AcceptDeviceButton.Click += AcceptDeviceButton_Click;
            // 
            // DeviceTreeView
            // 
            DeviceTreeView.Location = new Point(12, 12);
            DeviceTreeView.Name = "DeviceTreeView";
            DeviceTreeView.Size = new Size(283, 373);
            DeviceTreeView.TabIndex = 6;
            DeviceTreeView.AfterLabelEdit += DeviceTreeView_AfterLabelEdit;
            DeviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            DeviceTreeView.NodeMouseDoubleClick += DeviceTreeView_NodeMouseDoubleClick;
            // 
            // AddNewItemButton
            // 
            AddNewItemButton.Image = Properties.Resources.add;
            AddNewItemButton.ImageAlign = ContentAlignment.MiddleRight;
            AddNewItemButton.Location = new Point(526, 80);
            AddNewItemButton.Name = "AddNewItemButton";
            AddNewItemButton.Size = new Size(112, 34);
            AddNewItemButton.TabIndex = 7;
            AddNewItemButton.Text = "Add";
            AddNewItemButton.TextAlign = ContentAlignment.MiddleLeft;
            AddNewItemButton.UseVisualStyleBackColor = true;
            AddNewItemButton.Click += AddNewItemButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(DeleteButton);
            groupBox1.Controls.Add(TextboxLabel);
            groupBox1.Controls.Add(DeviceComboBox);
            groupBox1.Controls.Add(AddNewItemButton);
            groupBox1.Controls.Add(AcceptDeviceButton);
            groupBox1.Controls.Add(AddNewTB);
            groupBox1.Location = new Point(12, 391);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(958, 128);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Devices";
            // 
            // DeleteButton
            // 
            DeleteButton.Image = Properties.Resources.delete;
            DeleteButton.ImageAlign = ContentAlignment.MiddleRight;
            DeleteButton.Location = new Point(672, 80);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(112, 34);
            DeleteButton.TabIndex = 9;
            DeleteButton.Text = "Delete";
            DeleteButton.TextAlign = ContentAlignment.MiddleLeft;
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // TextboxLabel
            // 
            TextboxLabel.AutoSize = true;
            TextboxLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TextboxLabel.Location = new Point(528, 15);
            TextboxLabel.Name = "TextboxLabel";
            TextboxLabel.Size = new Size(63, 25);
            TextboxLabel.TabIndex = 8;
            TextboxLabel.Text = "label2";
            // 
            // DeviceAddForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 526);
            Controls.Add(groupBox1);
            Controls.Add(DeviceTreeView);
            Controls.Add(label1);
            Controls.Add(DeviceAddDGV);
            Name = "DeviceAddForm";
            Text = "DeviceAddForm";
            ((System.ComponentModel.ISupportInitialize)DeviceAddDGV).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DeviceAddDGV;
        private ComboBox DeviceComboBox;
        private Label label1;
        private TextBox AddNewTB;
        private Button AcceptDeviceButton;
        private TreeView DeviceTreeView;
        private Button AddNewItemButton;
        private GroupBox groupBox1;
        private Label TextboxLabel;
        private Button DeleteButton;
    }
}