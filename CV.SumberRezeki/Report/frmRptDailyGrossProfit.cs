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
using DataObject;
using DataObject.Cstm;
using LogicLayer.Helper;

namespace Sendang.Rejeki.Report
{
    public partial class frmRptDailyGrossProfit : Form
    {
        public frmRptDailyGrossProfit()
        {
            InitializeComponent();
        }

        private void frmRptDailyGrossProfit_Load(object sender, EventArgs e)
        {
            this.Text = "Total Sales per Customer Monthly";
            dtYear.Format = DateTimePickerFormat.Custom;
            dtYear.CustomFormat = "yyyy";
            dtYear.ShowUpDown = true;
            dtYear.Value = DateTime.Now;

            cboBulan.DataSource = Utilities.GetAllMonth();
            cboBulan.ValueMember = "ID";
            cboBulan.DisplayMember = "Name";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int selectedMonth = 0;
            int.TryParse(string.Format("{0}", cboBulan.SelectedValue), out selectedMonth);
            if (selectedMonth <= 0)
            {
                Utilities.ShowValidation("Pilih bulan!");
                return;
            }

            DateTime date = new DateTime(dtYear.Value.Year, selectedMonth, 1);
            List<DailyGrossProfit> list = HPPItem.GetGrossProfitPermonth(date);
            if (list.Count == 0)
            {
                Utilities.ShowInformation(string.Format("Tidak ada penjualan di bulan {0:MMM yyyy}", date));
                return;
            }

            btnApply.Enabled = false;
            //dataGridView1.DataSource = list;
            //txtProgress.Text = "Please waiiiittt...!!";
            progressStatus.UseWaitCursor = true;

            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;

            bgWorker.RunWorkerAsync(list);

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedMonth = 0;
            int.TryParse(string.Format("{0}", cboBulan.SelectedValue), out selectedMonth);
            if (selectedMonth <= 0)
            {
                Utilities.ShowValidation("Pilih bulan!");
                return;
            }
            DateTime date = new DateTime(dtYear.Value.Year, selectedMonth, 1);
            List<TotalSale> totalSaleList = HPPItem.GetTotalSalePermonth(date);

            List<DailyGrossProfit> list = HPPItem.GetGPPermonth(date);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    DailyGrossProfit item = list[i];
            //    decimal totalSale = totalSaleList.Where(t => t.CatalogID == item.ItemID && t.TransDate.Date == item.TransDate.Date).Select(t => t.Amount).FirstOrDefault();
            //    list[i].Sale= totalSale;

            //}
            string path = string.Empty;
            Excel.WriteExcel("xlsx", list,totalSaleList, out path);

            frmDialogResult f = new frmDialogResult();
            f.Message = "Excel sudah di buat di :";
            f.Result = path;
            f.Title = "Daily Gross Profit";
            f.ShowDialog();
        }


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<DailyGrossProfit> sources = (List<DailyGrossProfit>)e.Argument;
            List<DateTime> dtList = sources.OrderBy(t => t.TransDate.Date).Select(t => t.TransDate.Date).Distinct().ToList();


            if (dtList.Count > 0)
            {

                DateTime _start = dtList.Min();
                DateTime _end = dtList.Max();

                List<TotalSale> totalSaleList = HPPItem.GetTotalSalePermonth(_start);

                int total = (int)(_end - _start).TotalDays + 1;
                int i = 0;
                for (; _start <= _end; )
                {
                    decimal progress = (((decimal)(i + 1)) / total) * 100;
                    DateTime _currentDate = _start;
                    GrossProfitItem.DeleteDaily(_currentDate);
                    List<DailyGrossProfit> items = sources.Where(t => t.TransDate.Date == _currentDate.Date).ToList();
                    foreach (DailyGrossProfit item in items)
                    {
                        decimal totalSale = 0;// totalSaleList.Where(t => t.CatalogID == item.ItemID && t.TransDate.Date == item.TransDate.Date).Select(t => t.Amount).FirstOrDefault();
                        GrossProfitItem.SaveDaily(item.TransDate, item.ItemID, item.Quantity, item.Purchase, totalSale, item.Sale - item.Purchase);
                        worker.ReportProgress((int)progress, string.Format("Perhitungan Harga Pokok Penjualan untuk {0} tanggal :{1:dd-MMM-yyyy} di mulai..", item.Item, _currentDate));
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    i++;
                    _start = _start.AddDays(1);
                }

                e.Result = GrossProfitItem.GetGrossProfit(_start);
            }
            else
            {
                e.Result = new List<DailyGrossProfit>();
            }

        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //txtProgress.Text = string.Format("{0}% - {1}", e.ProgressPercentage, e.UserState.ToString());
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

            List<DailyGrossProfit> list = (List<DailyGrossProfit>)e.Result;
            dataGridView1.DataSource = list.OrderBy(t => t.TransDate).ToList();
        }

    }
}
