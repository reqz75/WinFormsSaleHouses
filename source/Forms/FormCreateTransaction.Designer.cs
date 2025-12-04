namespace WinFormsSaleHouses
{
    partial class FormCreateTransaction
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblPropertyInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCommission = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFinalAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRealtor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBuyer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // lblPropertyInfo
            this.lblPropertyInfo.AutoSize = true;
            this.lblPropertyInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblPropertyInfo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPropertyInfo.Location = new System.Drawing.Point(12, 12);
            this.lblPropertyInfo.Name = "lblPropertyInfo";
            this.lblPropertyInfo.Size = new System.Drawing.Size(110, 20);
            this.lblPropertyInfo.TabIndex = 0;
            this.lblPropertyInfo.Text = "Объект: ...";

            // groupBox1
            this.groupBox1.Controls.Add(this.txtCommission);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFinalAmount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpTransactionDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbRealtor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbBuyer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 220);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные сделки";

            // txtCommission
            this.txtCommission.Location = new System.Drawing.Point(150, 180);
            this.txtCommission.Name = "txtCommission";
            this.txtCommission.ReadOnly = true;
            this.txtCommission.Size = new System.Drawing.Size(150, 22);
            this.txtCommission.TabIndex = 9;

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Комиссия:";

            // txtFinalAmount
            this.txtFinalAmount.Location = new System.Drawing.Point(150, 145);
            this.txtFinalAmount.Name = "txtFinalAmount";
            this.txtFinalAmount.Size = new System.Drawing.Size(150, 22);
            this.txtFinalAmount.TabIndex = 7;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Сумма сделки:";

            // dtpTransactionDate
            this.dtpTransactionDate.Location = new System.Drawing.Point(150, 110);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(200, 22);
            this.dtpTransactionDate.TabIndex = 5;

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата сделки:";

            // cmbRealtor
            this.cmbRealtor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRealtor.FormattingEnabled = true;
            this.cmbRealtor.Location = new System.Drawing.Point(150, 75);
            this.cmbRealtor.Name = "cmbRealtor";
            this.cmbRealtor.Size = new System.Drawing.Size(300, 24);
            this.cmbRealtor.TabIndex = 3;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Риелтор:";

            // cmbBuyer
            this.cmbBuyer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuyer.FormattingEnabled = true;
            this.cmbBuyer.Location = new System.Drawing.Point(150, 40);
            this.cmbBuyer.Name = "cmbBuyer";
            this.cmbBuyer.Size = new System.Drawing.Size(300, 24);
            this.cmbBuyer.TabIndex = 1;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Покупатель:";

            // btnCreate
            this.btnCreate.BackColor = System.Drawing.Color.SeaGreen;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(300, 280);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 40);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = false;

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(412, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;

            // FormCreateTransaction
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 332);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPropertyInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание сделки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblPropertyInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbBuyer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRealtor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFinalAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCommission;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
    }
}