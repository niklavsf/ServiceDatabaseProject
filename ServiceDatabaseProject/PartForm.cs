using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ServiceDatabaseProject
{
    public partial class PartForm : Form
    {
        private DataTable partsTable = new DataTable();
        private DataTable manufacturersTable = new DataTable();
        private DataTable componentsTable = new DataTable();
        static Stack<long> UndoListPart = new Stack<long>();

        private bool isBinding = false;
        public long SelectedPartId;
        public PartForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            LoadPartStatuses();
            LoadManufacturers();
            LoadComponents();
            LoadParts();
            CriteriaComboBox.Items.Clear();

            CriteriaComboBox.Items.Add("Part number");
            CriteriaComboBox.Items.Add("Part name");
            CriteriaComboBox.Items.Add("Manufacturer");
            CriteriaComboBox.Items.Add("Component");

            CriteriaComboBox.SelectedIndex = 0;

            UseExistingManufacturerCheckBox.Checked = true;
        }


        private void LoadManufacturers()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                manufacturersTable.Clear();

                using (OracleDataAdapter adapter =
                       new OracleDataAdapter("SELECT MANUFACTURER_ID, NAME FROM MANUFACTURER WHERE IS_ACTIVE=1 ORDER BY NAME", conn))
                {
                    adapter.Fill(manufacturersTable);
                }
            }

            ManufacturerComboBox.DataSource = manufacturersTable;
            ManufacturerComboBox.DisplayMember = "NAME";
            ManufacturerComboBox.ValueMember = "MANUFACTURER_ID";

            if (manufacturersTable.Rows.Count > 0)
                ManufacturerComboBox.SelectedIndex = 0;
            else
                ManufacturerComboBox.SelectedIndex = -1;
        }

        private void LoadComponents()
        {
            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                componentsTable.Clear();

                using (OracleDataAdapter adapter =
                       new OracleDataAdapter("SELECT COMPONENT_ID, COMPONENT_NAME FROM COMPONENT ORDER BY COMPONENT_NAME", conn))
                {
                    adapter.Fill(componentsTable);
                }
            }

            ComponentComboBox.DataSource = componentsTable;
            ComponentComboBox.DisplayMember = "COMPONENT_NAME";
            ComponentComboBox.ValueMember = "COMPONENT_ID";

            if (componentsTable.Rows.Count > 0)
                ComponentComboBox.SelectedIndex = 0;
            else
                ComponentComboBox.SelectedIndex = -1;
        }

        private void LoadParts()
        {
            isBinding = true;

            using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
            {
                conn.Open();

                partsTable.Clear();

                string sql = @"
                    SELECT
                        p.PART_ID, 
                        p.PART_NUMBER,
                        p.PART_NAME,
                        p.COMPONENT_ID,
                        c.COMPONENT_NAME AS COMPONENT_TEXT,
                        p.PRICE,
                        p.QUANTITY,
                        p.PARTSTATUS_ID,
                        ps.STATUS AS PARTSTATUS_TEXT,
                        p.MANUFACTURER_ID,
                        p.MANUFACTURER_NAME,
                        p.IS_ACTIVE,
                        mf.NAME AS MANUFACTURER_FK_NAME,
                        CASE
                            WHEN p.MANUFACTURER_ID IS NOT NULL THEN mf.NAME
                            ELSE p.MANUFACTURER_NAME
                        END AS MANUFACTURER_DISPLAY
                    FROM PART p
                    LEFT JOIN MANUFACTURER mf ON p.MANUFACTURER_ID = mf.MANUFACTURER_ID
                    LEFT JOIN PARTSTATUS ps ON p.PARTSTATUS_ID = ps.PARTSTATUS_ID
                    LEFT JOIN COMPONENT c ON p.COMPONENT_ID = c.COMPONENT_ID
                    WHERE p.IS_ACTIVE=1
                    ORDER BY p.PART_ID";

                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, conn))
                {
                    adapter.Fill(partsTable);
                }
            }

            PartsDGV.DataSource = partsTable;

            PartsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            PartsDGV.MultiSelect = false;
            PartsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PartsDGV.ReadOnly = true;
            PartsDGV.AllowUserToAddRows = false;
            PartsDGV.AllowUserToDeleteRows = false;
            PartsDGV.AllowUserToResizeRows = false;

            if (PartsDGV.Columns.Contains("MANUFACTURER_ID")) PartsDGV.Columns["MANUFACTURER_ID"].Visible = false;
            if (PartsDGV.Columns.Contains("MANUFACTURER_NAME")) PartsDGV.Columns["MANUFACTURER_NAME"].Visible = false;
            if (PartsDGV.Columns.Contains("MANUFACTURER_FK_NAME")) PartsDGV.Columns["MANUFACTURER_FK_NAME"].Visible = false;
            if (PartsDGV.Columns.Contains("PARTSTATUS_ID")) PartsDGV.Columns["PARTSTATUS_ID"].Visible = false;
            if (PartsDGV.Columns.Contains("COMPONENT_ID")) PartsDGV.Columns["COMPONENT_ID"].Visible = false;
            if (PartsDGV.Columns.Contains("IS_ACTIVE")) PartsDGV.Columns["IS_ACTIVE"].Visible = false;

            if (PartsDGV.Columns.Contains("PART_ID")) PartsDGV.Columns["PART_ID"].HeaderText = "ID";
            if (PartsDGV.Columns.Contains("PART_NUMBER")) PartsDGV.Columns["PART_NUMBER"].HeaderText = "Part number";
            if (PartsDGV.Columns.Contains("PART_NAME")) PartsDGV.Columns["PART_NAME"].HeaderText = "Part name";
            if (PartsDGV.Columns.Contains("COMPONENT_TEXT")) PartsDGV.Columns["COMPONENT_TEXT"].HeaderText = "Component";
            if (PartsDGV.Columns.Contains("PRICE")) PartsDGV.Columns["PRICE"].HeaderText = "Price";
            if (PartsDGV.Columns.Contains("QUANTITY")) PartsDGV.Columns["QUANTITY"].HeaderText = "Qty";
            if (PartsDGV.Columns.Contains("PARTSTATUS_TEXT")) PartsDGV.Columns["PARTSTATUS_TEXT"].HeaderText = "Status";
            if (PartsDGV.Columns.Contains("MANUFACTURER_DISPLAY")) PartsDGV.Columns["MANUFACTURER_DISPLAY"].HeaderText = "Manufacturer";


            if (PartsDGV.Columns.Contains("PART_ID"))
                PartsDGV.Columns["PART_ID"].Width = 60;

            if (PartsDGV.Columns.Contains("PRICE"))
                PartsDGV.Columns["PRICE"].Width = 80;

            if (PartsDGV.Columns.Contains("QUANTITY"))
                PartsDGV.Columns["QUANTITY"].Width = 70;

            if (PartsDGV.Columns.Contains("PARTSTATUS_TEXT"))
                PartsDGV.Columns["PARTSTATUS_TEXT"].Width = 110;

            if (PartsDGV.Columns.Contains("PART_NUMBER"))
                PartsDGV.Columns["PART_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (PartsDGV.Columns.Contains("PART_NAME"))
                PartsDGV.Columns["PART_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (PartsDGV.Columns.Contains("MANUFACTURER_DISPLAY"))
                PartsDGV.Columns["MANUFACTURER_DISPLAY"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (PartsDGV.Columns.Contains("COMPONENT_TEXT"))
                PartsDGV.Columns["COMPONENT_TEXT"].Width = 140;

            PartsDGV.Columns["PRICE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PartsDGV.Columns["QUANTITY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            PartsDGV.Columns["PRICE"].DefaultCellStyle.Format = "0.00 €";

            PartsDGV.ClearSelection();
            isBinding = false;
        }

        private void LoadPartStatuses()
        {
            PartStatusComboBox.Items.Clear();
            PartStatusComboBox.Items.Add(new PartStatusItem(1, "In store"));
            PartStatusComboBox.Items.Add(new PartStatusItem(2, "Out of stock"));
            PartStatusComboBox.Items.Add(new PartStatusItem(3, "Ordered"));
            PartStatusComboBox.SelectedIndex = 0;
        }

        private class PartStatusItem
        {
            public int Id { get; private set; }
            public string Text { get; private set; }

            public PartStatusItem(int id, string text)
            {
                Id = id;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private int GetSelectedStatusId()
        {
            if (PartStatusComboBox.SelectedItem == null) return 1;

            PartStatusItem item = PartStatusComboBox.SelectedItem as PartStatusItem;
            if (item != null) return item.Id;

            return 1;
        }

        private void SetStatusCombo(int statusId)
        {
            int i;
            for (i = 0; i < PartStatusComboBox.Items.Count; i++)
            {
                PartStatusItem item = PartStatusComboBox.Items[i] as PartStatusItem;
                if (item != null && item.Id == statusId)
                {
                    PartStatusComboBox.SelectedIndex = i;
                    return;
                }
            }

            PartStatusComboBox.SelectedIndex = 0;
        }

        public void LastDeletionListRetrieve()
        {
            if (UndoListPart.Count == 0)
            {
                MessageBox.Show("Nothing to undo.");
            }
            else
            {
                long partIdRetrieve = UndoListPart.Pop();
                string command = "UPDATE PART SET IS_ACTIVE=1 WHERE PART_ID = :id";
                DatabaseUpdateFormPart(command, partIdRetrieve);
                ActionLogger.LogUserAction("Part", "Restore", UserAuthenticationForm.ActiveUserId, partIdRetrieve);
                ActionLogger.ClearDeactivationTime("PART", partIdRetrieve);
                LoadParts();
            }
        }

        public void DatabaseUpdateFormPart(string oracleCommand, long serviceId)
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


        private bool TryGetSelectedPartId(out long partId)
        {
            partId = 0;

            if (PartsDGV.SelectedRows.Count == 0) return false;

            object idObj = PartsDGV.SelectedRows[0].Cells["PART_ID"].Value;
            if (idObj == null || idObj == DBNull.Value) return false;

            partId = Convert.ToInt64(idObj);
            return true;
        }

        private string SafeToString(object value)
        {
            if (value == null || value == DBNull.Value) return "";
            return value.ToString();
        }

        private int SafeToInt(object value, int fallback)
        {
            if (value == null || value == DBNull.Value) return fallback;
            try { return Convert.ToInt32(value); } catch { return fallback; }
        }

        private void ClearInputs()
        {
            PartNumberTextBox.Text = "";
            PartNameTextBox.Text = "";
            PriceTextBox.Text = "";
            QuantityTextBox.Text = "";

            if (componentsTable.Rows.Count > 0)
                ComponentComboBox.SelectedIndex = 0;
            else
                ComponentComboBox.SelectedIndex = -1;
            UseExistingManufacturerCheckBox.Checked = true;
            if (manufacturersTable.Rows.Count > 0)
                ManufacturerComboBox.SelectedIndex = 0;
            else
                ManufacturerComboBox.SelectedIndex = -1;

            CustomManufacturerTextBox.Text = "";

            if (PartStatusComboBox.Items.Count > 0)
                PartStatusComboBox.SelectedIndex = 0;
        }

        private int GetSelectedComponentId()
        {
            if (ComponentComboBox.SelectedValue == null)
                return 0;

            return Convert.ToInt32(ComponentComboBox.SelectedValue);
        }

        private void UseExistingManufacturerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool useExisting = UseExistingManufacturerCheckBox.Checked;

            ManufacturerComboBox.Enabled = useExisting;
            CustomManufacturerTextBox.Enabled = !useExisting;

            if (useExisting)
            {
                CustomManufacturerTextBox.Text = "";
            }
            else
            {
                ManufacturerComboBox.SelectedIndex = -1;
            }
        }

        private void PartsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (isBinding) return;
            if (PartsDGV.SelectedRows.Count == 0) return;

            DataGridViewRow row = PartsDGV.SelectedRows[0];

            PartNumberTextBox.Text = SafeToString(row.Cells["PART_NUMBER"].Value);
            PartNameTextBox.Text = SafeToString(row.Cells["PART_NAME"].Value);
            PriceTextBox.Text = SafeToString(row.Cells["PRICE"].Value);
            QuantityTextBox.Text = SafeToString(row.Cells["QUANTITY"].Value);

            object compIdObj = row.Cells["COMPONENT_ID"].Value;
            if (compIdObj != null && compIdObj != DBNull.Value)
            {
                ComponentComboBox.SelectedValue = compIdObj;
            }

            int statusId = SafeToInt(row.Cells["PARTSTATUS_ID"].Value, 1);
            SetStatusCombo(statusId);

            object manuIdObj = row.Cells["MANUFACTURER_ID"].Value;

            if (manuIdObj != null && manuIdObj != DBNull.Value)
            {
                UseExistingManufacturerCheckBox.Checked = true;
                CustomManufacturerTextBox.Text = "";

                ManufacturerComboBox.SelectedValue = manuIdObj;
            }
            else
            {
                UseExistingManufacturerCheckBox.Checked = false;
                ManufacturerComboBox.SelectedIndex = -1;
                CustomManufacturerTextBox.Text = SafeToString(row.Cells["MANUFACTURER_NAME"].Value);
            }
        }

        private void AddPartButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PartNumberTextBox.Text))
            {
                MessageBox.Show("Part number is required.");
                return;
            }

            if (ComponentComboBox.SelectedValue == null)
            {
                MessageBox.Show("Select a component.");
                return;
            }

            int componentId = GetSelectedComponentId();

            int quantity;
            if (!int.TryParse(QuantityTextBox.Text.Trim(), out quantity))
            {
                MessageBox.Show("Quantity must be a number.");
                return;
            }

            decimal priceValue = 0m;
            bool hasPrice = false;
            if (!string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                bool okPrice = decimal.TryParse(PriceTextBox.Text.Trim(), out priceValue);
                if (!okPrice)
                {
                    MessageBox.Show("Price must be a number.");
                    return;
                }
                hasPrice = true;
            }

            int partStatusId = GetSelectedStatusId();

            object manufacturerIdValue = DBNull.Value;
            object manufacturerNameValue = DBNull.Value;

            if (UseExistingManufacturerCheckBox.Checked)
            {
                if (ManufacturerComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Select a manufacturer, or uncheck 'Use existing manufacturer' for custom.");
                    return;
                }

                long manuId = Convert.ToInt64(ManufacturerComboBox.SelectedValue);
                manufacturerIdValue = manuId;
                manufacturerNameValue = DBNull.Value;
            }
            else
            {
                string customName = CustomManufacturerTextBox.Text.Trim();
                manufacturerIdValue = DBNull.Value;
                manufacturerNameValue = string.IsNullOrWhiteSpace(customName) ? (object)DBNull.Value : customName;
            }

            long partId;
            bool isUpdate = TryGetSelectedPartId(out partId);

            try
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();

                    if (!isUpdate)
                    {
                        string sql = @"
                            INSERT INTO PART
                            (
                                PART_ID,
                                MANUFACTURER_ID,
                                COMPONENT_ID,
                                PART_NUMBER,
                                PART_NAME,
                                PRICE,
                                QUANTITY,
                                PARTSTATUS_ID,
                                MANUFACTURER_NAME
                            )
                            VALUES
                            (
                                PTPK.NEXTVAL,
                                :manufacturerId,
                                :componentId,
                                :partNumber,
                                :partName,
                                :price,
                                :quantity,
                                :partStatusId,
                                :manufacturerName
                            )";

                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add("manufacturerId", OracleDbType.Int64).Value = manufacturerIdValue;
                            cmd.Parameters.Add("componentId", OracleDbType.Int32).Value = componentId;
                            cmd.Parameters.Add("partNumber", OracleDbType.Varchar2).Value = PartNumberTextBox.Text.Trim();

                            if (string.IsNullOrWhiteSpace(PartNameTextBox.Text))
                                cmd.Parameters.Add("partName", OracleDbType.Varchar2).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("partName", OracleDbType.Varchar2).Value = PartNameTextBox.Text.Trim();

                            if (!hasPrice)
                                cmd.Parameters.Add("price", OracleDbType.Decimal).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("price", OracleDbType.Decimal).Value = priceValue;

                            cmd.Parameters.Add("quantity", OracleDbType.Int32).Value = quantity;
                            cmd.Parameters.Add("partStatusId", OracleDbType.Int32).Value = partStatusId;
                            cmd.Parameters.Add("manufacturerName", OracleDbType.Varchar2).Value = manufacturerNameValue;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sql = @"
                            UPDATE PART
                            SET MANUFACTURER_ID = :manufacturerId,
                                COMPONENT_ID = :componentId,
                                PART_NUMBER = :partNumber,
                                PART_NAME = :partName,
                                PRICE = :price,
                                QUANTITY = :quantity,
                                PARTSTATUS_ID = :partStatusId,
                                MANUFACTURER_NAME = :manufacturerName
                            WHERE PART_ID = :partId";

                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add("manufacturerId", OracleDbType.Int64).Value = manufacturerIdValue;
                            cmd.Parameters.Add("componentId", OracleDbType.Int32).Value = componentId;
                            cmd.Parameters.Add("partNumber", OracleDbType.Varchar2).Value = PartNumberTextBox.Text.Trim();

                            if (string.IsNullOrWhiteSpace(PartNameTextBox.Text))
                                cmd.Parameters.Add("partName", OracleDbType.Varchar2).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("partName", OracleDbType.Varchar2).Value = PartNameTextBox.Text.Trim();

                            if (!hasPrice)
                                cmd.Parameters.Add("price", OracleDbType.Decimal).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("price", OracleDbType.Decimal).Value = priceValue;

                            cmd.Parameters.Add("quantity", OracleDbType.Int32).Value = quantity;
                            cmd.Parameters.Add("partStatusId", OracleDbType.Int32).Value = partStatusId;
                            cmd.Parameters.Add("manufacturerName", OracleDbType.Varchar2).Value = manufacturerNameValue;
                            cmd.Parameters.Add("partId", OracleDbType.Int64).Value = partId;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                LoadParts();

                if (!isUpdate)
                {
                    ClearInputs();
                    PartsDGV.ClearSelection();
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Oracle error:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void DeletePartButton_Click(object sender, EventArgs e)
        {
            long partId;
            bool hasSelection = TryGetSelectedPartId(out partId);

            if (!hasSelection)
            {
                MessageBox.Show("Select a part first.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Delete this part?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            UndoListPart.Push(partId);

            try
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("UPDATE PART SET IS_ACTIVE=0 WHERE PART_ID = :id", conn))
                    {
                        cmd.Parameters.Add("id", OracleDbType.Int64).Value = partId;
                        cmd.ExecuteNonQuery();
                    }
                }
                ActionLogger.LogUserAction("Part", "Delete", UserAuthenticationForm.ActiveUserId, partId);
                ActionLogger.SetDeactivationTime("PART", partId);
                LoadParts();
                ClearInputs();
                PartsDGV.ClearSelection();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Oracle error:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearInputs();
            PartsDGV.ClearSelection();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SearchButton_Click(object sender, EventArgs e)
        {
            string[] searchFields = new string[]
            {
                "PART_NUMBER",
                "PART_NAME",
                "MANUFACTURER_DISPLAY",
                "COMPONENT_TEXT"
            };

            string searchText = SearchTextBox.Text.Trim();
            bool stockOnly = StockCheckbox.Checked;

            if (string.IsNullOrWhiteSpace(searchText) && !stockOnly)
            {
                PartsDGV.DataSource = partsTable;
                return;
            }

            int id_combox = CriteriaComboBox.SelectedIndex;
            if (id_combox < 0 || id_combox >= searchFields.Length)
                id_combox = 0;

            string column = searchFields[id_combox];

            string safeText = searchText.Replace("'", "''");

            string filter = "";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filter = $"{column} LIKE '%{safeText}%'";
            }

            if (stockOnly)
            {
                string stockFilter = "(PARTSTATUS_ID = 1 AND QUANTITY > 0)";

                if (string.IsNullOrWhiteSpace(filter))
                    filter = stockFilter;
                else
                    filter = stockFilter + " AND " + filter;
            }

            DataTable baseTable = partsTable;
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

            PartsDGV.DataSource = filtered;
        }

        private void PartsDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (SelectedPartId == null) return;

            long selectedPartId = Convert.ToInt64(PartsDGV.SelectedRows[0].Cells["PART_ID"].Value);
            SelectedPartId = selectedPartId;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            LastDeletionListRetrieve();
        }
    }
}
