using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WinFormsSaleHouses;

namespace RealEstateApp
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper dbHelper;

        public LoginForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbUserType.SelectedIndex = 0; // Устанавливаем значение по умолчанию
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string userType = cmbUserType.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Определяем таблицу в зависимости от типа пользователя
                string tableName = userType == "Риелтор" ? "Realtors" : "Clients";

                // Вариант для теста: сравниваем пароли без хэширования
                string query = "SELECT Id, Username, FullName FROM " + tableName +
                              " WHERE Username = @Username AND PasswordHash = @Password";

                SqlParameter[] parameters = {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password) // БЕЗ хэширования
        };

                DataTable result = dbHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    // Успешный вход
                    DataRow row = result.Rows[0];
                    int userId = Convert.ToInt32(row["Id"]);
                    string fullName = row["FullName"].ToString();

                    MessageBox.Show($"Добро пожаловать, {fullName}!", "Успешный вход",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    if (userType == "Риелтор")
                    {
                        
                        FormAddProperty realtorForm = new FormAddProperty();
                        realtorForm.Show();
                    }
                    else
                    {
                        FormClientProperties clientForm = new FormClientProperties(userId, fullName);
                         clientForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка входа",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Открываем форму регистрации как диалоговое окно
            using (RegistrationForm registrationForm = new RegistrationForm())
            {
                if (registrationForm.ShowDialog() == DialogResult.OK)
                {
                    // После успешной регистрации можно очистить поля или показать сообщение
                    MessageBox.Show("Регистрация завершена! Теперь вы можете войти в систему.",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}