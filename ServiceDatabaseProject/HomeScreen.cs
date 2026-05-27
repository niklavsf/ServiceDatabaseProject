using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;

namespace ServiceDatabaseProject
{
    public partial class HomeScreen : Form
    {
        public static string connString = $"User Id=C##serviss_admin;Password=freimanis;Data Source=localhost:1521/xe;";

        public static DataSet mainServiceDataSet = new DataSet();

        public static DataTable serviceDataTable = new DataTable();
        public static DataTable customerDataTable = new DataTable();
        public static DataTable deviceDataTable = new DataTable();
        public static DataTable modelDataTable = new DataTable();
        public static DataTable modelseriesDataTable = new DataTable();
        public static DataTable manufacturerDataTable = new DataTable();

        public static OracleDataAdapter deviceAdapter;
        public static OracleDataAdapter serviceAdapter;
        public static OracleDataAdapter customerAdapter;
        public static OracleDataAdapter modelAdapter;
        public static OracleDataAdapter modelseriesAdapter;
        public static OracleDataAdapter manufacturerAdapter;

        private DataTable homeDisplayTable = new DataTable();

        public Stack<long> UndoListService = new Stack<long>();
        public Stack<long> UndoListServicePart = new Stack<long>();



        public HomeScreen(bool isAdmin)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            if (!isAdmin)
            {
                AdminButton.Visible = false;
                AdminButton.Enabled = false;
            }


            PopulateSearchCriteria();

            if (mainServiceDataSet.Tables.Count == 0)
            {
                mainServiceDataSet.Tables.Add(serviceDataTable);
                mainServiceDataSet.Tables.Add(customerDataTable);
                mainServiceDataSet.Tables.Add(deviceDataTable);
                mainServiceDataSet.Tables.Add(modelDataTable);
                mainServiceDataSet.Tables.Add(modelseriesDataTable);
                mainServiceDataSet.Tables.Add(manufacturerDataTable);
            }

            MainWindowDataAlt();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (AppSession.CurrentUser != null)
            {
                ActionLogger.LogUserAction("", "Log out", UserAuthenticationForm.ActiveUserId, null);
            }

            base.OnFormClosing(e);
        }


        private void PopulateSearchCriteria()
        {
            SearchCriteriaCombo.Items.Clear();
            SearchCriteriaCombo.Items.Add("Service ID");
            SearchCriteriaCombo.Items.Add("Customer email");
            SearchCriteriaCombo.Items.Add("Model");
            SearchCriteriaCombo.Items.Add("Model series");
            SearchCriteriaCombo.SelectedIndex = 0;
        }

