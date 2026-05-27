namespace ServiceDatabaseProject
{
    partial class NotificationsForm
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
            OverdueList = new ListView();
            SuspendLayout();
            // 
            // OverdueList
            // 
            OverdueList.FullRowSelect = true;
            OverdueList.GridLines = true;
            OverdueList.Location = new Point(12, 12);
            OverdueList.MultiSelect = false;
            OverdueList.Name = "OverdueList";
            OverdueList.Size = new Size(434, 181);
            OverdueList.TabIndex = 2;
            OverdueList.UseCompatibleStateImageBehavior = false;
            OverdueList.View = View.Details;
            // 
            // NotificationsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 242);
            Controls.Add(OverdueList);
            Name = "NotificationsForm";
            Text = "NotificationsForm";
            ResumeLayout(false);
        }

        #endregion
        private ListView OverdueList;
    }
}