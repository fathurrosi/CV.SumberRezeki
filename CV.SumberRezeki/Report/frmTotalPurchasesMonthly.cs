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
using System.IO;
using Microsoft.Reporting.WinForms;
using DataObject;

namespace Sendang.Rejeki.Report
{
    public partial class frmTotalPurchasesMonthly : Form
    {
        public frmTotalPurchasesMonthly()
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = "Total Purchases Monthly";
            dtYear.Format = DateTimePickerFormat.Custom;
            dtYear.CustomFormat = "yyyy";
            dtYear.ShowUpDown = true;
            dtYear.Value = DateTime.Now;

            //cboSupplier.DataSource = SupplierItem.GetAll().OrderBy(t => t.Name).ToList();
            //cboSupplier.ValueMember = "Code";
            //cboSupplier.DisplayMember = "Name";

            cboBulan.DataSource = Utilities.GetAllMonth();
            cboBulan.ValueMember = "ID";
            cboBulan.DisplayMember = "Name";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int selectedYear = dtYear.Value.Year;
            int selectedMonth = 1;
            if (!int.TryParse(cboBulan.SelectedValue.ToString(), out selectedMonth))
            {
                Utilities.ShowValidation("Bulan harus dipilih!");
                return;
            }
            //string supplierCode = string.Format("{0}", cboSupplier.SelectedValue);
            DateTime thisMonth = new DateTime(selectedYear, selectedMonth, 1);
            dataGridView1.DataSource = PurchaseDetailItem.GetTotalPurchasesMonthly(thisMonth);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedYear = dtYear.Value.Year;
            //string supplierCode = string.Format("{0}", cboSupplier.SelectedValue);
            int selectedMonth = 1;
            if (!int.TryParse(cboBulan.SelectedValue.ToString(), out selectedMonth))
            {
                Utilities.ShowValidation("Bulan harus dipilih!");
                return;
            }

            DateTime thisMonth = new DateTime(selectedYear, selectedMonth, 1);
            List<CstmPurchasesPerSupplierMonthly> list = PurchaseDetailItem.GetTotalPurchasesMonthly(thisMonth);

            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            string title = string.Format("Total Purchases Monthly of ({0:MMM yyyy})", thisMonth.Date);

            rptViewer.Params.Add(new ReportParameter("title", title, false));
            rptViewer.ReportName = "DataSet1";
            rptViewer.ReportText = "Total Sales Per Customer Monthly";
            rptViewer.ReportPath = string.Format("{0}\\Report\\TotalPurchasesPerSupplierMonthly.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.Show();
        }
    }
}
