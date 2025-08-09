namespace Sendang.Rejeki.Transaction
{
    partial class frmUpdateExpiredDate
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
            this.ctlTransButton1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTransDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // ctlTransButton1
            // 
            this.ctlTransButton1.CancelButtonEnabled = true;
            this.ctlTransButton1.CancelButtonText = null;
            this.ctlTransButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlTransButton1.IsLookup = false;
            this.ctlTransButton1.Location = new System.Drawing.Point(0, 32);
            this.ctlTransButton1.Name = "ctlTransButton1";
            this.ctlTransButton1.SaveButtonEnabled = true;
            this.ctlTransButton1.SaveButtonText = null;
            this.ctlTransButton1.Size = new System.Drawing.Size(274, 45);
            this.ctlTransButton1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Jatuh Tempo :";
            // 
            // txtTransDate
            // 
            this.txtTransDate.CustomFormat = "dd-MMM-yyyy";
            this.txtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTransDate.Location = new System.Drawing.Point(92, 12);
            this.txtTransDate.Name = "txtTransDate";
            this.txtTransDate.Size = new System.Drawing.Size(167, 20);
            this.txtTransDate.TabIndex = 29;
            // 
            // frmUpdateExpiredDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 77);
            this.ControlBox = false;
            this.Controls.Add(this.txtTransDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctlTransButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmUpdateExpiredDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            this.Load += new System.EventHandler(this.frmUpdateExpiredDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control.ctlTransButton ctlTransButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtTransDate;
    }
}