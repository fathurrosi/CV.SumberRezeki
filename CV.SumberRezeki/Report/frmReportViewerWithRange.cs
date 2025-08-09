using Microsoft.Reporting.WinForms;
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
namespace Sendang.Rejeki.Report
{
    public partial class frmReportViewerWithRange : Form
    {
        public frmReportViewerWithRange()
        {
            InitializeComponent(); Params = new List<ReportParameter>();
        }

        public object DataSource { get; set; }
        public string ReportPath { get; set; }
        public string ReportName { get; set; }
        public ReportType RptType { get; set; }
        public List<ReportParameter> Params { get; set; }

        private void frmReportViewerWithRange_Load(object sender, EventArgs e)
        {
            object _DataSource = DataSource;
            //dtEnd.Format = DateTimePickerFormat.Custom;
            //dtEnd.CustomFormat = "MMMM yyyy";
            //dtEnd.ShowUpDown = true;
            //dtStart.Format = DateTimePickerFormat.Custom;
            //dtStart.CustomFormat = "MMMM yyyy";
            //dtStart.ShowUpDown = true;
            this.Text = ReportName;
            this.ClientSize = new System.Drawing.Size(950, 600);

            dtStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            btnApply_Click(null, null);

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Text = ReportName;
            this.ClientSize = new System.Drawing.Size(950, 600);
            object _DataSource = DataSource;
            reportViewer.LocalReport.DataSources.Clear();
            // Set Processing Mode.
            reportViewer.ProcessingMode = ProcessingMode.Local;

            DateTime start = dtStart.Value;// new DateTime(dtStart.Value.Year, dtStart.Value.Month, 1).AddMinutes(-1);
            DateTime end = dtEnd.Value;// new DateTime(dtEnd.Value.Year, dtEnd.Value.Month, DateTime.DaysInMonth(dtEnd.Value.Year, dtEnd.Value.Month)).AddDays(1);

            // Set RDL file.
            reportViewer.LocalReport.ReportPath = ReportPath;
            BindingSource bindingSource = new BindingSource();
            if (RptType == ReportType.Piutang)
            {
                this.Text = "Piutang Report";
                bindingSource.DataSource = SaleItem.GetPiutang(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.DailySalesPerCatalog)
            {
                this.Text = "Daily Sales (Per Catalog) Report";
                bindingSource.DataSource = SaleItem.GetDailySalesPerCatalog(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.DailyPurchases)
            {
                this.Text = "Laporan Pembelian";
                bindingSource.DataSource = SaleItem.GetTotalDailyPurchases(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.TotalDailySale)
            {
                this.Text = "Daily Sales Report";
                bindingSource.DataSource = SaleItem.GetTotalDailySales(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.TotalSalesPerCustomer)
            {
                this.Text = "Total Sales Per Customer Report";
                bindingSource.DataSource = SaleItem.GetTotalSalePerCustomer(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.SalesPerformancePerMonth)
            {
                bindingSource.DataSource = SaleItem.GetPerformancePerMonth(start, end);

                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.TotalSalesPerCatalog)
            {
                this.Text = "Total Catalog Sales Per Month Report";
                bindingSource.DataSource = SaleItem.GetTotalSalePerCatalog(start, end);
                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.MonthlyGrossProfit)
            {
                this.Text = "Monthly Gross Profit Report";
                bindingSource.DataSource = SaleItem.GetMonthlyGrossProfit(start, end);
                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.DailyGrossProfit)
            {
                this.Text = "Daily Gross Profit Report";
                bindingSource.DataSource = SaleItem.GetDailyGrossProfit(start, end);
                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);
            }
            else if (RptType == ReportType.StockDetail)
            {
                this.Text = "Stock Detail Report";
                List<CstmCatalogStock> stockList = CatalogStockItem.GetAll(start, end);

                bindingSource = new BindingSource();
                bindingSource.DataSource = stockList.Where(t => t.TotalSale > 0 || t.TotalPurchase > 0).OrderByDescending(t => t.StockDate).ToList();
                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);

                //bindingSource = new BindingSource();
                //bindingSource.DataSource = stockList.Where(t => string.Format("{0}", t.Unit).ToLower() != "kg" && (t.TotalPurchase > 0 || t.TotalSale > 0)).ToList();
                //rptSource = new ReportDataSource("StockAmpela", bindingSource);
                //reportViewer.LocalReport.DataSources.Add(rptSource);
            }

            this.reportViewer.RefreshReport();
        }
    }
}
