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
using static System.Windows.Forms.DataFormats;

namespace ServiceDatabaseProject
{
    public partial class AdvancedAddForm : Form
    {
        private DataTable manufacturerTable = new DataTable();
        private DataTable modelseriesTable = new DataTable();
        private DataTable modelTable = new DataTable();
        private DataTable specialistTable = new DataTable();

        public AdvancedAddForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadComboBoxData();
            LoadSpecialists();

            ManufacturerCB.SelectedIndexChanged += ManufacturerCB_SelectedIndexChanged;
            ModelseriesCB.SelectedIndexChanged += ModelseriesCB_SelectedIndexChanged;

            SpecialistTV.LabelEdit = true;
            SubLevel(null, null);
        }

        private void LoadComboBoxData()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                new OracleDataAdapter("SELECT * FROM MANUFACTURER", conn).Fill(manufacturerTable);
                new OracleDataAdapter("SELECT * FROM MODELSERIES", conn).Fill(modelseriesTable);
                new OracleDataAdapter("SELECT * FROM MODEL", conn).Fill(modelTable);
            }

            ManufacturerCB.DisplayMember = "NAME";
            ManufacturerCB.ValueMember = "MANUFACTURER_ID";
            ManufacturerCB.DataSource = manufacturerTable;
        }

        private void LoadSpecialists()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();
                OracleDataAdapter adapter = new OracleDataAdapter("SELECT SPECIALIST_ID, NAME, SURNAME, OVERSEER_ID FROM SPECIALIST", conn);
                adapter.Fill(specialistTable);
            }
        }

        public void SubLevel(int? parentId, TreeNode parentNode)
        {
            string filter = parentId == null ? "OVERSEER_ID IS NULL" : $"OVERSEER_ID = {parentId}";
            DataRow[] rows = specialistTable.Select(filter);

            foreach (DataRow row in rows)
            {
                TreeNode tn = new TreeNode($"{row["NAME"]} {row["SURNAME"]}")
                {
                    Name = row["SPECIALIST_ID"].ToString(),
                    Tag = Convert.ToInt32(row["SPECIALIST_ID"])
                };

                if (parentNode == null)
                    SpecialistTV.Nodes.Add(tn);
                else
                    parentNode.Nodes.Add(tn);

                SubLevel(Convert.ToInt32(row["SPECIALIST_ID"]), tn);
            }
        }

        public bool FormValidation()
        {
            int errorCount = 0;

            if (string.IsNullOrWhiteSpace(CustomerNameTB.Text))
            {
                MessageBox.Show("Customer name is required.");
                errorCount++;
            }

            if (string.IsNullOrWhiteSpace(CustomerSurnameTB.Text))
            {
                MessageBox.Show("Customer surname is required.");
                errorCount++;
            }

            if (string.IsNullOrWhiteSpace(CustomerEmailTB.Text))
            {
                MessageBox.Show("Customer email is required.");
                errorCount++;
            }

            if (string.IsNullOrWhiteSpace(DeviceSerialNumberTB.Text))
            {
                MessageBox.Show("Serial number is required.");
                errorCount++;
            }

            if (ManufacturerCB.SelectedItem == null)
            {
                MessageBox.Show("Please select a manufacturer.");
                errorCount++;
            }

            if (ModelseriesCB.SelectedItem == null)
            {
                MessageBox.Show("Please select a model series.");
                errorCount++;
            }

            if (ModelCB.SelectedItem == null)
            {
                MessageBox.Show("Please select a model.");
                errorCount++;
            }

            if (SpecialistTV.SelectedNode == null || SpecialistTV.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a specialist from the tree.");
                errorCount++;
            }

            if (string.IsNullOrWhiteSpace(PriceTB.Text))
            {
                MessageBox.Show("Service price is required.");
                errorCount++;
            }

            if (errorCount > 0)
            {
                MessageBox.Show("Please fix the highlighted errors before continuing.");
                return false;
            }

            return true;
        }

        private void SubmitAdvancedAddButton_Click(object sender, EventArgs e)
        {
            if (!FormValidation())
                return;

            string customerName = CustomerNameTB.Text.Trim();
            string customerSurname = CustomerSurnameTB.Text.Trim();
            string customerEmail = CustomerEmailTB.Text.Trim();
            string serialNumber = DeviceSerialNumberTB.Text.Trim();

            decimal servicePrice = 0;

            if (!decimal.TryParse(PriceTB.Text, out servicePrice))
            {
                MessageBox.Show("Invalid price format.");
                return;
            }

            int modelId = Convert.ToInt32(ModelCB.SelectedValue);
            int specialistId = Convert.ToInt32(SpecialistTV.SelectedNode.Tag);

            try
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();
                    using (OracleTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            int customerId = GetNextId(conn, "CPK");
                            int deviceId = GetNextId(conn, "DEPK");
                            int serviceId = GetNextId(conn, "SPPK");

                            using (OracleCommand cmd = new OracleCommand("INSERT INTO CUSTOMER (CUSTOMER_ID, CUSTOMER_NAME, CUSTOMER_SURNAME, EMAIL) VALUES (:id, :name, :surname, :email)", conn))
                            {
                                cmd.Transaction = tran;
                                cmd.Parameters.Add("id", customerId);
                                cmd.Parameters.Add("name", customerName);
                                cmd.Parameters.Add("surname", customerSurname);
                                cmd.Parameters.Add("email", customerEmail);
                                cmd.ExecuteNonQuery();
                            }

                            using (OracleCommand cmd = new OracleCommand("INSERT INTO DEVICE (DEVICE_ID, SERIAL_NUMBER, MODEL_ID) VALUES (:id, :serial, :model)", conn))
                            {
                                cmd.Transaction = tran;
                                cmd.Parameters.Add("id", deviceId);
                                cmd.Parameters.Add("serial", serialNumber);
                                cmd.Parameters.Add("model", modelId);
                                cmd.ExecuteNonQuery();
                            }

                            using (OracleCommand cmd = new OracleCommand("INSERT INTO SERVICE (SERVICE_ID, CUSTOMER_ID, DEVICE_ID, SPECIALIST_ID, PRICE) VALUES (:id, :cust, :dev, :spec, :price)", conn))
                            {
                                cmd.Transaction = tran;
                                cmd.Parameters.Add("id", serviceId);
                                cmd.Parameters.Add("cust", customerId);
                                cmd.Parameters.Add("dev", deviceId);
                                cmd.Parameters.Add("spec", specialistId);
                                cmd.Parameters.Add("price", servicePrice);
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                            MessageBox.Show("Insert successful.");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show("Insert failed:\n" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error:\n" + ex.Message);
            }
        }

        private int GetNextId(OracleConnection conn, string sequenceName)
        {
            using (var cmd = new OracleCommand($"SELECT {sequenceName}.NEXTVAL FROM DUAL", conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void CancelAdvancedAddButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManufacturerCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManufacturerCB.SelectedValue == null)
                return;

            int manufacturerId = Convert.ToInt32(ManufacturerCB.SelectedValue);

            DataView filteredSeries = new DataView(modelseriesTable);
            filteredSeries.RowFilter = $"MANUFACTURER_ID = {manufacturerId}";

            ModelseriesCB.DisplayMember = "MODELSERIES_NAME";
            ModelseriesCB.ValueMember = "MODELSERIES_ID";
            ModelseriesCB.DataSource = filteredSeries;

            ModelCB.DataSource = null;
        }

        private void ModelseriesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModelseriesCB.SelectedValue == null)
                return;

            int modelseriesId = Convert.ToInt32(ModelseriesCB.SelectedValue);

            DataView filteredModels = new DataView(modelTable);
            filteredModels.RowFilter = $"MODELSERIES_ID = {modelseriesId}";

            ModelCB.DisplayMember = "MODEL_NAME";
            ModelCB.ValueMember = "MODEL_ID";
            ModelCB.DataSource = filteredModels;
        }
    }
}
