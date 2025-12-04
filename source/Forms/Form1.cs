using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsSaleHouses
{
    public partial class FormAddProperty : Form
    {
        private PropertyRepository propertyRepository;
        private ToolTip toolTip;

        public FormAddProperty()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Инициализация репозитория
            propertyRepository = new PropertyRepository();

            // Инициализация ToolTip
            toolTip = new ToolTip();

            // Настройка формы
            this.Text = "Добавление недвижимости";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Настройка DataGridView
            ConfigureDataGridView();

            // Загрузка данных
            LoadProperties();

            // Настройка подсказок
            SetupToolTips();

            // Привязка событий
            AttachEvents();
        }

        private void ConfigureDataGridView()
        {
            dgvProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProperties.ReadOnly = true;
            dgvProperties.AllowUserToAddRows = false;
            dgvProperties.AllowUserToDeleteRows = false;

            // Очистка существующих колонок и добавление своих
            dgvProperties.Columns.Clear();

            dgvProperties.Columns.Add("Id", "ID");
            dgvProperties.Columns.Add("Address", "Адрес");
            dgvProperties.Columns.Add("Area", "Площадь");
            dgvProperties.Columns.Add("Condition", "Состояние");
            dgvProperties.Columns.Add("Price", "Цена");
            dgvProperties.Columns.Add("IsAvailable", "Доступно");

            // Настройка ширины колонок
            dgvProperties.Columns["Id"].Width = 50;
            dgvProperties.Columns["Address"].Width = 200;
            dgvProperties.Columns["Area"].Width = 80;
            dgvProperties.Columns["Condition"].Width = 100;
            dgvProperties.Columns["Price"].Width = 100;
            dgvProperties.Columns["IsAvailable"].Width = 80;

            // Форматирование
            dgvProperties.Columns["Area"].DefaultCellStyle.Format = "N2";
            dgvProperties.Columns["Price"].DefaultCellStyle.Format = "C2";
        }

        private void LoadProperties()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Загрузка данных...";

                DataTable properties = propertyRepository.GetAllProperties();

                // Очистка существующих строк
                dgvProperties.Rows.Clear();

                // Заполнение DataGridView
                foreach (DataRow row in properties.Rows)
                {
                    dgvProperties.Rows.Add(
                        row["Id"],
                        row["Address"],
                        row["Area"],
                        row["Condition"],
                        row["Price"],
                        Convert.ToBoolean(row["IsAvailable"]) ? "Да" : "Нет"
                    );
                }

                lblStatus.Text = $"Загружено записей: {properties.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Ошибка загрузки данных";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SetupToolTips()
        {
            toolTip.SetToolTip(txtAddress, "Введите полный адрес объекта недвижимости");
            toolTip.SetToolTip(txtArea, "Введите площадь в квадратных метрах (например: 85.5)");
            toolTip.SetToolTip(txtPrice, "Введите стоимость объекта в долларах (например: 150000)");
            toolTip.SetToolTip(cmbCondition, "Выберите состояние объекта недвижимости");
        }

        private void AttachEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnClear.Click += BtnClear_Click;
            btnClose.Click += BtnClose_Click;
            txtArea.KeyPress += NumericTextBox_KeyPress;
            txtPrice.KeyPress += NumericTextBox_KeyPress;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация
                if (!ValidateInput())
                    return;

                // Подготовка данных
                string address = txtAddress.Text.Trim();
                decimal area = decimal.Parse(txtArea.Text.Trim(), CultureInfo.InvariantCulture);
                string condition = cmbCondition.SelectedItem.ToString();
                decimal price = decimal.Parse(txtPrice.Text.Trim(), CultureInfo.InvariantCulture);

                // Добавление в БД
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Добавление...";

                int result = propertyRepository.AddProperty(address, area, condition, price);

                if (result > 0)
                {
                    MessageBox.Show("Недвижимость успешно добавлена!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadProperties();
                    ClearForm();
                    lblStatus.Text = "Недвижимость добавлена";
                }
                else
                {
                    MessageBox.Show("Не удалось добавить недвижимость",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Ошибка добавления";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат числовых значений",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
            // Проверка адреса
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Введите адрес", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            // Проверка площади
            if (string.IsNullOrWhiteSpace(txtArea.Text))
            {
                MessageBox.Show("Введите площадь", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtArea.Focus();
                return false;
            }

            if (!decimal.TryParse(txtArea.Text, out decimal area) || area <= 0)
            {
                MessageBox.Show("Площадь должна быть положительным числом",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtArea.Focus();
                txtArea.SelectAll();
                return false;
            }

            // Проверка состояния
            if (cmbCondition.SelectedItem == null)
            {
                MessageBox.Show("Выберите состояние", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCondition.Focus();
                return false;
            }

            // Проверка цены
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Введите цену", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть неотрицательным числом",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                txtPrice.SelectAll();
                return false;
            }

            return true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblStatus.Text = "Форма очищена";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearForm()
        {
            txtAddress.Clear();
            txtArea.Clear();
            txtPrice.Clear();
            cmbCondition.SelectedIndex = 0;
            txtAddress.Focus();
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, точку и управляющие символы
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Проверяем только одну точку
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}