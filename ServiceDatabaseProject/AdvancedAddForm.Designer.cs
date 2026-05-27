namespace ServiceDatabaseProject
{
    partial class AdvancedAddForm
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
            CustomerNameTB = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            CustomerEmailTB = new TextBox();
            label3 = new Label();
            CustomerSurnameTB = new TextBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            DeviceSerialNumberTB = new TextBox();
            label7 = new Label();
            ModelCB = new ComboBox();
            label6 = new Label();
            ModelseriesCB = new ComboBox();
            label5 = new Label();
            ManufacturerCB = new ComboBox();
            label4 = new Label();
            panel1 = new Panel();
            CancelAdvancedAddButton = new Button();
            SubmitAdvancedAddButton = new Button();
            Specialist = new GroupBox();
            SpecialistTV = new TreeView();
            label8 = new Label();
            PriceTB = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            Specialist.SuspendLayout();
            SuspendLayout();
            // 
            // CustomerNameTB
            // 
            CustomerNameTB.Location = new Point(6, 67);
            CustomerNameTB.Name = "CustomerNameTB";
            CustomerNameTB.Size = new Size(267, 31);
            CustomerNameTB.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 39);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CustomerEmailTB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(CustomerSurnameTB);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(CustomerNameTB);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(279, 269);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Customer";
            // 
            // CustomerEmailTB
            // 
            CustomerEmailTB.Location = new Point(6, 227);
            CustomerEmailTB.Name = "CustomerEmailTB";
            CustomerEmailTB.Size = new Size(267, 31);
            CustomerEmailTB.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 199);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
            label3.TabIndex = 4;
            label3.Text = "Email";
            // 
            // CustomerSurnameTB
            // 
            CustomerSurnameTB.Location = new Point(6, 146);
            CustomerSurnameTB.Name = "CustomerSurnameTB";
            CustomerSurnameTB.Size = new Size(267, 31);
            CustomerSurnameTB.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 118);
            label2.Name = "label2";
            label2.Size = new Size(82, 25);
            label2.TabIndex = 2;
            label2.Text = "Surname";
            // 
            // groupBox2
            // 
            groupBox2.AccessibleRole = AccessibleRole.Sound;
            groupBox2.Controls.Add(DeviceSerialNumberTB);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(ModelCB);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(ModelseriesCB);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(ManufacturerCB);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(312, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(279, 352);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Device";
            // 
            // DeviceSerialNumberTB
            // 
            DeviceSerialNumberTB.Location = new Point(6, 307);
            DeviceSerialNumberTB.Name = "DeviceSerialNumberTB";
            DeviceSerialNumberTB.Size = new Size(267, 31);
            DeviceSerialNumberTB.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 279);
            label7.Name = "label7";
            label7.Size = new Size(121, 25);
            label7.TabIndex = 6;
            label7.Text = "Serial number";
            // 
            // ModelCB
            // 
            ModelCB.FormattingEnabled = true;
            ModelCB.Location = new Point(6, 227);
            ModelCB.Name = "ModelCB";
            ModelCB.Size = new Size(267, 33);
            ModelCB.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 199);
            label6.Name = "label6";
            label6.Size = new Size(63, 25);
            label6.TabIndex = 4;
            label6.Text = "Model";
            // 
            // ModelseriesCB
            // 
            ModelseriesCB.FormattingEnabled = true;
            ModelseriesCB.Location = new Point(6, 146);
            ModelseriesCB.Name = "ModelseriesCB";
            ModelseriesCB.Size = new Size(267, 33);
            ModelseriesCB.TabIndex = 3;
            ModelseriesCB.SelectedIndexChanged += ModelseriesCB_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 118);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 2;
            label5.Text = "Modelseries";
            // 
            // ManufacturerCB
            // 
            ManufacturerCB.FormattingEnabled = true;
            ManufacturerCB.Location = new Point(6, 67);
            ManufacturerCB.Name = "ManufacturerCB";
            ManufacturerCB.Size = new Size(267, 33);
            ManufacturerCB.TabIndex = 1;
            ManufacturerCB.SelectedIndexChanged += ManufacturerCB_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 39);
            label4.Name = "label4";
            label4.Size = new Size(117, 25);
            label4.TabIndex = 0;
            label4.Text = "Manufacturer";
            // 
            // panel1
            // 
            panel1.Controls.Add(PriceTB);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(CancelAdvancedAddButton);
            panel1.Controls.Add(SubmitAdvancedAddButton);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(579, 450);
            panel1.TabIndex = 4;
            // 
            // CancelAdvancedAddButton
            // 
            CancelAdvancedAddButton.Location = new Point(300, 407);
            CancelAdvancedAddButton.Name = "CancelAdvancedAddButton";
            CancelAdvancedAddButton.Size = new Size(112, 34);
            CancelAdvancedAddButton.TabIndex = 7;
            CancelAdvancedAddButton.Text = "Cancel";
            CancelAdvancedAddButton.UseVisualStyleBackColor = true;
            CancelAdvancedAddButton.Click += CancelAdvancedAddButton_Click;
            // 
            // SubmitAdvancedAddButton
            // 
            SubmitAdvancedAddButton.Location = new Point(461, 407);
            SubmitAdvancedAddButton.Name = "SubmitAdvancedAddButton";
            SubmitAdvancedAddButton.Size = new Size(112, 34);
            SubmitAdvancedAddButton.TabIndex = 6;
            SubmitAdvancedAddButton.Text = "Submit";
            SubmitAdvancedAddButton.UseVisualStyleBackColor = true;
            SubmitAdvancedAddButton.Click += SubmitAdvancedAddButton_Click;
            // 
            // Specialist
            // 
            Specialist.Controls.Add(SpecialistTV);
            Specialist.Location = new Point(6, 291);
            Specialist.Name = "Specialist";
            Specialist.Size = new Size(285, 168);
            Specialist.TabIndex = 5;
            Specialist.TabStop = false;
            Specialist.Text = "Specialist";
            // 
            // SpecialistTV
            // 
            SpecialistTV.Location = new Point(6, 27);
            SpecialistTV.Name = "SpecialistTV";
            SpecialistTV.Size = new Size(273, 135);
            SpecialistTV.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(403, 361);
            label8.Name = "label8";
            label8.Size = new Size(64, 25);
            label8.TabIndex = 8;
            label8.Text = "Price €";
            // 
            // PriceTB
            // 
            PriceTB.Location = new Point(473, 358);
            PriceTB.Name = "PriceTB";
            PriceTB.Size = new Size(100, 31);
            PriceTB.TabIndex = 9;
            // 
            // AdvancedAddForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(604, 474);
            Controls.Add(Specialist);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "AdvancedAddForm";
            Text = "AdvancedAddForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Specialist.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox CustomerNameTB;
        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox CustomerEmailTB;
        private Label label3;
        private TextBox CustomerSurnameTB;
        private GroupBox groupBox2;
        private Label label5;
        private ComboBox ManufacturerCB;
        private Label label4;
        private TextBox DeviceSerialNumberTB;
        private Label label7;
        private ComboBox ModelCB;
        private Label label6;
        private ComboBox ModelseriesCB;
        private Panel panel1;
        private GroupBox Specialist;
        private TreeView SpecialistTV;
        private Button SubmitAdvancedAddButton;
        private Button CancelAdvancedAddButton;
        private TextBox PriceTB;
        private Label label8;
    }
}