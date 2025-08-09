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
    public partial class frmSaleReturn : MasterPage, IMasterHeader, ITransButton
    {
        public override void _Bayar()
        {
            if (IsValid())
                Save();
            base._Bayar();
        }

        public override void _Add()
        {
            Add();
            base._Add();
        }
        public override void _Edit()
        {
            Edit();
            base._Edit();
        }

        public override void _Delete()
        {
            Delete();
            base._Delete();
        }

        public frmSaleReturn()
        {
            InitializeComponent();

            this.btnTambah.Image = global::CV.SumberRezeki.Properties.Resources.add;
            this.btnEdit.Image = global::CV.SumberRezeki.Properties.Resources.pencil;
            this.btnHapus.Image = global::CV.SumberRezeki.Properties.Resources.delete;
            //this.KeyPreview = true; this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);

            txtUsername.ReadOnly = true;
            grid.AutoGenerateColumns = false;
            //txtCustomerARAmount.TextAlign = HorizontalAlignment.Right;
            //txtCustomerARInstallment.TextAlign = HorizontalAlignment.Right;
            //txtCustomerAPAmount.TextAlign = HorizontalAlignment.Right;
        }

        public string SaleReturnNo { get; set; }


        private void frm_Load(object sender, EventArgs e)
        {
            List<Variable> list = SaleItem.GetAllSalesCounter();
            list.Insert(0, new Variable() { code = "", display = "", sequence = 0 });
            cboNoStruk.DataSource = list.OrderBy(t => t.display).ToList();
            cboNoStruk.DisplayMember = "display";
            cboNoStruk.ValueMember = "code";

            txtUsername.Text = Utilities.CurrentUser.Username;
            SaleReturn item = SaleReturnItem.GetByCode(SaleReturnNo);
            if (item != null)
            {
                txtTransDate.Value = item.ReturnDate;
                txtTransDate.Enabled = false;
                cboNoStruk.SelectedValue = item.Counter.ToString();
                cboNoStruk.Enabled = false;
                toolStrip1.Visible = false;
                LoadDetail(item.Details);
            }

            if (IsView)
            {
                //btnSearch.Enabled = false;
                //this.pnlheader.Enabled = false;
                //this.ctlHeader1.Enabled = false;
                this.ctlTransButton1.SaveButtonEnabled = false;
                this.ctlTransButton1.CancelButtonText = "Close";

            }
        }

        void LoadDetail(List<SaleReturnDetail> list)
        {
            //List<SaleReturnDetail> _list = new List<SaleReturnDetail>();
            //decimal total = 0;
            //if (list != null)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        //list[i].Price = list[i].Price;
            //        //list[i].TotalPrice = Convert.ToDecimal(list[i].Qty) * (list[i].Price);
            //        total += list[i].TotalPrice;
            //    }
            //}
            //txtGrandTotal.Text = Utilities.FormatToMoney(total);
            grid.DataSource = null;
            grid.DataSource = list;
        }

        public bool IsValid()
        {
            try
            {
                List<SaleReturnDetail> details = (List<SaleReturnDetail>)grid.DataSource;
                if (txtTransDate.Value.Year <= 1900)
                {
                    Utilities.ShowValidation("Maaf, Tanggal tidak valid");
                    txtTransDate.Focus();
                    return false;
                }
                else if (details == null || details.Count == 0)
                {
                    Utilities.ShowValidation("Maaf, silahkan input catalog yang akan diisikan ke dalam stock");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        public void Cancel()
        {
            if (Modal) this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else this.Close();
        }

        public void Add()
        {
            int counter = 0;
            if (cboNoStruk.SelectedIndex <= 0)
            {
                Utilities.ShowValidation("Anda harus menentukan nomor struk terlebih dahulu");
                return;
            }
            if (!int.TryParse(string.Format("{0}", cboNoStruk.SelectedValue), out counter))
            {
                Utilities.ShowValidation("Anda harus menentukan nomor struk terlebih dahulu");
                return;
            }

            if (counter <= 0)
            {
                Utilities.ShowValidation("Anda harus menentukan nomor struk terlebih dahulu");
                return;
            }
            List<SaleReturnDetail> list = (List<SaleReturnDetail>)grid.DataSource;
            frmSaleReturnDetail fb = new frmSaleReturnDetail();
            fb.SaleCounter = counter;
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (list == null)
                {
                    list = new List<SaleReturnDetail>();
                }

                if (fb.Detail != null)
                {
                    if (fb.Detail.Sequence <= 0) fb.Detail.Sequence = list.Count + 1;
                }

                list.Add(fb.Detail);
            }

            LoadDetail(list);
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;

            string uniqueID = string.Format("{0}", grid["colUniqueID", Row].Value);
            List<SaleReturnDetail> list = (List<SaleReturnDetail>)grid.DataSource;
            SaleReturnDetail selected = list.Where(t => string.Format("{0}", t.UniqueID) == uniqueID).FirstOrDefault();
            frmSaleReturnDetail fb = new frmSaleReturnDetail();

            fb.Detail = selected;
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (string.Format("{0}", list[i].UniqueID) == uniqueID)
                        {
                            list[i] = fb.Detail;
                        }
                    }
                }
            }
            LoadDetail(list);
        }

        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string code = string.Format("{0}", grid["colUniqueID", Row].Value);
                List<SaleReturnDetail> list = (List<SaleReturnDetail>)grid.DataSource;
                if (list == null) list = new List<SaleReturnDetail>();
                else
                {
                    SaleReturnDetail selected = list.Where(t => string.Format("{0}", t.UniqueID) == code).FirstOrDefault();
                    if (selected != null)
                    {
                        list.Remove(selected);
                    }
                }

                LoadDetail(list);
            }
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (btn.Text.ToLower() == "edit") Edit();
            else if (btn.Text.ToLower() == "add") Add();
            else if (btn.Text.ToLower() == "delete") Delete();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void LoadSale(string strukNo)
        {
            int counter = 0;
            int.TryParse(strukNo, out counter);
            Sale item = SaleItem.GetByCounter(counter);
            if (item == null)
            {
                Utilities.ShowInformation(string.Format("Tidak ada transaksi dengan no struk : {0}", strukNo));
                return;
            }
            txtSaleNo.Text = item.TransactionID;
            txtSaleDate.Text = string.Format("{0:dd MMMM yyyy}", item.TransactionDate);
            
            Customer customer = CustomerItem.GetByID(item.MemberID.Value);
            if (customer != null)
            {
                txtCustomer.Text = customer.FullName;
                txtCustomerCode.Text = customer.ID.ToString();
                txtAddress.Text = customer.Address;
            }

            List<SaleReturnDetail> list = new List<SaleReturnDetail>();// item.Details;
            LoadDetail(list);
        }

        public void Search()
        {
            // do nothing
        }



        public void Save()
        {
            List<SaleReturnDetail> details = (List<SaleReturnDetail>)grid.DataSource;

            int counter = 0;
            int.TryParse(string.Format("{0}", cboNoStruk.SelectedValue), out counter);
            string CustomerCode = txtCustomerCode.Text;
            decimal totalQty = 0;
            decimal totalPrice = 0;

            SaleReturn item = SaleReturnItem.GetByCode(SaleReturnNo);
            if (item == null)
            {
                item = new SaleReturn();
                item.Counter = counter;
                item.CreatedBy = Utilities.Username;
                item.ReturnDate = txtTransDate.Value;
                int index = SaleReturnItem.GetIndexPerdate(item.ReturnDate);
                item.ReturnNo = string.Format("{0}-{1}", item.ReturnDate.ToString(Utilities.FORMAT_DateTime_Flat), index);
                item.TransactionID = txtSaleNo.Text;
                item.Customer = CustomerCode;
                item.Terminal = Utilities.GetIpAddress();
            }
            else
            {
                item.ReturnDate = txtTransDate.Value;
                item.Customer = CustomerCode;
                item.Terminal = Utilities.GetIpAddress();
            }
            item.Details = details;
            foreach (SaleReturnDetail detail in item.Details)
            {
                totalPrice += detail.TotalPrice;
                totalQty += detail.Qty;
            }
            item.TotalPrice = totalPrice;
            item.TotalQty = totalQty;

            List<string> unitList = details.Select(t => t.Unit).Distinct().ToList();
            item.Notes = string.Empty;
            foreach (string unit in unitList)
            {
                decimal? unitQuantity = details.Where(t => t.Unit == unit).Sum(t => t.Qty);
                if (unit == unitList.Last())
                    item.Notes += unitQuantity.HasValue ? string.Format("{0:N2}({1})", unitQuantity.Value, unit) : string.Empty;
                else
                    item.Notes += unitQuantity.HasValue ? string.Format("{0:N2}({1}),", unitQuantity.Value, unit) : string.Empty;
            }

            int result = SaleReturnItem.Insert(item);
            if (result > 0)
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public bool IsView { get; set; }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void cboNoStruk_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo != null)
            {
                if (cbo.SelectedValue != null && cbo.SelectedValue.GetType() == typeof(string))
                {
                    string code = string.Format("{0}", cbo.SelectedValue);
                    LoadSale(code);
                }
            }
        }


    }
}

