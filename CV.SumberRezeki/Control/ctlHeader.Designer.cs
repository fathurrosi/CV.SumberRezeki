namespace Sendang.Rejeki.Control
{
    partial class ctlHeader
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMaterGrid = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.separatorAdd = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.separatorEdit = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.separatorDelete = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.separatorReport = new System.Windows.Forms.ToolStripSeparator();
            this.lblSearch = new System.Windows.Forms.ToolStripLabel();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripMaterGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMaterGrid
            // 
            this.toolStripMaterGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.separatorAdd,
            this.btnEdit,
            this.separatorEdit,
            this.btnDelete,
            this.separatorDelete,
            this.btnPrint,
            this.separatorReport,
            this.lblSearch,
            this.txtSearch,
            this.btnSearch});
            this.toolStripMaterGrid.Location = new System.Drawing.Point(0, 0);
            this.toolStripMaterGrid.Name = "toolStripMaterGrid";
            this.toolStripMaterGrid.Size = new System.Drawing.Size(682, 25);
            this.toolStripMaterGrid.TabIndex = 1;
            this.toolStripMaterGrid.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CV.SumberRezeki.Properties.Resources.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(49, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.ToolTipText = "Add New Record";
            this.btnAdd.Click += new System.EventHandler(this.btn_Click);
            // 
            // separatorAdd
            // 
            this.separatorAdd.Name = "separatorAdd";
            this.separatorAdd.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::CV.SumberRezeki.Properties.Resources.pencil;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.ToolTipText = "Edit selected record";
            this.btnEdit.Click += new System.EventHandler(this.btn_Click);
            // 
            // separatorEdit
            // 
            this.separatorEdit.Name = "separatorEdit";
            this.separatorEdit.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::CV.SumberRezeki.Properties.Resources.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete seleted record";
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // separatorDelete
            // 
            this.separatorDelete.Name = "separatorDelete";
            this.separatorDelete.Size = new System.Drawing.Size(6, 25);
            this.separatorDelete.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::CV.SumberRezeki.Properties.Resources.printer;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(62, 22);
            this.btnPrint.Text = "Report";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btn_Click);
            // 
            // separatorReport
            // 
            this.separatorReport.Name = "separatorReport";
            this.separatorReport.Size = new System.Drawing.Size(6, 25);
            // 
            // lblSearch
            // 
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(51, 22);
            this.lblSearch.Text = "Search : ";
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 25);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::CV.SumberRezeki.Properties.Resources.zoom;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Text = "Search";
            this.btnSearch.ToolTipText = "Search data containing inputted text";
            this.btnSearch.Click += new System.EventHandler(this.btn_Click);
            // 
            // ctlHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripMaterGrid);
            this.Name = "ctlHeader";
            this.Size = new System.Drawing.Size(682, 25);
            this.toolStripMaterGrid.ResumeLayout(false);
            this.toolStripMaterGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMaterGrid;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator separatorReport;
        private System.Windows.Forms.ToolStripLabel lblSearch;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator separatorAdd;
        private System.Windows.Forms.ToolStripSeparator separatorEdit;
        private System.Windows.Forms.ToolStripSeparator separatorDelete;
    }
}
