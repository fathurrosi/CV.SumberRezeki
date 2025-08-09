using DataLayer;
using DataObject;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmPosView : Form
    {
        public frmPosView()
        {
            InitializeComponent();
        }

        public string TransactionID { get; set; }
        Sale sale;
        private void frmPosView_Load(object sender, EventArgs e)
        {
            grid.AutoGenerateColumns = false;
            txtTransDate.Text = DateTime.Now.ToString(Utilities.FORMAT_DateTime);
            txtTransNo.Text = TransactionID;

            List<Catalog> catalogList = CatalogItem.GetAllStockedCatalog();
            List<object> paymentList = new List<object>();
            paymentList.Add(new { ID = 1, Name = "Cash" });
            paymentList.Add(new { ID = 2, Name = "Transfer" });
            paymentList.Add(new { ID = 3, Name = "Credit" });

            cboPaymentType.DataSource = paymentList;
            cboPaymentType.ValueMember = "ID";
            cboPaymentType.DisplayMember = "Name";

            if (!string.IsNullOrEmpty(TransactionID))
            {
                sale = SaleItem.GetByTransID(TransactionID);
                //item.MemberID = customerID;
                txtNotes.Text = sale.Notes;
                txtTransDate.Text = sale.TransactionDate.ToString(Utilities.FORMAT_DateTime);

                //           txtTransDate.Text = DateTime.Now.ToString(Utilities.FORMAT_DateTime);
                //   txtTransNo.Text = DateTime.Now.ToString(Utilities.FORMAT_DateTime_Flat);


                txtTotalPrice.Text = sale.TotalPrice.ToString(Utilities.FORMAT_Money);
                txtReturn.Text = sale.TotalPaymentReturn.Value.ToString(Utilities.FORMAT_Money);
                txtTotalPayed.Text = sale.TotalPayment.ToString(Utilities.FORMAT_Money);
                txtPayment.Text = sale.TotalPayment.ToString(Utilities.FORMAT_Money);

                Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
                txtCustomer.Text = cust == null ? string.Empty : cust.FullName;
                txtAddress.Text = cust == null ? string.Empty : cust.Address;
                grid.DataSource = sale.Details;
                DisableControl();
            }

        }

        void DisableControl()
        {
            btnPrint.Enabled = true;
            btnPrintToFile.Enabled = true;
            toolStrip.Enabled = false;
            btnCancel.Text = "Close";
            btnSave.Enabled = false;
            txtCustomer.ReadOnly = true;
            cboPaymentType.Enabled = false;
            txtNotes.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtPayment.ReadOnly = true;
            txtTransNo.ReadOnly = true;
            grid.Enabled = false;
        }

        private void btnPrintToFile_Click(object sender, EventArgs e)
        {
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Receipt.rdlc";
            Report.frmReportViewer f = new Report.frmReportViewer();

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("TransDate", txtTransDate.Text, true));
            parameters.Add(new ReportParameter("TransNo", txtTransNo.Text, true));
            parameters.Add(new ReportParameter("CustomerName", txtCustomer.Text, true));
            parameters.Add(new ReportParameter("CustomerAddress", txtAddress.Text, true));
            parameters.Add(new ReportParameter("Note", txtNotes.Text, true));
            parameters.Add(new ReportParameter("TotalPrice", txtTotalPrice.Text, true));
            parameters.Add(new ReportParameter("TotalPayed", txtTotalPayed.Text, true));
            parameters.Add(new ReportParameter("TotalReturn", txtReturn.Text, true));


            f.ReportName = "Sale";
            f.ReportPath = reportPath;
            f.DataSource = sale.Details;
            f.Params = parameters;
            f.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Receipt.rdlc";

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("TransDate", txtTransDate.Text, true));
            parameters.Add(new ReportParameter("TransNo", txtTransNo.Text, true));
            parameters.Add(new ReportParameter("CustomerName", txtCustomer.Text, true));
            parameters.Add(new ReportParameter("CustomerAddress", txtAddress.Text, true));
            parameters.Add(new ReportParameter("Note", txtNotes.Text, true));
            parameters.Add(new ReportParameter("TotalPrice", txtTotalPrice.Text, true));
            parameters.Add(new ReportParameter("TotalPayed", txtTotalPayed.Text, true));
            parameters.Add(new ReportParameter("TotalReturn", txtReturn.Text, true));

            LogicLayer.Helper.Report rpt = new LogicLayer.Helper.Report();
            rpt.Print(reportPath, "Sale", sale.Details, parameters);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

    }
}
