namespace ServiceDatabaseProject
{
    partial class AddNewServiceForm
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
            gp1 = new GroupBox();
            ManufacturerTextBox = new TextBox();
            ModelSeriesTextBox = new TextBox();
            ModelTextBox = new TextBox();
            ChangeDeviceButton = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            SerialNumberTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            ServiceIdTextBox = new TextBox();
            groupBox1 = new GroupBox();
            ProblemDescriptionTextBox = new TextBox();
            label7 = new Label();
            groupBox2 = new GroupBox();
            label8 = new Label();
            ChangeCustomerButton = new Button();
            EmailTextBox = new TextBox();
            label13 = new Label();
            PhoneNumberTextBox = new TextBox();
            label12 = new Label();
            SurnameTextBox = new TextBox();
            label11 = new Label();
            NameTextBox = new TextBox();
            label10 = new Label();
            label9 = new Label();
            groupBox4 = new GroupBox();
            CancelButton = new Button();
            ConfirmButton = new Button();
            label16 = new Label();
            PriceTextBox = new TextBox();
            label15 = new Label();
            groupBox5 = new GroupBox();
            label17 = new Label();
            StartDateTextBox = new TextBox();
            gp1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // gp1
            // 
            gp1.Controls.Add(ManufacturerTextBox);
            gp1.Controls.Add(ModelSeriesTextBox);
            gp1.Controls.Add(ModelTextBox);
            gp1.Controls.Add(ChangeDeviceButton);
            gp1.Controls.Add(label6);
            gp1.Controls.Add(label5);
            gp1.Controls.Add(label4);
            gp1.Controls.Add(label3);
            gp1.Controls.Add(SerialNumberTextBox);
            gp1.Controls.Add(label2);
            gp1.Controls.Add(label1);
            gp1.Controls.Add(ServiceIdTextBox);
            gp1.Location = new Point(12, 42);
            gp1.Name = "gp1";
            gp1.Size = new Size(483, 313);
            gp1.TabIndex = 0;
            gp1.TabStop = false;
            // 
            // ManufacturerTextBox
            // 
            ManufacturerTextBox.Location = new Point(182, 177);
            ManufacturerTextBox.Name = "ManufacturerTextBox";
            ManufacturerTextBox.ReadOnly = true;
            ManufacturerTextBox.Size = new Size(295, 31);
            ManufacturerTextBox.TabIndex = 14;
            // 
            // ModelSeriesTextBox
            // 
            ModelSeriesTextBox.Location = new Point(182, 222);
            ModelSeriesTextBox.Name = "ModelSeriesTextBox";
            ModelSeriesTextBox.ReadOnly = true;
            ModelSeriesTextBox.Size = new Size(295, 31);
            ModelSeriesTextBox.TabIndex = 13;
            // 
            // ModelTextBox
            // 
            ModelTextBox.Location = new Point(182, 265);
            ModelTextBox.Name = "ModelTextBox";
            ModelTextBox.ReadOnly = true;
            ModelTextBox.Size = new Size(295, 31);
            ModelTextBox.TabIndex = 12;
            // 
            // ChangeDeviceButton
            // 
            ChangeDeviceButton.Image = Properties.Resources.devices;
            ChangeDeviceButton.ImageAlign = ContentAlignment.MiddleRight;
            ChangeDeviceButton.Location = new Point(312, 87);
            ChangeDeviceButton.Name = "ChangeDeviceButton";
            ChangeDeviceButton.Size = new Size(165, 34);
            ChangeDeviceButton.TabIndex = 11;
            ChangeDeviceButton.Text = "Change device";
            ChangeDeviceButton.TextAlign = ContentAlignment.MiddleLeft;
            ChangeDeviceButton.UseVisualStyleBackColor = true;
            ChangeDeviceButton.Click += ChangeDeviceButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 271);
            label6.Name = "label6";
            label6.Size = new Size(63, 25);
            label6.TabIndex = 9;
            label6.Text = "Model";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 228);
            label5.Name = "label5";
            label5.Size = new Size(112, 25);
            label5.TabIndex = 7;
            label5.Text = "Model series";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 180);
            label4.Name = "label4";
            label4.Size = new Size(117, 25);
            label4.TabIndex = 5;
            label4.Text = "Manufacturer";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 135);
            label3.Name = "label3";
            label3.Size = new Size(121, 25);
            label3.TabIndex = 4;
            label3.Text = "Serial number";
            // 
            // SerialNumberTextBox
            // 
            SerialNumberTextBox.Location = new Point(182, 129);
            SerialNumberTextBox.Name = "SerialNumberTextBox";
            SerialNumberTextBox.ReadOnly = true;
            SerialNumberTextBox.Size = new Size(295, 31);
            SerialNumberTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 87);
            label2.Name = "label2";
            label2.Size = new Size(69, 25);
            label2.TabIndex = 2;
            label2.Text = "Device";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(98, 25);
            label1.TabIndex = 1;
            label1.Text = "Service ID";
            // 
            // ServiceIdTextBox
            // 
            ServiceIdTextBox.Location = new Point(182, 30);
            ServiceIdTextBox.Name = "ServiceIdTextBox";
            ServiceIdTextBox.ReadOnly = true;
            ServiceIdTextBox.Size = new Size(295, 31);
            ServiceIdTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ProblemDescriptionTextBox);
            groupBox1.Controls.Add(label7);
            groupBox1.Location = new Point(12, 361);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(483, 166);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // ProblemDescriptionTextBox
            // 
            ProblemDescriptionTextBox.Location = new Point(6, 55);
            ProblemDescriptionTextBox.Multiline = true;
            ProblemDescriptionTextBox.Name = "ProblemDescriptionTextBox";
            ProblemDescriptionTextBox.Size = new Size(471, 100);
            ProblemDescriptionTextBox.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(6, 27);
            label7.Name = "label7";
            label7.Size = new Size(183, 25);
            label7.TabIndex = 0;
            label7.Text = "Problem description";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(ChangeCustomerButton);
            groupBox2.Controls.Add(EmailTextBox);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(PhoneNumberTextBox);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(SurnameTextBox);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(NameTextBox);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Location = new Point(564, 42);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(483, 313);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(128, 289);
            label8.Name = "label8";
            label8.Size = new Size(355, 21);
            label8.TabIndex = 10;
            label8.Text = "Note: Double click on the row to select a customer";
            // 
            // ChangeCustomerButton
            // 
            ChangeCustomerButton.Image = Properties.Resources.customers;
            ChangeCustomerButton.ImageAlign = ContentAlignment.MiddleRight;
            ChangeCustomerButton.Location = new Point(295, 57);
            ChangeCustomerButton.Name = "ChangeCustomerButton";
            ChangeCustomerButton.Size = new Size(182, 34);
            ChangeCustomerButton.TabIndex = 9;
            ChangeCustomerButton.Text = "Change customer";
            ChangeCustomerButton.TextAlign = ContentAlignment.MiddleLeft;
            ChangeCustomerButton.UseVisualStyleBackColor = true;
            ChangeCustomerButton.Click += ChangeCustomerButton_Click;
            // 
            // EmailTextBox
            // 
            EmailTextBox.Location = new Point(182, 235);
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.ReadOnly = true;
            EmailTextBox.Size = new Size(295, 31);
            EmailTextBox.TabIndex = 8;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 238);
            label13.Name = "label13";
            label13.Size = new Size(61, 25);
            label13.TabIndex = 7;
            label13.Text = "E-mail";
            // 
            // PhoneNumberTextBox
            // 
            PhoneNumberTextBox.Location = new Point(182, 189);
            PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            PhoneNumberTextBox.ReadOnly = true;
            PhoneNumberTextBox.Size = new Size(295, 31);
            PhoneNumberTextBox.TabIndex = 6;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 192);
            label12.Name = "label12";
            label12.Size = new Size(129, 25);
            label12.TabIndex = 5;
            label12.Text = "Phone number";
            // 
            // SurnameTextBox
            // 
            SurnameTextBox.Location = new Point(182, 143);
            SurnameTextBox.Name = "SurnameTextBox";
            SurnameTextBox.ReadOnly = true;
            SurnameTextBox.Size = new Size(295, 31);
            SurnameTextBox.TabIndex = 4;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 146);
            label11.Name = "label11";
            label11.Size = new Size(82, 25);
            label11.TabIndex = 3;
            label11.Text = "Surname";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(182, 97);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.ReadOnly = true;
            NameTextBox.Size = new Size(295, 31);
            NameTextBox.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 100);
            label10.Name = "label10";
            label10.Size = new Size(59, 25);
            label10.TabIndex = 1;
            label10.Text = "Name";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(6, 27);
            label9.Name = "label9";
            label9.Size = new Size(199, 25);
            label9.TabIndex = 0;
            label9.Text = "Customer information";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(CancelButton);
            groupBox4.Controls.Add(ConfirmButton);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(PriceTextBox);
            groupBox4.Controls.Add(label15);
            groupBox4.Location = new Point(560, 453);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(481, 74);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            // 
            // CancelButton
            // 
            CancelButton.ImageAlign = ContentAlignment.MiddleRight;
            CancelButton.Location = new Point(229, 25);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(109, 34);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.TextAlign = ContentAlignment.MiddleLeft;
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // ConfirmButton
            // 
            ConfirmButton.Image = Properties.Resources.check;
            ConfirmButton.ImageAlign = ContentAlignment.MiddleRight;
            ConfirmButton.Location = new Point(365, 24);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(110, 34);
            ConfirmButton.TabIndex = 5;
            ConfirmButton.Text = "Confirm";
            ConfirmButton.TextAlign = ContentAlignment.MiddleLeft;
            ConfirmButton.UseVisualStyleBackColor = true;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(175, 30);
            label16.Name = "label16";
            label16.Size = new Size(22, 25);
            label16.TabIndex = 4;
            label16.Text = "€";
            // 
            // PriceTextBox
            // 
            PriceTextBox.Location = new Point(66, 27);
            PriceTextBox.Name = "PriceTextBox";
            PriceTextBox.Size = new Size(103, 31);
            PriceTextBox.TabIndex = 3;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(6, 27);
            label15.Name = "label15";
            label15.Size = new Size(54, 25);
            label15.TabIndex = 2;
            label15.Text = "Price";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label17);
            groupBox5.Controls.Add(StartDateTextBox);
            groupBox5.Location = new Point(564, 1);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(483, 50);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(6, 16);
            label17.Name = "label17";
            label17.Size = new Size(97, 25);
            label17.TabIndex = 1;
            label17.Text = "Start date";
            // 
            // StartDateTextBox
            // 
            StartDateTextBox.BorderStyle = BorderStyle.None;
            StartDateTextBox.Location = new Point(375, 16);
            StartDateTextBox.Name = "StartDateTextBox";
            StartDateTextBox.ReadOnly = true;
            StartDateTextBox.Size = new Size(102, 24);
            StartDateTextBox.TabIndex = 0;
            // 
            // AddNewServiceForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 541);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(gp1);
            Name = "AddNewServiceForm";
            Text = "ServiceViewForm";
            gp1.ResumeLayout(false);
            gp1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        private GroupBox gp1;
        private Label label1;
        private TextBox ServiceIdTextBox;
        private Label label3;
        private TextBox SerialNumberTextBox;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;
        private GroupBox groupBox1;
        private TextBox ProblemDescriptionTextBox;
        private Label label7;
        private GroupBox groupBox2;
        private Label label9;
        private Label label13;
        private TextBox PhoneNumberTextBox;
        private Label label12;
        private TextBox SurnameTextBox;
        private Label label11;
        private TextBox NameTextBox;
        private Label label10;
        private TextBox EmailTextBox;
        private GroupBox groupBox4;
        private Button CancelButton;
        private Button ConfirmButton;
        private Label label16;
        private TextBox PriceTextBox;
        private Label label15;
        private Button ChangeDeviceButton;
        private Button ChangeCustomerButton;
        private GroupBox groupBox5;
        private TextBox StartDateTextBox;
        private Label label17;
        private TextBox ModelSeriesTextBox;
        private TextBox ModelTextBox;
        private TextBox ManufacturerTextBox;

        #endregion

        private Label label8;
    }
}