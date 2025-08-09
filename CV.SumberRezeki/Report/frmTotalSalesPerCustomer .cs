using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using System.IO;
using DataObject;

namespace Sendang.Rejeki.Report
{
    public partial class frmTotalSalesPerCustomer : Form
    {
        public frmTotalSalesPerCustomer()
        {
            InitializeComponent();
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
            int customerID = (int)cboCustomer.SelectedValue;
            DateTime thisMonth = new DateTime(selectedYear, selectedMonth, 1);
            dataGridView1.DataSource = SaleDetailItem.GetTotalSalesPerCustomerMonthly(customerID, thisMonth);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedYear = dtYear.Value.Year;
            int customerID = (int)cboCustomer.SelectedValue;
            int selectedMonth = 1;
            if (!int.TryParse(cboBulan.SelectedValue.ToString(), out selectedMonth))
            {
                Utilities.ShowValidation("Bulan harus dipilih!");
                return;
            }

            DateTime thisMonth = new DateTime(selectedYear, selectedMonth, 1);
            List<CstmSalesPerCustomerMonthly> list = SaleDetailItem.GetTotalSalesPerCustomerMonthly(customerID, thisMonth);

            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            string title = string.Format("Total Sales Monthly of {0} ({1:MMM yyyy})", cboCustomer.Text, thisMonth.Date);

            rptViewer.Params.Add(new ReportParameter("title", title, false));
            rptViewer.ReportName = "DataSet1";
            rptViewer.ReportText = "Total Sales Per Customer Monthly";
            rptViewer.ReportPath = string.Format("{0}\\Report\\TotalSalesPerCustomerMonthly.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.MdiParent = this.ParentForm;
            rptViewer.Show();
        }

        private void frmTotalSalesPerCustomer_Load(object sender, EventArgs e)
        {
            this.Text = "Total Sales per Customer Monthly";
            dtYear.Format = DateTimePickerFormat.Custom;
            dtYear.CustomFormat = "yyyy";
            dtYear.ShowUpDown = true;
            dtYear.Value = DateTime.Now;

            cboCustomer.DataSource = CustomerItem.GetAll().OrderBy(t => t.FullName).ToList();
            cboCustomer.ValueMember = "ID";
            cboCustomer.DisplayMember = "FullName";

            cboBulan.DataSource = Utilities.GetAllMonth();
            cboBulan.ValueMember = "ID";
            cboBulan.DisplayMember = "Name";
        }
    }
}
