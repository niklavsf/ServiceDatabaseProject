using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceDatabaseProject
{
    public partial class AdminDashboardForm : Form
    {
        private const string AppVersion = "v1.1.0";
        private long selectedUserId = 0;

        public AdminDashboardForm()
        {
            InitializeComponent();
            this.Text = "Admin Dashboard " + AppVersion;
            this.StartPosition = FormStartPosition.CenterScreen;
            TablenameCB.Items.Clear();

            TablenameCB.Items.Add("CUSTOMER");
            TablenameCB.Items.Add("DEVICE");
            TablenameCB.Items.Add("MANUFACTURER");
            TablenameCB.Items.Add("MODELSERIES");
            TablenameCB.Items.Add("MODEL");
            TablenameCB.Items.Add("SERVICE");
            TablenameCB.Items.Add("PART");
            TablenameCB.SelectedIndex = 0;

            SetupLogListView();
            LoadAdminLog();
            FillUsersDGV();

            FillItemSearchComboBox();
            FillDeletedItemsDGV();

            FillLogSearchComboBox();

            // Users section
            FillUserSearchCriteriaCB();

        }

        private void FillUsersDGV()
        {
            string sqlCommand = @"
        SELECT
            u.user_id                          AS USER_ID,
            u.username                         AS USERNAME,
            u.last_login                       AS LAST_LOGIN,
            ut.user_type                   AS USER_TYPE,
                    CASE WHEN u.disabled = 1 THEN 'Yes' ELSE 'No' END          AS DISABLED,
                    CASE WHEN u.is_online = 1 THEN 'Online' ELSE 'Offline' END AS IS_ONLINE,
                    CASE WHEN u.password_change = 1 THEN 'Requested' ELSE 'No' END AS PASSWORD_CHANGE
                FROM users u
                INNER JOIN usertype ut ON ut.usertype_id = u.usertype_id
                ORDER BY u.user_id";

            DataTable usersTable = new DataTable();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(usersTable);
                    }
                }
            }

            UsersDGV.AutoGenerateColumns = true;
            UsersDGV.DataSource = usersTable;

            FormatUsersDGV();
        }
        private void FormatUsersDGV()
        {
            UsersDGV.RowHeadersVisible = false;
            UsersDGV.AllowUserToAddRows = false;
            UsersDGV.AllowUserToResizeRows = false;
            UsersDGV.ReadOnly = true;
            UsersDGV.MultiSelect = false;
            UsersDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (UsersDGV.Columns.Contains("USER_ID"))
            {
                UsersDGV.Columns["USER_ID"].HeaderText = "ID";
                UsersDGV.Columns["USER_ID"].Width = 60;
            }

            if (UsersDGV.Columns.Contains("USERNAME"))
            {
                UsersDGV.Columns["USERNAME"].HeaderText = "Username";
                UsersDGV.Columns["USERNAME"].Width = 160;
            }

            if (UsersDGV.Columns.Contains("LAST_LOGIN"))
            {
                UsersDGV.Columns["LAST_LOGIN"].HeaderText = "Last login";
                UsersDGV.Columns["LAST_LOGIN"].Width = 140;
                UsersDGV.Columns["LAST_LOGIN"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            }

            if (UsersDGV.Columns.Contains("USER_TYPE"))
            {
                UsersDGV.Columns["USER_TYPE"].HeaderText = "User type";
                UsersDGV.Columns["USER_TYPE"].Width = 120;
            }

            if (UsersDGV.Columns.Contains("DISABLED"))
            {
                UsersDGV.Columns["DISABLED"].HeaderText = "Disabled";
                UsersDGV.Columns["DISABLED"].Width = 90;
            }

            if (UsersDGV.Columns.Contains("IS_ONLINE"))
            {
                UsersDGV.Columns["IS_ONLINE"].HeaderText = "Status";
                UsersDGV.Columns["IS_ONLINE"].Width = 90;
            }

            if (UsersDGV.Columns.Contains("PASSWORD_CHANGE"))
            {
                UsersDGV.Columns["PASSWORD_CHANGE"].HeaderText = "Pwd change";
                UsersDGV.Columns["PASSWORD_CHANGE"].Width = 110;
            }

            UsersDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }


        private void FillItemSearchComboBox()
        {
            ItemCB.Items.Clear();

            ItemCB.Items.Add("ID");
            ItemCB.Items.Add("Deactivated date");

            ItemCB.SelectedIndex = 0;
        }


        private void SetupLogListView()
        {
            LogListView.View = View.Details;
            LogListView.FullRowSelect = true;
            LogListView.GridLines = true;
            LogListView.HideSelection = false;

            LogListView.Columns.Clear();
            LogListView.Columns.Add("Time", 150);
            LogListView.Columns.Add("User", 120);
            LogListView.Columns.Add("Action", 90);
            LogListView.Columns.Add("Table", 120);
            LogListView.Columns.Add("Entity ID", 90);
        }

        private void LoadAdminLog(DateTime? selectedDate = null)
        {
            string sqlCommand = @"
        SELECT 
            al.log_time,
            u.username,
            al.action,
            al.tablename,
            al.entity_id
        FROM adminlog al
        LEFT JOIN users u ON u.user_id = al.user_id
        WHERE 1 = 1 ";

            if (selectedDate.HasValue)
            {
                sqlCommand += " AND TRUNC(al.log_time) = :selected_date ";
            }

            sqlCommand += " ORDER BY al.log_time DESC";

            LogListView.BeginUpdate();
            LogListView.Items.Clear();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;

                    if (selectedDate.HasValue)
                    {
                        // Bind as Date; Oracle will match TRUNC(log_time) to this date
                        cmd.Parameters.Add(":selected_date", OracleDbType.Date).Value = selectedDate.Value.Date;
                    }

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime logTime = reader.IsDBNull(0)
                                ? DateTime.MinValue
                                : reader.GetDateTime(0);

                            string username = reader.IsDBNull(1)
                                ? "(unknown)"
                                : reader.GetString(1);

                            string action = reader.IsDBNull(2)
                                ? ""
                                : reader.GetString(2);

                            string tableName = reader.IsDBNull(3)
                                ? ""
                                : reader.GetString(3);

                            string entityId = reader.IsDBNull(4)
                                ? ""
                                : reader.GetInt64(4).ToString();

                            ListViewItem item = new ListViewItem(
                                logTime == DateTime.MinValue
                                    ? ""
                                    : logTime.ToString("dd.MM.yyyy HH:mm")
                            );

                            item.SubItems.Add(username);
                            item.SubItems.Add(action);
                            item.SubItems.Add(tableName);
                            item.SubItems.Add(entityId);

                            LogListView.Items.Add(item);
                        }
                    }
                }
            }

            LogListView.EndUpdate();
        }


        private void EnableUserButton_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            string sqlCommand = "UPDATE users SET disabled = 0 WHERE user_id = :id";

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(":id", OracleDbType.Int64).Value = selectedUserId;
                    cmd.ExecuteNonQuery();
                }
            }

            FillUsersDGV();
        }

        private void DisableUserButton_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            string sqlCommand = "UPDATE users SET disabled = 1 WHERE user_id = :id";

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(":id", OracleDbType.Int64).Value = selectedUserId;
                    cmd.ExecuteNonQuery();
                }
            }

            FillUsersDGV();
        }

        private void PassGenButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextbox.Text.Trim();

            if (username.Length == 0)
            {
                MessageBox.Show("Username cannot be empty.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "This will set a temporary password.\nThe user will be required to change it on next login.\n\nContinue?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            string newPassword = PasswordHasher.GenerateRandomPassword(8);
            string passwordHash = PasswordHasher.HashPassword(newPassword);

            long userId = GetUserIdByUsername(username);

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                if (userId > 0)
                {
                    string updateCommand = @"
                UPDATE users
                SET password_hash = :hash,
                    password_change = 1
                WHERE user_id = :id";

                    using (OracleCommand cmd = new OracleCommand(updateCommand, conn))
                    {
                        cmd.BindByName = true;
                        cmd.Parameters.Add(":hash", OracleDbType.Varchar2).Value = passwordHash;
                        cmd.Parameters.Add(":id", OracleDbType.Int64).Value = userId;
                        cmd.ExecuteNonQuery();
                    }

                    selectedUserId = userId;
                }
                else
                {
                    string insertCommand = @"
                INSERT INTO users
                (user_id, username, password_hash, last_login, usertype_id, disabled, name, surname, password_change, is_online)
                VALUES
                (upk.NEXTVAL, :username, :hash, NULL, 2, 0, NULL, NULL, 1, 0)";

                    using (OracleCommand cmd = new OracleCommand(insertCommand, conn))
                    {
                        cmd.BindByName = true;
                        cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add(":hash", OracleDbType.Varchar2).Value = passwordHash;
                        cmd.ExecuteNonQuery();
                    }
                    userId = GetUserIdByUsername(username);
                    selectedUserId = userId;
                }
            }

            PassGenOutput.Text = newPassword;
            FillUsersDGV();

            EnableUserButton.Enabled = (selectedUserId > 0);
            DisableUserButton.Enabled = (selectedUserId > 0);
            PassGenButton.Enabled = true;

            MessageBox.Show("Temporary password set. User must change it on next login.");
        }


        // Get the user's id
        private long GetUserIdByUsername(string username)
        {
            string sqlCommand = "SELECT user_id FROM users WHERE username = :username";

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;

                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt64(result);
                }
            }
        }

        // Deleted items
        private void FillDeletedItemsDGV()
        {
            if (TablenameCB.SelectedItem == null)
            {
                return;
            }

            string tableName = TablenameCB.SelectedItem.ToString();
            string sqlCommand = GetDeletedSelectSql(tableName);

            if (sqlCommand == null)
            {
                MessageBox.Show("Unsupported table selection.");
                return;
            }

            DataTable deletedTable = new DataTable();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(deletedTable);
                    }
                }
            }

            DeletedItemsDGV.AutoGenerateColumns = true;
            DeletedItemsDGV.DataSource = deletedTable;

            FormatDeletedItemsDGV(tableName);
        }


        private string GetDeletedSelectSql(string tableName)
        {
            tableName = tableName.ToUpper();

            if (tableName == "CUSTOMER")
            {
                return @"
            SELECT
                c.customer_id AS CUSTOMER_ID,
                c.email       AS EMAIL,
                (c.customer_name || ' ' || c.customer_surname) AS NAME,
                c.deactivated_at AS DEACTIVATED_AT
            FROM customer c
            WHERE c.is_active = 0
            ORDER BY c.deactivated_at DESC";
            }

            if (tableName == "PART")
            {
                return @"
            SELECT
                p.part_id        AS PART_ID,
                p.part_name      AS PART_NAME,
                p.part_number    AS PART_NUMBER,
                p.deactivated_at AS DEACTIVATED_AT
            FROM part p
            WHERE p.is_active = 0
            ORDER BY p.deactivated_at DESC";
            }

            if (tableName == "DEVICE")
            {
                return @"
            SELECT
                d.device_id      AS DEVICE_ID,
                d.serial_number  AS SERIAL_NUMBER,
                d.model_id       AS MODEL_ID,
                d.deactivated_at AS DEACTIVATED_AT
            FROM device d
            WHERE d.is_active = 0
            ORDER BY d.deactivated_at DESC";
            }

            if (tableName == "SERVICE")
            {
                return @"
            SELECT
                s.service_id     AS SERVICE_ID,
                s.start_date     AS START_DATE,
                s.deactivated_at AS DEACTIVATED_AT
            FROM service s
            WHERE s.is_active = 0
            ORDER BY s.deactivated_at DESC";
            }

            if (tableName == "MANUFACTURER")
            {
                return @"
            SELECT
                m.manufacturer_id AS MANUFACTURER_ID,
                m.name            AS NAME,
                m.deactivated_at  AS DEACTIVATED_AT
            FROM manufacturer m
            WHERE m.is_active = 0
            ORDER BY m.deactivated_at DESC";
            }

            if (tableName == "MODELSERIES")
            {
                return @"
            SELECT
                ms.modelseries_id   AS MODELSERIES_ID,
                ms.modelseries_name AS MODELSERIES_NAME,
                ms.manufacturer_id  AS MANUFACTURER_ID,
                ms.deactivated_at   AS DEACTIVATED_AT
            FROM modelseries ms
            WHERE ms.is_active = 0
            ORDER BY ms.deactivated_at DESC";
            }

            if (tableName == "MODEL")
            {
                return @"
            SELECT
                mo.model_id       AS MODEL_ID,
                mo.model_name     AS MODEL_NAME,
                mo.modelseries_id AS MODELSERIES_ID,
                mo.deactivated_at AS DEACTIVATED_AT
            FROM model mo
            WHERE mo.is_active = 0
            ORDER BY mo.deactivated_at DESC";
            }

            return null;
        }


        private void FormatDeletedItemsDGV(string tableName)
        {
            DeletedItemsDGV.RowHeadersVisible = false;
            DeletedItemsDGV.AllowUserToAddRows = false;
            DeletedItemsDGV.AllowUserToResizeRows = false;
            DeletedItemsDGV.ReadOnly = true;
            DeletedItemsDGV.MultiSelect = false;
            DeletedItemsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DeletedItemsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (DeletedItemsDGV.Columns.Contains("DEACTIVATED_AT"))
            {
                DeletedItemsDGV.Columns["DEACTIVATED_AT"].HeaderText = "Deactivated";
                DeletedItemsDGV.Columns["DEACTIVATED_AT"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            }

            if (DeletedItemsDGV.Columns.Contains("IS_ACTIVE"))
            {
                DeletedItemsDGV.Columns["IS_ACTIVE"].Visible = false; // since it will always be 0 here
            }
        }

        private void RestoreItemButton_Click(object sender, EventArgs e)
        {
            if (DeletedItemsDGV.CurrentRow == null || TablenameCB.SelectedItem == null)
            {
                MessageBox.Show("Select an item first.");
                return;
            }

            string tableName = TablenameCB.SelectedItem.ToString().ToUpper();
            DataGridViewRow row = DeletedItemsDGV.CurrentRow;

            DialogResult result = MessageBox.Show(
                "Restore this item?",
                "Confirm restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                switch (tableName)
                {
                    case "CUSTOMER":
                        Restore(conn, "CUSTOMER", "CUSTOMER_ID", row.Cells["CUSTOMER_ID"].Value);
                        break;

                    case "PART":
                        Restore(conn, "PART", "PART_ID", row.Cells["PART_ID"].Value);
                        break;

                    case "SERVICE":
                        Restore(conn, "SERVICE", "SERVICE_ID", row.Cells["SERVICE_ID"].Value);
                        break;

                    case "DEVICE":
                        Restore(conn, "DEVICE", "DEVICE_ID", row.Cells["DEVICE_ID"].Value);
                        Restore(conn, "MODEL", "MODEL_ID", row.Cells["MODEL_ID"].Value);
                        Restore(conn, "MODELSERIES", "MODELSERIES_ID", row.Cells["MODELSERIES_ID"].Value);
                        Restore(conn, "MANUFACTURER", "MANUFACTURER_ID", row.Cells["MANUFACTURER_ID"].Value);
                        break;

                    case "MODEL":
                        Restore(conn, "MODEL", "MODEL_ID", row.Cells["MODEL_ID"].Value);
                        Restore(conn, "MODELSERIES", "MODELSERIES_ID", row.Cells["MODELSERIES_ID"].Value);
                        Restore(conn, "MANUFACTURER", "MANUFACTURER_ID", row.Cells["MANUFACTURER_ID"].Value);
                        break;

                    case "MODELSERIES":
                        Restore(conn, "MODELSERIES", "MODELSERIES_ID", row.Cells["MODELSERIES_ID"].Value);
                        Restore(conn, "MANUFACTURER", "MANUFACTURER_ID", row.Cells["MANUFACTURER_ID"].Value);
                        break;

                    case "MANUFACTURER":
                        Restore(conn, "MANUFACTURER", "MANUFACTURER_ID", row.Cells["MANUFACTURER_ID"].Value);
                        break;
                }
            }

            FillDeletedItemsDGV();
        }

        private void Restore(
            OracleConnection conn, string tableName, string idColumn, object idValue)
        {
            if (idValue == null || idValue == DBNull.Value)
            {
                return;
            }

            string sql =
                "UPDATE " + tableName +
                " SET IS_ACTIVE = 1, DEACTIVATED_AT = NULL " +
                "WHERE " + idColumn + " = :id";

            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add(":id", OracleDbType.Int64).Value = Convert.ToInt64(idValue);
                cmd.ExecuteNonQuery();
            }
        }



        private void UsersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = UsersDGV.Rows[e.RowIndex];

            object idValue = row.Cells["USER_ID"].Value;
            object usernameValue = row.Cells["USERNAME"].Value;

            if (idValue == null || usernameValue == null)
            {
                return;
            }

            selectedUserId = Convert.ToInt64(idValue);
            UsernameTextbox.Text = usernameValue.ToString();

            EnableUserButton.Enabled = true;
            DisableUserButton.Enabled = true;
            PassGenButton.Enabled = true;
        }

        private void ClearFieldButton_Click(object sender, EventArgs e)
        {
            UsernameTextbox.Text = "";
            PassGenOutput.Text = "";
        }

        private void TablenameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDeletedItemsDGV();
        }

        private string GetIdColumnForTable(string tableName)
        {
            tableName = tableName.ToUpper();

            switch (tableName)
            {
                case "CUSTOMER":
                    return "CUSTOMER_ID";

                case "DEVICE":
                    return "DEVICE_ID";

                case "MANUFACTURER":
                    return "MANUFACTURER_ID";

                case "MODELSERIES":
                    return "MODELSERIES_ID";

                case "MODEL":
                    return "MODEL_ID";

                case "SERVICE":
                    return "SERVICE_ID";

                case "PART":
                    return "PART_ID";

                default:
                    return null;
            }
        }


        private void ItemsSearchButton_Click(object sender, EventArgs e)
        {
            if (TablenameCB.SelectedItem == null)
            {
                return;
            }

            string tableName = TablenameCB.SelectedItem.ToString().ToUpper();
            string searchType = ItemCB.SelectedItem.ToString();
            string searchText = ItemSearchTextbox.Text.Trim();

            if (searchText.Length == 0)
            {
                FillDeletedItemsDGV();
                return;
            }

            string idColumn = GetIdColumnForTable(tableName);

            string sqlCommand = "SELECT * FROM " + tableName + " WHERE IS_ACTIVE = 0 ";

            if (searchType == "ID")
            {
                sqlCommand += "AND " + idColumn + " = :value";
            }
            else if (searchType == "Deactivated date")
            {
                sqlCommand += "AND TRUNC(DEACTIVATED_AT) = TO_DATE(:value, 'DD.MM.YYYY')";
            }

            DataTable table = new DataTable();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;

                    if (searchType == "ID")
                    {
                        long parsedId;

                        if (!long.TryParse(searchText, out parsedId))
                        {
                            MessageBox.Show("ID must be a number.");
                            return;
                        }

                        cmd.Parameters.Add(":value", OracleDbType.Int64).Value = parsedId;
                    }
                    else
                    {
                        cmd.Parameters.Add(":value", OracleDbType.Varchar2).Value = searchText;
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            DeletedItemsDGV.DataSource = table;
            FormatDeletedItemsDGV(tableName);
        }

        private void FillLogSearchComboBox()
        {
            LogCB.Items.Clear();

            LogCB.Items.Add("Action");
            LogCB.Items.Add("Username");
            LogCB.Items.Add("Date");

            LogCB.SelectedIndex = 0;
        }

        private void FillUserSearchCriteriaCB()
        {
            SearchCriteriaCB.Items.Clear();

            SearchCriteriaCB.Items.Add("Username");
            SearchCriteriaCB.Items.Add("User ID");

            SearchCriteriaCB.SelectedIndex = 0;
        }


        private void LogSearchButton_Click(object sender, EventArgs e)
        {
            string searchType = LogCB.SelectedItem.ToString();
            string searchText = LogSearchTextbox.Text.Trim();

            string sqlCommand = @"
                SELECT
                    a.log_time,
                    u.username,
                    a.action,
                    a.tablename,
                    a.entity_id
                FROM adminlog a
                LEFT JOIN users u ON u.user_id = a.user_id
                WHERE 1 = 1 ";

            if (searchText.Length > 0)
            {
                if (searchType == "Action")
                {
                    sqlCommand += "AND UPPER(a.action) = UPPER(:value)";
                }
                else if (searchType == "Username")
                {
                    sqlCommand += "AND UPPER(u.username) = UPPER(:value)";
                }
                else if (searchType == "Date")
                {
                    sqlCommand += "AND TRUNC(a.log_time) = TO_DATE(:value, 'DD.MM.YYYY')";
                }
            }

            sqlCommand += " ORDER BY a.log_time DESC";

            LogListView.Items.Clear();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;

                    if (searchText.Length > 0)
                    {
                        cmd.Parameters.Add(":value", OracleDbType.Varchar2).Value = searchText;
                    }

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(
                                Convert.ToDateTime(reader["LOG_TIME"]).ToString("dd.MM.yyyy HH:mm")
                            );

                            item.SubItems.Add(reader["USERNAME"] == DBNull.Value ? "" : reader["USERNAME"].ToString());
                            item.SubItems.Add(reader["ACTION"].ToString());
                            item.SubItems.Add(reader["TABLENAME"].ToString());
                            item.SubItems.Add(reader["ENTITY_ID"].ToString());

                            LogListView.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void UserSearchButton_Click(object sender, EventArgs e)
        {
            string criteria = SearchCriteriaCB.SelectedItem.ToString();
            string searchText = UserSearchTextbox.Text.Trim();
            bool onlineOnly = OnlineCheckbox.Checked;

            string sqlCommand = @"
                    SELECT
                        u.user_id,
                        u.username,
                        u.last_login,
                        ut.user_type,
                        CASE WHEN u.disabled = 1 THEN 'Yes' ELSE 'No' END AS DISABLED,
                        CASE WHEN u.is_online = 1 THEN 'Online' ELSE 'Offline' END AS STATUS,
                        CASE WHEN u.password_change = 1 THEN 'Requested' ELSE 'No' END AS PASSWORD_CHANGE
                    FROM users u
                    JOIN usertype ut ON ut.usertype_id = u.usertype_id
                    WHERE 1 = 1 ";

            if (searchText.Length > 0)
            {
                if (criteria == "Username")
                {
                    sqlCommand += "AND UPPER(u.username) LIKE UPPER(:value) ";
                }
                else if (criteria == "User ID")
                {
                    sqlCommand += "AND u.user_id = :value ";
                }
            }

            if (onlineOnly == true)
            {
                sqlCommand += "AND u.is_online = 1 ";
            }

            DataTable table = new DataTable();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.BindByName = true;

                    if (searchText.Length > 0)
                    {
                        if (criteria == "User ID")
                        {
                            long parsedUserId;

                            if (!long.TryParse(searchText, out parsedUserId))
                            {
                                MessageBox.Show("User ID must be a number.");
                                return;
                            }

                            cmd.Parameters.Add(":value", OracleDbType.Int64).Value = parsedUserId;
                        }
                        else
                        {
                            cmd.Parameters.Add(":value", OracleDbType.Varchar2).Value = "%" + searchText + "%";
                        }
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            UsersDGV.DataSource = table;
            FormatUsersDGV();
        }

        private void ClearCalendarButton_Click(object sender, EventArgs e)
        {
            LogDatePicker.Value = DateTime.Today;
            LoadAdminLog(null);
        }

        private void LogDatePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadAdminLog(LogDatePicker.Value.Date);
        }
    }
}
