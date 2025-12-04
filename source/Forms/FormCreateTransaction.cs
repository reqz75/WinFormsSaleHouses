using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsSaleHouses
{
    public partial class FormCreateTransaction : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private int propertyId;
        private string propertyAddress;
        private decimal propertyPrice;

        public FormCreateTransaction(int propertyId, string propertyAddress, decimal propertyPrice)
        {
            InitializeComponent();
            this.propertyId = propertyId;
            this.propertyAddress = propertyAddress;
            this.propertyPrice = propertyPrice;
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Заполняем информацию о недвижимости
            lblPropertyInfo.Text = $"Объект: {propertyAddress}\nБазовая цена: {propertyPrice:C2}";

            // Загружаем клиентов
            LoadClients();

            // Загружаем риелторов
            LoadRealtors();

            // Устанавливаем текущую дату
            dtpTransactionDate.Value = DateTime.Now;

            // Устанавливаем начальную сумму сделки
            txtFinalAmount.Text = propertyPrice.ToString();

            // Рассчитываем комиссию (5%)
            CalculateCommission();

            // Привязка событий
            txtFinalAmount.TextChanged += TxtFinalAmount_TextChanged;
            btnCreate.Click += BtnCreate_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void LoadClients()
        {
            try
            {
                string query = "SELECT Id, FullName FROM Clients ORDER BY FullName";
                DataTable clients = dbHelper.ExecuteQuery(query);

                cmbBuyer.DisplayMember = "FullName";
                cmbBuyer.ValueMember = "Id";
                cmbBuyer.DataSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRealtors()
        {
            try
            {
                string query = "SELECT Id, FullName FROM Realtors ORDER BY FullName";
                DataTable realtors = dbHelper.ExecuteQuery(query);

                cmbRealtor.DisplayMember = "FullName";
                cmbRealtor.ValueMember = "Id";
                cmbRealtor.DataSource = realtors;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки риелторов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtFinalAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateCommission();
        }

        private void CalculateCommission()
        {
            if (decimal.TryParse(txtFinalAmount.Text, out decimal amount) && amount > 0)
            {
                // Комиссия 5% от суммы сделки
                decimal commission = amount * 0.05m;
                txtCommission.Text = commission.ToString("F2");
            }
        }

        private bool ValidateInput()
        {
            // Проверка покупателя
            if (cmbBuyer.SelectedValue == null)
            {
                MessageBox.Show("Выберите покупателя", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBuyer.Focus();
                return false;
            }

            // Проверка риелтора
            if (cmbRealtor.SelectedValue == null)
            {
                MessageBox.Show("Выберите риелтора", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRealtor.Focus();
                return false;
            }

            // Проверка суммы сделки
            if (string.IsNullOrWhiteSpace(txtFinalAmount.Text))
            {
                MessageBox.Show("Введите сумму сделки", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFinalAmount.Focus();
                return false;
            }

            if (!decimal.TryParse(txtFinalAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму сделки", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFinalAmount.Focus();
                txtFinalAmount.SelectAll();
                return false;
            }

            // Проверка даты
            if (dtpTransactionDate.Value > DateTime.Now)
            {
                DialogResult result = MessageBox.Show("Дата сделки в будущем. Продолжить?",
                    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return false;
            }

            return true;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                // Начинаем транзакцию
                using (SqlConnection connection = new SqlConnection(dbHelper.GetConnectionString()))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Создаем сделку
                        string createTransactionQuery = @"
                            INSERT INTO Transactions 
                                (TransactionDate, PropertyId, BuyerId, RealtorId, FinalAmount, Commission) 
                            VALUES 
                                (@TransactionDate, @PropertyId, @BuyerId, @RealtorId, @FinalAmount, @Commission)";

                        SqlCommand command = new SqlCommand(createTransactionQuery, connection, transaction);
                        command.Parameters.AddWithValue("@TransactionDate", dtpTransactionDate.Value);
                        command.Parameters.AddWithValue("@PropertyId", propertyId);
                        command.Parameters.AddWithValue("@BuyerId", cmbBuyer.SelectedValue);
                        command.Parameters.AddWithValue("@RealtorId", cmbRealtor.SelectedValue);
                        command.Parameters.AddWithValue("@FinalAmount", decimal.Parse(txtFinalAmount.Text));
                        command.Parameters.AddWithValue("@Commission", decimal.Parse(txtCommission.Text));
                        command.ExecuteNonQuery();

                        // 2. Обновляем статус недвижимости
                        string updatePropertyQuery = "UPDATE Properties SET IsAvailable = 0 WHERE Id = @PropertyId";
                        command.CommandText = updatePropertyQuery;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@PropertyId", propertyId);
                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Сделка успешно создана!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Ошибка при создании сделки: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}