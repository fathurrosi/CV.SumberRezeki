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
    public partial class frmReturnQty : MasterPage
    {

        public decimal Quantity { get; set; }
        public decimal MaxColly { get; set; }
        public decimal MaxQty { get; set; }
        public decimal Colly { get; set; }
        public frmReturnQty()
        {
            InitializeComponent();
            txtQty.Leave += new EventHandler(tb_Leave);
            txtQty.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            txtQty.Enter += new EventHandler(tb_Enter);

            txtColly.Leave += new EventHandler(tb_Leave);
            txtColly.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            txtColly.Enter += new EventHandler(tb_Enter);
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void tb_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                decimal number = Utilities.ToDecimal(tb.Text);
                tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
                if (number > 0 && tb.Text.Contains(Utilities.CommaSeparator))
                {
                    tb.SelectionStart = tb.Text.Length - 2;
                }
                else
                {
                    tb.SelectionStart = tb.Text.Length - 3;
                }
                tb.SelectionLength = 0;
            }


        }

        private void tb_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.RawNumberFormat(tb.Text);
            }
        }

        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
            }
        }


        public override void _OK()
        {
            btnOK_Click(null, null);
            base._OK();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            try
            {
                Quantity = Utilities.ToDecimal(txtQty.Text);
                Colly = Utilities.ToDecimal(txtColly.Text);
                if (Quantity == 0)
                {
                    Utilities.ShowValidation("Tolong masukan jumlah barang yang benar!");
                    txtQty.Text = "0";
                    txtQty.Focus();
                    isValid = false;
                }
            }
            catch (Exception)
            {
                Utilities.ShowValidation("Tolong masukan jumlah barang yang benar!");
                txtQty.Text = "0";
                txtQty.Focus();
                isValid = false;
            }


            if (MaxQty < Quantity)
            {
                Utilities.ShowValidation("Tidak boleh melebihi Qty penjualan!");
                txtQty.Text = "0";
                txtQty.Focus();
                isValid = false;
            }

            Colly = Utilities.ToDecimal(txtColly.Text);
            //if (MaxColly < Colly)
            //{
            //    Utilities.ShowValidation("Tidak boleh melebihi Colly penjualan!");
            //    txtColly.Focus();
            //    txtColly.Text = "0";
            //    isValid = false;
            //}


            if (isValid)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Quantity = 0;
            Colly = 0;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmPurchaseQty_Load(object sender, EventArgs e)
        {
            if (MaxQty <= 0)
            {
                //lblQty.Text = "Barang ini tidak ada dalam struk penjualan.";
                txtQty.Enabled = false;
                txtColly.Enabled = false;
                btnOK.Enabled = false;
            }
        }
    }
}
