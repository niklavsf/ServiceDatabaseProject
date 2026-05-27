using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ServiceDatabaseProject
{
    public partial class ServiceViewForm : Form
    {
        public long serviceIdReceived;

        // In-memory "current" values (what user is editing)
        private long selectedDeviceId;
        private long selectedCustomerId;

        // For optional "dirty check"
        private long originalDeviceId;
        private long originalCustomerId;

        public ServiceViewForm(long serviceId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            serviceIdReceived = serviceId;

            LoadServiceIntoForm(); // loads original values, sets IDs in memory
            FillPartListService(serviceId);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close(); // no save
        }

        private void LoadServiceIntoForm()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(@"
                    SELECT
                        s.SERVICE_ID,
                        s.CUSTOMER_ID,
                        s.DEVICE_ID,
                        s.PROBLEM_DESCRIPTION,
                        s.FIX_DESCRIPTION,
                        s.PRICE,
                        s.COMPLETED,
                        s.START_DATE,
                        s.END_DATE
                    FROM SERVICE s
                    WHERE s.SERVICE_ID = :id
                ", conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = serviceIdReceived;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Service not found.");
                            this.Close();
                            return;
                        }

                        ServiceIdTextBox.Text = Convert.ToInt64(reader["SERVICE_ID"]).ToString();

                        selectedCustomerId = reader["CUSTOMER_ID"] != DBNull.Value ? Convert.ToInt64(reader["CUSTOMER_ID"]) : 0L;
                        selectedDeviceId = reader["DEVICE_ID"] != DBNull.Value ? Convert.ToInt64(reader["DEVICE_ID"]) : 0L;

                        originalCustomerId = selectedCustomerId;
                        originalDeviceId = selectedDeviceId;

                        ProblemDescriptionTextBox.Text = reader["PROBLEM_DESCRIPTION"] != DBNull.Value ? reader["PROBLEM_DESCRIPTION"].ToString() : "";
                        FixDescriptionTextBox.Text = reader["FIX_DESCRIPTION"] != DBNull.Value ? reader["FIX_DESCRIPTION"].ToString() : "";

                        if (reader["PRICE"] != DBNull.Value)
                            PriceTextBox.Text = Convert.ToDecimal(reader["PRICE"]).ToString("0.00");
                        else
                            PriceTextBox.Text = "";

                        long completed = reader["COMPLETED"] != DBNull.Value ? Convert.ToInt64(reader["COMPLETED"]) : 0L;
                        CompletedRadio.Checked = completed == 1;
                        NotCompletedRadio.Checked = completed != 1;

                        StartDateTextBox.Text = reader["START_DATE"] != DBNull.Value
                            ? Convert.ToDateTime(reader["START_DATE"]).ToString("dd.MM.yyyy")
                            : "";

                        FinishDateTextBox.Text = reader["END_DATE"] != DBNull.Value
                            ? Convert.ToDateTime(reader["END_DATE"]).ToString("dd.MM.yyyy")
                            : "";
                    }
                }
            }

            FillCustomerFieldsById(selectedCustomerId);
            FillDeviceFieldsById(selectedDeviceId);
        }

        private void FillCustomerFieldsById(long customerId)
        {
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";

            if (customerId <= 0) return;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(@"
                    SELECT CUSTOMER_NAME, CUSTOMER_SURNAME, EMAIL, TELEPHONE_NUMBER
                    FROM CUSTOMER
                    WHERE CUSTOMER_ID = :id
                ", conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = customerId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return;

                        NameTextBox.Text = reader["CUSTOMER_NAME"] != DBNull.Value ? reader["CUSTOMER_NAME"].ToString() : "";
                        SurnameTextBox.Text = reader["CUSTOMER_SURNAME"] != DBNull.Value ? reader["CUSTOMER_SURNAME"].ToString() : "";
                        EmailTextBox.Text = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : "";
                        PhoneNumberTextBox.Text = reader["TELEPHONE_NUMBER"] != DBNull.Value ? reader["TELEPHONE_NUMBER"].ToString() : "";
                    }
                }
            }
        }

        private void FillDeviceFieldsById(long deviceId)
        {
            SerialNumberTextBox.Text = "";
            ManufacturerTextBox.Text = "";
            ModelSeriesTextBox.Text = "";
            ModelTextBox.Text = "";

            if (deviceId <= 0) return;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(@"
                    SELECT
                        d.SERIAL_NUMBER,
                        m.MODEL_NAME,
                        ms.MODELSERIES_NAME,
                        mf.NAME AS MANUFACTURER_NAME
                    FROM DEVICE d
                    LEFT JOIN MODEL m ON m.MODEL_ID = d.MODEL_ID
                    LEFT JOIN MODELSERIES ms ON ms.MODELSERIES_ID = m.MODELSERIES_ID
                    LEFT JOIN MANUFACTURER mf ON mf.MANUFACTURER_ID = ms.MANUFACTURER_ID
                    WHERE d.DEVICE_ID = :id
                ", conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = deviceId;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return;

                        SerialNumberTextBox.Text = reader["SERIAL_NUMBER"] != DBNull.Value ? reader["SERIAL_NUMBER"].ToString() : "";
                        ManufacturerTextBox.Text = reader["MANUFACTURER_NAME"] != DBNull.Value ? reader["MANUFACTURER_NAME"].ToString() : "";
                        ModelSeriesTextBox.Text = reader["MODELSERIES_NAME"] != DBNull.Value ? reader["MODELSERIES_NAME"].ToString() : "";
                        ModelTextBox.Text = reader["MODEL_NAME"] != DBNull.Value ? reader["MODEL_NAME"].ToString() : "";
                    }
                }
            }
        }

        private void ChangeDeviceButton_Click(object sender, EventArgs e)
        {
            using (DeviceAddForm deviceForm = new DeviceAddForm())
            {
                if (deviceForm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedDeviceId = deviceForm.SelectedDeviceId;
                    if (selectedDeviceId <= 0)
                    {
                        MessageBox.Show("No device selected.");
                        return;
                    }

                    FillDeviceFieldsById(selectedDeviceId);
                }
            }
        }

        private void FillPartListService(long serviceId)
        {
            string sqlCommand = @"
                SELECT sp.SERVICEPART_ID, p.PART_NAME, p.PRICE
                FROM PART p
                INNER JOIN SERVICEPART sp ON p.PART_ID = sp.PART_ID
                WHERE sp.SERVICE_ID = :id";
            DataTable serviceParts = new DataTable();

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = serviceId;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(serviceParts);
                    }
                }
            }
            PartsDGV.AutoGenerateColumns = true;
            PartsDGV.DataSource = serviceParts;
            PartsDGV.Columns["SERVICEPART_ID"].Visible = false;

            if (PartsDGV.Columns.Contains("PART_NAME"))
            {
                PartsDGV.Columns["PART_NAME"].HeaderText = "Part";
                PartsDGV.Columns["PART_NAME"].Width = 311;
            }

            if (PartsDGV.Columns.Contains("PRICE"))
            {
                var priceCol = PartsDGV.Columns["PRICE"];
                priceCol.HeaderText = "Price";
                priceCol.Width = 135;
                priceCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                priceCol.DefaultCellStyle.Format = "0.00 €";
            }

            PartsDGV.RowHeadersVisible = false;
            PartsDGV.AllowUserToAddRows = false;
            PartsDGV.AllowUserToResizeRows = false;
            PartsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AddPartButton_Click(object sender, EventArgs e)
        {
            string sqlCommand = "INSERT INTO SERVICEPART (SERVICEPART_ID, SERVICE_ID, PART_ID) VALUES (SEPTPK.nextval,:id,:p_id)";
            using (PartForm partForm = new PartForm())
            {
                if (partForm.ShowDialog(this) == DialogResult.OK)
                {
                    long partId = partForm.SelectedPartId;
                    using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                    {
                        conn.Open();
                        using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                        {
                            cmd.Parameters.Add("id", OracleDbType.Int64).Value = serviceIdReceived;
                            cmd.Parameters.Add(":p_id", OracleDbType.Int64).Value = partId;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    FillPartListService(serviceIdReceived);
                }
            }
        }

        private void ChangeCustomerButton_Click(object sender, EventArgs e)
        {
            using (CustomerForm customerForm = new CustomerForm())
            {
                if (customerForm.ShowDialog(this) == DialogResult.OK)
                {
                    long newCustomerId = customerForm.SelectedCustomerId;
                    if (newCustomerId <= 0)
                    {
                        MessageBox.Show("No customer selected.");
                        return;
                    }

                    selectedCustomerId = newCustomerId;
                    FillCustomerFieldsById(selectedCustomerId);
                }
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId <= 0)
            {
                MessageBox.Show("Select a customer.");
                return;
            }

            if (selectedDeviceId <= 0)
            {
                MessageBox.Show("Select a device.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ProblemDescriptionTextBox.Text))
            {
                MessageBox.Show("Problem description is required.");
                return;
            }

            decimal priceValue = 0m;
            if (!string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                bool ok = decimal.TryParse(PriceTextBox.Text.Trim(), out priceValue);
                if (!ok)
                {
                    MessageBox.Show("Invalid price.");
                    return;
                }
            }

            long completedValue = CompletedRadio.Checked ? 1L : 0L;

            DateTime? endDateToSave = null;
            if (completedValue == 1L)
            {
                if (string.IsNullOrWhiteSpace(FinishDateTextBox.Text))
                    endDateToSave = DateTime.Now;
                else
                {
                    DateTime parsed;
                    bool okDate = DateTime.TryParse(FinishDateTextBox.Text.Trim(), out parsed);
                    if (okDate) endDateToSave = parsed;
                }
            }
            else
            {
                endDateToSave = null;
            }

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(@"
                    UPDATE SERVICE
                    SET
                        CUSTOMER_ID = :customerId,
                        DEVICE_ID = :deviceId,
                        PROBLEM_DESCRIPTION = :problem,
                        FIX_DESCRIPTION = :fix,
                        PRICE = :price,
                        COMPLETED = :completed,
                        END_DATE = :endDate
                    WHERE SERVICE_ID = :serviceId
                ", conn))
                {
                    cmd.Parameters.Add("customerId", OracleDbType.Int64).Value = selectedCustomerId;
                    cmd.Parameters.Add("deviceId", OracleDbType.Int64).Value = selectedDeviceId;
                    cmd.Parameters.Add("problem", OracleDbType.Varchar2).Value = ProblemDescriptionTextBox.Text.Trim();

                    if (string.IsNullOrWhiteSpace(FixDescriptionTextBox.Text))
                        cmd.Parameters.Add("fix", OracleDbType.Varchar2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("fix", OracleDbType.Varchar2).Value = FixDescriptionTextBox.Text.Trim();

                    cmd.Parameters.Add("price", OracleDbType.Decimal).Value = priceValue;
                    cmd.Parameters.Add("completed", OracleDbType.Int64).Value = completedValue;

                    if (endDateToSave.HasValue)
                        cmd.Parameters.Add("endDate", OracleDbType.Date).Value = endDateToSave.Value;
                    else
                        cmd.Parameters.Add("endDate", OracleDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.Add("serviceId", OracleDbType.Int64).Value = serviceIdReceived;

                    cmd.ExecuteNonQuery();
                }
            }
            ActionLogger.LogUserAction("Service", "Update", UserAuthenticationForm.ActiveUserId, serviceIdReceived);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RemovePartButton_Click(object sender, EventArgs e)
        {
            if (PartsDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a part to remove.");
                return;
            }

            long servicePartId = Convert.ToInt64(PartsDGV.SelectedRows[0].Cells["SERVICEPART_ID"].Value);

            DialogResult dr = MessageBox.Show(
                "Remove selected part from this service?",
                "Confirm",
                MessageBoxButtons.YesNo);

            if (dr != DialogResult.Yes)
                return;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(
                    "DELETE FROM SERVICEPART WHERE SERVICEPART_ID = :spid", conn))
                {
                    cmd.Parameters.Add("spid", OracleDbType.Int64).Value = servicePartId;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        MessageBox.Show("Nothing was removed (record not found).");
                        return;
                    }
                }
            }
            ActionLogger.LogUserAction("Service", "Remove part", UserAuthenticationForm.ActiveUserId, serviceIdReceived);

            FillPartListService(serviceIdReceived);
        }

    }
}
