namespace Sendang.Rejeki.Transaction
{
    partial class frmCatalogPriceList
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
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colCatalogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatalogName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyPricePerUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplierCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 384);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(863, 23);
            this.ctlFooter1.TabIndex = 8;
            this.ctlFooter1.TotalRows = 0;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = false;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = false;
            this.ctlHeader1.NewButtonText = "Add";
            this.ctlHeader1.Size = new System.Drawing.Size(863, 25);
            this.ctlHeader1.TabIndex = 7;
            this.ctlHeader1.TextToSearch = "";
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.grid);
            this.panelContainer.Controls.Add(this.ctlHeader1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(863, 384);
            this.panelContainer.TabIndex = 10;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCatalogID,
            this.colPriceDate,
            this.colCatalogName,
            this.colBuyPricePerUnit,
            this.colSellPrice,
            this.colSupplier,
            this.ModifiedBy,
            this.colSupplierCode});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(863, 359);
            this.grid.TabIndex = 12;
            // 
            // colCatalogID
            // 
            this.colCatalogID.DataPropertyName = "CatalogID";
            this.colCatalogID.HeaderText = "CatalogID";
            this.colCatalogID.Name = "colCatalogID";
            this.colCatalogID.ReadOnly = true;
            this.colCatalogID.Visible = false;
            // 
            // colPriceDate
            // 
            this.colPriceDate.DataPropertyName = "PriceDate";
            dataGridViewCellStyle1.Format = "dd - MMM - yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.colPriceDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPriceDate.HeaderText = "Tanggal Pembelian";
            this.colPriceDate.Name = "colPriceDate";
            this.colPriceDate.ReadOnly = true;
            this.colPriceDate.Width = 150;
            // 
            // colCatalogName
            // 
            this.colCatalogName.DataPropertyName = "CatalogName";
            this.colCatalogName.HeaderText = "Jenis Daging";
            this.colCatalogName.Name = "colCatalogName";
            this.colCatalogName.ReadOnly = true;
            this.colCatalogName.Width = 250;
            // 
            // colBuyPricePerUnit
            // 
            this.colBuyPricePerUnit.DataPropertyName = "BuyPricePerUnit";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.colBuyPricePerUnit.DefaultCellStyle = dataGridViewCellStyle2;
            this.colBuyPricePerUnit.HeaderText = "Harga Beli/Unit";
            this.colBuyPricePerUnit.Name = "colBuyPricePerUnit";
            this.colBuyPricePerUnit.ReadOnly = true;
            this.colBuyPricePerUnit.Width = 150;
            // 
            // colSellPrice
            // 
            this.colSellPrice.DataPropertyName = "SellPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.colSellPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSellPrice.HeaderText = "Harga Jual/Unit";
            this.colSellPrice.Name = "colSellPrice";
            this.colSellPrice.ReadOnly = true;
            // 
            // colSupplier
            // 
            this.colSupplier.DataPropertyName = "SupplierName";
            this.colSupplier.HeaderText = "Supplier";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.ReadOnly = true;
            this.colSupplier.Width = 200;
            // 
            // ModifiedBy
            // 
            this.ModifiedBy.DataPropertyName = "CreatedBy";
            this.ModifiedBy.HeaderText = "Di Input Oleh";
            this.ModifiedBy.Name = "ModifiedBy";
            this.ModifiedBy.ReadOnly = true;
            this.ModifiedBy.Visible = false;
            this.ModifiedBy.Width = 150;
            // 
            // colSupplierCode
            // 
            this.colSupplierCode.DataPropertyName = "SupplierCode";
            this.colSupplierCode.HeaderText = "Supplier Code";
            this.colSupplierCode.Name = "colSupplierCode";
            this.colSupplierCode.ReadOnly = true;
            this.colSupplierCode.Visible = false;
            // 
            // frmCatalogPriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 407);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.ctlFooter1);
            this.Name = "frmCatalogPriceList";
            this.Text = "Catalog Price List";
            this.Load += new System.EventHandler(this.frmCatalogPriceList_Load);
            this.panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctlFooter ctlFooter1;
        private Control.ctlHeader ctlHeader1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyPricePerUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplierCode;
    }
}