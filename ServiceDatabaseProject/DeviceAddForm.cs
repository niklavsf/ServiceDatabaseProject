using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.DataFormats;


namespace ServiceDatabaseProject
{
    public partial class DeviceAddForm : Form
    {
        private DataTable manufacturerDataTable = new DataTable();
        private DataTable modelseriesDataTable = new DataTable();
        private DataTable modelDataTable = new DataTable();
        private DataTable devicesDataTable = new DataTable();
        public long SelectedDeviceId { get; set; }

        public DeviceAddForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            DeviceComboBox.Items.AddRange(new[] { "Manufacturer", "Model Series", "Model", "Device" });
            DeviceComboBox.SelectedIndex = 0;
            DeviceTreeView.LabelEdit = true;
            DeviceTreeView.AfterLabelEdit += DeviceTreeView_AfterLabelEdit;
            DeviceAddDGV.CellEndEdit += DeviceAddDGV_CellEndEdit;


            FillData();


        }
        private void FillData()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                manufacturerDataTable.Clear();
                modelseriesDataTable.Clear();
                modelDataTable.Clear();
                devicesDataTable.Clear();

                new OracleDataAdapter("SELECT * FROM MANUFACTURER", conn).Fill(manufacturerDataTable);
                new OracleDataAdapter("SELECT * FROM MODELSERIES", conn).Fill(modelseriesDataTable);
                new OracleDataAdapter("SELECT * FROM MODEL", conn).Fill(modelDataTable);
                string deviceQuery = @"
                SELECT
                    d.DEVICE_ID,
                    d.MODEL_ID,
                    d.SERIAL_NUMBER AS SERIAL_NUMBER,
                    m.MODEL_NAME AS ""Model"",
                    ms.MODELSERIES_NAME AS ""Series"",
                    mf.NAME AS ""Manufacturer""
                FROM DEVICE d
                JOIN MODEL m ON d.MODEL_ID = m.MODEL_ID
                JOIN MODELSERIES ms ON m.MODELSERIES_ID = ms.MODELSERIES_ID
                JOIN MANUFACTURER mf ON ms.MANUFACTURER_ID = mf.MANUFACTURER_ID";

