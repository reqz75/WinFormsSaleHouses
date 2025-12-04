namespace WinFormsSaleHouses
{
    partial class FormAddProperty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbPropertyInfo = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCondition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPropertiesList = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.gbPropertyInfo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPropertyInfo
            // 
            this.gbPropertyInfo.Controls.Add(this.label4);
            this.gbPropertyInfo.Controls.Add(this.txtPrice);
            this.gbPropertyInfo.Controls.Add(this.label3);
            this.gbPropertyInfo.Controls.Add(this.cmbCondition);
            this.gbPropertyInfo.Controls.Add(this.label2);
            this.gbPropertyInfo.Controls.Add(this.txtArea);
            this.gbPropertyInfo.Controls.Add(this.label1);
            this.gbPropertyInfo.Controls.Add(this.txtAddress);
            this.gbPropertyInfo.Controls.Add(this.btnAdd);
            this.gbPropertyInfo.Controls.Add(this.btnClear);
            this.gbPropertyInfo.Controls.Add(this.btnClose);
            this.gbPropertyInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbPropertyInfo.Location = new System.Drawing.Point(12, 12);
            this.gbPropertyInfo.Name = "gbPropertyInfo";
            this.gbPropertyInfo.Size = new System.Drawing.Size(660, 180);
            this.gbPropertyInfo.TabIndex = 0;
            this.gbPropertyInfo.TabStop = false;
            this.gbPropertyInfo.Text = "Информация о недвижимости";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Цена:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(397, 62);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(150, 24);
            this.txtPrice.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Состояние:";
            // 
            // cmbCondition
            // 
            this.cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition.FormattingEnabled = true;
            this.cmbCondition.Items.AddRange(new object[] {
            "Отличное",
            "Хорошее",
            "Среднее",
            "Требует ремонта",
            "Строящийся"});
            this.cmbCondition.Location = new System.Drawing.Point(100, 97);
            this.cmbCondition.Name = "cmbCondition";
            this.cmbCondition.Size = new System.Drawing.Size(200, 26);
            this.cmbCondition.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Площадь:";
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(100, 62);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(150, 24);
            this.txtArea.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Адрес:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(100, 27);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(447, 24);
            this.txtAddress.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(100, 135);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 35);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(240, 135);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 35);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(380, 135);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblPropertiesList
            // 
            this.lblPropertiesList.AutoSize = true;
            this.lblPropertiesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPropertiesList.Location = new System.Drawing.Point(12, 205);
            this.lblPropertiesList.Name = "lblPropertiesList";
            this.lblPropertiesList.Size = new System.Drawing.Size(318, 20);
            this.lblPropertiesList.TabIndex = 2;
            this.lblPropertiesList.Text = "Список объектов недвижимости:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 492);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 20);
            this.lblStatus.Text = "Готово";
            // 
            // dgvProperties
            // 
            this.dgvProperties.AllowUserToAddRows = false;
            this.dgvProperties.AllowUserToDeleteRows = false;
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.Location = new System.Drawing.Point(12, 230);
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.ReadOnly = true;
            this.dgvProperties.RowHeadersWidth = 51;
            this.dgvProperties.RowTemplate.Height = 24;
            this.dgvProperties.Size = new System.Drawing.Size(660, 250);
            this.dgvProperties.TabIndex = 1;
            // 
            // FormAddProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 518);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPropertiesList);
            this.Controls.Add(this.dgvProperties);
            this.Controls.Add(this.gbPropertyInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbPropertyInfo.ResumeLayout(false);
            this.gbPropertyInfo.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPropertyInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCondition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPropertiesList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.DataGridView dgvProperties;
    }
}