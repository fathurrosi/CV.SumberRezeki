namespace Sendang.Rejeki.Transaction
{
    partial class frmCatalogStock
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
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCatalogName = new System.Windows.Forms.TextBox();
            this.ctlTransButton1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColly = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(99, 38);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(145, 20);
            this.txtStock.TabIndex = 2;
            this.txtStock.Enter += new System.EventHandler(this.tb_Enter);
            this.txtStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtStock.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // txtUnit
            // 
            this.txtUnit.Enabled = false;
            this.txtUnit.Location = new System.Drawing.Point(250, 38);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(78, 20);
            this.txtUnit.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Item :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Stock :";
            // 
            // txtCatalogName
            // 
            this.txtCatalogName.Enabled = false;
            this.txtCatalogName.Location = new System.Drawing.Point(99, 12);
            this.txtCatalogName.Name = "txtCatalogName";
            this.txtCatalogName.Size = new System.Drawing.Size(228, 20);
            this.txtCatalogName.TabIndex = 34;
            // 
            // ctlTransButton1
            // 
            this.ctlTransButton1.CancelButtonEnabled = true;
            this.ctlTransButton1.CancelButtonText = null;
            this.ctlTransButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlTransButton1.IsLookup = false;
            this.ctlTransButton1.Location = new System.Drawing.Point(0, 90);
            this.ctlTransButton1.Name = "ctlTransButton1";
            this.ctlTransButton1.SaveButtonEnabled = true;
            this.ctlTransButton1.SaveButtonText = null;
            this.ctlTransButton1.Size = new System.Drawing.Size(340, 45);
            this.ctlTransButton1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Stock (Colly) :";
            // 
            // txtColly
            // 
            this.txtColly.Location = new System.Drawing.Point(99, 64);
            this.txtColly.Name = "txtColly";
            this.txtColly.Size = new System.Drawing.Size(102, 20);
            this.txtColly.TabIndex = 35;
            // 
            // frmCatalogStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 135);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtColly);
            this.Controls.Add(this.txtCatalogName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.ctlTransButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCatalogStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Item Stock";
            this.Load += new System.EventHandler(this.frmCatalogStock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control.ctlTransButton ctlTransButton1;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCatalogName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColly;
    }
}