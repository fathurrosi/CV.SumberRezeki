using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataObject;
using DataLayer;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Sendang.Rejeki.Report
{
    public partial class frmMonthlySales : Form
    {
        public frmMonthlySales()
        {
            InitializeComponent();
        }

        public class DateRange
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public double TotalDays { get; set; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int selectedYear = 0;
            int.TryParse(cboTahun.Text, out selectedYear);
            if (selectedYear <= 0)
            {
                Utilities.ShowValidation("Tahun harus dipilih!");
                return;
            }
            int tahun = selectedYear;
            
            DateTime date = new DateTime(tahun, 1, 15);
            DateTime dateTo = new DateTime(tahun, 12, 15);

            if (date > dateTo)
            {
                Utilities.ShowInformation("Bulan hingga harus lebih besar dari bulan dari");
                return;
            }

            double totalday = (date - dateTo).TotalDays;
            DateRange rng = new DateRange()
            {
                To = dateTo,
                From = date,
                TotalDays = totalday
            };


            //dataGridView1.DataSource = list;
            textBoxStatus.Text = "Please waiiiittt...!!";
            progressStatus.UseWaitCursor = true;

            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            btnApply.Enabled = false;
            bgWorker.RunWorkerAsync(rng);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedYear = 0;
            int.TryParse(cboTahun.Text, out selectedYear);
            if (selectedYear <= 0)
            {
                Utilities.ShowValidation("Tahun harus dipilih!");
                return;
            }


            List<SumGrossProfit> list = GrossProfitItem.GetSumGrossProfitYearly(new DateTime(selectedYear, 1, 1));

            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            //string title = string.Format("Total Sales of {0}", selectedYear);

            //rptViewer.Params.Add(new ReportParameter("title", title, false));
            rptViewer.ReportName = "MonthlySales";
            rptViewer.ReportText = "Total Sales Per Year";
            rptViewer.ReportPath = string.Format("{0}\\Report\\MonthlySales.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.MdiParent = this.ParentForm;

            rptViewer.Show();
        }


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<SumGrossProfit> result = new List<SumGrossProfit>();
            BackgroundWorker worker = sender as BackgroundWorker;
            DateRange rng = (DateRange)e.Argument;
            for (; rng.From <= rng.To; )
            {
                DateTime month = rng.From;
                List<DailyGrossProfit> sources = HPPItem.GetGrossProfitPermonth(month);
                List<DateTime> dtList = sources.OrderBy(t => t.TransDate.Date).Select(t => t.TransDate.Date).Distinct().ToList();
                if (dtList.Count > 0)
                {
                    DateTime _start = dtList.Min();
                    DateTime _end = dtList.Max();

                    int total = (int)(_end - _start).TotalDays + 1;
                    int i = 0;
                    for (; _start <= _end; )
                    {
                        decimal progress = (((decimal)(i + 1)) / total) * 100;
                        DateTime _currentDate = _start;

                        List<DailyGrossProfit> items = sources.Where(t => t.TransDate.Date == _currentDate.Date).ToList();
                        foreach (DailyGrossProfit item in items)
                        {
                            GrossProfitItem.SaveDaily(item.TransDate, item.ItemID, item.Quantity, item.Purchase, item.Sale, item.Sale - item.Purchase);
                            worker.ReportProgress((int)progress, string.Format("Perhitungan Harga Pokok Penjualan tanggal : {1:dd-MMM-yyyy} untuk {0} ..", item.Item, _currentDate));
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        i++;
                        _start = _start.AddDays(1);
                    }                    
                }
                rng.From = rng.From.AddMonths(1);
            }

            result.AddRange(GrossProfitItem.GetSumGrossProfitYearly(rng.To));
            e.Result = result;
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textBoxStatus.Text = string.Format("{0}% - {1}", e.ProgressPercentage, e.UserState.ToString());
            progressStatus.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressStatus.UseWaitCursor = false;
            btnApply.Enabled = false;
            if (e.Error != null)
            {
                btnApply.Enabled = true;
                MessageBox.Show("Error: " + e.Error.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Cancelled)
            {
                btnApply.Enabled = true;
                //MessageBox.Show("Word counting canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnApply.Enabled = true;
                //MessageBox.Show("Finished", "Result Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            progressStatus.UseWaitCursor = false;
            //txtProgress.Text = "Finished!";

            List<SumGrossProfit> list = (List<SumGrossProfit>)e.Result;
            list = list.OrderBy(t => t.Sequence).ToList();
            dataGridView1.DataSource = list;//.OrderBy(t => t.Sequence);
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {

            int selectedYear = DateTime.Now.Year;
            int.TryParse(cboTahun.Text, out selectedYear);

            if (selectedYear <= 0)
            {
                Utilities.ShowValidation("Tahun harus dipilih!");
                return;
            }


            List<SumGrossProfit> list = GrossProfitItem.GetSumGrossProfitYearly(new DateTime(selectedYear, 1, 1));

            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            //string title = string.Format("Total Sales of {0}", selectedYear);

            //rptViewer.Params.Add(new ReportParameter("title", title, false));
            rptViewer.ReportName = "SumGrossProfit";
            rptViewer.ReportText = "Total Sales Per Year";
            rptViewer.ReportPath = string.Format("{0}\\Report\\MonthlySalesExcelReport.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.MdiParent = this.ParentForm;

            rptViewer.Show();
        }

        private void frmMonthlySales_Load(object sender, EventArgs e)
        {
            int start = DateTime.Now.Year - 5;
            for (int i = start; i < start + 10; i++)
            {
                cboTahun.Items.Add(i.ToString());
            }
        }


    }
}
