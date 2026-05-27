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
    public partial class CustomerForm : Form
    {
        private DataTable customersTable = new DataTable();
        public long SelectedCustomerId;
        public Stack<long> UndoList = new Stack<long>();
        public CustomerForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;


            LoadCustomers();

            CriteriaComboBox.Items.Clear();

            CriteriaComboBox.Items.Add("Name");
            CriteriaComboBox.Items.Add("Surname");
            CriteriaComboBox.Items.Add("Phone");
            CriteriaComboBox.Items.Add("Email");

            CriteriaComboBox.SelectedIndex = 0;
            CustomersDGV.SelectionChanged += CustomersDGV_SelectionChanged;
            CustomersDGV.CellDoubleClick += CustomersDGV_CellDoubleClick;
        }

        public void DatabaseUpdateFormCustomers(string oracleCommand, long customerId)
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(oracleCommand, conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = customerId;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            if (CustomersDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a customer first.");
                return;
            }

            object idObj = CustomersDGV.SelectedRows[0].Cells["CUSTOMER_ID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;

            long customerId = Convert.ToInt64(idObj);

            DialogResult confirm = MessageBox.Show("Delete this customer?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            UndoList.Push(customerId);
            string command = "UPDATE CUSTOMER SET IS_ACTIVE=0 WHERE CUSTOMER_ID = :id";
            ActionLogger.LogUserAction("Customer", "Delete", UserAuthenticationForm.ActiveUserId, customerId);
            ActionLogger.SetDeactivationTime("CUSTOMER", customerId);
            DatabaseUpdateFormCustomers(command, customerId);

            LoadCustomers();
            CustomersDGV.ClearSelection();
        }



        public void LastDeletionListRetrieve()
        {
            if (UndoList.Count == 0)
            {
                MessageBox.Show("Nothing to undo.");
            }
            else
            {
                long customerIdRetrieve = UndoList.Pop();
                string command = "UPDATE CUSTOMER SET IS_ACTIVE=1 WHERE CUSTOMER_ID = :id";
                DatabaseUpdateFormCustomers(command, customerIdRetrieve);
                ActionLogger.LogUserAction("Customer", "Restore", UserAuthenticationForm.ActiveUserId, customerIdRetrieve);
                ActionLogger.ClearDeactivationTime("CUSTOMER", customerIdRetrieve);
                LoadCustomers();
            }
        }


        private void UndoButton_Click(object sender, EventArgs e)
        {
            LastDeletionListRetrieve();
        }

        private void LoadCustomers()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                customersTable.Clear();

                using (OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM CUSTOMER WHERE IS_ACTIVE=1 ORDER BY CUSTOMER_ID", conn))
                {
                    adapter.Fill(customersTable);
                }
            }

            if (customersTable.Columns.Contains("DISPLAY_NAME") == false)
            {
                customersTable.Columns.Add("DISPLAY_NAME", typeof(string));
            }

            for (int i = 0; i < customersTable.Rows.Count; i++)
            {
                DataRow row = customersTable.Rows[i];

                string name = row["CUSTOMER_NAME"] == DBNull.Value ? "" : row["CUSTOMER_NAME"].ToString();
                string surname = row["CUSTOMER_SURNAME"] == DBNull.Value ? "" : row["CUSTOMER_SURNAME"].ToString();

                row["DISPLAY_NAME"] = (name + " " + surname).Trim();
            }

            CustomersDGV.DataSource = customersTable;

            FormatCustomersDGV();
        }


        private void CustomersDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomersDGV.SelectedRows.Count == 0) return;

            DataGridViewRow row = CustomersDGV.SelectedRows[0];

            object idObj = row.Cells["CUSTOMER_ID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;

            SelectedCustomerId = Convert.ToInt64(idObj);

            NameTextBox.Text = row.Cells["CUSTOMER_NAME"].Value == DBNull.Value ? "" : row.Cells["CUSTOMER_NAME"].Value.ToString();
            SurnameTextBox.Text = row.Cells["CUSTOMER_SURNAME"].Value == DBNull.Value ? "" : row.Cells["CUSTOMER_SURNAME"].Value.ToString();
            PhoneTextBox.Text = row.Cells["TELEPHONE_NUMBER"].Value == DBNull.Value ? "" : row.Cells["TELEPHONE_NUMBER"].Value.ToString();
            EmailTextBox.Text = row.Cells["EMAIL"].Value == DBNull.Value ? "" : row.Cells["EMAIL"].Value.ToString();
        }



        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(SurnameTextBox.Text))
            {
                MessageBox.Show("Name and surname are required.");
                return;
            }

            bool hasSelectedRow = CustomersDGV.SelectedRows.Count > 0;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                if (!hasSelectedRow)
                {
                    string sql = @"
                INSERT INTO CUSTOMER
                (
                    CUSTOMER_ID,
                    CUSTOMER_NAME,
                    CUSTOMER_SURNAME,
                    TELEPHONE_NUMBER,
                    EMAIL
                )
                VALUES
                (
                    CRPK.NEXTVAL,
                    :name,
                    :surname,
                    :phone,
                    :email
                )";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = NameTextBox.Text.Trim();
                        cmd.Parameters.Add("surname", OracleDbType.Varchar2).Value = SurnameTextBox.Text.Trim();

                        cmd.Parameters.Add("phone", OracleDbType.Varchar2).Value =
                            string.IsNullOrWhiteSpace(PhoneTextBox.Text) ? (object)DBNull.Value : PhoneTextBox.Text.Trim();

                        cmd.Parameters.Add("email", OracleDbType.Varchar2).Value =
                            string.IsNullOrWhiteSpace(EmailTextBox.Text) ? (object)DBNull.Value : EmailTextBox.Text.Trim();

                        cmd.ExecuteNonQuery();
                        ActionLogger.LogUserAction("Customer", "Update", UserAuthenticationForm.ActiveUserId, SelectedCustomerId);

                    }
                }
                else
                {
                    object idObj = CustomersDGV.SelectedRows[0].Cells["CUSTOMER_ID"].Value;
                    if (idObj == null || idObj == DBNull.Value)
                    {
                        MessageBox.Show("Invalid selected customer.");
                        return;
                    }

                    long customerId = Convert.ToInt64(idObj);

                    string sql = @"
                        UPDATE CUSTOMER
                        SET CUSTOMER_NAME = :name,
                            CUSTOMER_SURNAME = :surname,
                            TELEPHONE_NUMBER = :phone,
                            EMAIL = :email
                        WHERE CUSTOMER_ID = :id";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = NameTextBox.Text.Trim();
                        cmd.Parameters.Add("surname", OracleDbType.Varchar2).Value = SurnameTextBox.Text.Trim();

                        cmd.Parameters.Add("phone", OracleDbType.Varchar2).Value =
                            string.IsNullOrWhiteSpace(PhoneTextBox.Text) ? (object)DBNull.Value : PhoneTextBox.Text.Trim();

                        cmd.Parameters.Add("email", OracleDbType.Varchar2).Value =
                            string.IsNullOrWhiteSpace(EmailTextBox.Text) ? (object)DBNull.Value : EmailTextBox.Text.Trim();

                        cmd.Parameters.Add("id", OracleDbType.Int64).Value = customerId;

                        cmd.ExecuteNonQuery();
                    }
                    ActionLogger.LogUserAction("Customer", "Add", UserAuthenticationForm.ActiveUserId, customerId);

                }
            }
            LoadCustomers();
        }

        private void FormatCustomersDGV()
        {
            CustomersDGV.RowHeadersVisible = false;
            CustomersDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            CustomersDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomersDGV.MultiSelect = false;
            CustomersDGV.ReadOnly = true;
            CustomersDGV.AllowUserToAddRows = false;
            CustomersDGV.AllowUserToDeleteRows = false;

            for (int i = 0; i < CustomersDGV.Columns.Count; i++)
            {
                CustomersDGV.Columns[i].Visible = false;
            }

            if (CustomersDGV.Columns.Contains("CUSTOMER_ID"))
            {
                CustomersDGV.Columns["CUSTOMER_ID"].Visible = true;
                CustomersDGV.Columns["CUSTOMER_ID"].HeaderText = "ID";
                CustomersDGV.Columns["CUSTOMER_ID"].Width = 70;
            }

            if (CustomersDGV.Columns.Contains("DISPLAY_NAME"))
            {
                CustomersDGV.Columns["DISPLAY_NAME"].Visible = true;
                CustomersDGV.Columns["DISPLAY_NAME"].HeaderText = "Name";
                CustomersDGV.Columns["DISPLAY_NAME"].Width = 240;
            }

            if (CustomersDGV.Columns.Contains("EMAIL"))
            {
                CustomersDGV.Columns["EMAIL"].Visible = true;
                CustomersDGV.Columns["EMAIL"].HeaderText = "Email";
                CustomersDGV.Columns["EMAIL"].Width = 240;
            }

            if (CustomersDGV.Columns.Contains("TELEPHONE_NUMBER"))
            {
                CustomersDGV.Columns["TELEPHONE_NUMBER"].Visible = true;
                CustomersDGV.Columns["TELEPHONE_NUMBER"].HeaderText = "Phone";
                CustomersDGV.Columns["TELEPHONE_NUMBER"].Width = 140;
            }
        }



        private void CustomersDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (SelectedCustomerId == null) return;

            long selectedCustomerId = Convert.ToInt64(CustomersDGV.SelectedRows[0].Cells["CUSTOMER_ID"].Value);
            SelectedCustomerId = selectedCustomerId;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            CustomersDGV.ClearSelection();
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";

            NameTextBox.Focus();
        }
        private void CustomerCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string[] searchFields = new string[]
            {
                "CUSTOMER_NAME",
                "CUSTOMER_SURNAME",
                "TELEPHONE_NUMBER",
                "EMAIL"
            };

            string searchText = SearchTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                CustomersDGV.DataSource = customersTable;
                return;
            }

            int idx = CriteriaComboBox.SelectedIndex;
            if (idx < 0 || idx >= searchFields.Length)
                idx = 0;

            string column = searchFields[idx];

            string safeText = searchText.Replace("'", "''");

            string filter = $"{column} LIKE '%{safeText}%'";

            DataTable baseTable = customersTable;
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

            CustomersDGV.DataSource = filtered;
        }

    }
}
