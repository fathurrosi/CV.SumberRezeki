namespace Sendang.Rejeki.Transaction
{
    partial class frmPosList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumlahBayar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSisa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiredDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.DeleteButtonVisible = true;
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = true;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.EditButtonVisible = true;
            this.ctlHeader1.EnableSearch = true;
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = true;
            this.ctlHeader1.NewButtonText = "Create New Transaction";
            this.ctlHeader1.NewButtonVisible = true;
            this.ctlHeader1.PrintButtonText = "Report";
            this.ctlHeader1.PrintButtonVisible = false;
            this.ctlHeader1.Size = new System.Drawing.Size(1057, 25);
            this.ctlHeader1.TabIndex = 1;
            this.ctlHeader1.TextToSearch = "";
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 413);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(1057, 23);
            this.ctlFooter1.TabIndex = 2;
            this.ctlFooter1.TotalRows = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCounter,
            this.colTransactionDate,
            this.colCustomerName,
            this.colTransactionID,
            this.colTotalQty,
            this.colTotalPrice,
            this.colJumlahBayar,
            this.colSisa,
            this.colExpiredDate,
            this.colNotes});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(1057, 388);
            this.grid.TabIndex = 5;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            // 
            // colCounter
            // 
            this.colCounter.DataPropertyName = "Counter";
            this.colCounter.HeaderText = "No.";
            this.colCounter.Name = "colCounter";
            this.colCounter.ReadOnly = true;
            this.colCounter.Width = 50;
            // 
            // colTransactionDate
            // 
            this.colTransactionDate.DataPropertyName = "TransactionDate";
            dataGridViewCellStyle1.Format = "dd - MMM - yyyy";
            this.colTransactionDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTransactionDate.HeaderText = "Tgl Transaksi";
            this.colTransactionDate.Name = "colTransactionDate";
            this.colTransactionDate.ReadOnly = true;
            // 
            // colCustomerName
            // 
            this.colCustomerName.DataPropertyName = "CustomerName";
            this.colCustomerName.HeaderText = "Customer";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            // 
            // colTransactionID
            // 
            this.colTransactionID.DataPropertyName = "TransactionID";
            this.colTransactionID.HeaderText = "Nomor Transaksi";
            this.colTransactionID.Name = "colTransactionID";
            this.colTransactionID.ReadOnly = true;
            this.colTransactionID.Width = 150;
            // 
            // colTotalQty
            // 
            this.colTotalQty.DataPropertyName = "TotalQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            this.colTotalQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTotalQty.HeaderText = "Total Penjualan";
            this.colTotalQty.Name = "colTotalQty";
            this.colTotalQty.ReadOnly = true;
            this.colTotalQty.Width = 200;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.colTotalPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTotalPrice.HeaderText = "Total Harga";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.ReadOnly = true;
            this.colTotalPrice.Width = 200;
            // 
            // colJumlahBayar
            // 
            this.colJumlahBayar.DataPropertyName = "TotalPayment";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.colJumlahBayar.DefaultCellStyle = dataGridViewCellStyle4;
            this.colJumlahBayar.HeaderText = "Jumlah Bayar";
            this.colJumlahBayar.Name = "colJumlahBayar";
            this.colJumlahBayar.ReadOnly = true;
            this.colJumlahBayar.Width = 200;
            // 
            // colSisa
            // 
            this.colSisa.DataPropertyName = "TotalPaymentReturn";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.colSisa.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSisa.HeaderText = "Piutang";
            this.colSisa.Name = "colSisa";
            this.colSisa.ReadOnly = true;
            this.colSisa.Width = 200;
            // 
            // colExpiredDate
            // 
            this.colExpiredDate.DataPropertyName = "ExpiredDate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "dd - MMM - yyyy";
            this.colExpiredDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colExpiredDate.HeaderText = "Jatuh Tempo";
            this.colExpiredDate.Name = "colExpiredDate";
            this.colExpiredDate.ReadOnly = true;
            // 
            // colNotes
            // 
            this.colNotes.DataPropertyName = "Notes";
            this.colNotes.HeaderText = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // frmPosList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 436);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlFooter1);
            this.Controls.Add(this.ctlHeader1);
            this.Name = "frmPosList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPosList";
            this.Load += new System.EventHandler(this.frmPosList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctlHeader ctlHeader1;
        private Control.ctlFooter ctlFooter1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJumlahBayar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpiredDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
    }
}