        public void MainWindowDataAlt()
        {
            using (OracleConnection conn = new OracleConnection(connString))
            {
                try
                {
                    conn.Open();

                    serviceAdapter = new OracleDataAdapter("SELECT * FROM SERVICE WHERE IS_ACTIVE=1", conn);
                    customerAdapter = new OracleDataAdapter("SELECT * FROM CUSTOMER WHERE IS_ACTIVE=1", conn);
                    deviceAdapter = new OracleDataAdapter("SELECT * FROM DEVICE WHERE IS_ACTIVE=1", conn);
                    modelAdapter = new OracleDataAdapter("SELECT * FROM MODEL WHERE IS_ACTIVE=1", conn);
                    modelseriesAdapter = new OracleDataAdapter("SELECT * FROM MODELSERIES WHERE IS_ACTIVE=1", conn);
                    manufacturerAdapter = new OracleDataAdapter("SELECT * FROM MANUFACTURER WHERE IS_ACTIVE=1", conn);

                    serviceDataTable.Clear();
                    customerDataTable.Clear();
                    deviceDataTable.Clear();
                    modelDataTable.Clear();
                    modelseriesDataTable.Clear();
                    manufacturerDataTable.Clear();

                    serviceAdapter.Fill(serviceDataTable);
                    customerAdapter.Fill(customerDataTable);
                    deviceAdapter.Fill(deviceDataTable);
                    modelAdapter.Fill(modelDataTable);
                    modelseriesAdapter.Fill(modelseriesDataTable);
                    manufacturerAdapter.Fill(manufacturerDataTable);

                    deviceDataTable.PrimaryKey = new[] { deviceDataTable.Columns["DEVICE_ID"] };
                    customerDataTable.PrimaryKey = new[] { customerDataTable.Columns["CUSTOMER_ID"] };
                    modelDataTable.PrimaryKey = new[] { modelDataTable.Columns["MODEL_ID"] };
                    modelseriesDataTable.PrimaryKey = new[] { modelseriesDataTable.Columns["MODELSERIES_ID"] };
                    manufacturerDataTable.PrimaryKey = new[] { manufacturerDataTable.Columns["MANUFACTURER_ID"] };
                    serviceDataTable.PrimaryKey = new[] { serviceDataTable.Columns["SERVICE_ID"] };

                    homeDisplayTable = BuildHomeDisplayView();

                    MainScreenDGV.DataSource = null;
                    MainScreenDGV.Rows.Clear();
                    MainScreenDGV.Columns.Clear();

                    //MainScreenDGV.DataSource = homeDisplayTable;
                    ApplyHomeSorting();
                    MainScreenDGV.Columns["Completed"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.ColumnHeader;
                    MainScreenDGV.RowHeadersVisible = false;

                    FormatHomeGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private DataTable BuildHomeDisplayView()
        {
            DataTable display = new DataTable();

            display.Columns.Add("Service ID", typeof(long));
            display.Columns.Add("Start date", typeof(DateTime));
            display.Columns.Add("End date", typeof(DateTime));
            display.Columns.Add("Customer email", typeof(string));
            display.Columns.Add("Model series", typeof(string));
            display.Columns.Add("Model", typeof(string));
            display.Columns.Add("Price", typeof(decimal));
            display.Columns.Add("Completed", typeof(string));

            foreach (DataRow serviceRow in serviceDataTable.Rows)
            {
                object serviceIdObj = serviceRow["SERVICE_ID"];
                object customerIdObj = serviceRow["CUSTOMER_ID"];
                object deviceIdObj = serviceRow["DEVICE_ID"];

                long serviceId = serviceIdObj == DBNull.Value ? 0L : Convert.ToInt64(serviceIdObj);

                DateTime? startDate = null;
                if (serviceRow.Table.Columns.Contains("START_DATE") && serviceRow["START_DATE"] != DBNull.Value)
                    startDate = Convert.ToDateTime(serviceRow["START_DATE"]);

                DateTime? endDate = null;
                if (serviceRow.Table.Columns.Contains("END_DATE") && serviceRow["END_DATE"] != DBNull.Value)
                    endDate = Convert.ToDateTime(serviceRow["END_DATE"]);

                decimal price = serviceRow["PRICE"] == DBNull.Value ? 0m : Convert.ToDecimal(serviceRow["PRICE"]);

                long completedNum = serviceRow["COMPLETED"] == DBNull.Value ? 0L : Convert.ToInt64(serviceRow["COMPLETED"]);
                string completedText = completedNum == 1 ? "Yes" : "No";

                string email = "";
                DataRow customerRow = customerIdObj == DBNull.Value ? null : customerDataTable.Rows.Find(customerIdObj);
                if (customerRow != null && customerRow["EMAIL"] != DBNull.Value)
                    email = customerRow["EMAIL"].ToString();

                string modelName = "";
                string seriesName = "";

                DataRow deviceRow = deviceIdObj == DBNull.Value ? null : deviceDataTable.Rows.Find(deviceIdObj);
                if (deviceRow != null && deviceRow["MODEL_ID"] != DBNull.Value)
                {
                    object modelIdObj = deviceRow["MODEL_ID"];
                    DataRow modelRow = modelDataTable.Rows.Find(modelIdObj);

                    if (modelRow != null)
                    {
                        if (modelRow["MODEL_NAME"] != DBNull.Value)
                            modelName = modelRow["MODEL_NAME"].ToString();

                        if (modelRow["MODELSERIES_ID"] != DBNull.Value)
                        {
                            object seriesIdObj = modelRow["MODELSERIES_ID"];
                            DataRow seriesRow = modelseriesDataTable.Rows.Find(seriesIdObj);

                            if (seriesRow != null && seriesRow["MODELSERIES_NAME"] != DBNull.Value)
                                seriesName = seriesRow["MODELSERIES_NAME"].ToString();
                        }
                    }
                }

                DataRow newRow = display.NewRow();
                newRow["Service ID"] = serviceId;
                newRow["Start date"] = startDate.HasValue ? (object)startDate.Value : DBNull.Value;
                newRow["End date"] = endDate.HasValue ? (object)endDate.Value : DBNull.Value;
                newRow["Customer email"] = email;
                newRow["Model series"] = seriesName;
                newRow["Model"] = modelName;
                newRow["Price"] = price;
                newRow["Completed"] = completedText;

                display.Rows.Add(newRow);
            }

            return display;
        }

        private void FormatHomeGrid()
        {
            MainScreenDGV.ReadOnly = true;
            MainScreenDGV.AllowUserToAddRows = false;
            MainScreenDGV.AllowUserToDeleteRows = false;
            MainScreenDGV.AllowUserToResizeRows = false;
            MainScreenDGV.MultiSelect = false;
            MainScreenDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            MainScreenDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (MainScreenDGV.Columns.Contains("Service ID"))
                MainScreenDGV.Columns["Service ID"].Width = 95;

            if (MainScreenDGV.Columns.Contains("Start date"))
                MainScreenDGV.Columns["Start date"].Width = 110;

            if (MainScreenDGV.Columns.Contains("End date"))
                MainScreenDGV.Columns["End date"].Width = 110;

            if (MainScreenDGV.Columns.Contains("Price"))
                MainScreenDGV.Columns["Price"].Width = 80;

            if (MainScreenDGV.Columns.Contains("Completed"))
                MainScreenDGV.Columns["Completed"].Width = 90;

            if (MainScreenDGV.Columns.Contains("Customer email"))
                MainScreenDGV.Columns["Customer email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (MainScreenDGV.Columns.Contains("Model series"))
                MainScreenDGV.Columns["Model series"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (MainScreenDGV.Columns.Contains("Model"))
                MainScreenDGV.Columns["Model"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (MainScreenDGV.Columns.Contains("Start date"))
                MainScreenDGV.Columns["Start date"].DefaultCellStyle.Format = "dd.MM.yyyy";

            if (MainScreenDGV.Columns.Contains("End date"))
                MainScreenDGV.Columns["End date"].DefaultCellStyle.Format = "dd.MM.yyyy";

            if (MainScreenDGV.Columns.Contains("Price"))
            {
                MainScreenDGV.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                MainScreenDGV.Columns["Price"].DefaultCellStyle.Format = "0.00 €";
            }

            MainScreenDGV.CellFormatting -= MainScreenDGV_CellFormatting;
            MainScreenDGV.CellFormatting += MainScreenDGV_CellFormatting;
        }

        private void MainScreenDGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = MainScreenDGV.Rows[e.RowIndex];
            object completedObj = row.Cells["Completed"].Value;

            string completedText = completedObj == null ? "" : completedObj.ToString();

            if (completedText == "No")
            {
                row.DefaultCellStyle.BackColor = Color.Khaki;
            }
            else
            {
                row.DefaultCellStyle.BackColor = MainScreenDGV.DefaultCellStyle.BackColor;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string[] searchFields = new string[]
            {
                "Service ID",
                "Customer email",
                "Model",
                "Model series"
            };

            string searchText = SearchBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MainScreenDGV.DataSource = homeDisplayTable;
                return;
            }

            int id_selected = SearchCriteriaCombo.SelectedIndex;
            if (id_selected < 0 || id_selected >= searchFields.Length)
                id_selected = 0;

            string column = searchFields[id_selected];

            string safeText = searchText.Replace("'", "''");

            string filter;

            if (column == "Service ID")
            {
                filter = $"Convert([{column}], 'System.String') LIKE '%{safeText}%'";
            }
            else
            {
                filter = $"[{column}] LIKE '%{safeText}%'";
            }

            DataTable baseTable = homeDisplayTable;
            if (baseTable == null)
                return;

            DataRow[] results = baseTable.Select(filter);

            if (results.Length == 0)
            {
                MessageBox.Show("No results found.");
                return;
            }

            DataTable filtered = baseTable.Clone();
            foreach (DataRow row in results)
                filtered.ImportRow(row);

            MainScreenDGV.DataSource = filtered;
        }


        private void DeleteMainWindow_Click(object sender, EventArgs e)
        {
            if (MainScreenDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a service first.");
                return;
            }

            long serviceId = Convert.ToInt64(MainScreenDGV.SelectedRows[0].Cells["Service ID"].Value);
            DialogResult confirm = MessageBox.Show("Delete this service entry?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;
            UndoListService.Push(serviceId);
            string command = "UPDATE SERVICE SET IS_ACTIVE = 0 WHERE SERVICE_ID=:id";
            DatabaseUpdateFormService(command, serviceId);
            command = "UPDATE SERVICEPART SET IS_ACTIVE = 0 WHERE SERVICE_ID=:id";
            DatabaseUpdateFormService(command, serviceId);
            ActionLogger.LogUserAction("Service", "Delete", UserAuthenticationForm.ActiveUserId, serviceId);
            ActionLogger.SetDeactivationTime("Service", serviceId);
            MainWindowDataAlt();
        }


        public class OverdueServiceInfo
        {
            public long ServiceId { get; set; }
            public int DaysActive { get; set; }
            public int WeekNumber { get; set; }
            public DateTime StartDate { get; set; }
        }

        private void NotificationButton_Click(object sender, EventArgs e)
        {
            MainWindowDataAlt();

            List<NotificationItem> items = UpdateNotificationStoreFromServices();
            List<NotificationItem> unread = items.Where(x => x.IsRead == false).ToList();

            if (unread.Count == 0)
            {
                MessageBox.Show("No unread notifications.", "Notifications");
                return;
            }

            using (NotificationsForm f = new NotificationsForm(unread))
            {
                f.StartPosition = FormStartPosition.Manual;
                Rectangle btnRect = NotificationButton.RectangleToScreen(NotificationButton.ClientRectangle);

                int gap = 8;

                int x = btnRect.Left - f.Width - gap;

                int y = btnRect.Top;

                Rectangle wa = Screen.FromControl(this).WorkingArea;

                if (x < wa.Left) x = wa.Left + gap;
                if (y < wa.Top) y = wa.Top + gap;
                if (y + f.Height > wa.Bottom) y = wa.Bottom - f.Height - gap;

                f.Location = new Point(x, y);

                f.ShowDialog(this);
            }
        }


        private List<NotificationItem> UpdateNotificationStoreFromServices()
        {
            List<NotificationItem> store = NotificationStore.Load();
            DateTime today = DateTime.Today;
            HashSet<long> completedIds = new HashSet<long>();
            foreach (DataRow s in serviceDataTable.Rows)
            {
                long sid = Convert.ToInt64(s["SERVICE_ID"]);
                long completed = s["COMPLETED"] == DBNull.Value ? 0L : Convert.ToInt64(s["COMPLETED"]);
                if (completed == 1L)
                    completedIds.Add(sid);
            }
            store = store.Where(n => !completedIds.Contains(n.ServiceId)).ToList();

            foreach (DataRow s in serviceDataTable.Rows)
            {
                if (s["START_DATE"] == DBNull.Value) continue;

                long completed = s["COMPLETED"] == DBNull.Value ? 0L : Convert.ToInt64(s["COMPLETED"]);
                if (completed == 1L) continue;

                long serviceId = Convert.ToInt64(s["SERVICE_ID"]);
                DateTime start = Convert.ToDateTime(s["START_DATE"]).Date;
                int days = (today - start).Days;

                if (days < 7) continue;

                int currentWeek = (days / 7) + 1;

                NotificationItem existing = store.FirstOrDefault(x => x.ServiceId == serviceId);

                if (existing == null)
                {
                    store.Add(new NotificationItem
                    {
                        ServiceId = serviceId,
                        SentDate = DateTime.Now,
                        IsRead = false,
                        WeekNumber = currentWeek
                    });
                    continue;
                }

                if ((DateTime.Now - existing.SentDate).TotalDays >= 7)
                {
                    existing.IsRead = false;
                    existing.SentDate = DateTime.Now;
                    existing.WeekNumber = currentWeek;
                }
                else
                {
                    existing.WeekNumber = currentWeek;
                }
            }

            NotificationStore.Save(store);
            return store;
        }

        private void ApplyHomeSorting()
        {
            if (homeDisplayTable == null)
                return;

            DataView view = new DataView(homeDisplayTable);

            view.Sort = "[Completed] DESC, [Start date] DESC";

            MainScreenDGV.DataSource = view;
        }




        private void AdvancedAddButton_Click(object sender, EventArgs e)
        {
            if (MainScreenDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service row first.");
                return;
            }

            long serviceId = Convert.ToInt64(MainScreenDGV.SelectedRows[0].Cells["Service ID"].Value);
            ServiceViewForm advancedAddForm = new ServiceViewForm((int)serviceId);
            advancedAddForm.ShowDialog();
        }

        private void ExportToCSVButton_Click(object sender, EventArgs e)
        {
            DataTable exportTable = homeDisplayTable;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV files (*.csv)|*.csv";
            saveFile.Title = "Export Service Data";
            saveFile.FileName = "services_export.csv";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine(string.Join(",", exportTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                    foreach (DataRow row in exportTable.Rows)
                    {
                        IEnumerable<string> values = row.ItemArray.Select(v =>
                        {
                            string s = v == null || v == DBNull.Value ? "" : v.ToString();
                            return s.Replace(",", " ");
                        });

                        sw.WriteLine(string.Join(",", values));
                    }
                }

                MessageBox.Show("Service data exported successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export service data:\n" + ex.Message);
            }
        }

        private void AddNewServiceButton_Click(object sender, EventArgs e)
        {
            AddNewServiceForm addservice = new AddNewServiceForm();
            addservice.ShowDialog(this);
            MainWindowDataAlt();
        }

        private void ManageCustomerButton_Click(object sender, EventArgs e)
        {
            CustomerForm customers = new CustomerForm();
            customers.ShowDialog(this);
            MainWindowDataAlt();
        }

        private void PartViewButton_Click(object sender, EventArgs e)
        {
            PartForm parts = new PartForm();
            parts.ShowDialog(this);
            MainWindowDataAlt();
        }

        private void MainScreenDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            long serviceId = Convert.ToInt64(
                MainScreenDGV.Rows[e.RowIndex].Cells["Service ID"].Value
            );

            using (ServiceViewForm form = new ServiceViewForm(serviceId))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    MainWindowDataAlt();
                }
            }
        }

        private void ManageDevicesButton_Click(object sender, EventArgs e)
        {
            DeviceAddForm deviceAddForm = new DeviceAddForm();
            deviceAddForm.ShowDialog(this);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            LastDeletionListRetrieve();
        }

        public void DatabaseUpdateFormService(string oracleCommand, long serviceId)
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(oracleCommand, conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = serviceId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LastDeletionListRetrieve()
        {
            if (UndoListService.Count == 0)
            {
                MessageBox.Show("Nothing to undo.");
            }
            else
            {
                long serviceIdRetrieve = UndoListService.Pop();
                string command = "UPDATE SERVICE SET IS_ACTIVE=1 WHERE SERVICE_ID = :id";
                DatabaseUpdateFormService(command, serviceIdRetrieve);
                command = "UPDATE SERVICEPART SET IS_ACTIVE=1 WHERE SERVICE_ID = :id";
                DatabaseUpdateFormService(command, serviceIdRetrieve);
                ActionLogger.LogUserAction("Service", "Restore", UserAuthenticationForm.ActiveUserId, serviceIdRetrieve);
                ActionLogger.ClearDeactivationTime("SERVICE", serviceIdRetrieve);
                MainWindowDataAlt();
            }
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            AdminDashboardForm admin = new AdminDashboardForm();
            admin.ShowDialog(this);
            MainWindowDataAlt();
        }
    }
}
