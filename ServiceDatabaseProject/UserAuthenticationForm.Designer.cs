namespace ServiceDatabaseProject
{
    partial class UserAuthenticationForm
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
            groupBox1 = new GroupBox();
            ErrorMessageText = new Label();
            PasswordTextbox = new TextBox();
            SecondLabel = new Label();
            FirstLabel = new Label();
            UsernameTextbox = new TextBox();
            LoginButton = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ErrorMessageText);
            groupBox1.Controls.Add(PasswordTextbox);
            groupBox1.Controls.Add(SecondLabel);
            groupBox1.Controls.Add(FirstLabel);
            groupBox1.Controls.Add(UsernameTextbox);
            groupBox1.Controls.Add(LoginButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(359, 217);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // ErrorMessageText
            // 
            ErrorMessageText.AutoSize = true;
            ErrorMessageText.Font = new Font("Arial", 7F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ErrorMessageText.Location = new Point(6, 146);
            ErrorMessageText.Name = "ErrorMessageText";
            ErrorMessageText.Size = new Size(161, 16);
            ErrorMessageText.TabIndex = 6;
            ErrorMessageText.Text = "Passwords do not match";
            // 
            // PasswordTextbox
            // 
            PasswordTextbox.Location = new Point(6, 112);
            PasswordTextbox.Name = "PasswordTextbox";
            PasswordTextbox.Size = new Size(347, 31);
            PasswordTextbox.TabIndex = 4;
            // 
            // SecondLabel
            // 
            SecondLabel.AutoSize = true;
            SecondLabel.Location = new Point(0, 84);
            SecondLabel.Name = "SecondLabel";
            SecondLabel.Size = new Size(87, 25);
            SecondLabel.TabIndex = 3;
            SecondLabel.Text = "Password";
            // 
            // FirstLabel
            // 
            FirstLabel.AutoSize = true;
            FirstLabel.Location = new Point(0, 22);
            FirstLabel.Name = "FirstLabel";
            FirstLabel.Size = new Size(91, 25);
            FirstLabel.TabIndex = 2;
            FirstLabel.Text = "Username";
            // 
            // UsernameTextbox
            // 
            UsernameTextbox.Location = new Point(6, 50);
            UsernameTextbox.Name = "UsernameTextbox";
            UsernameTextbox.Size = new Size(347, 31);
            UsernameTextbox.TabIndex = 1;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(241, 166);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(112, 34);
            LoginButton.TabIndex = 0;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // UserAuthenticationForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 238);
            Controls.Add(groupBox1);
            Name = "UserAuthenticationForm";
            Text = "UserAuthenticationForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox PasswordTextbox;
        private Label SecondLabel;
        private Label FirstLabel;
        private TextBox UsernameTextbox;
        private Button LoginButton;
        private Label ErrorMessageText;
    }
}