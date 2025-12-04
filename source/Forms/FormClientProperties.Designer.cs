namespace WinFormsSaleHouses
{
    partial class FormClientProperties
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
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDesc = new System.Windows.Forms.RadioButton();
            this.rbAsc = new System.Windows.Forms.RadioButton();
            this.cmbSortBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // dgvProperties
            this.dgvProperties.AllowUserToAddRows = false;
            this.dgvProperties.AllowUserToDeleteRows = false;
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.Location = new System.Drawing.Point(12, 80);
            this.dgvProperties.MultiSelect = false;
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.ReadOnly = true;
            this.dgvProperties.RowHeadersWidth = 51;
            this.dgvProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProperties.Size = new System.Drawing.Size(660, 300);
            this.dgvProperties.TabIndex = 0;

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;

            // btnBuy
            this.btnBuy.BackColor = System.Drawing.Color.SeaGreen;
            this.btnBuy.Enabled = false;
            this.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy.ForeColor = System.Drawing.Color.White;
            this.btnBuy.Location = new System.Drawing.Point(572, 386);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(100, 35);
            this.btnBuy.TabIndex = 2;
            this.btnBuy.Text = "Купить";
            this.btnBuy.UseVisualStyleBackColor = false;

            // groupBox1
            this.groupBox1.Controls.Add(this.btnSort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbSortBy);
            this.groupBox1.Controls.Add(this.rbDesc);
            this.groupBox1.Controls.Add(this.rbAsc);
            this.groupBox1.Location = new System.Drawing.Point(118, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сортировка";

            // rbDesc
            this.rbDesc.AutoSize = true;
            this.rbDesc.Location = new System.Drawing.Point(260, 25);
            this.rbDesc.Name = "rbDesc";
            this.rbDesc.Size = new System.Drawing.Size(59, 20);
            this.rbDesc.TabIndex = 2;
            this.rbDesc.TabStop = true;
            this.rbDesc.Text = "Убыв";
            this.rbDesc.UseVisualStyleBackColor = true;

            // rbAsc
            this.rbAsc.AutoSize = true;
            this.rbAsc.Checked = true;
            this.rbAsc.Location = new System.Drawing.Point(200, 25);
            this.rbAsc.Name = "rbAsc";
            this.rbAsc.Size = new System.Drawing.Size(54, 20);
            this.rbAsc.TabIndex = 1;
            this.rbAsc.TabStop = true;
            this.rbAsc.Text = "Возр";
            this.rbAsc.UseVisualStyleBackColor = true;

            // cmbSortBy
            this.cmbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortBy.FormattingEnabled = true;
            this.cmbSortBy.Items.AddRange(new object[] {
            "По цене",
            "По площади",
            "По дате добавления",
            "По состоянию"});
            this.cmbSortBy.Location = new System.Drawing.Point(60, 25);
            this.cmbSortBy.Name = "cmbSortBy";
            this.cmbSortBy.Size = new System.Drawing.Size(130, 24);
            this.cmbSortBy.TabIndex = 3;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Сорт:";

            // btnSort
            this.btnSort.Location = new System.Drawing.Point(325, 20);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(65, 30);
            this.btnSort.TabIndex = 5;
            this.btnSort.Text = "Сорт";
            this.btnSort.UseVisualStyleBackColor = true;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 396);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Статус";

            // lblSelectedInfo
            this.lblSelectedInfo.AutoSize = true;
            this.lblSelectedInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSelectedInfo.Location = new System.Drawing.Point(12, 60);
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(101, 18);
            this.lblSelectedInfo.TabIndex = 5;
            this.lblSelectedInfo.Text = "Выбрано: ...";

            // FormClientProperties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 430);
            this.Controls.Add(this.lblSelectedInfo);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvProperties);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormClientProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр недвижимости - Клиент";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSortBy;
        private System.Windows.Forms.RadioButton rbDesc;
        private System.Windows.Forms.RadioButton rbAsc;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSelectedInfo;
    }
}