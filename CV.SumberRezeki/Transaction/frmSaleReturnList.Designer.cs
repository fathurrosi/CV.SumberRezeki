namespace Sendang.Rejeki.Transaction
{
    partial class frmSaleReturnList
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
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colPaymentTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReturnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conStruk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Hapus";
            this.ctlHeader1.DeleteButtonVisible = true;
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = true;
            this.ctlHeader1.EditButtonText = "Lihat";
            this.ctlHeader1.EditButtonVisible = true;
            this.ctlHeader1.EnableSearch = true;
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = true;
            this.ctlHeader1.NewButtonText = "Buat Retur";
            this.ctlHeader1.NewButtonVisible = true;
            this.ctlHeader1.PrintButtonText = "Report";
            this.ctlHeader1.PrintButtonVisible = false;
            this.ctlHeader1.Size = new System.Drawing.Size(847, 25);
            this.ctlHeader1.TabIndex = 5;
            this.ctlHeader1.TextToSearch = "";
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 373);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(847, 23);
            this.ctlFooter1.TabIndex = 6;
            this.ctlFooter1.TotalRows = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPaymentTypeDesc,
            this.colSaleDate,
            this.colCustomerName,
            this.colTotalQty,
            this.colNotes,
            this.colUnit,
            this.colTotalPrice,
            this.colReturnNo,
            this.conStruk});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(847, 348);
            this.grid.TabIndex = 7;
            // 
            // colPaymentTypeDesc
            // 
            this.colPaymentTypeDesc.DataPropertyName = "ReturnNo";
            this.colPaymentTypeDesc.HeaderText = "Return No";
            this.colPaymentTypeDesc.Name = "colPaymentTypeDesc";
            this.colPaymentTypeDesc.ReadOnly = true;
            this.colPaymentTypeDesc.Visible = false;
            this.colPaymentTypeDesc.Width = 80;
            // 
            // colSaleDate
            // 
            this.colSaleDate.DataPropertyName = "ReturnDate";
            dataGridViewCellStyle1.Format = "dd - MMM - yyyy";
            this.colSaleDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSaleDate.HeaderText = "Tgl Retur";
            this.colSaleDate.Name = "colSaleDate";
            this.colSaleDate.ReadOnly = true;
            // 
            // colCustomerName
            // 
            this.colCustomerName.DataPropertyName = "CustomerName";
            this.colCustomerName.HeaderText = "Pelanggan";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            // 
            // colTotalQty
            // 
            this.colTotalQty.DataPropertyName = "TotalQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            this.colTotalQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTotalQty.HeaderText = "Total Retur";
            this.colTotalQty.Name = "colTotalQty";
            this.colTotalQty.ReadOnly = true;
            this.colTotalQty.Visible = false;
            this.colTotalQty.Width = 200;
            // 
            // colNotes
            // 
            this.colNotes.DataPropertyName = "Notes";
            this.colNotes.HeaderText = "Jumlah";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "Unit";
            this.colUnit.HeaderText = "Satuan";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Visible = false;
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
            // colReturnNo
            // 
            this.colReturnNo.DataPropertyName = "ReturnNo";
            this.colReturnNo.HeaderText = "Nomor Transaksi";
            this.colReturnNo.Name = "colReturnNo";
            this.colReturnNo.ReadOnly = true;
            this.colReturnNo.Visible = false;
            this.colReturnNo.Width = 150;
            // 
            // conStruk
            // 
            this.conStruk.DataPropertyName = "NoStruk";
            this.conStruk.HeaderText = "No Struk Penjualan";
            this.conStruk.Name = "conStruk";
            this.conStruk.ReadOnly = true;
            this.conStruk.Width = 150;
            // 
            // frmSaleReturnList
            // 
            this.AllowCreate = true;
            this.AllowDelete = true;
            this.AllowEdit = true;
            this.AllowPrint = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 396);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlFooter1);
            this.Controls.Add(this.ctlHeader1);
            this.Name = "frmSaleReturnList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daftar Retur";
            this.Load += new System.EventHandler(this.frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctlHeader ctlHeader1;
        private Control.ctlFooter ctlFooter1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentTypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReturnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn conStruk;
    }
}