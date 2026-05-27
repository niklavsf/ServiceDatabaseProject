namespace ServiceDatabaseProject
{
    partial class AdminDashboardForm
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
            groupBox2 = new GroupBox();
            ClearCalendarButton = new Button();
            LogDatePicker = new DateTimePicker();
            LogListView = new ListView();
            UsersDGV = new DataGridView();
            groupBox3 = new GroupBox();
            label1 = new Label();
            UsernameTextbox = new TextBox();
            PassGenButton = new Button();
            label2 = new Label();
            groupBox4 = new GroupBox();
            label8 = new Label();
            ClearFieldButton = new Button();
            label7 = new Label();
            PassGenOutput = new TextBox();
            EnableUserButton = new Button();
            OnlineCheckbox = new CheckBox();
            DisableUserButton = new Button();
            label4 = new Label();
            UserSearchButton = new Button();
            SearchCriteriaCB = new ComboBox();
            UserSearchTextbox = new TextBox();
            groupBox5 = new GroupBox();
            DeletedItemsDGV = new DataGridView();
            groupBox6 = new GroupBox();
            ItemsSearchButton = new Button();
            label6 = new Label();
            ItemSearchTextbox = new TextBox();
            ItemCB = new ComboBox();
            label5 = new Label();
            RestoreItemButton = new Button();
            TablenameCB = new ComboBox();
            groupBox7 = new GroupBox();
            label3 = new Label();
            LogSearchButton = new Button();
            LogSearchTextbox = new TextBox();
            LogCB = new ComboBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UsersDGV).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DeletedItemsDGV).BeginInit();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ClearCalendarButton);
            groupBox2.Controls.Add(LogDatePicker);
            groupBox2.Controls.Add(LogListView);
            groupBox2.Location = new Point(1187, 34);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(598, 926);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Log";
            // 
            // ClearCalendarButton
            // 
            ClearCalendarButton.Image = Properties.Resources.clear;
            ClearCalendarButton.Location = new Point(224, 21);
            ClearCalendarButton.Name = "ClearCalendarButton";
            ClearCalendarButton.Size = new Size(31, 31);
            ClearCalendarButton.TabIndex = 2;
            ClearCalendarButton.UseVisualStyleBackColor = true;
            ClearCalendarButton.Click += ClearCalendarButton_Click;
            // 
            // LogDatePicker
            // 
            LogDatePicker.Location = new Point(260, 21);
            LogDatePicker.Name = "LogDatePicker";
            LogDatePicker.Size = new Size(332, 31);
            LogDatePicker.TabIndex = 1;
            LogDatePicker.ValueChanged += LogDatePicker_ValueChanged;
            // 
            // LogListView
            // 
            LogListView.Location = new Point(6, 58);
            LogListView.Name = "LogListView";
            LogListView.Size = new Size(585, 858);
            LogListView.TabIndex = 0;
            LogListView.UseCompatibleStateImageBehavior = false;
            // 
            // UsersDGV
            // 
            UsersDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersDGV.Location = new Point(6, 30);
            UsersDGV.Name = "UsersDGV";
            UsersDGV.RowHeadersWidth = 62;
            UsersDGV.Size = new Size(782, 424);
            UsersDGV.TabIndex = 2;
            UsersDGV.CellClick += UsersDGV_CellClick;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(UsersDGV);
            groupBox3.Location = new Point(383, 34);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(798, 462);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Users";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 205);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // UsernameTextbox
            // 
            UsernameTextbox.Location = new Point(6, 233);
            UsernameTextbox.Name = "UsernameTextbox";
            UsernameTextbox.Size = new Size(350, 31);
            UsernameTextbox.TabIndex = 1;
            // 
            // PassGenButton
            // 
            PassGenButton.Image = Properties.Resources.generate;
            PassGenButton.ImageAlign = ContentAlignment.MiddleRight;
            PassGenButton.Location = new Point(193, 336);
            PassGenButton.Name = "PassGenButton";
            PassGenButton.Size = new Size(161, 34);
            PassGenButton.TabIndex = 2;
            PassGenButton.Text = "PassGen";
            PassGenButton.TextAlign = ContentAlignment.MiddleLeft;
            PassGenButton.UseVisualStyleBackColor = true;
            PassGenButton.Click += PassGenButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(836, 10);
            label2.Name = "label2";
            label2.Size = new Size(942, 21);
            label2.TabIndex = 3;
            label2.Text = "Note: PassGen generates a new random password for the selected user. The user will need to change their password on their next login.";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(ClearFieldButton);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(PassGenOutput);
            groupBox4.Controls.Add(EnableUserButton);
            groupBox4.Controls.Add(OnlineCheckbox);
            groupBox4.Controls.Add(DisableUserButton);
            groupBox4.Controls.Add(PassGenButton);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(UserSearchButton);
            groupBox4.Controls.Add(UsernameTextbox);
            groupBox4.Controls.Add(SearchCriteriaCB);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(UserSearchTextbox);
            groupBox4.Location = new Point(15, 34);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(362, 462);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "User manager";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(25, 433);
            label8.Name = "label8";
            label8.Size = new Size(334, 21);
            label8.TabIndex = 8;
            label8.Text = "Note: To create a new user, clear the username";
            // 
            // ClearFieldButton
            // 
            ClearFieldButton.Image = Properties.Resources.clear;
            ClearFieldButton.ImageAlign = ContentAlignment.MiddleRight;
            ClearFieldButton.Location = new Point(6, 336);
            ClearFieldButton.Name = "ClearFieldButton";
            ClearFieldButton.Size = new Size(152, 34);
            ClearFieldButton.TabIndex = 7;
            ClearFieldButton.Text = "Clear";
            ClearFieldButton.TextAlign = ContentAlignment.MiddleLeft;
            ClearFieldButton.UseVisualStyleBackColor = true;
            ClearFieldButton.Click += ClearFieldButton_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(3, 271);
            label7.Name = "label7";
            label7.Size = new Size(186, 25);
            label7.TabIndex = 6;
            label7.Text = "Generated password";
            // 
            // PassGenOutput
            // 
            PassGenOutput.Location = new Point(6, 299);
            PassGenOutput.Name = "PassGenOutput";
            PassGenOutput.ReadOnly = true;
            PassGenOutput.Size = new Size(348, 31);
            PassGenOutput.TabIndex = 5;
            // 
            // EnableUserButton
            // 
            EnableUserButton.Image = Properties.Resources.check;
            EnableUserButton.ImageAlign = ContentAlignment.MiddleRight;
            EnableUserButton.Location = new Point(193, 376);
            EnableUserButton.Name = "EnableUserButton";
            EnableUserButton.Size = new Size(161, 34);
            EnableUserButton.TabIndex = 4;
            EnableUserButton.Text = "Enable";
            EnableUserButton.TextAlign = ContentAlignment.MiddleLeft;
            EnableUserButton.UseVisualStyleBackColor = true;
            EnableUserButton.Click += EnableUserButton_Click;
            // 
            // OnlineCheckbox
            // 
            OnlineCheckbox.AutoSize = true;
            OnlineCheckbox.Location = new Point(6, 152);
            OnlineCheckbox.Name = "OnlineCheckbox";
            OnlineCheckbox.Size = new Size(89, 29);
            OnlineCheckbox.TabIndex = 4;
            OnlineCheckbox.Text = "Online";
            OnlineCheckbox.UseVisualStyleBackColor = true;
            // 
            // DisableUserButton
            // 
            DisableUserButton.Image = Properties.Resources.cross;
            DisableUserButton.ImageAlign = ContentAlignment.MiddleRight;
            DisableUserButton.Location = new Point(6, 376);
            DisableUserButton.Name = "DisableUserButton";
            DisableUserButton.Size = new Size(152, 34);
            DisableUserButton.TabIndex = 3;
            DisableUserButton.Text = "Disable";
            DisableUserButton.TextAlign = ContentAlignment.MiddleLeft;
            DisableUserButton.UseVisualStyleBackColor = true;
            DisableUserButton.Click += DisableUserButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(6, 43);
            label4.Name = "label4";
            label4.Size = new Size(100, 25);
            label4.TabIndex = 3;
            label4.Text = "Search by:";
            // 
            // UserSearchButton
            // 
            UserSearchButton.Image = Properties.Resources.search;
            UserSearchButton.ImageAlign = ContentAlignment.MiddleRight;
            UserSearchButton.Location = new Point(201, 147);
            UserSearchButton.Name = "UserSearchButton";
            UserSearchButton.Size = new Size(155, 34);
            UserSearchButton.TabIndex = 2;
            UserSearchButton.Text = "Search users";
            UserSearchButton.TextAlign = ContentAlignment.MiddleLeft;
            UserSearchButton.UseVisualStyleBackColor = true;
            UserSearchButton.Click += UserSearchButton_Click;
            // 
            // SearchCriteriaCB
            // 
            SearchCriteriaCB.FormattingEnabled = true;
            SearchCriteriaCB.Location = new Point(6, 71);
            SearchCriteriaCB.Name = "SearchCriteriaCB";
            SearchCriteriaCB.Size = new Size(350, 33);
            SearchCriteriaCB.TabIndex = 1;
            // 
            // UserSearchTextbox
            // 
            UserSearchTextbox.Location = new Point(6, 110);
            UserSearchTextbox.Name = "UserSearchTextbox";
            UserSearchTextbox.Size = new Size(350, 31);
            UserSearchTextbox.TabIndex = 0;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(DeletedItemsDGV);
            groupBox5.Location = new Point(383, 502);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(798, 458);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "Deleted items";
            // 
            // DeletedItemsDGV
            // 
            DeletedItemsDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DeletedItemsDGV.Location = new Point(6, 30);
            DeletedItemsDGV.Name = "DeletedItemsDGV";
            DeletedItemsDGV.RowHeadersWidth = 62;
            DeletedItemsDGV.Size = new Size(782, 418);
            DeletedItemsDGV.TabIndex = 0;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ItemsSearchButton);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(ItemSearchTextbox);
            groupBox6.Controls.Add(ItemCB);
            groupBox6.Controls.Add(label5);
            groupBox6.Controls.Add(RestoreItemButton);
            groupBox6.Controls.Add(TablenameCB);
            groupBox6.Location = new Point(12, 694);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(365, 266);
            groupBox6.TabIndex = 6;
            groupBox6.TabStop = false;
            groupBox6.Text = "Search deleted items";
            // 
            // ItemsSearchButton
            // 
            ItemsSearchButton.Image = Properties.Resources.search;
            ItemsSearchButton.ImageAlign = ContentAlignment.MiddleRight;
            ItemsSearchButton.Location = new Point(201, 222);
            ItemsSearchButton.Name = "ItemsSearchButton";
            ItemsSearchButton.Size = new Size(155, 34);
            ItemsSearchButton.TabIndex = 6;
            ItemsSearchButton.Text = "Search items";
            ItemsSearchButton.TextAlign = ContentAlignment.MiddleLeft;
            ItemsSearchButton.UseVisualStyleBackColor = true;
            ItemsSearchButton.Click += ItemsSearchButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(6, 118);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 4;
            label6.Text = "Search by:";
            // 
            // ItemSearchTextbox
            // 
            ItemSearchTextbox.Location = new Point(6, 185);
            ItemSearchTextbox.Name = "ItemSearchTextbox";
            ItemSearchTextbox.Size = new Size(353, 31);
            ItemSearchTextbox.TabIndex = 5;
            // 
            // ItemCB
            // 
            ItemCB.FormattingEnabled = true;
            ItemCB.Location = new Point(6, 146);
            ItemCB.Name = "ItemCB";
            ItemCB.Size = new Size(353, 33);
            ItemCB.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(6, 39);
            label5.Name = "label5";
            label5.Size = new Size(57, 25);
            label5.TabIndex = 3;
            label5.Text = "Table";
            // 
            // RestoreItemButton
            // 
            RestoreItemButton.Image = Properties.Resources.restore;
            RestoreItemButton.ImageAlign = ContentAlignment.MiddleRight;
            RestoreItemButton.Location = new Point(6, 222);
            RestoreItemButton.Name = "RestoreItemButton";
            RestoreItemButton.Size = new Size(152, 34);
            RestoreItemButton.TabIndex = 2;
            RestoreItemButton.Text = "Restore";
            RestoreItemButton.TextAlign = ContentAlignment.MiddleLeft;
            RestoreItemButton.UseVisualStyleBackColor = true;
            RestoreItemButton.Click += RestoreItemButton_Click;
            // 
            // TablenameCB
            // 
            TablenameCB.FormattingEnabled = true;
            TablenameCB.Location = new Point(9, 67);
            TablenameCB.Name = "TablenameCB";
            TablenameCB.Size = new Size(350, 33);
            TablenameCB.TabIndex = 0;
            TablenameCB.SelectedIndexChanged += TablenameCB_SelectedIndexChanged;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label3);
            groupBox7.Controls.Add(LogSearchButton);
            groupBox7.Controls.Add(LogSearchTextbox);
            groupBox7.Controls.Add(LogCB);
            groupBox7.Location = new Point(15, 502);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(362, 186);
            groupBox7.TabIndex = 7;
            groupBox7.TabStop = false;
            groupBox7.Text = "Search log";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(6, 40);
            label3.Name = "label3";
            label3.Size = new Size(100, 25);
            label3.TabIndex = 3;
            label3.Text = "Search by:";
            // 
            // LogSearchButton
            // 
            LogSearchButton.Image = Properties.Resources.search;
            LogSearchButton.ImageAlign = ContentAlignment.MiddleRight;
            LogSearchButton.Location = new Point(195, 144);
            LogSearchButton.Name = "LogSearchButton";
            LogSearchButton.Size = new Size(161, 34);
            LogSearchButton.TabIndex = 2;
            LogSearchButton.Text = "Search log";
            LogSearchButton.TextAlign = ContentAlignment.MiddleLeft;
            LogSearchButton.UseVisualStyleBackColor = true;
            LogSearchButton.Click += LogSearchButton_Click;
            // 
            // LogSearchTextbox
            // 
            LogSearchTextbox.Location = new Point(6, 107);
            LogSearchTextbox.Name = "LogSearchTextbox";
            LogSearchTextbox.Size = new Size(350, 31);
            LogSearchTextbox.TabIndex = 1;
            // 
            // LogCB
            // 
            LogCB.FormattingEnabled = true;
            LogCB.Location = new Point(6, 68);
            LogCB.Name = "LogCB";
            LogCB.Size = new Size(350, 33);
            LogCB.TabIndex = 0;
            // 
            // AdminDashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1793, 967);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(label2);
            Controls.Add(groupBox2);
            Name = "AdminDashboardForm";
            Text = "AdminDashboardForm";
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)UsersDGV).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DeletedItemsDGV).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox2;
        private ListView LogListView;
        private DataGridView UsersDGV;
        private GroupBox groupBox3;
        private Button PassGenButton;
        private TextBox UsernameTextbox;
        private Label label1;
        private Label label2;
        private GroupBox groupBox4;
        private TextBox UserSearchTextbox;
        private ComboBox SearchCriteriaCB;
        private Button UserSearchButton;
        private Button DisableUserButton;
        private Button EnableUserButton;
        private GroupBox groupBox5;
        private DataGridView DeletedItemsDGV;
        private GroupBox groupBox6;
        private Button RestoreItemButton;
        private ComboBox TablenameCB;
        private GroupBox groupBox7;
        private TextBox LogSearchTextbox;
        private ComboBox LogCB;
        private Label label4;
        private Label label3;
        private Button LogSearchButton;
        private Label label5;
        private ComboBox ItemCB;
        private TextBox ItemSearchTextbox;
        private Label label6;
        private Button ItemsSearchButton;
        private CheckBox OnlineCheckbox;
        private Label label7;
        private TextBox PassGenOutput;
        private Button ClearFieldButton;
        private Label label8;
        private DateTimePicker LogDatePicker;
        private Button ClearCalendarButton;
    }
}