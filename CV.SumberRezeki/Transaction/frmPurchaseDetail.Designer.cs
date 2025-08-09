
using Sendang.Rejeki.Control;
namespace Sendang.Rejeki.Transaction
{
    partial class frmPurchaseDetail
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPricePerUnit = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCatalog = new System.Windows.Forms.ComboBox();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlahColi = new System.Windows.Forms.TextBox();
            this.ctlMaster1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Harga / Unit :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Quantity :";
            // 
            // txtPricePerUnit
            // 
            this.txtPricePerUnit.Location = new System.Drawing.Point(107, 98);
            this.txtPricePerUnit.Name = "txtPricePerUnit";
            this.txtPricePerUnit.Size = new System.Drawing.Size(235, 20);
            this.txtPricePerUnit.TabIndex = 1;
            this.txtPricePerUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPricePerUnit.Enter += new System.EventHandler(this.tb_Enter);
            this.txtPricePerUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtPricePerUnit.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(108, 47);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(128, 20);
            this.txtQty.TabIndex = 0;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.Enter += new System.EventHandler(this.tb_Enter);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtQty.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Item :";
            // 
            // cboCatalog
            // 
            this.cboCatalog.FormattingEnabled = true;
            this.cboCatalog.Location = new System.Drawing.Point(108, 20);
            this.cboCatalog.Name = "cboCatalog";
            this.cboCatalog.Size = new System.Drawing.Size(235, 21);
            this.cboCatalog.TabIndex = 38;
            this.cboCatalog.SelectedIndexChanged += new System.EventHandler(this.cboCatalog_SelectedIndexChanged);
            // 
            // cboUnit
            // 
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Location = new System.Drawing.Point(280, 47);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(63, 21);
            this.cboUnit.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Unit :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Jumlah(Colly) :";
            // 
            // txtJumlahColi
            // 
            this.txtJumlahColi.Location = new System.Drawing.Point(107, 73);
            this.txtJumlahColi.Name = "txtJumlahColi";
            this.txtJumlahColi.Size = new System.Drawing.Size(128, 20);
            this.txtJumlahColi.TabIndex = 44;
            this.txtJumlahColi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtJumlahColi.Enter += new System.EventHandler(this.tb_Enter);
            this.txtJumlahColi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtJumlahColi.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // ctlMaster1
            // 
            this.ctlMaster1.CancelButtonEnabled = true;
            this.ctlMaster1.CancelButtonText = null;
            this.ctlMaster1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlMaster1.IsLookup = false;
            this.ctlMaster1.Location = new System.Drawing.Point(0, 127);
            this.ctlMaster1.Name = "ctlMaster1";
            this.ctlMaster1.SaveButtonEnabled = true;
            this.ctlMaster1.SaveButtonText = null;
            this.ctlMaster1.SaveButtonVisible = true;
            this.ctlMaster1.Size = new System.Drawing.Size(356, 45);
            this.ctlMaster1.TabIndex = 5;
            this.ctlMaster1.Load += new System.EventHandler(this.ctlMaster1_Load);
            // 
            // frmPurchaseDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 172);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtJumlahColi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboUnit);
            this.Controls.Add(this.cboCatalog);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ctlMaster1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPricePerUnit);
            this.Controls.Add(this.txtQty);
            this.Name = "frmPurchaseDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock Detail";
            this.Load += new System.EventHandler(this.frmPurchaseDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPricePerUnit;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label6;
        private Control.ctlTransButton ctlMaster1;
        private System.Windows.Forms.ComboBox cboCatalog;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlahColi;
    }
}