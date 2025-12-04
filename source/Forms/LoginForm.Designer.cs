using System;
using System.Drawing;
using System.Windows.Forms;

namespace RealEstateApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblUserType;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cmbUserType;
        private Button btnLogin;
        private Button btnRegister;
        private LinkLabel linkForgotPassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblUserType = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.cmbUserType = new ComboBox();
            this.btnLogin = new Button();
            this.btnRegister = new Button();
            this.linkForgotPassword = new LinkLabel();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(120, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(160, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Авторизация";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(50, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(106, 13);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Имя пользователя:";

            // txtUsername
            this.txtUsername.Location = new Point(160, 77);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(180, 20);
            this.txtUsername.TabIndex = 0;

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(50, 120);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(48, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Пароль:";

            // txtPassword
            this.txtPassword.Location = new Point(160, 117);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(180, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtPassword_KeyPress);

            // lblUserType
            this.lblUserType.AutoSize = true;
            this.lblUserType.Location = new Point(50, 160);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new Size(77, 13);
            this.lblUserType.TabIndex = 5;
            this.lblUserType.Text = "Тип учетной записи:";

            // cmbUserType
            this.cmbUserType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] { "Риелтор", "Клиент" });
            this.cmbUserType.Location = new Point(160, 157);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new Size(180, 21);
            this.cmbUserType.TabIndex = 2;

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(0, 123, 255);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(50, 210);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(290, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // btnRegister
            this.btnRegister.BackColor = Color.FromArgb(40, 167, 69);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.Location = new Point(50, 260);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new Size(290, 35);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            // linkForgotPassword
            this.linkForgotPassword.AutoSize = true;
            this.linkForgotPassword.Location = new Point(200, 310);
            this.linkForgotPassword.Name = "linkForgotPassword";
            this.linkForgotPassword.Size = new Size(140, 13);
            this.linkForgotPassword.TabIndex = 5;
            this.linkForgotPassword.TabStop = true;
            this.linkForgotPassword.Text = "Забыли пароль?";
            this.linkForgotPassword.Visible = false; // Пока скрываем, можно реализовать позже

            // LoginForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(384, 361);
            this.Controls.Add(this.linkForgotPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cmbUserType);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.Load += new EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}