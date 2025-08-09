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
using System.Threading;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmHPP : Form
    {
        public frmHPP()
        {
            InitializeComponent();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<CstmTotalPurchase> sources = (List<CstmTotalPurchase>)e.Argument;
            List<DateTime> dtList = sources.OrderBy(t => t.PurchaseDate.Date).Select(t => t.PurchaseDate.Date).Distinct().ToList();
            List<Catalog> allCatalog = CatalogItem.GetItems();

            DateTime _start = dtList.Min();
            DateTime _end = dtList.Max();

            if (_start < dtRangeEnd.Value)
                _end = dtRangeEnd.Value;

            if (_start < dtRangeStart.Value)
                _start = dtRangeStart.Value;

            HPPItem.Delete(_start, _end);
            int total = (int)(_end - _start).TotalDays + 1;
            int i = 0;
            for (; _start <= _end; )
            {
                decimal progress = (((decimal)(i + 1)) / total) * 100;
                DateTime _currentDate = _start;

                foreach (Catalog catalog in allCatalog)
                {
                    cstmHPP hppKemarin = HPPItem.GetLastHpp(catalog.ID, _currentDate);

                    worker.ReportProgress((int)progress, string.Format("Perhitungan Harga Pokok Penjualan untuk {0} tanggal :{1:dd-MMM-yyyy} di mulai..", catalog.Name, _currentDate));
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    //if (catalog.ID == 72)
                    //{
                    //    string test = catalog.ID.ToString();
                    //}

                    List<CstmTotalPurchase> pNowList = sources.Where(t => t.PurchaseDate.Date == _currentDate.Date && t.CatalogID == catalog.ID).ToList();
                    DateTime dateKemarin = _currentDate.AddDays(-1).Date;
                    //stok kemarin
                    CurrentStock stock = CurrentStockItem.GetStock(catalog.ID, dateKemarin);
                    decimal C7 = (stock == null) ? 0 : stock.Stock;
                    if (C7 < 0)
                    {
                        C7 = 0;
                    }

                    if (pNowList.Count == 0)
                    {
                        List<cstmHPP> hppList = HPPItem.GetHPPPerCatalogByDate(dateKemarin, catalog.ID);
                        foreach (cstmHPP item in hppList)
                        {
                            item.TransDate = _currentDate.Date;
                            worker.ReportProgress((int)progress, string.Format("Copy Harga Pokok Penjualan (HPP) {3} dari Tanggal : {0:dd-MMM-yyyy}, Catalog: {1}, HPP: {2:N2} ", dateKemarin, item.CatalogName, item.HPP, catalog.Name));
                            HPPItem.Save(item, C7, (hppKemarin != null) ? hppKemarin.HPP.Value : 0, 0, 0);
                        }
                    }
                    else
                    {
                        decimal HPP = 0;
                        decimal E7 = 0;
                        decimal E5 = 0;
                        decimal C5 = 0;

                        //pembelian hari ini
                        List<CstmTotalPurchase> pNowCatalogs = pNowList.Where(t => t.CatalogID == catalog.ID).ToList();

                        cstmHPP item = new cstmHPP();
                        if (hppKemarin != null) // jika normal
                        {
                            //C7 = (stock == null) ? 0 : stock.Stock;
                            C5 = pNowCatalogs.Sum(t => t.Quantity);
                            E7 = C7 * hppKemarin.HPP.Value;
                            E5 = pNowCatalogs.Sum(t => t.Quantity * t.PricePerUnit);
                            HPP = (E7 + E5) / (C5 + C7);

                            item.HPP = HPP;
                            item.CatalogID = catalog.ID;
                            item.CatalogName = pNowCatalogs.FirstOrDefault().CatalogName;
                            item.TransDate = _currentDate;


                            HPPItem.Save(item, C7, hppKemarin.HPP.Value, C5, E5);
                            worker.ReportProgress((int)progress, string.Format("Hitung dan Simpan HPP {3} untuk tanggal : {0:dd-MMM-yyyy}, Catalog: {1}, HPP: {2:N2} ", _currentDate, item.CatalogName, item.HPP, catalog.Name));

                        }
                        else // belom ada --> dimulai dari awal
                        {
                            if (pNowCatalogs.Count > 0)
                            {
                                item.CatalogID = catalog.ID;
                                item.CatalogName = pNowCatalogs.FirstOrDefault().CatalogName;
                                item.TransDate = _currentDate;
                                // jumlah hargaPerunit dibagi jumlahItem
                                //item.HPP = pNowCatalogs.Sum(t => t.PricePerUnit) / pNowCatalogs.Count;
                                item.HPP = pNowCatalogs.Sum(t => t.TotalPrice) / pNowCatalogs.Sum(t => t.Quantity);
                                worker.ReportProgress((int)progress, string.Format("Date: {0:dd-MMM-yyyy}, Catalog: {1}, HPP: {2:N2} ", _currentDate, item.CatalogName, item.HPP));

                                HPPItem.Save(item, C7, 0, pNowCatalogs.Sum(t => t.Quantity), pNowCatalogs.Sum(t => t.Quantity * t.PricePerUnit));
                            }
                            else
                            {
                                item.CatalogID = catalog.ID;
                                item.CatalogName = catalog.Name;
                                item.TransDate = _currentDate;
                                item.HPP = 0;
                                worker.ReportProgress((int)progress, string.Format("Date: {0:dd-MMM-yyyy}, Catalog: {1}, HPP: {2:N2} ", _currentDate, item.CatalogName, item.HPP));

                                HPPItem.Save(item, C7, 0, 0, 0);
                            }
                        }

                    } //end of (pNowList.Count == 0)
                }//end form each catalog
                i++;
                _start = _start.AddDays(1);
            }
            //    e.Result = iFileList;
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtProgress.Text = string.Format("{0}% - {1}", e.ProgressPercentage, e.UserState.ToString());
            progressStatus.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressStatus.UseWaitCursor = false;
            btnGenerate.Enabled = false;
            if (e.Error != null)
            {
                btnGenerate.Enabled = true;
                MessageBox.Show("Error: " + e.Error.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Cancelled)
            {
                btnGenerate.Enabled = true;
                MessageBox.Show("Word counting canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnGenerate.Enabled = true;
                MessageBox.Show("Finished", "Result Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            progressStatus.UseWaitCursor = false;
            txtProgress.Text = "Finished!";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new List<cstmHPP>();

            //List<DateTime> dtList = Purchases.Select(t => t.PurchaseDate.Date).Distinct().ToList();
            //int totalRecords = dtList.Count;
            DialogResult result = MessageBox.Show(string.Format("Apakah anda yakin ingin me-generate Harga Pokok Penjualan (HPP)?.\nMelakukan ini akan menghapus data Harga Pokok Penjualan (HPP) sebelumnya dan akan di-generate ulang dimulai dari tanggal {0:dd-MMM-yyyy} hingga tanggal {1:dd-MMM-yyyy}!", dtRangeStart.Value.Date, dtRangeEnd.Value.Date), "Result Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                txtProgress.Text = "Please waiiiittt...!!";
                progressStatus.UseWaitCursor = true;

                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.WorkerReportsProgress = true;

                bgWorker.RunWorkerAsync(Purchases.Where(t => t.PurchaseDate >= dtRangeStart.Value.Date && t.PurchaseDate <= dtRangeEnd.Value.Date).ToList());
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bgWorker.CancelAsync();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int? catalogID = (int?)cboCatalog.SelectedValue;
            //dataGridView1.DataSource = HPPItem.GetAll(dtStart.Value, dtEnd.Value, catalogID);
            List<cstmHPP> list = new List<cstmHPP>();
            if (catalogID.HasValue && catalogID > 0)
            {
                list = HPPItem.GetAll(catalogID.Value, dtStart.Value, dtEnd.Value);
            }
            else
            {
                list = HPPItem.GetAll(dtStart.Value, dtEnd.Value);
            }
            dataGridView1.DataSource = list;
        }
        public List<CstmTotalPurchase> Purchases
        {
            get
            {
                List<CstmTotalPurchase> list = SaleItem.GetAllPurchases();
                return list;
            }
        }
        private void frmHPP_Load(object sender, EventArgs e)
        {
            List<Catalog> catalogs = CatalogItem.GetItems();

            catalogs.Insert(0, new Catalog());

            cboCatalog.DisplayMember = "Name";
            cboCatalog.ValueMember = "ID";
            cboCatalog.DataSource = catalogs;

            dtEnd.Value = DateTime.Now;
            dtStart.Value = DateTime.Now.AddYears(-1);


            List<DateTime> dtList = Purchases.Select(t => t.PurchaseDate.Date).Distinct().ToList();

            dtRangeStart.Value = dtList.Min();
            dtRangeEnd.Value = dtList.Max();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
