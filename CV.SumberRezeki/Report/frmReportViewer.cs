using DataLayer;
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

namespace Sendang.Rejeki.Report
{
    public partial class frmReportViewer : MasterPage
    {
        public frmReportViewer()
        {
            InitializeComponent();
            Params = new List<ReportParameter>();

            this.reportViewer.PrintingBegin += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.reportViewer_PrintingBegin);
        }

        public object DataSource { get; set; }
        public string ReportPath { get; set; }
        public string ReportName { get; set; }
        public List<ReportParameter> Params { get; set; }

        public string ReportText { get; set; }
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0}", ReportText).Length > 0 ? ReportText : ReportName;
            //this.ClientSize = new System.Drawing.Size(950, 600);

            // Set Processing Mode.
            reportViewer.ProcessingMode = ProcessingMode.Local;

            //System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 400, 400);
            //reportViewer.SetPageSettings(pageSettings);

            //System.Drawing.Printing.PaperSize pageSize = reportViewer.PrinterSettings.DefaultPageSettings.PaperSize;//= new System.Drawing.Printing.PaperSize("Custom", 650, 325);
            //PaperSize("Custom", 650, 325)

            
var ps = new System.Drawing.Printing.PageSettings();
ps.Landscape = false;

//ReportViewer1.SetPageSettings(ps)
//Me.ReportViewer1.RefreshReport()


            
            // Set RDL file.            
            //reportViewer.PrinterSettings.DefaultPageSettings.Landscape = false;
            reportViewer.LocalReport.ReportPath = ReportPath;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = DataSource;
            ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
            if (Params != null)
                reportViewer.LocalReport.SetParameters(Params);
            reportViewer.LocalReport.DataSources.Add(rptSource);
            //reportViewer.SetPageSettings(ps);
            this.reportViewer.RefreshReport();
        }

        void reportViewer_PrintingBegin(object sender, ReportPrintEventArgs e)
        {
            e.PrinterSettings.DefaultPageSettings.Landscape = false;
        }

        public override void _OK()
        {
            this.reportViewer.PrintDialog();
            base._OK();
        }
    }
}
