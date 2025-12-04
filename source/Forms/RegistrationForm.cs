using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RealEstateApp
{
    public partial class RegistrationForm : Form
    {
        private DatabaseHelper dbHelper;

        public RegistrationForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            cmbUserType.SelectedIndex = 0; // Устанавливаем значение по умолчанию
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Валидация данных
            if (!ValidateInput())
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string userType = cmbUserType.SelectedItem.ToString();

            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                // Проверяем, существует ли пользователь с таким именем
                string checkUserQuery = "SELECT COUNT(*) FROM (SELECT Username FROM Realtors UNION SELECT Username FROM Clients) AS AllUsers WHERE Username = @Username";
                SqlParameter[] checkParams = { new SqlParameter("@Username", username) };

                int userCount = Convert.ToInt32(dbHelper.ExecuteScalar(checkUserQuery, checkParams));

                if (userCount > 0)
                {
                    MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                    return;
                }

                // Определяем таблицу для вставки
                string tableName = userType == "Риелтор" ? "Realtors" : "Clients";

                // В реальном приложении пароль должен хэшироваться!
                string query = $@"INSERT INTO {tableName} (Username, PasswordHash, FullName, PhoneNumber) 
                                 VALUES (@Username, @Password, @FullName, @PhoneNumber)";

                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password), // В реальном приложении: PasswordHasher.Hash(password)
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@PhoneNumber", phoneNumber)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Регистрация прошла успешно!\nДобро пожаловать, {fullName}!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Возвращаемся на форму логина
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при регистрации. Попробуйте снова.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Проверка имени пользователя
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Length < 3)
            {
                MessageBox.Show("Имя пользователя должно содержать не менее 3 символов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Проверка пароля
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Проверка подтверждения пароля
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Подтвердите пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            // Проверка полного имени
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || txtFullName.Text.Length < 2)
            {
                MessageBox.Show("Введите корректное полное имя!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Проверка номера телефона (простая проверка на наличие цифр)
            string phonePattern = @"^[\d\s\-\+\(\)]+$";
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                !Regex.IsMatch(txtPhoneNumber.Text, phonePattern))
            {
                MessageBox.Show("Введите корректный номер телефона!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Возвращаемся на форму логина
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Обновляем индикатор сложности пароля
            UpdatePasswordStrength();
        }

        private void UpdatePasswordStrength()
        {
            string password = txtPassword.Text;
            int strength = 0;

            if (password.Length >= 6) strength++;
            if (Regex.IsMatch(password, @"[a-z]")) strength++;
            if (Regex.IsMatch(password, @"[A-Z]")) strength++;
            if (Regex.IsMatch(password, @"\d")) strength++;
            if (Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]")) strength++;

            lblPasswordStrength.Text = "Сложность пароля: " + GetStrengthText(strength);
            lblPasswordStrength.ForeColor = GetStrengthColor(strength);
        }

        private string GetStrengthText(int strength)
        {
            if (strength == 0 || strength == 1)
                return "Слабый";
            else if (strength == 2 || strength == 3)
                return "Средний";
            else
                return "Сильный";
        }

        private Color GetStrengthColor(int strength)
        {
            if (strength == 0 || strength == 1)
                return Color.Red;
            else if (strength == 2 || strength == 3)
                return Color.Orange;
            else
                return Color.Green;
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры, пробел, +, - и управляющие символы
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '+' && e.KeyChar != '-' &&
                e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Показывать/скрывать пароль
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        // ДОБАВЛЕННЫЙ МЕТОД, который был в RegistrationForm.Designer.cs
        private void linkTerms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Регистрируясь в системе, вы соглашаетесь с условиями использования и политикой конфиденциальности.",
                "Пользовательское соглашение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}