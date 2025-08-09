namespace Sendang.Rejeki.Master
{
    partial class frmUserList
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
            this.ctlFooter2 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader2 = new Sendang.Rejeki.Control.ctlHeader();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColiSActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColiSLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlFooter2
            // 
            this.ctlFooter2.BackColor = System.Drawing.SystemColors.Control;
            this.ctlFooter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter2.Location = new System.Drawing.Point(0, 375);
            this.ctlFooter2.Name = "ctlFooter2";
            this.ctlFooter2.Offset = 0;
            this.ctlFooter2.PageIndex = 1;
            this.ctlFooter2.Size = new System.Drawing.Size(963, 26);
            this.ctlFooter2.TabIndex = 0;
            this.ctlFooter2.TotalRows = 0;
            // 
            // ctlHeader2
            // 
            this.ctlHeader2.DeleteButton = null;
            this.ctlHeader2.DeleteButtonEnabled = true;
            this.ctlHeader2.DeleteButtonText = "Delete";
            this.ctlHeader2.DeleteButtonVisible = false;
            this.ctlHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader2.EditButton = null;
            this.ctlHeader2.EditButtonEnabled = true;
            this.ctlHeader2.EditButtonText = "Lihat";
            this.ctlHeader2.EditButtonVisible = true;
            this.ctlHeader2.IsLookup = false;
            this.ctlHeader2.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader2.Name = "ctlHeader2";
            this.ctlHeader2.NewButton = null;
            this.ctlHeader2.NewButtonEnabled = true;
            this.ctlHeader2.NewButtonText = "Add";
            this.ctlHeader2.NewButtonVisible = false;
            this.ctlHeader2.PrintButtonText = "Report";
            this.ctlHeader2.PrintButtonVisible = false;
            this.ctlHeader2.Size = new System.Drawing.Size(963, 24);
            this.ctlHeader2.TabIndex = 1;
            this.ctlHeader2.TextToSearch = "";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colRoleName,
            this.colLastLogin,
            this.ColiSActive,
            this.ColiSLogin});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 24);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(963, 351);
            this.grid.TabIndex = 3;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "Username";
            this.colCode.HeaderText = "User Name";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.Width = 120;
            // 
            // colRoleName
            // 
            this.colRoleName.DataPropertyName = "RoleName";
            this.colRoleName.HeaderText = "Role Name";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.ReadOnly = true;
            this.colRoleName.Width = 300;
            // 
            // colLastLogin
            // 
            this.colLastLogin.DataPropertyName = "LastLogin";
            dataGridViewCellStyle1.Format = "F";
            dataGridViewCellStyle1.NullValue = null;
            this.colLastLogin.DefaultCellStyle = dataGridViewCellStyle1;
            this.colLastLogin.HeaderText = "Last Login";
            this.colLastLogin.Name = "colLastLogin";
            this.colLastLogin.ReadOnly = true;
            // 
            // ColiSActive
            // 
            this.ColiSActive.DataPropertyName = "IsActive";
            this.ColiSActive.HeaderText = "Active";
            this.ColiSActive.Name = "ColiSActive";
            this.ColiSActive.ReadOnly = true;
            // 
            // ColiSLogin
            // 
            this.ColiSLogin.DataPropertyName = "IsLogin";
            this.ColiSLogin.HeaderText = "Logged In";
            this.ColiSLogin.Name = "ColiSLogin";
            this.ColiSLogin.ReadOnly = true;
            // 
            // frmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 401);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlHeader2);
            this.Controls.Add(this.ctlFooter2);
            this.Name = "frmUserList";
            this.Text = "frmUserList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUserList_FormClosed);
            this.Load += new System.EventHandler(this.frmUserList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private Control.ctlHeader ctlHeader1;
        //private Control.ctlFooter ctlFooter1;
        //private System.Windows.Forms.DataGridView grid;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private Control.ctlFooter ctlFooter2;
        private Control.ctlHeader ctlHeader2;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColiSActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColiSLogin;
    }
}