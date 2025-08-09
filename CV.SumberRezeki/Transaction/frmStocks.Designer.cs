namespace Sendang.Rejeki.Transaction
{
    partial class frmStocks
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.colCatalogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatalogName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCatalogID,
            this.colCatalogName,
            this.colStock,
            this.colUnit,
            this.colTotalSales,
            this.colColi,
            this.colStockUpdated});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(890, 213);
            this.grid.TabIndex = 9;
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 238);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(890, 23);
            this.ctlFooter1.TabIndex = 8;
            this.ctlFooter1.TotalRows = 0;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.DeleteButtonVisible = false;
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = true;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.EditButtonVisible = true;
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = false;
            this.ctlHeader1.NewButtonText = "Add";
            this.ctlHeader1.NewButtonVisible = false;
            this.ctlHeader1.PrintButtonText = "Export to Excel";
            this.ctlHeader1.PrintButtonVisible = true;
            this.ctlHeader1.Size = new System.Drawing.Size(890, 25);
            this.ctlHeader1.TabIndex = 7;
            this.ctlHeader1.TextToSearch = "";
            // 
            // colCatalogID
            // 
            this.colCatalogID.DataPropertyName = "CatalogID";
            this.colCatalogID.HeaderText = "Catalog ID";
            this.colCatalogID.Name = "colCatalogID";
            this.colCatalogID.ReadOnly = true;
            this.colCatalogID.Visible = false;
            // 
            // colCatalogName
            // 
            this.colCatalogName.DataPropertyName = "CatalogName";
            this.colCatalogName.HeaderText = "Catalog";
            this.colCatalogName.Name = "colCatalogName";
            this.colCatalogName.ReadOnly = true;
            this.colCatalogName.Width = 150;
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Stock";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.colStock.DefaultCellStyle = dataGridViewCellStyle1;
            this.colStock.HeaderText = "Stock";
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            this.colStock.Width = 150;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "Unit";
            this.colUnit.HeaderText = "Satuan";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Width = 70;
            // 
            // colTotalSales
            // 
            this.colTotalSales.DataPropertyName = "TotalSales";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.colTotalSales.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTotalSales.HeaderText = "Total Sales";
            this.colTotalSales.Name = "colTotalSales";
            this.colTotalSales.ReadOnly = true;
            this.colTotalSales.Visible = false;
            this.colTotalSales.Width = 150;
            // 
            // colColi
            // 
            this.colColi.DataPropertyName = "Coli";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "n0";
            this.colColi.DefaultCellStyle = dataGridViewCellStyle3;
            this.colColi.HeaderText = "Colly";
            this.colColi.Name = "colColi";
            this.colColi.ReadOnly = true;
            // 
            // colStockUpdated
            // 
            this.colStockUpdated.DataPropertyName = "StockDate";
            dataGridViewCellStyle4.Format = "dd MMM yyyy";
            dataGridViewCellStyle4.NullValue = " ";
            this.colStockUpdated.DefaultCellStyle = dataGridViewCellStyle4;
            this.colStockUpdated.HeaderText = "Update Stock";
            this.colStockUpdated.Name = "colStockUpdated";
            this.colStockUpdated.ReadOnly = true;
            this.colStockUpdated.Width = 150;
            // 
            // frmStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 261);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlFooter1);
            this.Controls.Add(this.ctlHeader1);
            this.Name = "frmStocks";
            this.Text = "frmStocks";
            this.Load += new System.EventHandler(this.frmStocks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private Control.ctlFooter ctlFooter1;
        private Control.ctlHeader ctlHeader1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockUpdated;
    }
}