                new OracleDataAdapter(deviceQuery, conn).Fill(devicesDataTable);
            }

            DeviceTreeView.Nodes.Clear();
            BuildTreeView();
        }


        private void BuildTreeView()
        {
            DeviceTreeView.Nodes.Clear();

            foreach (DataRow manu in manufacturerDataTable.Rows)
            {
                TreeNode manuNode = new TreeNode(manu["NAME"].ToString());
                manuNode.Tag = manu["MANUFACTURER_ID"];


                DataRow[] series = modelseriesDataTable.Select($"MANUFACTURER_ID = {manu["MANUFACTURER_ID"]}");
                foreach (DataRow ser in series)
                {
                    TreeNode seriesNode = new TreeNode(ser["MODELSERIES_NAME"].ToString());
                    seriesNode.Tag = ser["MODELSERIES_ID"];


                    DataRow[] models = modelDataTable.Select($"MODELSERIES_ID = {ser["MODELSERIES_ID"]}");
                    foreach (DataRow mod in models)
                    {
                        TreeNode modelNode = new TreeNode(mod["MODEL_NAME"].ToString());
                        modelNode.Tag = mod["MODEL_ID"];
                        seriesNode.Nodes.Add(modelNode);
                    }

                    manuNode.Nodes.Add(seriesNode);
                }

                DeviceTreeView.Nodes.Add(manuNode);
            }

            DeviceTreeView.ExpandAll();
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 2) return;

            int modelId = Convert.ToInt32(e.Node.Tag);

            DataView filtered = new DataView(devicesDataTable);
            filtered.RowFilter = $"MODEL_ID = {modelId}";

            DeviceAddDGV.DataSource = filtered;
            DeviceAddDGV.Columns["DEVICE_ID"].Visible = false;
            DeviceAddDGV.Columns["MODEL_ID"].Visible = false;

            //DeviceAddDGV.CellEndEdit -= DeviceAddDGV_CellEndEdit;
            //DeviceAddDGV.CellEndEdit += DeviceAddDGV_CellEndEdit;

        }

        private void SelectCurrentDeviceAndClose()
        {
            if (DeviceAddDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a device.");
                return;
            }

            object raw = DeviceAddDGV.SelectedRows[0].Cells["DEVICE_ID"].Value;
            if (raw == null || raw == DBNull.Value)
            {
                MessageBox.Show("Invalid device.");
                return;
            }

            SelectedDeviceId = Convert.ToInt64(raw);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AcceptDeviceButton_Click(object sender, EventArgs e)
        {
            if (DeviceAddDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a device.");
                return;
            }



            long selectedDeviceId = Convert.ToInt64(DeviceAddDGV.SelectedRows[0].Cells["DEVICE_ID"].Value);
            SelectedDeviceId = selectedDeviceId;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //private void AddNewItemButton_Click(object sender, EventArgs e)
        //{
        //    string nameToAdd = AddNewTB.Text.Trim();
        //    string type = DeviceComboBox.SelectedItem?.ToString();

        //    if (string.IsNullOrWhiteSpace(nameToAdd) || string.IsNullOrWhiteSpace(type))
        //    {
        //        MessageBox.Show("Enter a value and select a type.");
        //        return;
        //    }

        //    TreeNode selected = DeviceTreeView.SelectedNode;

        //    using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
        //    {
        //        conn.Open();

        //        if (type == "Manufacturer")
        //        {
        //            string query = "INSERT INTO MANUFACTURER (MANUFACTURER_ID, NAME) VALUES (MRPK.NEXTVAL, :name)";
        //            using (var cmd = new OracleCommand(query, conn))
        //            {
        //                cmd.Parameters.Add("name", nameToAdd);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        else if (type == "Model Series")
        //        {
        //            if (selected == null || selected.Level != 0)
        //            {
        //                MessageBox.Show("Select a manufacturer node first.");
        //                return;
        //            }

        //            int manufacturerId = Convert.ToInt32(selected.Tag);
        //            string query = "INSERT INTO MODELSERIES (MODELSERIES_ID, MODELSERIES_NAME, MANUFACTURER_ID) VALUES (MLSSPK.NEXTVAL, :name, :manuId)";
        //            using (var cmd = new OracleCommand(query, conn))
        //            {
        //                cmd.Parameters.Add("name", nameToAdd);
        //                cmd.Parameters.Add("manuId", manufacturerId);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        else if (type == "Model")
        //        {
        //            if (selected == null || selected.Level != 1)
        //            {
        //                MessageBox.Show("Select a model series node first.");
        //                return;
        //            }

        //            int seriesId = Convert.ToInt32(selected.Tag);
        //            string query = "INSERT INTO MODEL (MODEL_ID, MODEL_NAME, MODELSERIES_ID) VALUES (MLPK.NEXTVAL, :name, :seriesId)";
        //            using (var cmd = new OracleCommand(query, conn))
        //            {
        //                cmd.Parameters.Add("name", nameToAdd);
        //                cmd.Parameters.Add("seriesId", seriesId);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        else if (type == "Device")
        //        {
        //            if (selected == null || selected.Level != 2)
        //            {
        //                MessageBox.Show("Select a model node first.");
        //                return;
        //            }

        //            int modelId = Convert.ToInt32(selected.Tag);
        //            string serial = nameToAdd;

        //            using (var checkCmd = new OracleCommand("SELECT COUNT(*) FROM DEVICE WHERE SERIAL_NUMBER = :serial", conn))
        //            {
        //                checkCmd.Parameters.Add("serial", serial);
        //                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
        //                if (exists > 0)
        //                {
        //                    MessageBox.Show("A device with this serial number already exists.");
        //                    return;
        //                }
        //            }

        //            string insert = "INSERT INTO DEVICE (DEVICE_ID, MODEL_ID, SERIAL_NUMBER) VALUES (DEPK.NEXTVAL, :modelId, :serial)";
        //            using (var cmd = new OracleCommand(insert, conn))
        //            {
        //                cmd.Parameters.Add("modelId", modelId);
        //                cmd.Parameters.Add("serial", serial);
        //                cmd.ExecuteNonQuery();
        //            }

        //            MessageBox.Show("Device added.");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid type selected.");
        //            return;
        //        }

        //        AddNewTB.Clear();
        //        FillData();
        //    }

        //}

        private void AddNewItemButton_Click(object sender, EventArgs e)
        {
            string value = AddNewTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show("Enter a value first.");
                return;
            }

            string type = DeviceComboBox.SelectedItem == null ? "" : DeviceComboBox.SelectedItem.ToString();
            TreeNode selected = DeviceTreeView.SelectedNode;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                if (type == "Manufacturer")
                {
                    InsertManufacturer(conn, value);
                }
                else if (type == "Model Series")
                {
                    if (selected == null || selected.Level != 0)
                    {
                        MessageBox.Show("Select a Manufacturer node first.");
                        return;
                    }

                    int manufacturerId = Convert.ToInt32(selected.Tag);
                    InsertModelSeries(conn, manufacturerId, value);
                }
                else if (type == "Model")
                {
                    if (selected == null || selected.Level != 1)
                    {
                        MessageBox.Show("Select a Model Series node first.");
                        return;
                    }

                    int seriesId = Convert.ToInt32(selected.Tag);
                    InsertModel(conn, seriesId, value);
                }
                else if (type == "Device")
                {
                    if (selected == null || selected.Level != 2)
                    {
                        MessageBox.Show("Select a Model node first (then enter the serial number).");
                        return;
                    }

                    int modelId = Convert.ToInt32(selected.Tag);
                    InsertDevice(conn, modelId, value);
                }
                else
                {
                    MessageBox.Show("Pick what you want to add.");
                    return;
                }
            }

            AddNewTB.Clear();
            FillData();
        }

        private void InsertManufacturer(OracleConnection conn, string name)
        {
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO MANUFACTURER (MANUFACTURER_ID, NAME) VALUES (MRPK.NEXTVAL, :name)", conn))
            {
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name;
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertModelSeries(OracleConnection conn, int manufacturerId, string seriesName)
        {
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO MODELSERIES (MODELSERIES_ID, MODELSERIES_NAME, MANUFACTURER_ID) VALUES (MLSSPK.NEXTVAL, :name, :manuId)", conn))
            {
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = seriesName;
                cmd.Parameters.Add("manuId", OracleDbType.Int32).Value = manufacturerId;
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertModel(OracleConnection conn, int seriesId, string modelName)
        {
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO MODEL (MODEL_ID, MODEL_NAME, MODELSERIES_ID) VALUES (MLPK.NEXTVAL, :name, :seriesId)", conn))
            {
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = modelName;
                cmd.Parameters.Add("seriesId", OracleDbType.Int32).Value = seriesId;
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDevice(OracleConnection conn, int modelId, string serial)
        {
            using (OracleCommand checkCmd = new OracleCommand(
                "SELECT COUNT(*) FROM DEVICE WHERE SERIAL_NUMBER = :serial", conn))
            {
                checkCmd.Parameters.Add("serial", OracleDbType.Varchar2).Value = serial;
                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (exists > 0)
                {
                    MessageBox.Show("A device with this serial number already exists.");
                    return;
                }
            }

            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO DEVICE (DEVICE_ID, MODEL_ID, SERIAL_NUMBER) VALUES (DEPK.NEXTVAL, :modelId, :serial)", conn))
            {
                cmd.Parameters.Add("modelId", OracleDbType.Int32).Value = modelId;
                cmd.Parameters.Add("serial", OracleDbType.Varchar2).Value = serial;
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Device added.");
        }


        private void DeviceTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || string.IsNullOrWhiteSpace(e.Label) || e.Label == e.Node.Text)
            {
                e.CancelEdit = true;
                return;
            }

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                string updateQuery = "";
                string columnName = "";
                string tableName = "";

                if (e.Node.Level == 0)
                {
                    tableName = "MANUFACTURER";
                    columnName = "NAME";
                }
                else if (e.Node.Level == 1)
                {
                    tableName = "MODELSERIES";
                    columnName = "MODELSERIES_NAME";
                }
                else if (e.Node.Level == 2)
                {
                    tableName = "MODEL";
                    columnName = "MODEL_NAME";
                }
                else
                {
                    e.CancelEdit = true;
                    return;
                }

                updateQuery = $"UPDATE {tableName} SET {columnName} = :newName WHERE {tableName}_ID = :id";

                using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
                {
                    cmd.Parameters.Add("newName", e.Label);
                    cmd.Parameters.Add("id", Convert.ToInt32(e.Node.Tag));
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Name updated.");
            FillData();
        }

        private void DeviceTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level <= 2)
            {
                DeviceTreeView.SelectedNode = e.Node;
                e.Node.BeginEdit();
            }
        }

        private void DeviceAddDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var grid = DeviceAddDGV;
            var columnName = grid.Columns[e.ColumnIndex].Name;

            if (columnName != "SERIAL_NUMBER") return;

            var row = grid.Rows[e.RowIndex];
            int deviceId = Convert.ToInt32(row.Cells["DEVICE_ID"].Value);
            string newSerial = row.Cells["SERIAL_NUMBER"].Value?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(newSerial))
            {
                MessageBox.Show("Serial number cannot be empty.");
                FillData();
                return;
            }

            try
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();

                    using (OracleCommand checkCmd = new OracleCommand(
                        "SELECT COUNT(*) FROM DEVICE WHERE SERIAL_NUMBER = :serial AND DEVICE_ID != :id", conn))
                    {
                        checkCmd.Parameters.Add("serial", newSerial);
                        checkCmd.Parameters.Add("id", deviceId);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (exists > 0)
                        {
                            MessageBox.Show("A device with this serial number already exists.");
                            FillData();
                            return;
                        }
                    }

                    using (OracleCommand updateCmd = new OracleCommand(
                        "UPDATE DEVICE SET SERIAL_NUMBER = :serial WHERE DEVICE_ID = :id", conn))
                    {
                        updateCmd.Parameters.Add("serial", newSerial);
                        updateCmd.Parameters.Add("id", deviceId);
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Serial number updated.");
                        }
                        else
                        {
                            MessageBox.Show("Update failed (no matching device found).");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating device:\n" + ex.Message);
            }

            FillData();
        }

        private void DeviceAddDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            SelectCurrentDeviceAndClose();
        }

        private void DeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = DeviceComboBox.SelectedItem == null ? "" : DeviceComboBox.SelectedItem.ToString();

            if (type == "Manufacturer")
                TextboxLabel.Text = "Manufacturer name";
            else if (type == "Model Series")
                TextboxLabel.Text = "Model series name";
            else if (type == "Model")
                TextboxLabel.Text = "Model name";
            else if (type == "Device")
                TextboxLabel.Text = "Device serial number";
            else
                TextboxLabel.Text = "Value";
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            TreeNode node = DeviceTreeView.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("Select something in the tree to delete.");
                return;
            }

            string name = node.Text;
            DialogResult dr = MessageBox.Show(
                "Delete '" + name + "' and all related items?",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes)
                return;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                List<long> blockingServices = GetBlockingServiceIds(conn, node);
                if (blockingServices.Count > 0)
                {
                    string preview = string.Join(", ", blockingServices.Take(15));
                    string extra = blockingServices.Count > 15 ? " ..." : "";

                    MessageBox.Show(
                        "Cannot delete because these services use one or more devices in this branch:\n" +
                        preview + extra + "\n\nDelete the service(s) first.",
                        "Delete blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (node.Level == 0)
                {
                    int manufacturerId = Convert.ToInt32(node.Tag);
                    DeleteManufacturerCascade(conn, manufacturerId);
                }
                else if (node.Level == 1)
                {
                    int seriesId = Convert.ToInt32(node.Tag);
                    DeleteModelSeriesCascade(conn, seriesId);
                }
                else if (node.Level == 2)
                {
                    int modelId = Convert.ToInt32(node.Tag);
                    DeleteModelCascade(conn, modelId);
                }
                else
                {
                    MessageBox.Show("Unsupported node type.");
                    return;
                }
            }

            FillData();
        }

        private List<long> GetBlockingServiceIds(OracleConnection conn, TreeNode node)
        {
            List<long> serviceIds = new List<long>();

            if (node.Level == 2)
            {
                int modelId = Convert.ToInt32(node.Tag);

                using (OracleCommand cmd = new OracleCommand(@"
                    SELECT DISTINCT s.SERVICE_ID
                    FROM SERVICE s
                    JOIN DEVICE d ON d.DEVICE_ID = s.DEVICE_ID
                    WHERE d.MODEL_ID = :modelId
                ", conn))
                {
                    cmd.Parameters.Add("modelId", OracleDbType.Int32).Value = modelId;

                    using (OracleDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            serviceIds.Add(Convert.ToInt64(r["SERVICE_ID"]));
                    }
                }

                return serviceIds;
            }

            if (node.Level == 1)
            {
                int seriesId = Convert.ToInt32(node.Tag);

                using (OracleCommand cmd = new OracleCommand(@"
            SELECT DISTINCT s.SERVICE_ID
            FROM SERVICE s
            JOIN DEVICE d ON d.DEVICE_ID = s.DEVICE_ID
            JOIN MODEL m ON m.MODEL_ID = d.MODEL_ID
            WHERE m.MODELSERIES_ID = :seriesId
        ", conn))
                {
                    cmd.Parameters.Add("seriesId", OracleDbType.Int32).Value = seriesId;

                    using (OracleDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            serviceIds.Add(Convert.ToInt64(r["SERVICE_ID"]));
                    }
                }

                return serviceIds;
            }

            if (node.Level == 0)
            {
                int manufacturerId = Convert.ToInt32(node.Tag);

                using (OracleCommand cmd = new OracleCommand(@"
                    SELECT DISTINCT s.SERVICE_ID
                    FROM SERVICE s
                    JOIN DEVICE d ON d.DEVICE_ID = s.DEVICE_ID
                    JOIN MODEL m ON m.MODEL_ID = d.MODEL_ID
                    JOIN MODELSERIES ms ON ms.MODELSERIES_ID = m.MODELSERIES_ID
                    WHERE ms.MANUFACTURER_ID = :manuId
                ", conn))
                {
                    cmd.Parameters.Add("manuId", OracleDbType.Int32).Value = manufacturerId;

                    using (OracleDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            serviceIds.Add(Convert.ToInt64(r["SERVICE_ID"]));
                    }
                }

                return serviceIds;
            }

            return serviceIds;
        }


        private void DeleteManufacturerCascade(OracleConnection conn, int manufacturerId)
        {
            using (OracleCommand cmd = new OracleCommand(@"
                DELETE FROM DEVICE
                WHERE MODEL_ID IN (
                    SELECT m.MODEL_ID
                    FROM MODEL m
                    JOIN MODELSERIES ms ON ms.MODELSERIES_ID = m.MODELSERIES_ID
                    WHERE ms.MANUFACTURER_ID = :mid
                )", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = manufacturerId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(@"
                DELETE FROM MODEL
                WHERE MODELSERIES_ID IN (
                    SELECT MODELSERIES_ID FROM MODELSERIES WHERE MANUFACTURER_ID = :mid
                )", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = manufacturerId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM MODELSERIES WHERE MANUFACTURER_ID = :mid", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = manufacturerId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM MANUFACTURER WHERE MANUFACTURER_ID = :mid", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = manufacturerId;
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteModelSeriesCascade(OracleConnection conn, int seriesId)
        {
            using (OracleCommand cmd = new OracleCommand(@"
        DELETE FROM DEVICE
        WHERE MODEL_ID IN (SELECT MODEL_ID FROM MODEL WHERE MODELSERIES_ID = :sid)", conn))
            {
                cmd.Parameters.Add("sid", OracleDbType.Int32).Value = seriesId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM MODEL WHERE MODELSERIES_ID = :sid", conn))
            {
                cmd.Parameters.Add("sid", OracleDbType.Int32).Value = seriesId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM MODELSERIES WHERE MODELSERIES_ID = :sid", conn))
            {
                cmd.Parameters.Add("sid", OracleDbType.Int32).Value = seriesId;
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteModelCascade(OracleConnection conn, int modelId)
        {
            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM DEVICE WHERE MODEL_ID = :mid", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = modelId;
                cmd.ExecuteNonQuery();
            }

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM MODEL WHERE MODEL_ID = :mid", conn))
            {
                cmd.Parameters.Add("mid", OracleDbType.Int32).Value = modelId;
                cmd.ExecuteNonQuery();
            }
        }


    }
}
