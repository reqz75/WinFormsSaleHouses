
﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

                // Получаем данные для документа
                string buyerName = cmbBuyer.Text;
                string realtorName = cmbRealtor.Text;
                decimal finalAmount = decimal.Parse(txtFinalAmount.Text);
                decimal commission = decimal.Parse(txtCommission.Text);
                DateTime transactionDate = dtpTransactionDate.Value;

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
                        command.Parameters.AddWithValue("@TransactionDate", transactionDate);
                        command.Parameters.AddWithValue("@PropertyId", propertyId);
                        command.Parameters.AddWithValue("@BuyerId", cmbBuyer.SelectedValue);
                        command.Parameters.AddWithValue("@RealtorId", cmbRealtor.SelectedValue);
                        command.Parameters.AddWithValue("@FinalAmount", finalAmount);
                        command.Parameters.AddWithValue("@Commission", commission);
                        command.ExecuteNonQuery();

                        // 2. Обновляем статус недвижимости
                        string updatePropertyQuery = "UPDATE Properties SET IsAvailable = 0 WHERE Id = @PropertyId";
                        command.CommandText = updatePropertyQuery;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@PropertyId", propertyId);
                        command.ExecuteNonQuery();

                        transaction.Commit();

                        // 3. Генерируем Word-документ
                        GenerateTransactionDocument(
                            buyerName,
                            realtorName,
                            propertyAddress,
                            finalAmount,
                            commission,
                            transactionDate);

                        MessageBox.Show("Сделка успешно создана! Документ сохранен в папке 'Документы'.", "Успех",
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

        private void GenerateTransactionDocument(
            string buyerName,
            string realtorName,
            string propertyAddress,
            decimal finalAmount,
            decimal commission,
            DateTime transactionDate)
        {
            try
            {
                // Создаем папку для документов, если ее нет
                string documentsFolder = Path.Combine(Application.StartupPath, "Документы");
                if (!Directory.Exists(documentsFolder))
                {
                    Directory.CreateDirectory(documentsFolder);
                }

                // Генерируем имя файла
                string fileName = $"Сделка_{propertyAddress.Replace(" ", "_").Replace(",", "")}_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                string filePath = Path.Combine(documentsFolder, fileName);

                // Создаем Word-документ
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    // Добавляем основную часть документа
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Заголовок
                    Paragraph titleParagraph = new Paragraph();
                    Run titleRun = new Run();
                    Text titleText = new Text("ДОГОВОР КУПЛИ-ПРОДАЖИ НЕДВИЖИМОСТИ");
                    titleRun.Append(titleText);
                    titleRun.RunProperties = new RunProperties(
                        new Bold(),
                        new FontSize() { Val = "28" },
                        new Justification() { Val = JustificationValues.Center }
                    );
                    titleParagraph.Append(titleRun);
                    body.Append(titleParagraph);

                    // Пустая строка
                    body.Append(new Paragraph(new Run(new Text(""))));

                    // Даты
                    Paragraph dateParagraph = new Paragraph();
                    Run dateRun = new Run();
                    Text dateText = new Text($"Дата заключения сделки: {transactionDate:dd.MM.yyyy}");
                    dateRun.Append(dateText);
                    dateParagraph.Append(dateRun);
                    body.Append(dateParagraph);

                    // Пустая строка
                    body.Append(new Paragraph(new Run(new Text(""))));

                    // Информация о сделке в таблице
                    Table table = new Table();

                    // Настройки таблицы
                    TableProperties tableProperties = new TableProperties(
                        new TableBorders(
                            new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                            new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 2 },
                            new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 2 }
                        )
                    );
                    table.AppendChild(tableProperties);

                    // Добавляем строки с информацией
                    AddTableRow(table, "Покупатель:", buyerName);
                    AddTableRow(table, "Риелтор:", realtorName);
                    AddTableRow(table, "Объект недвижимости:", propertyAddress);
                    AddTableRow(table, "Сумма сделки:", $"{finalAmount:C2}");
                    AddTableRow(table, "Комиссия риелтора:", $"{commission:C2}");
                    AddTableRow(table, "Итоговая сумма:", $"{(finalAmount + commission):C2}");

                    body.Append(table);

                    // Пустая строка
                    body.Append(new Paragraph(new Run(new Text(""))));

                    // Условия сделки
                    Paragraph termsParagraph = new Paragraph();
                    Run termsRun = new Run();
                    Text termsText = new Text("Условия сделки:\n" +
                        "1. Покупатель обязуется оплатить указанную сумму в течение 5 банковских дней.\n" +
                        "2. Риелтор гарантирует юридическую чистоту сделки.\n" +
                        "3. Все расходы по оформлению документов несет покупатель.\n" +
                        "4. Сделка считается завершенной после полной оплаты и регистрации в Росреестре.");
                    termsRun.Append(termsText);
                    termsParagraph.Append(termsRun);
                    body.Append(termsParagraph);

                    // Пустая строка
                    body.Append(new Paragraph(new Run(new Text(""))));

                    // Подписи
                    Paragraph signaturesParagraph = new Paragraph();
                    Run signaturesRun = new Run();
                    Text signaturesText = new Text("Подписи сторон:\n\n" +
                        "__________________________          __________________________\n" +
                        "Покупатель                                     Риелтор\n\n" +
                        $"Дата: {DateTime.Now:dd.MM.yyyy}");
                    signaturesRun.Append(signaturesText);
                    signaturesParagraph.Append(signaturesRun);
                    body.Append(signaturesParagraph);

                    // Добавляем тело к документу
                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }

                // Показываем сообщение об успешном создании
                MessageBox.Show($"Документ сохранен по пути:\n{filePath}", "Документ создан",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании документа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Не прерываем выполнение, так как сделка уже создана
            }
        }

        private void AddTableRow(Table table, string header, string value)
        {
            TableRow row = new TableRow();

            TableCell cell1 = new TableCell();
            Paragraph para1 = new Paragraph();
            Run run1 = new Run();
            Text text1 = new Text(header);
            run1.Append(text1);
            run1.RunProperties = new RunProperties(new Bold());
            para1.Append(run1);
            cell1.Append(para1);

            TableCell cell2 = new TableCell();
            Paragraph para2 = new Paragraph();
            Run run2 = new Run();
            Text text2 = new Text(value);
            run2.Append(text2);
            para2.Append(run2);
            cell2.Append(para2);

            row.Append(cell1, cell2);
            table.Append(row);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
