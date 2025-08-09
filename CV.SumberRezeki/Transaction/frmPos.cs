using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataLayer;
using Sendang.Rejeki.Lookup;
using DataObject;
using System.IO;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;

using System.Drawing.Printing;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmPos : MasterPage
    {
        public class PosHelper
        {
            public int Index { get; set; }
            public string CatalogName { get; set; }
            public int CatalogID { get; set; }
            public string Qty { get; set; }

            public string Unit { get; set; }
            public string Price { get; set; }
            public string SubTotal { get; set; }
            public string Coli { get; set; }


        }

        #region Funtions

        private void Recalculate()
        {
            txtReturn.Text = "0";
            grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            int rowIndex = grid.CurrentCell != null ? grid.CurrentCell.RowIndex : -1;
            if (rowIndex > -1)
            {
                object qty = grid.Rows[rowIndex].Cells[3].Value;
                object price = grid.Rows[rowIndex].Cells[6].Value;

                decimal tempQty = 0;
                decimal tempPrice = 0;
                decimal.TryParse(string.Format("{0}", qty), out tempQty);
                decimal.TryParse(string.Format("{0}", price), out tempPrice);


                decimal total = tempQty * tempPrice;
                grid.Rows[rowIndex].Cells[7].Value = Utilities.ToString(total);

                decimal totalPrice = 0;
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    tempQty = 0;
                    tempPrice = 0;
                    total = 0;
                    qty = grid.Rows[i].Cells[3].Value;
                    price = grid.Rows[i].Cells[6].Value;

                    decimal.TryParse(string.Format("{0}", qty), out tempQty);
                    decimal.TryParse(string.Format("{0}", price), out tempPrice);
                    total = tempQty * tempPrice;
                    totalPrice += total;
                }

                txtTotalPrice.Text = Utilities.ToString(totalPrice);
                if (!string.IsNullOrEmpty(TransactionID))
                {
                    RecalculatePayment();
                }
            }
        }

        //private void GetCatalog(Catalog catalog)
        //{
        //    List<PosHelper> helperList = (List<PosHelper>)grid.DataSource;
        //    int qty = 1;
        //    int rowIndex = 1;
        //    DataGridViewCell cell = grid.CurrentCell;
        //    CatalogPrice pPrice = CatalogPriceItem.GetByCatalogID(catalog.ID);
        //    //decimal discount = GetDiscount(catalog.ProductCode, qty);
        //    if (cell != null) rowIndex = cell.RowIndex + 1;

        //    for (int i = 0; i < helperList.Count; i++)
        //    {
        //        if (helperList[i].Index == rowIndex)
        //        {

        //            helperList[i].CatalogID = catalog.ID;
        //            helperList[i].Price = string.Format("{0}", pPrice == null ? 0 : pPrice.SellPrice);
        //            helperList[i].CatalogName = catalog.Name;
        //            helperList[i].Qty = qty.ToString();
        //            break;
        //        }
        //    }
        //    grid.DataSource = null;
        //    grid.DataSource = helperList.OrderBy(t => t.Index).ToList();
        //}

        #endregion

        public frmPos()
        {
            InitializeComponent();
        }

        List<Catalog> catalogList = new List<Catalog>();
        Sale sale = null;
        private void frmPos_Load(object sender, EventArgs e)
        {
            DateTime NOW = DateTime.Now;
            txtTransDate.CustomFormat = Utilities.FORMAT_DateTime;
            txtTransDate.Value = NOW;
            // dibuka karena permintaan ibu Yani
            txtTransDate.Enabled = true;// Utilities.IsSuperAdmin();
            int saleIndex = SaleItem.GetNewIndex(NOW);
            txtTransNo.Text = (string.IsNullOrEmpty(TransactionID)) ? string.Format((saleIndex <= 1000) ? "{0}{1:000}" : "{0}{1:0000}", NOW.ToString(Utilities.FORMAT_Date_Flat), saleIndex) : TransactionID;
            List<Customer> list = CustomerItem.GetAll();
            list.Insert(0, new Customer(0, string.Empty));
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.DataSource = list.OrderBy(t => t.FullName).ToList();

            catalogList = CatalogItem.GetAll();

            cboPaymentType.DataSource = VariableItem.GetBycategory("PeymentType");
            cboPaymentType.ValueMember = "code";
            cboPaymentType.DisplayMember = "display";


            if (!string.IsNullOrEmpty(TransactionID))
            {
                sale = SaleItem.GetByTransID(TransactionID);
                if (sale == null)
                {
                    Utilities.ShowInformation("No transaction found!");
                    this.Enabled = false;
                    return;
                }

                btnSave.Text = "Update";
                txtTransDate.Value = sale.TransactionDate;
                txtTransDate.Enabled = false;
                txtTransNo.Enabled = false;
                txtNotes.Text = sale.Notes;
                txtTotalPrice.Text = Utilities.ToString(sale.TotalPrice);
                txtReturn.Text = Utilities.ToString(sale.TotalPaymentReturn.Value);
                txtTotalPayed.Text = Utilities.ToString(sale.TotalPayment);
                txtPayment.Text = Utilities.ToString(sale.TotalPayment);
                cboPaymentType.SelectedValue = sale.PaymentType;

                Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
                cboCustomer.Text = cust == null ? string.Empty : cust.FullName;
                txtAddress.Text = cust == null ? string.Empty : cust.Address;
                foreach (SaleDetail detailItem in sale.Details)
                {
                    int row = grid.Rows.Add();
                    grid.Rows[row].Cells[0].Value = detailItem.Sequence;
                    grid.Rows[row].Cells[0].ReadOnly = true;

                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "Name"; //Name column of contact datasource
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).ValueMember = "ID";//Value column of contact datasource        
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).DataSource = catalogList;
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).Value = detailItem.CatalogID;
                    grid.Rows[row].Cells[1].ReadOnly = true;

                    grid.Rows[row].Cells[2].Value = detailItem.CatalogID;
                    grid.Rows[row].Cells[3].Value = detailItem.Quantity;
                    grid.Rows[row].Cells[4].Value = detailItem.Unit;
                    grid.Rows[row].Cells[4].ReadOnly = true;

                    grid.Rows[row].Cells[5].Value = detailItem.Coli;
                    grid.Rows[row].Cells[6].Value = detailItem.Price;
                    grid.Rows[row].Cells[7].Value = detailItem.TotalPrice;
                    grid.Rows[row].Cells[8].Value = detailItem.ID;
                }

                txtAddress.Enabled = false;
                grid.BeginEdit(false);
                btnPrint.Enabled = true;
                btnPrintToFile.Enabled = true;
            }
        }

        public override void _Add()
        {
            btnAdd_Click(null, null);
            base._Add();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int row = grid.Rows.Add();
            int totalRow = grid.Rows.Count;
            int maxValue = 0;

            for (int i = 0; i < totalRow; i++)
            {
                int value = 0;
                Int32.TryParse(string.Format("{0}", grid.Rows[i].Cells["colNo"].Value), out value);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }

            grid.Rows[row].Cells["colNo"].Value = (maxValue + 1);
            grid.Rows[row].Cells["colNo"].ReadOnly = true;

            this.grid.CurrentCell = this.grid["colProduct", row];
            grid.BeginEdit(true);
        }

        public override void _Delete()
        {
            btnDelete_Click(null, null);
            base._Delete();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = grid.CurrentRow;
            if (currentRow != null)
            {
                int rowIndex = currentRow.Index;
                grid.Rows.RemoveAt(rowIndex);
                Recalculate();
            }
        }

        #region Grid Event

        #endregion

        public string TransactionID { get; set; }

        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (grid.CurrentCell.ColumnIndex == 3 || grid.CurrentCell.ColumnIndex == 6)
            {
                TextBox tb = (TextBox)e.Control;
                tb.Name = grid.CurrentCell.OwningColumn.Name;
                tb.KeyPress += tb_KeyPress;
                tb.Leave += tb_Leave;
                tb.KeyUp += tb_KeyUp;
                tb.Enter += tb_Enter;
            }
            else if (grid.CurrentCell.ColumnIndex == 1)
            {
                ComboBox cbo = (ComboBox)e.Control;
                cbo.SelectedIndexChanged += cbo_SelectedIndexChanged;
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

        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                //if (string.Format("{0}", tb.Name).ToLower() == "colqty")
                //    tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
                //else if (string.Format("{0}", tb.Name).ToLower() == "colColi".ToLower())
                //{
                tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
                //}
                //else tb.Text = Utilities.CorrectFormat(tb.Text);

                Recalculate();
            }
        }

        void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo != null)
            {
                Catalog cat = (Catalog)cbo.SelectedItem;
                if (cat != null)
                {
                    int rowIndex = grid.CurrentCell.RowIndex;
                    grid.Rows[rowIndex].Cells[2].Value = cat.ID;
                    grid.Rows[rowIndex].Cells[4].Value = cat.Unit;
                    grid.Rows[rowIndex].Cells[4].ReadOnly = true;

                }
            }
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.grid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn) ||
            (this.grid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn))
            {
                this.grid.BeginEdit(false);

                //if (e.ColumnIndex == 4)
                //{
                //    DataGridViewComboBoxCell cbo = (DataGridViewComboBoxCell)(grid.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                //    cbo.Items.Clear();
                //    cbo.Items.AddRange(Config.GetCatalogUnit());
                //}
            }
        }

        private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewComboBoxCell cboCatalog = (DataGridViewComboBoxCell)(grid.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                cboCatalog.DataSource = null;
                cboCatalog.DisplayMember = "Name"; //Name column of contact datasource
                cboCatalog.ValueMember = "ID";//Value column of contact datasource        
                cboCatalog.DataSource = catalogList;
            }
        }

        private void RecalculatePayment()
        {
            decimal payment = 0;
            decimal totalPrice = 0;
            decimal.TryParse(txtPayment.Text, out payment);
            decimal.TryParse(txtTotalPrice.Text, out totalPrice);

            if (payment > 0)
            {
                decimal result = payment - totalPrice;
                txtReturn.Text = Utilities.ToString(result);
            }
        }
        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtPayment_KeyUp(object sender, KeyEventArgs e)
        {
            decimal payment = 0;
            decimal.TryParse(txtPayment.Text, out payment);
            txtTotalPayed.Text = Utilities.ToString(payment);
            RecalculatePayment();
        }
        private void txtPayment_Leave(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Customer cust = (Customer)cbo.SelectedItem;
            if (cust != null)
            {
                txtAddress.Text = cust.Address;
                if (cust.PaymentType != null)
                    cboPaymentType.SelectedValue = cust.PaymentType;
            }
        }

        //public override void _Print()
        //{
        //    btnSave_Click(null, null);
        //    base._Save();
        //}

        public override void _Print()
        {
            btnSave_Click(null, null);
            base._Print();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int paymentTipe = 0;
            int customerID = 0;
            int.TryParse(string.Format("{0}", cboPaymentType.SelectedValue), out paymentTipe);

            int.TryParse(string.Format("{0}", cboCustomer.SelectedValue), out customerID);
            if (customerID <= 0)
            {
                Utilities.ShowValidation("Mohon pilih Customer.");
                cboCustomer.Focus();
                return;
            }

            Sale existingItem = SaleItem.GetByTransID(txtTransNo.Text.Trim());
            if (string.IsNullOrEmpty(TransactionID) && existingItem != null)
            {
                Utilities.ShowValidation("Transaksi sudah ada. Coba ganti nomor transaksi");
                txtTransNo.Focus();
                return;
            }

            if (grid.Rows.Count == 0)
            {
                Utilities.ShowValidation("Mohon isi detail transaksi.");
                return;
            }

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                decimal tempQty = 0;
                decimal tempPrice = 0;
                object qty = grid.Rows[i].Cells[3].Value;
                object objColi = grid.Rows[i].Cells[5].Value;
                object price = grid.Rows[i].Cells[6].Value;
                int catalogID = 0;

                if (!int.TryParse(string.Format("{0}", grid.Rows[i].Cells[2].Value), out catalogID))
                {
                    Utilities.ShowValidation("Catalog tidak boleh kosong!");
                    return;
                }
                else if (!decimal.TryParse(string.Format("{0}", qty), out tempQty))
                {
                    Utilities.ShowValidation("Qty tidak boleh kosong!");
                    return;
                }
                else if (tempQty == 0)
                {
                    Utilities.ShowValidation("Qty tidak boleh nol");
                    return;
                }
                else if (!decimal.TryParse(string.Format("{0}", price), out tempPrice))
                {
                    Utilities.ShowValidation("Harga tidak boleh kosong!");
                    return;
                }
                else if (tempPrice == 0)
                {
                    Utilities.ShowValidation("Harga tidak boleh nol");
                    return;
                }
            }

            if (paymentTipe <= 0)
            {
                Utilities.ShowValidation("Mohon pilih tipe pembayaran.");
                cboPaymentType.Focus();
                return;
            }

            //if (customerID == 0 && cboCustomer.Text.Length > 0)
            //{
            //    //simpan dulu
            //    Customer customer = CustomerItem.Insert(cboCustomer.Text, txtAddress.Text, "", Utilities.Username);
            //    customerID = customer.ID;
            //}

            //int.TryParse(string.Format("{0}", cboPaymentType.SelectedValue), out paymentTipe);
            Sale item = new Sale();
            item.TransactionID = (string.IsNullOrEmpty(TransactionID)) ? txtTransNo.Text : TransactionID;
            item.PaymentType = paymentTipe;
            List<SaleDetail> details = new List<SaleDetail>();
            //decimal totalQty = 0;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                int sequence = Convert.ToInt32(grid.Rows[i].Cells["colNo"].Value);
                SaleDetail detail = new SaleDetail();
                decimal Coli = 0;
                decimal tempQty = 0;
                decimal tempPrice = 0;
                //decimal tempTotalPrice = 0;

                int catalogID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[i].Cells[2].Value), out catalogID);

                object qty = grid.Rows[i].Cells[3].Value;
                decimal.TryParse(string.Format("{0}", qty), out tempQty);

                int ID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[i].Cells[8].Value), out ID);

                decimal.TryParse(string.Format("{0}", grid.Rows[i].Cells[5].Value), out Coli);

                object price = grid.Rows[i].Cells[6].Value;
                decimal.TryParse(string.Format("{0}", price), out tempPrice);

                decimal tempTotalPrice = tempQty * tempPrice;
                //totalQty += tempQty;
                detail.Discount = 0;
                detail.Price = tempPrice;
                detail.Quantity = tempQty;
                detail.TotalPrice = tempTotalPrice;
                detail.TransactionID = item.TransactionID;
                detail.CatalogID = catalogID;
                detail.Sequence = sequence;
                detail.ID = ID;
                detail.Coli = Coli;
                detail.Unit = string.Format("{0}", grid.Rows[i].Cells[4].Value).Trim();
                details.Add(detail);
            }

            item.MemberID = customerID;
            item.Notes = txtNotes.Text;
            item.PaymentType = paymentTipe;
            item.Tax = 0;
            item.Terminal = Utilities.GetComputerName();

            item.Username = Utilities.Username;
            item.TransactionDate = txtTransDate.Value;

            decimal totalPayed = 0;
            decimal.TryParse(txtTotalPayed.Text, out totalPayed);

            item.TotalPrice = decimal.Round(details.Sum(t => t.TotalPrice));
            item.TotalPaymentReturn = totalPayed - item.TotalPrice;
            item.TotalPayment = totalPayed;

            List<string> unitList = details.Select(t => t.Unit).Distinct().ToList();
            foreach (string unit in unitList)
            {
                decimal? unitQuantity = details.Where(t => t.Unit == unit).Sum(t => t.Quantity);
                if (unit == unitList.Last())
                    item.TotalQty += unitQuantity.HasValue ? string.Format("{0:N2}({1})", unitQuantity.Value, unit) : string.Empty;
                else
                    item.TotalQty += unitQuantity.HasValue ? string.Format("{0:N2}({1}),", unitQuantity.Value, unit) : string.Empty;
            }
            item.Details = details;

            int result = -1;

            txtTotalPrice.Text = Utilities.ToString(item.TotalPrice);
            txtReturn.Text = Utilities.ToString(item.TotalPaymentReturn.HasValue ? item.TotalPaymentReturn.Value : 0);
            txtTotalPayed.Text = Utilities.ToString(item.TotalPayment);
            existingItem = SaleItem.GetByTransID(TransactionID);
            if (existingItem == null)
            {
                result = SaleItem.Insert(item);
                if (result > 0)
                {
                    Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item)));
                    sale = SaleItem.GetByTransID(item.TransactionID);
                }
            }
            else
            {

                result = SaleItem.Update(item);
                if (result > 0)
                {
                    Log.Update(string.Format("{0}-{1}", this.Text, string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item))));
                    sale = SaleItem.GetByTransID(item.TransactionID);

                }
            }
            if (result > 0)
            {
                btnPrintToFile_Click(sender, e);

                if (this.Modal)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                Utilities.ShowInformation("Ups..! Mohon maaf data tidak berhasil disimpan");
            }
        }

        //        private int m_currentPageIndex;
        //        private IList<Stream> m_streams;
        //        // Routine to provide to the report renderer, in order to
        //        //    save an image for each page of the report.
        //        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        //        {
        //            Stream stream = new MemoryStream();
        //            m_streams.Add(stream);
        //            return stream;
        //        }

        //        private void Export(LocalReport report)
        //        {
        //            string deviceInfo =
        //              @"<DeviceInfo>
        //                <OutputFormat>EMF</OutputFormat>
        //                <PageWidth>8.5in</PageWidth>
        //                <PageHeight>11in</PageHeight>
        //                <MarginTop>0.25in</MarginTop>
        //                <MarginLeft>0.25in</MarginLeft>
        //                <MarginRight>0.25in</MarginRight>
        //                <MarginBottom>0.25in</MarginBottom>
        //
        //    <PageHeight>21cm</PageHeight>
        //    <PageWidth>14.8cm</PageWidth>
        //    <LeftMargin>0.54cm</LeftMargin>
        //    <RightMargin>0.54cm</RightMargin>
        //    <TopMargin>0.54cm</TopMargin>
        //    <BottomMargin>0.54cm</BottomMargin>
        //    <ColumnSpacing>1.27cm</ColumnSpacing>
        //
        //            </DeviceInfo>";
        //            Warning[] warnings;
        //            List<Stream> streams = new List<Stream>();
        //            report.Render("Image", deviceInfo, CreateStream, out warnings);
        //            foreach (Stream stream in streams)
        //                stream.Position = 0;
        //        }

        void DisableControl()
        {
            btnPrint.Enabled = true;
            btnPrintToFile.Enabled = true;
            toolStrip.Enabled = false;
            btnCancel.Text = "Close";
            btnSave.Enabled = false;
            cboCustomer.Enabled = false;
            cboPaymentType.Enabled = false;
            txtNotes.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtPayment.ReadOnly = true;
            txtTransNo.ReadOnly = true;
            txtTransDate.Enabled = false;
            grid.Enabled = false;
        }

        void ClearCOntrol()
        {
            cboCustomer.SelectedIndex = -1;
            cboPaymentType.SelectedIndex = -1;
            txtNotes.Text = "";
            txtAddress.Text = "";
            txtPayment.Text = "";
            txtTransNo.Text = "";
            txtTransDate.Text = "";
            //grid.DataSource = null;

            //grid.DataSource = null;
            //grid.DataSource = new List<PosHelper>();
            //int row = grid.Rows.Add();
            //int totalRow = grid.Rows.Count;
            //for (int i = 0; i < totalRow; i++) {
            //    var row = grid.Rows[i];
            grid.Rows.Clear();
            //Recalculate();
            //}


            btnAdd_Click(null, null);
            btnDelete_Click(null, null);

            txtReturn.Text = "";
            txtTotalPayed.Text = "";
            txtTotalPrice.Text = "";
            txtTransNo.Text = "";
            txtTransDate.Value = DateTime.Now;

            catalogList = new List<Catalog>();
            sale = null;
            frmPos_Load(null, null);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    PrintDocument pd = new PrintDocument();
            //    pd.PrinterSettings.DefaultPageSettings.Landscape = false;
            //    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            //    pd.Print();
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex.ToString());
            //}
        }

        //private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        //{
        //    Font printFont = new Font("Calibri", 10);
        //    int maxRecord = 11;
        //    List<SaleDetail> details = new List<SaleDetail>();
        //    details = sale.Details;
        //    if (details.Count < maxRecord)
        //    {
        //        int max = details.Select(t => t.Sequence).Max();
        //        int loop = maxRecord - details.Count;
        //        for (int i = 0; i < loop; i++)
        //        {
        //            max++;
        //            SaleDetail sd = new SaleDetail();
        //            sd.Sequence = max;
        //            details.Add(sd);
        //        }
        //    }

        //    /*****************************************************************
        //     * HEADER
        //     * **************************************************************/
        //    float topMargin = Config.TopMargin();
        //    if (Config.GetIsUsingTemplateFromat())
        //    {
        //        #region MENGGUNAKAN FORMAT
        //        #region HEADER
        //        float y = 8 + topMargin;
        //        float w = 575;
        //        ev.Graphics.DrawString(sale.TransactionDate.ToString(Utilities.FORMAT_DateTime), printFont, Brushes.Black, w, y);
        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString(sale.TransactionID, printFont, Brushes.Black, w, y);
        //        y += printFont.GetHeight(ev.Graphics);
        //        Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
        //        string customer = cust == null ? string.Empty : cust.FullName;
        //        string address = cust == null ? string.Empty : cust.Address;
        //        int custLength = address.Length >= 35 ? address.Length / 35 : 1;
        //        if (custLength > 3) custLength = 3;
        //        ev.Graphics.DrawString(customer, printFont, Brushes.Black, w, y);
        //        for (int i = 0; i < custLength; i++)
        //        {
        //            y += printFont.GetHeight(ev.Graphics);
        //            int start = i == 0 ? 0 : (i * 35);
        //            int end = address.Length > 35 ? i == 0 ? 35 : (i + 1) * 35 : address.Length;
        //            int sisa = address.Length - end;
        //            if (sisa < 35)
        //            {
        //                sisa = address.Length;
        //            }
        //            string s = address.Substring(start, (sisa < 35) ? sisa : 35);
        //            ev.Graphics.DrawString(s, printFont, Brushes.Black, w, y);

        //        }
        //        #endregion

        //        y = 152.1107845F + topMargin;
        //        float yTotal = 355.561371F + topMargin;
        //        float xNo = Config.GetX_No();
        //        float xCatalogName = Config.GetX_Item();
        //        float xColly = Config.GetX_Colly();
        //        float xQTY = Config.GetX_Qty();
        //        float xPrice = Config.GetX_Harga();
        //        //float xQTY = 570;            
        //        //float xPrice = 670;
        //        float xTotalPrice = Config.GetX_TotalHarga();
        //        List<SaleDetail> detals = sale.Details.OrderBy(t => t.Sequence).ToList();
        //        decimal totalPrice = 0;
        //        foreach (SaleDetail detail in details)
        //        {
        //            if (string.IsNullOrEmpty(detail.CatalogName)) continue;
        //            ev.Graphics.DrawString(string.Format("{0}", detail.Sequence), printFont, Brushes.Black, xNo, y);
        //            //ev.Graphics.DrawString(string.Format("{0}  /  {1:N0} Colly", detail.CatalogName, detail.Coli), printFont, Brushes.Black, xCatalogName, y);
        //            ev.Graphics.DrawString(string.Format("{0}", detail.CatalogName), printFont, Brushes.Black, xCatalogName, y);

        //            StringFormat stringFormat = new StringFormat();
        //            stringFormat.Alignment = StringAlignment.Far;

        //            ev.Graphics.DrawString(Utilities.ToString(detail.Coli, "N2"), printFont, Brushes.Black, xColly, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Quantity, "N2"), printFont, Brushes.Black, xQTY, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Price), printFont, Brushes.Black, xPrice, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Quantity * detail.Price), printFont, Brushes.Black, xTotalPrice, y, stringFormat);

        //            totalPrice += detail.Quantity * detail.Price;
        //            y += printFont.GetHeight(ev.Graphics);

        //        }
        //        ev.Graphics.DrawString(Utilities.ToString(totalPrice), printFont, Brushes.Black, xTotalPrice, yTotal, new StringFormat() { Alignment = StringAlignment.Far });
        //        // If more lines exist, print another page.
        //        //if (line != null)
        //        //ev.HasMorePages = true;
        //        //else
        //        ev.HasMorePages = false;
        //        #endregion
        //    }
        //    else
        //    {
        //        float xNo = Config.GetX_No();
        //        float xCatalogName = Config.GetX_Item();
        //        float xColly = Config.GetX_Colly();
        //        float xQTY = Config.GetX_Qty();
        //        float xPrice = Config.GetX_Harga();
        //        float xTotalPrice = Config.GetX_TotalHarga();

        //        #region FORMAT BEBAS LENGKAP DENGAN LABEL
        //        #region HEADER
        //        float y = 8 + topMargin;
        //        float wLabel = 570;
        //        float w = 575;

        //        //                string HEADER = @"
        //        //SUMBER REZEKI INTERNASIONAL
        //        //Jl. Cendrawasih Raya, No.45 Rt./Rw. 02/03
        //        //Kel. Sawah Baru Bintaro - Tangerang Selatan 15413
        //        //Hp. 0859 2158 9354 - 0812 1227 3837
        //        //    087777 1217 036 - 021 7463 6905
        //        //Web   : www.sumberrezekiint.com www.ayamkarkasfrozen.com
        //        //email : heryanisri.hs@gmail.com
        //        //";
        //        ev.Graphics.DrawString("SUMBER REZEKI INTERNASIONAL", new Font("Calibri", 14), Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        y += printFont.GetHeight(ev.Graphics);

        //        ev.Graphics.DrawString("Jl. Cendrawasih Raya, No.45 Rt./Rw. 02/03", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        ev.Graphics.DrawString("Tanggal : ", printFont, Brushes.Black, wLabel, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        ev.Graphics.DrawString(sale.TransactionDate.ToString(Utilities.FORMAT_DateTime), printFont, Brushes.Black, w, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        y += printFont.GetHeight(ev.Graphics);

        //        ev.Graphics.DrawString("Kel. Sawah Baru Bintaro - Tangerang Selatan 15413", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        ev.Graphics.DrawString("NOTA No : ", printFont, Brushes.Black, wLabel, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        ev.Graphics.DrawString(sale.TransactionID, printFont, Brushes.Black, w, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        y += printFont.GetHeight(ev.Graphics);

        //        ev.Graphics.DrawString("Hp. 0859 2158 9354 - 0812 1227 3837", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        ev.Graphics.DrawString("Kepada Yth : ", printFont, Brushes.Black, wLabel, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
        //        string customer = cust == null ? string.Empty : cust.FullName;
        //        string address = cust == null ? string.Empty : cust.Address;
        //        int maxAddressLength = 100;
        //        int custLength = address.Length >= maxAddressLength ? address.Length / maxAddressLength : 1;
        //        if (custLength > 3) custLength = 3;
        //        ev.Graphics.DrawString(customer, printFont, Brushes.Black, w, y, new StringFormat() { Alignment = StringAlignment.Near });

        //        float y1 = y;

        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("     087777 1217 036 - 021 7463 6905", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });

        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("Web   : www.sumberrezekiint.com www.ayamkarkasfrozen.com", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("email : heryanisri.hs@gmail.com", printFont, Brushes.Black, xNo, y, new StringFormat() { Alignment = StringAlignment.Near });


        //        for (int i = 0; i < custLength; i++)
        //        {
        //            y1 += printFont.GetHeight(ev.Graphics);
        //            int start = i == 0 ? 0 : (i * maxAddressLength);
        //            int end = address.Length > maxAddressLength ? i == 0 ? maxAddressLength : (i + 1) * maxAddressLength : address.Length;
        //            int sisa = address.Length - end;
        //            if (sisa < maxAddressLength)
        //            {
        //                sisa = address.Length;
        //            }
        //            string s = address.Substring(start, (sisa < maxAddressLength) ? sisa : maxAddressLength);
        //            ev.Graphics.DrawString(s, printFont, Brushes.Black, w, y1, new StringFormat() { Alignment = StringAlignment.Near });

        //        }
        //        #endregion

        //        //y = 152.1107845F + topMargin;
        //        y += printFont.GetHeight(ev.Graphics);
        //        y += printFont.GetHeight(ev.Graphics);
        //        y += printFont.GetHeight(ev.Graphics);

        //        //float yTotal = 355.561371F + topMargin;


        //        #region MEMBUAT KOLOM
        //        ev.Graphics.DrawString(string.Format("NO."), printFont, Brushes.Black, xNo, y);
        //        //ev.Graphics.DrawString(string.Format("{0}  /  {1:N0} Colly", detail.CatalogName, detail.Coli), printFont, Brushes.Black, xCatalogName, y);
        //        ev.Graphics.DrawString(string.Format("NAMA BARANG"), printFont, Brushes.Black, xCatalogName, y);

        //        StringFormat stringFormat = new StringFormat();
        //        stringFormat.Alignment = StringAlignment.Far;

        //        ev.Graphics.DrawString("COLLY", printFont, Brushes.Black, xColly, y, stringFormat);
        //        ev.Graphics.DrawString("QUANTITY", printFont, Brushes.Black, xQTY, y, stringFormat);
        //        ev.Graphics.DrawString("HARGA SATUAN", printFont, Brushes.Black, xPrice, y, stringFormat);
        //        ev.Graphics.DrawString("JUMLAH", printFont, Brushes.Black, xTotalPrice, y, stringFormat);

        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawLine(new Pen(Brushes.Black), xNo, y, xTotalPrice, y);
        //        y += 1;
        //        ev.Graphics.DrawLine(new Pen(Brushes.Black), xNo, y, xTotalPrice, y);

        //        #endregion

        //        List<SaleDetail> detals = sale.Details.OrderBy(t => t.Sequence).ToList();
        //        decimal totalPrice = 0;
        //        foreach (SaleDetail detail in details)
        //        {
        //            if (string.IsNullOrEmpty(detail.CatalogName)) continue;
        //            ev.Graphics.DrawString(string.Format("{0}", detail.Sequence), printFont, Brushes.Black, xNo, y);
        //            //ev.Graphics.DrawString(string.Format("{0}  /  {1:N0} Colly", detail.CatalogName, detail.Coli), printFont, Brushes.Black, xCatalogName, y);
        //            ev.Graphics.DrawString(string.Format("{0}", detail.CatalogName), printFont, Brushes.Black, xCatalogName, y);

        //            stringFormat = new StringFormat();
        //            stringFormat.Alignment = StringAlignment.Far;

        //            ev.Graphics.DrawString(Utilities.ToString(detail.Coli, "N2"), printFont, Brushes.Black, xColly, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Quantity, "N2"), printFont, Brushes.Black, xQTY, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Price), printFont, Brushes.Black, xPrice, y, stringFormat);
        //            ev.Graphics.DrawString(Utilities.ToString(detail.Quantity * detail.Price), printFont, Brushes.Black, xTotalPrice, y, stringFormat);

        //            totalPrice += detail.Quantity * detail.Price;
        //            y += printFont.GetHeight(ev.Graphics);

        //        }
        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawLine(new Pen(Brushes.Black), xNo, y, xTotalPrice, y);

        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("JUMLAH :", printFont, Brushes.Black, xPrice, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        ev.Graphics.DrawString(Utilities.ToString(totalPrice), printFont, Brushes.Black, xTotalPrice, y, new StringFormat() { Alignment = StringAlignment.Far });

        //        ev.Graphics.DrawString("Penerima", printFont, Brushes.Black, 80, y, new StringFormat() { Alignment = StringAlignment.Center });
        //        ev.Graphics.DrawString("Driver", printFont, Brushes.Black, 270, y, new StringFormat() { Alignment = StringAlignment.Center });
        //        ev.Graphics.DrawString("Hormat Kami", printFont, Brushes.Black, 460, y, new StringFormat() { Alignment = StringAlignment.Center });



        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("UANG MUKA :", printFont, Brushes.Black, xPrice, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        ev.Graphics.DrawString(Utilities.ToString(sale.TotalPayment), printFont, Brushes.Black, xTotalPrice, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        //ev.Graphics.DrawString("Barang yang sudah dibeli", printFont, Brushes.Black, xCatalogName, y, new StringFormat() { Alignment = StringAlignment.Near });


        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("SISA :", printFont, Brushes.Black, xPrice, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        decimal piutang = sale.TotalPaymentReturn.HasValue ? sale.TotalPaymentReturn.Value : 0;
        //        if (piutang > 0)
        //            piutang = 0;
        //        ev.Graphics.DrawString(Utilities.ToString(piutang * -1), printFont, Brushes.Black, xTotalPrice, y, new StringFormat() { Alignment = StringAlignment.Far });
        //        //ev.Graphics.DrawString("tidak dapat dikembalikan", printFont, Brushes.Black, xCatalogName, y, new StringFormat() { Alignment = StringAlignment.Near });


        //        y += printFont.GetHeight(ev.Graphics);
        //        //ev.Graphics.DrawString("atau ditukar kecuali ada", printFont, Brushes.Black, xCatalogName, y, new StringFormat() { Alignment = StringAlignment.Near });
        //        y += printFont.GetHeight(ev.Graphics);
        //        ev.Graphics.DrawString("(............................)", printFont, Brushes.Black, 80, y, new StringFormat() { Alignment = StringAlignment.Center });
        //        ev.Graphics.DrawString("(............................)", printFont, Brushes.Black, 270, y, new StringFormat() { Alignment = StringAlignment.Center });
        //        ev.Graphics.DrawString("(............................)", printFont, Brushes.Black, 460, y, new StringFormat() { Alignment = StringAlignment.Center });


        //        // If more lines exist, print another page.
        //        //if (line != null)
        //        //ev.HasMorePages = true;
        //        //else
        //        ev.HasMorePages = false;
        //        #endregion
        //    }
        //}

        private void txtTransDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime NOW = txtTransDate.Value;
            int saleIndex = SaleItem.GetNewIndex(NOW);
            if (string.IsNullOrEmpty(TransactionID))
                txtTransNo.Text = string.Format((saleIndex <= 1000) ? "{0}{1:000}" : "{0}{1:0000}", NOW.ToString(Utilities.FORMAT_Date_Flat), saleIndex);

        }


        private void btnPrintToFile_Click(object sender, EventArgs e)
        {
            this.Hide();
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Receipt.rdlc";
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Print.rdlc";
            Report.frmReportViewer f = new Report.frmReportViewer();

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("TransDate", txtTransDate.Text.Length > 0 ? txtTransDate.Text : " ", true));
            parameters.Add(new ReportParameter("TransNo", txtTransNo.Text.Length > 0 ? txtTransNo.Text : " ", true));
            parameters.Add(new ReportParameter("CustomerName", cboCustomer.Text.Length > 0 ? cboCustomer.Text : " ", true));
            parameters.Add(new ReportParameter("CustomerAddress", txtAddress.Text.Length > 0 ? txtAddress.Text : " ", true));
            parameters.Add(new ReportParameter("Note", txtNotes.Text.Length > 0 ? txtNotes.Text : " ", true));
            parameters.Add(new ReportParameter("TotalPrice", txtTotalPrice.Text.Length > 0 ? txtTotalPrice.Text : " ", true));
            parameters.Add(new ReportParameter("TotalPayed", txtTotalPayed.Text.Length > 0 ? txtTotalPayed.Text : " ", true));
            parameters.Add(new ReportParameter("TotalReturn", txtReturn.Text.Length > 0 ? txtReturn.Text : " ", true));

            Profile profile = ProfileItem.GetProfile();

            parameters.Add(new ReportParameter("pNamaPerusahaan", string.Format("{0}", profile == null ? " " : profile.Nama), false));
            parameters.Add(new ReportParameter("pBidangPerusahaan", string.Format("{0}", profile == null ? " " : profile.Keterangan), false));
            parameters.Add(new ReportParameter("pAlamatPerusahaan", string.Format("{0}", profile == null ? " " : profile.Alamat), false));
            string telp = " ";
            if (profile != null)
            {
                if (!string.IsNullOrEmpty(profile.Telp1) && !string.IsNullOrEmpty(profile.Telp2))
                    telp = string.Format("Telp. {0} / {1}", profile.Telp1, profile.Telp2);
                else if (string.IsNullOrEmpty(profile.Telp1) && !string.IsNullOrEmpty(profile.Telp2))
                    telp = string.Format("Telp. {0}", profile.Telp2);
                else if (!string.IsNullOrEmpty(profile.Telp1) && string.IsNullOrEmpty(profile.Telp2))
                    telp = string.Format("Telp. {0}", profile.Telp1);
            }

            parameters.Add(new ReportParameter("pAlamatPerusahaan2", string.Format("{0}", profile == null ? " " : profile.Alamat2), false));
            parameters.Add(new ReportParameter("pLabelAlamat", string.Format("{0}", profile == null ? " " : profile.LabelAlamat), false));
            parameters.Add(new ReportParameter("pLabelAlamat2", string.Format("{0}", profile == null ? " " : profile.LabelAlamat2), false));
            parameters.Add(new ReportParameter("pPhone", telp, false));
            parameters.Add(new ReportParameter("pWeb", string.Format("{0}", profile == null ? " " : profile.web), false));
            parameters.Add(new ReportParameter("pEmail", string.Format("{0}", profile == null ? " " : profile.email), false));
            parameters.Add(new ReportParameter("pInstagram", string.Format("{0}", profile == null ? " " : profile.instagram), false));


            f.ReportName = "Sale";
            f.ReportPath = reportPath;

            f.DataSource = sale.Details;
            f.Params = parameters;
            f.ShowDialog();
            ClearCOntrol();
            this.Show();
        }

    }
}
