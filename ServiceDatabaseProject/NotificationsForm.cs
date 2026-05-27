using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ServiceDatabaseProject.HomeScreen;

namespace ServiceDatabaseProject
{
    public partial class NotificationsForm : Form
    {
        private List<NotificationItem> items;
        private ListView listView;
        private Button markReadButton;
        private Button closeButton;
        public NotificationsForm(List<NotificationItem> items)
        {
            this.items = items;

            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = SystemColors.Window;
            this.Width = 360;
            this.Height = 220;

            PositionTopRight();

            BuildUi();
            LoadList();
        }

        private void PositionTopRight()
        {
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(r.Right - this.Width - 10, r.Top + 10);
        }

        private void BuildUi()
        {
            listView = new ListView();
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.HideSelection = false;
            listView.Location = new Point(10, 10);
            listView.Size = new Size(this.Width - 20, this.Height - 60);

            listView.Columns.Add("Service", 90);
            listView.Columns.Add("Days", 60);
            listView.Columns.Add("Week", 60);
            listView.Columns.Add("Sent", 110);

            markReadButton = new Button();
            markReadButton.Text = "Mark read";
            markReadButton.Size = new Size(100, 30);
            markReadButton.Location = new Point(10, this.Height - 40);
            markReadButton.Click += MarkReadButton_Click;

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Size = new Size(80, 30);
            closeButton.Location = new Point(this.Width - 90, this.Height - 40);
            closeButton.Click += CloseButton_Click;

            this.Controls.Add(listView);
            this.Controls.Add(markReadButton);
            this.Controls.Add(closeButton);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MarkReadButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;

            // you need to map selected row -> NotificationItem
            // easiest: store ServiceId in Tag
            ListViewItem sel = listView.SelectedItems[0];
            long serviceId = Convert.ToInt64(sel.Tag);

            // mark read in memory
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ServiceId == serviceId)
                {
                    items[i].IsRead = true;
                    break;
                }
            }

            // persist
            List<NotificationItem> store = NotificationStore.Load();
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].ServiceId == serviceId)
                {
                    store[i].IsRead = true;
                    break;
                }
            }
            NotificationStore.Save(store);

            // remove from list UI
            listView.Items.Remove(sel);
        }

        private void LoadList()
        {
            listView.Items.Clear();

            DateTime today = DateTime.Today;

            for (int i = 0; i < items.Count; i++)
            {
                NotificationItem n = items[i];

                // If you have DaysActive already, show it.
                // If you only stored SentDate, you can show age since start or since sent.
                string days = ""; // optional
                string week = n.WeekNumber.ToString();
                string sent = n.SentDate.ToString("dd.MM");

                ListViewItem li = new ListViewItem(n.ServiceId.ToString());
                li.SubItems.Add(days);
                li.SubItems.Add(week);
                li.SubItems.Add(sent);
                li.Tag = n.ServiceId;

                listView.Items.Add(li);
            }
        }

    }

}
