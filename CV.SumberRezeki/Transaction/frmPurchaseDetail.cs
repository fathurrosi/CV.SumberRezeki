using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataObject;
using DataLayer;
using System.Globalization;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmPurchaseDetail : Form, ITransButton
    {
        public frmPurchaseDetail()
        {
            InitializeComponent();
        }

        private PurchaseDetail _Detail;

        public PurchaseDetail Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }

        public bool IsValid()
        {
            try
            {
                if (cboCatalog.SelectedItem == null)
                {
                    Utilities.ShowValidation("Catalog tidak boleh kosong");
                    cboCatalog.Focus();
                    return false;
                }
                else if (txtQty.Text.Length == 0)
                {
                    Utilities.ShowValidation("Quantity tidak boleh kosong");
                    txtQty.Focus();
                    return false;
                }
                else if (txtPricePerUnit.Text.Length == 0)
                {
                    Utilities.ShowValidation("Price per unit tidak boleh kosong");
                    txtPricePerUnit.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogicLayer.Log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        public void Save()
        {
            if (!IsValid()) return;
            try
            {
                if (Detail == null)
                {
                    _Detail = new PurchaseDetail();
                    _Detail.UniqueID = Guid.NewGuid();
                }
                _Detail.CreatedBy = Utilities.Username;
                _Detail.CreatedDate = DateTime.Now;


                _Detail.PricePerUnit = Utilities.ToDecimal(txtPricePerUnit.Text);
                _Detail.Coli = Utilities.ToDecimal(txtJumlahColi.Text);
                Catalog item = (Catalog)cboCatalog.SelectedItem;
                _Detail.Qty = Utilities.ToDecimal(txtQty.Text);
                _Detail.TotalPrice = _Detail.Qty * _Detail.PricePerUnit;
                if (item != null)
                {

                    _Detail.CatalogName = item.Name;
                    _Detail.CatalogID = item.ID;
                    _Detail.Unit = item.Unit;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogicLayer.Log.Error(ex.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmPurchaseDetail_Load(object sender, EventArgs e)
        {
            cboCatalog.DataSource = null;
            List<Catalog> list = CatalogItem.GetItems();
            cboCatalog.DataSource = list;

            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(Config.GetCatalogUnit());

            cboCatalog.DisplayMember = "Name";
            cboCatalog.ValueMember = "ID";
            if (Detail != null)
            {
                cboCatalog.SelectedValue = Detail.CatalogID;
                txtPricePerUnit.Text = Utilities.ToString(Detail.PricePerUnit);
                txtQty.Text = Detail.Qty.ToString();
                txtJumlahColi.Text = Utilities.ToString(Detail.Coli);
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
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

                //tb.Text = Utilities.CorrectFormat(tb.Text);
                //tb.Text = 
                if (string.Format("{0}", tb.Name).ToLower() == "txtqty")
                    tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
                else if (string.Format("{0}", tb.Name).ToLower() == "txtJumlahColi".ToLower())
                {
                    tb.Text = Utilities.CorrectFormat(tb.Text, "N0");
                }

                else tb.Text = Utilities.CorrectFormat(tb.Text);
            }
        }

        private void cboCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Catalog item = (Catalog)cbo.SelectedItem;
            cboUnit.Text = item.Unit;
        }

        private void ctlMaster1_Load(object sender, EventArgs e)
        {

        }

    }
}
