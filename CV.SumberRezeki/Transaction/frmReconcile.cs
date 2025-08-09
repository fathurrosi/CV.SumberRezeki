using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using DataObject;
using LogicLayer;
using Newtonsoft.Json;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmReconcile : Form
    {
        public frmReconcile()
        {
            InitializeComponent();
        }

        private void frmReconcile_Load(object sender, EventArgs e)
        {
            //List<Catalog> productList = new List<Catalog>();
            List<Catalog> catalogList = CatalogItem.GetAll();
            //productList.AddRange(catalogList);
            cboCatalog.DataSource = null;
            cboCatalog.DisplayMember = "Name"; //Name column of contact datasource
            cboCatalog.ValueMember = "ID";//Value column of contact datasource        
            cboCatalog.DataSource = catalogList.Where(t => string.Format("{0}", t.Type).ToLower() == "Item".ToLower()).ToList();

            catalogList.Insert(0, new Catalog() { ID = 0, Name = "", Type = "Product" });
            cboProduct.DataSource = null;
            cboProduct.DisplayMember = "Name"; //Name column of contact datasource
            cboProduct.ValueMember = "ID";//Value column of contact datasource        
            cboProduct.DataSource = catalogList.Where(t => string.Format("{0}", t.Type).ToLower() == "Product".ToLower()).ToList();

            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(Config.GetCatalogUnit());

            if (!string.IsNullOrEmpty(ReconcileID))
            {
                List<ReconcileDetail> itemDetails = ReconcileItem.GetDetail(Convert.ToInt32(ReconcileID));
                Reconcile item = ReconcileItem.GetByID(Convert.ToInt32(ReconcileID));
                if (item != null)
                {
                    txtTransDate.Text = item.ProccessDate.ToString(Utilities.FORMAT_DateTime);
                }
                //btnProccess.Enabled = false;
                if (itemDetails.Count > 0)
                {
                    ReconcileDetail itemDetail = itemDetails.FirstOrDefault();
                    cboCatalog.SelectedValue = itemDetail.CatalogID;
                    txtDate.Text = itemDetail.CatalogPriceDate.HasValue ? itemDetail.CatalogPriceDate.Value.ToString(Utilities.FORMAT_Date) : string.Empty;

                    txtQty.Text = itemDetail.CatalogQty.ToString();
                    txtPricePerUnit.Text = Utilities.FormatToMoney(itemDetail.CatalogPrice);
                    txtProductPricePerUnit.Text = Utilities.FormatToMoney(itemDetail.CatalogPrice);
                    Catalog cat = CatalogItem.GetByID(itemDetail.CatalogID);
                    if (cat != null)
                    {
                        txtUnit.Text = cat.Unit;
                    }

                    cboProduct.SelectedValue = itemDetail.ProductID;
                    txtProductPricePerUnit.Text = Utilities.FormatToMoney(itemDetail.ProductPrice);
                    txtProductQty.Text = itemDetail.ProductQty.ToString();

                    cat = CatalogItem.GetByID(itemDetail.ProductID);
                    if (cat != null)
                    {
                        cboUnit.SelectedValue = cat.Unit;
                    }
                }

                //cboCatalog.Enabled = false;
                //txtDate.Enabled = false;
                //txtQty.Enabled = false;
                //txtPricePerUnit.Enabled = false;
                //txtCatalogSellPricePerUnit.Enabled = false;
                //txtUnit.Enabled = false;
                //cboProduct.Enabled = false;
                //txtProductPricePerUnit.Enabled = false;
                //txtProductQty.Enabled = false;
                //txtTransDate.Enabled = false;
                //cboUnit.Enabled = false;

            }
        }

        private void cboCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Catalog c = (Catalog)cbo.SelectedItem;
            if (c != null)
            {
                CstmPurchaseDetail detailItem = PurchaseItem.GetCatalogReconcilePrice(c.ID);
                if (detailItem != null)
                {
                    List<CstmPurchasePriceRate> list = PurchaseItem.GetHargaBeliRata(c.ID.ToString(), DateTime.Now);
                    decimal price = list.Sum(t => t.Price) / list.Count;
                    txtPricePerUnit.Text = string.Format("{0:N2}", price);
                    txtDate.Text = string.Format("{0:dd MMM yyyy}", detailItem.PurchaseDate);
                    txtProductPricePerUnit.Text = string.Format("{0:N2}", price);
                }
                else
                {
                    txtPricePerUnit.Text = string.Format("{0:N2}", 0);
                    txtDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
                    txtProductPricePerUnit.Text = string.Format("{0:N2}", 0);
                }
                txtUnit.Text = c.Unit;
            }
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            decimal catQty = 0;
            decimal prodQty = 0;
            decimal catalogPricePerUnit = 0;
            decimal productPricePerUnit = 0;
            if (string.Format("{0}", cboProduct.Text).Length == 0)
            {
                Utilities.ShowValidation("Product tidak boleh kosong!");
                cboProduct.Focus();
                return;
            }
            else if (!decimal.TryParse(txtQty.Text, out catQty))
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboCatalog.SelectedItem.ToString()));
                txtQty.Focus();
                return;
            }
            else if (catQty <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboCatalog.SelectedItem.ToString()));
                txtQty.Focus();
                return;
            }
            else if (txtTransDate.Value == null)
            {
                Utilities.ShowValidation(string.Format("Pilih Tanggal transaksi {0}!", cboProduct.SelectedItem.ToString()));
                txtTransDate.Focus();
                return;
            }
            else if (cboUnit.Text.Trim().Length == 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan satuan {0}!", cboProduct.SelectedItem.ToString()));
                cboUnit.Focus();
                return;
            }
            else if (!decimal.TryParse(txtProductPricePerUnit.Text, out catalogPricePerUnit))
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboCatalog.Text));
                txtProductPricePerUnit.Focus();
                return;
            }

            else if (!decimal.TryParse(txtProductQty.Text, out prodQty))
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboProduct.Text));
                txtProductQty.Focus();
                return;
            }
            else if (prodQty <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboProduct.Text));
                txtProductQty.Focus();
                return;
            }
            else if (!decimal.TryParse(txtProductPricePerUnit.Text, out productPricePerUnit))
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboProduct.Text));
                txtProductPricePerUnit.Focus();
                return;
            }
            else if (productPricePerUnit <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboProduct.Text));
                txtProductPricePerUnit.Focus();
                return;
            }

            Catalog product = null;
            if (cboProduct.SelectedValue == null &&
                string.Format("{0}", cboProduct.Text).Length > 0)
            {
                product = CatalogItem.GetCatalog(cboProduct.Text.Trim(), cboUnit.Text.Trim());
                if (product == null)
                {
                    product = CatalogItem.Insert(cboProduct.Text, cboUnit.Text, string.Empty, string.Empty, null, Utilities.Username, "Product");
                    Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(product)));
                    List<Catalog> productList = (List<Catalog>)cboProduct.DataSource;
                    productList.Add(product);
                    cboProduct.DataSource = null;
                    cboProduct.DataSource = productList.OrderBy(t => t.Name).ToList();
                    cboProduct.SelectedValue = product.ID;
                }
            }
            else
            {
                product = (Catalog)cboProduct.SelectedItem;
            }


            ReconcileDetail detail = new ReconcileDetail();
            detail.CatalogID = (int)cboCatalog.SelectedValue;
            Catalog cat = (Catalog)cboCatalog.SelectedItem;
            detail.CatalogPrice = catalogPricePerUnit;
            detail.CatalogQty = catQty;
            detail.CreatedDate = DateTime.Now;
            detail.CreatedBy = Utilities.Username;
            detail.CatalogUnit = cat.Unit;
            try
            {
                detail.CatalogPriceDate = Convert.ToDateTime(txtDate.Text);
            }
            catch (Exception)
            {
                detail.CatalogPriceDate = (DateTime?)null;
            }
            detail.ProductID = product.ID;
            detail.ProductPrice = productPricePerUnit;
            detail.ProductQty = prodQty;
            detail.ProductUnit = product.Unit;
            string desc = string.Format("{0:N2}{1} {2} ----> {3:N2}{4} {5}.", catQty, cat.Unit, cat.ToString(), prodQty, product.Unit, product.ToString());
            DialogResult dialogResult = MessageBox.Show("Are you sure want to procces this?\nProccessing this would update current stock", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(ReconcileID))
                {
                    int deleteResult = ReconcileItem.Delete(ReconcileID);
                    if (deleteResult > 0)
                    {
                        Reconcile result = ReconcileItem.Insert(txtTransDate.Value, desc, detail);
                        if (result != null)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            Utilities.ShowValidation("Done!");
                        }
                        else
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            Utilities.ShowValidation("Failed!");
                        }
                    }
                }
                else
                {

                    Reconcile result = ReconcileItem.Insert(txtTransDate.Value, desc, detail);
                    if (result != null)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        Utilities.ShowValidation("Done!");
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        Utilities.ShowValidation("Failed!");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Catalog c = (Catalog)cbo.SelectedItem;
            if (c != null && c.ID > 0)
            {
                cboUnit.Text = c.Unit;
                cboUnit.Enabled = false;
            }
            else
            {
                cboUnit.Text = "";
                cboUnit.Enabled = true;
            }
        }


        void tb_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.RawNumberFormat(tb.Text);
            }
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
            Recalculate();
        }

        private void Recalculate()
        {
            //decimal productQty = 0;
            //decimal productPricePerUnit = 0;
            //decimal catalogSellPricePerUnit = 0;
            //decimal catalogQty = 0;
            //decimal.TryParse(txtQty.Text, out catalogQty);
            //decimal.TryParse(txtProductQty.Text, out productQty);
            ////decimal.TryParse(txtProductPricePerUnit.Text, out productPricePerUnit);
            //decimal.TryParse(txtCatalogSellPricePerUnit.Text, out catalogSellPricePerUnit);

            //productPricePerUnit = (productQty > 0) ? (catalogSellPricePerUnit * catalogQty) / productQty : 0;
            //if (productPricePerUnit > 0)
            //{
            //    txtProductPricePerUnit.Text = Utilities.CorrectFormat(string.Format("{0}", productPricePerUnit));
            //}

        }
        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb != null)
            {
                tb.Text = (string.Format("{0}", tb.Name).ToLower() == "txtProductQty".ToLower() || string.Format("{0}", tb.Name).ToLower() == "txtQty".ToLower()) ? Utilities.CorrectFormat(tb.Text, "N2") : Utilities.CorrectFormat(tb.Text);
                if (tb.Name == "txtProductQty" || tb.Name == "txtCatalogSellPricePerUnit")
                {
                    Recalculate();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        public string ReconcileID { get; set; }
    }
}
