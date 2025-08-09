using DataLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataObject;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmCatalogStock : Form, ITransButton
    {
        public frmCatalogStock()
        {
            InitializeComponent();
        }

        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal CurrentColly { get; set; }
        public string CatalogUnit { get; set; }

        public bool IsValid()
        {
            return true;
        }

        public void Save()
        {
            decimal stock = 0;
            decimal.TryParse(txtStock.Text, out stock);

            decimal colly = 0;
            decimal.TryParse(txtColly.Text, out colly);

            decimal prevStock = 0;
            decimal prevColly = 0;
            CatalogStock cs = CatalogStockItem.GetActiveByCatalogID(CatalogID);
            if (cs != null)
            {
                prevStock = CurrentStock - cs.Stock;
                stock = stock - prevStock;
                
                prevColly = CurrentColly - cs.Colly;
                colly = colly - prevColly;
            }
            else
            {
                stock = stock - CurrentStock;

                colly = colly - CurrentColly;
            }

            int result = CatalogStockItem.UppdateStock(CatalogID, stock, colly, Utilities.Username);
            if (result > 0)
            {
                Log.Update(string.Format("{0}-{1}", this.Text, string.Format("Update stock catalogid : {0}, stock :{1}", CatalogID, stock)));
                Utilities.ShowInformation("Simpan stock berhasil");
                frmPos f = new frmPos();
                f.Close();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmCatalogStock_Load(object sender, EventArgs e)
        {

            txtCatalogName.Text = CatalogName;
            //if (CatalogUnit.ToLower() == "kg")
            //{
            txtStock.Text = CurrentStock == 0 ? string.Empty : string.Format("{0:N2}", CurrentStock);

            txtColly.Text = CurrentColly== 0 ? string.Empty : string.Format("{0:N2}", CurrentColly);
            //}
            //else
            //{
            //    txtStock.Text = Convert.ToInt32(CurrentStock).ToString();
            //}
            txtUnit.Text = CatalogUnit;
        }


        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtUnit.Text.ToLower() == "kg")
            //{
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
            }
            //}
            //else
            //{
            //    if (!Utilities.IsValidNumber(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
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
                //tb.Text = Utilities.CorrectFormat(tb.Text);
                tb.Text = string.Format("{0}", tb.Name).ToLower() == "txtStock".ToLower() ? Utilities.CorrectFormat(tb.Text, "N2") : Utilities.CorrectFormat(tb.Text);
            }
        }

    }
}
