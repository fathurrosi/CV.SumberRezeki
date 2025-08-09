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

namespace Sendang.Rejeki.Transaction
{
    public partial class frmSaleReturnDetail : MasterPage, ITransButton
    {
        public int SaleCounter { get; set; }
        public string ReturnNo { get; set; }
        public frmSaleReturnDetail()
        {
            InitializeComponent();
            txtSearch.KeyUp += new KeyEventHandler(txtSearch_KeyUp);

        }

        public override void _OK()
        {
            if (IsValid())
                Save();
            base._OK();
        }

        private SaleReturnDetail _Detail;

        public SaleReturnDetail Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }

        public string CustomerAddress { get; set; }
        public string CustomerCode { get; set; }

        public bool IsValid()
        {
            try
            {
                if (grid.CurrentRow == null)
                {
                    Utilities.ShowValidation("Barang harus dipilih");
                    txtSearch.Focus();
                }
                int Row = grid.CurrentRow.Index;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        public void Save()
        {

            try
            {
                if (Detail == null)
                {
                    _Detail = new SaleReturnDetail();
                    _Detail.UniqueID = Guid.NewGuid();
                }
                _Detail.CreatedBy = Utilities.Username;
                _Detail.CreatedDate = DateTime.Now;


                int Row = grid.CurrentRow.Index;
                int CatalogId = 0;
                int.TryParse(string.Format("{0}", grid["colCatalogID", Row].Value), out CatalogId);


                Sale sItem = SaleItem.GetByCounter(SaleCounter);
                if (sItem == null)
                {
                    sItem.Details = new List<SaleDetail>();
                }



                SaleDetail detailOnSales = sItem.Details.Where(t => t.CatalogID == CatalogId).FirstOrDefault();
                if (detailOnSales == null)
                {
                    Utilities.ShowValidation("Barang harus dipilih");
                    return;
                }


                frmReturnQty frmQty = new frmReturnQty();
                //frmQty.lblQty.Text = string.Format("Masukan Jumlah Untuk Produk Ini (Max - {0}) :", Utilities.ToString(detailOnSales.Quantity, "N2"));
                frmQty.MaxQty = detailOnSales.Quantity;
                frmQty.MaxColly = detailOnSales.Coli;
                if (frmQty.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _Detail.Colly = frmQty.Colly;
                    _Detail.Qty = frmQty.Quantity;
                    if (_Detail.Qty > frmQty.MaxQty)
                    {
                        Utilities.ShowValidation("Tidak boleh melebihi Qty penjualan");
                        return;
                    }
                }
                else
                {
                    return;
                }
                Catalog item = CatalogItem.GetByID(CatalogId);
                _Detail.Item = item;
                _Detail.Price = detailOnSales.Price;
                _Detail.TotalPrice = _Detail.Qty * _Detail.Price;
                if (item != null)
                {
                    _Detail.CatalogName = item.Name;
                    _Detail.Catalog = item.ID.ToString();
                    _Detail.Unit = detailOnSales.Unit;
                    //_Detail.UnitName = detailOnSales.UnitName;
                    _Detail.ReturnNo = ReturnNo;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frm_Load(object sender, EventArgs e)
        {
            LoadGrid(txtSearch.Text);
            txtSearch.Focus();
        }


        void LoadGrid(string text)
        {
            grid.AutoGenerateColumns = false;
            Sale sItem = SaleItem.GetByCounter(SaleCounter);
            if (sItem == null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }

            List<SaleDetail> list = sItem.Details;
            grid.DataSource = (from t in list
                               select new { CatalogID = t.CatalogID, Name = t.CatalogName, t.Unit, t.Quantity, t.Price, t.Coli })
                               .Where(t => string.Format("{0}", t.Name).ToLower().Contains(text.ToLower())).ToList();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.ActiveControl = grid;
            }
            else
            {
                LoadGrid(txtSearch.Text);
            }
        }

    }
}
