using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsSaleHouses
{
    public partial class FormClientProperties : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string currentSearchFilter = "";

        public FormClientProperties()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
           
            LoadAvailableProperties();

           
            cmbSortBy.SelectedIndex = 0;

           
            btnRefresh.Click += BtnRefresh_Click;
            btnSort.Click += BtnSort_Click;
            btnBuy.Click += BtnBuy_Click;
            btnSearch.Click += BtnSearch_Click;
            btnClearSearch.Click += BtnClearSearch_Click;
            txtSearchAddress.KeyPress += TxtSearchAddress_KeyPress;
            dgvProperties.CellDoubleClick += DgvProperties_CellDoubleClick;
            dgvProperties.SelectionChanged += DgvProperties_SelectionChanged;
        }

        private void LoadAvailableProperties(string addressFilter = "")
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
                    SELECT 
                        Id,
                        Address,
                        Area,
                        Condition,
                        Price,
                        CreatedAt
                    FROM Properties 
                    WHERE IsAvailable = 1";

                SqlParameter[] parameters = null;

               
                if (!string.IsNullOrEmpty(addressFilter))
                {
                    query += " AND Address LIKE @AddressFilter";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@AddressFilter", "%" + addressFilter + "%")
                    };
                }

                DataTable properties = dbHelper.ExecuteQuery(query, parameters);
                dgvProperties.DataSource = properties;

              
                dgvProperties.Columns["Id"].Visible = false;
                dgvProperties.Columns["Price"].DefaultCellStyle.Format = "C2";
                dgvProperties.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProperties.Columns["Area"].DefaultCellStyle.Format = "N2";
                dgvProperties.Columns["Area"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProperties.Columns["CreatedAt"].DefaultCellStyle.Format = "dd.MM.yyyy";

                lblStatus.Text = $"Найдено объектов: {properties.Rows.Count}";

              
                currentSearchFilter = addressFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchAddress.Clear();
            LoadAvailableProperties();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string sortColumn = "";
                string sortOrder = rbAsc.Checked ? "ASC" : "DESC";

                switch (cmbSortBy.SelectedIndex)
                {
                    case 0: sortColumn = "Price"; break;
                    case 1: sortColumn = "Area"; break;
                    case 2: sortColumn = "CreatedAt"; break;
                    case 3: sortColumn = "Condition"; break;
                }

                string query = @"
                    SELECT 
                        Id,
                        Address,
                        Area,
                        Condition,
                        Price,
                        CreatedAt
                    FROM Properties 
                    WHERE IsAvailable = 1";

                SqlParameter[] parameters = null;

               
                if (!string.IsNullOrEmpty(currentSearchFilter))
                {
                    query += " AND Address LIKE @AddressFilter";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@AddressFilter", "%" + currentSearchFilter + "%")
                    };
                }

               
                query += $" ORDER BY {sortColumn} {sortOrder}";

                DataTable properties = dbHelper.ExecuteQuery(query, parameters);
                dgvProperties.DataSource = properties;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сортировки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchAddress.Text.Trim();
            LoadAvailableProperties(searchText);
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchAddress.Clear();
            LoadAvailableProperties();
        }

        private void TxtSearchAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void DgvProperties_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProperties.SelectedRows.Count > 0)
            {
                btnBuy.Enabled = true;
                UpdateSelectedPropertyInfo();
            }
            else
            {
                btnBuy.Enabled = false;
            }
        }

        private void UpdateSelectedPropertyInfo()
        {
            if (dgvProperties.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProperties.SelectedRows[0];
                lblSelectedInfo.Text = $"Выбрано: {row.Cells["Address"].Value} | " +
                                      $"Цена: {Convert.ToDecimal(row.Cells["Price"].Value):C2}";
            }
        }

        private void BtnBuy_Click(object sender, EventArgs e)
        {
            if (dgvProperties.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProperties.SelectedRows[0];
                int propertyId = Convert.ToInt32(row.Cells["Id"].Value);
                string address = row.Cells["Address"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                
                FormCreateTransaction form = new FormCreateTransaction(propertyId, address, price);
                form.ShowDialog();

               
                LoadAvailableProperties(currentSearchFilter);
            }
        }

        private void DgvProperties_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnBuy.PerformClick();
            }
        }
    }
}