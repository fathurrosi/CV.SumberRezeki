using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmUpdateExpiredDate : Form, ITransButton
    {
        public frmUpdateExpiredDate()
        {
            InitializeComponent();
        }

        public DateTime ExpiredDate { get; set; }

        private void frmUpdateExpiredDate_Load(object sender, EventArgs e)
        {
            DateTime NOW = DateTime.Now;
            txtTransDate.CustomFormat = Utilities.FORMAT_DateTime;
        }

        public bool IsValid()
        {
            try
            {
                if (txtTransDate.Value < DateTime.Now)
                {
                    Utilities.ShowValidation(string.Format("Tanggal Jatuh Tempo harus lebih besar dari {0}", DateTime.Now.ToString(Utilities.FORMAT_DateTime)));
                    txtTransDate.Focus();
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Save()
        {
            ExpiredDate = txtTransDate.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
