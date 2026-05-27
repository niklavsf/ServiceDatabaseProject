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
using ServiceDatabaseProject;

namespace ServiceDatabaseProject
{
    public partial class AddNewServiceForm : Form
    {

        private long selectedDeviceId = -1;
        private long selectedCustomerId = -1;

        public AddNewServiceForm()
        {
            InitializeComponent();
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(
                    "SELECT NVL(MAX(SERVICE_ID), 0) + 1, SYSDATE FROM SERVICE", conn))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long nextServiceId = Convert.ToInt64(reader.GetValue(0));
                            DateTime sysDate = Convert.ToDateTime(reader.GetValue(1));

                            ServiceIdTextBox.Text = nextServiceId.ToString();
                            StartDateTextBox.Text = sysDate.ToString("dd.MM.yyyy");
                        }
                    }
                }
            }
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private long GetNextServiceId(OracleConnection conn)
        {
            using OracleCommand cmd = new OracleCommand("SELECT NVL(MAX(SERVICE_ID), 0) + 1 FROM SERVICE", conn);

            return Convert.ToInt64(cmd.ExecuteScalar());
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (selectedDeviceId == -1)
            {
                MessageBox.Show("Pick a device first.");
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
                bool ok = decimal.TryParse(PriceTextBox.Text, out priceValue);
                if (!ok)
                {
                    MessageBox.Show("Invalid price.");
                    return;
                }
            }

            if (selectedCustomerId == null)
            {
                MessageBox.Show("Pick a customer first.");
                return;
            }

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                long serviceId = GetNextServiceId(conn);

                string sql = @"
                    INSERT INTO SERVICE
                    (
                        SERVICE_ID,
                        CUSTOMER_ID,
                        PROBLEM_DESCRIPTION,
                        PRICE,
                        COMPLETED,
                        DEVICE_ID,
                        START_DATE,
                        END_DATE
                        
                    )
                    VALUES
                    (
                        :serviceId,
                        :customerId,
                        :problem,
                        :price,
                        :completed,
                        :deviceId,
                        :startDate,
                        :endDate
                        
                    )";

                //SPECIALIST_ID
                //:specialistId

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("serviceId", OracleDbType.Int64).Value = serviceId;
                    cmd.Parameters.Add("customerId", OracleDbType.Int64).Value = selectedCustomerId;

                    cmd.Parameters.Add("problem", OracleDbType.Varchar2).Value = ProblemDescriptionTextBox.Text;

                    cmd.Parameters.Add("price", OracleDbType.Decimal).Value = priceValue;
                    cmd.Parameters.Add("completed", OracleDbType.Int64).Value = 0;
                    cmd.Parameters.Add("deviceId", OracleDbType.Int64).Value = selectedDeviceId;

                    cmd.Parameters.Add("startDate", OracleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("endDate", OracleDbType.Date).Value = DBNull.Value;

                    //cmd.Parameters.Add("specialistId", OracleDbType.Int64).Value = DBNull.Value; 

                    cmd.ExecuteNonQuery();
                }
                ActionLogger.LogUserAction("Service", "Add", UserAuthenticationForm.ActiveUserId, serviceId);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        private void ChangeDeviceButton_Click(object sender, EventArgs e)
        {
            DeviceAddForm picker = new DeviceAddForm();

            if (picker.ShowDialog() == DialogResult.OK)
            {
                selectedDeviceId = Convert.ToInt32(picker.SelectedDeviceId);
                FillDeviceFieldsByDeviceId(selectedDeviceId);
            }
        }

        private void FillDeviceFieldsByDeviceId(long deviceId)
        {
            // DEVICE row
            DataRow deviceRow = HomeScreen.deviceDataTable.Rows.Find(deviceId);
            if (deviceRow == null)
            {
                SerialNumberTextBox.Text = "";
                ManufacturerTextBox.Text = "";
                ModelSeriesTextBox.Text = "";
                ModelTextBox.Text = "";
                MessageBox.Show("Device not found in memory. Refresh the main screen data first.");
                return;
            }

            SerialNumberTextBox.Text = deviceRow["SERIAL_NUMBER"] == DBNull.Value ? "" : deviceRow["SERIAL_NUMBER"].ToString();

            // MODEL row
            object modelIdObj = deviceRow["MODEL_ID"];
            if (modelIdObj == null || modelIdObj == DBNull.Value)
            {
                ManufacturerTextBox.Text = "";
                ModelSeriesTextBox.Text = "";
                ModelTextBox.Text = "";
                return;
            }

            long modelId = Convert.ToInt64(modelIdObj);
            DataRow modelRow = HomeScreen.modelDataTable.Rows.Find(modelId);

            ModelTextBox.Text = modelRow == null || modelRow["MODEL_NAME"] == DBNull.Value ? "" : modelRow["MODEL_NAME"].ToString();

            // MODELSERIES row
            if (modelRow == null) return;

            object seriesIdObj = modelRow["MODELSERIES_ID"];
            if (seriesIdObj == null || seriesIdObj == DBNull.Value)
                return;

            long seriesId = Convert.ToInt64(seriesIdObj);
            DataRow seriesRow = HomeScreen.modelseriesDataTable.Rows.Find(seriesId);

            ModelSeriesTextBox.Text = seriesRow == null || seriesRow["MODELSERIES_NAME"] == DBNull.Value ? "" : seriesRow["MODELSERIES_NAME"].ToString();

            // MANUFACTURER row
            if (seriesRow == null) return;

            object manuIdObj = seriesRow["MANUFACTURER_ID"];
            if (manuIdObj == null || manuIdObj == DBNull.Value)
                return;

            long manuId = Convert.ToInt64(manuIdObj);
            DataRow manuRow = HomeScreen.manufacturerDataTable.Rows.Find(manuId);

            ManufacturerTextBox.Text = manuRow == null || manuRow["NAME"] == DBNull.Value ? "" : manuRow["NAME"].ToString();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCustomerFieldsByCustomerId(long customerId)
        {
            DataRow row = HomeScreen.customerDataTable.Rows.Find(customerId);
            if (row == null)
            {
                NameTextBox.Text = "";
                SurnameTextBox.Text = "";
                PhoneNumberTextBox.Text = "";
                EmailTextBox.Text = "";
                MessageBox.Show("Customer not found in memory. Refresh main screen.");
                return;
            }

            NameTextBox.Text = row["CUSTOMER_NAME"] == DBNull.Value ? "" : row["CUSTOMER_NAME"].ToString();
            SurnameTextBox.Text = row["CUSTOMER_SURNAME"] == DBNull.Value ? "" : row["CUSTOMER_SURNAME"].ToString();
            PhoneNumberTextBox.Text = row["TELEPHONE_NUMBER"] == DBNull.Value ? "" : row["TELEPHONE_NUMBER"].ToString();
            EmailTextBox.Text = row["EMAIL"] == DBNull.Value ? "" : row["EMAIL"].ToString();
        }

        private void ChangeCustomerButton_Click(object sender, EventArgs e)
        {
            CustomerForm f = new CustomerForm(); // picker mode
            DialogResult result = f.ShowDialog(this);

            if (result != DialogResult.OK || f.SelectedCustomerId == null)
                return;

            selectedCustomerId = Convert.ToInt64(f.SelectedCustomerId);

            FillCustomerFieldsByCustomerId(selectedCustomerId);
        }
    }
}
