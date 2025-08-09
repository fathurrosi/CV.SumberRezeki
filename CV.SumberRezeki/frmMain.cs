using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using Sendang.Rejeki.Master;
using Sendang.Rejeki.Transaction;
using System.Globalization;
using System.Threading;
using System.IO;
using DataLayer;
using DataObject;
using Microsoft.Reporting.WinForms;
using Sendang.Rejeki.Report;
namespace Sendang.Rejeki
{
    public partial class frmMain : Form
    {
        //LogicLayer.Log log = LogicLayer.Log.CreateInstance();
        public frmMain()
        {
            InitializeComponent();
        }

        Form GetInstance(string formName)
        {
            Form activeForm = null;
            //LogicLayer.Log log = LogicLayer.Log.CreateInstance();
            switch (formName.ToLower().Trim())
            {
                case "logout":
                    Log.Info("Logout");
                    Application.Restart();
                    break;
                case "exit": ;
                    Log.Info("Close");
                    Application.Exit();
                    break;
                case "profile":
                    activeForm = new frmCompanyProfile();
                    break;
                case "catalog":
                    activeForm = new frmCatalogList(); break;
                case "supplier":
                    activeForm = new frmSupplierList(); break;
                case "purchase":
                    activeForm = new frmPurchase(); break;
                case "menu":
                    activeForm = new frmMenuList(); break;
                case "user":
                    activeForm = new frmUserList(); break;
                case "role":
                    activeForm = new frmRoleList(); break;
                case "previllage":
                    activeForm = new frmPrevillage(); break;
                case "catalogprice":
                    activeForm = new frmCatalogPriceList(); break;
                case "salespoint":
                    activeForm = new frmPos(); break;
                case "salelist":
                    activeForm = new frmPosList(); break;
                case "polist":
                    activeForm = new frmPurchaseList(); break;
                case "customer":
                    activeForm = new frmCustomerList(); break;
                case "stock":
                    activeForm = new frmStocks(); break;
                //case "s":
                //    activeForm = new frmStockList(); break;
                case "recon":
                    activeForm = new frmReconcileList(); break;
                case "hpp":
                    activeForm = new frmRptHPP(); break;
                case "genhpp":
                    activeForm = new frmHPP(); break;
                case "rppc":
                    activeForm = new frmRptItemPurchasedPerCustomer(); break;
                case "frmtscm":
                    activeForm = new frmTotalSalesPerCustomer(); break;
                case "ndgp":
                    activeForm = new frmRptDailyGrossProfit(); break;
                case "tppsmonthly":
                    activeForm = new frmTotalPurchasesPerSupplier(); break;
                case "tpmonthly":
                    activeForm = new frmTotalPurchasesMonthly(); break;
                case "monthlysales":
                    activeForm = new frmMonthlySales(); break;
                case "return_s": activeForm = new frmSaleReturnList(); break;
                case "return_p": activeForm = new frmPurchaseReturnList(); break;
                default:
                    break;
            }

            if (activeForm != null)
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == activeForm.Name)
                    {
                        activeForm = f;
                        break;
                    }
                }
            }

            return activeForm;
        }

        void OnClick(object sender, EventArgs e)
        {
            try
            {
                Profile profile = ProfileItem.GetProfile();
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("pNamaPerusahaan", string.Format("{0}", profile == null ? " " : profile.Nama), false));
                parameters.Add(new ReportParameter("pBidangPerusahaan", string.Format("{0}", profile == null ? " " : profile.Keterangan), false));
                parameters.Add(new ReportParameter("pAlamatPerusahaan", string.Format("{0}", profile == null ? " " : profile.Alamat), false));
                string telp = " ";
                if (profile != null)
                {
                    if (!string.IsNullOrEmpty(profile.Telp1) && !string.IsNullOrEmpty(profile.Telp2))
                        telp = string.Format("{0} / {1}", profile.Telp1, profile.Telp2);
                    else if (string.IsNullOrEmpty(profile.Telp1) && !string.IsNullOrEmpty(profile.Telp2))
                        telp = string.Format("{0}", profile.Telp2);
                    else if (!string.IsNullOrEmpty(profile.Telp1) && string.IsNullOrEmpty(profile.Telp2))
                        telp = string.Format("{0}", profile.Telp1);
                }
                parameters.Add(new ReportParameter("pAlamatPerusahaan2", string.Format("{0}", profile == null ? " " : profile.Alamat2), false));
                parameters.Add(new ReportParameter("pLabelAlamat", string.Format("{0}", profile == null ? " " : profile.LabelAlamat), false));
                parameters.Add(new ReportParameter("pLabelAlamat2", string.Format("{0}", profile == null ? " " : profile.LabelAlamat2), false));
                parameters.Add(new ReportParameter("pPhone", telp, false));
                parameters.Add(new ReportParameter("pWeb", string.Format("{0}", profile == null ? " " : profile.web), false));
                parameters.Add(new ReportParameter("pEmail", string.Format("{0}", profile == null ? " " : profile.email), false));
                parameters.Add(new ReportParameter("pInstagram", string.Format("{0}", profile == null ? " " : profile.instagram), false));


                ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
                Form form = GetInstance(item.Name);
                if (form == null)
                {
                    string reportPath = Directory.GetCurrentDirectory();
                    Report.frmReportViewer rptViewer = null;
                    Report.frmReportViewerWithRange rptViewerRange = null;
                    switch (item.Name.ToLower().Trim())
                    {

                        case "dailysalescatalog":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailySalesPerCatalog;
                            rptViewerRange.ReportName = "DailyCatalogSales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailyCatalogSales.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;

                        case "dailypurchases":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailyPurchases;
                            //rptViewerRange.ReportName = "CstmDailySale";
                            rptViewerRange.ReportName = "DailySales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailyPurchases.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;

                        case "dailysales":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalDailySale;
                            //rptViewerRange.ReportName = "CstmDailySale";
                            rptViewerRange.ReportName = "DailySales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailySales.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;
                        case "ar":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.Piutang;
                            rptViewerRange.ReportName = "Piutang";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\Piutang.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;
                        case "rptcatalog":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Catalog";
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Catalog.rdlc", reportPath);
                            rptViewer.DataSource = CatalogItem.GetAll();
                            rptViewer.Params.AddRange(parameters);
                            //rptViewer.ShowDialog();
                            break;
                        case "graphofsales":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "graphofsales";
                            rptViewer.ReportPath = string.Format("{0}\\Report\\graphofsales.rdlc", reportPath);
                            //rptViewer.ShowDialog();
                            rptViewer.Params.AddRange(parameters);
                            break;

                        case "dailygrossprofit":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailyGrossProfit;
                            rptViewerRange.ReportName = "DailyGrossProfit";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailyGrossProfit.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;
                        case "monthlygrossprofit":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.MonthlyGrossProfit;
                            rptViewerRange.ReportName = "MonthlyGrossProfit";
                            //rptViewerRange.DataSource = SaleItem.GetMonthlyGrossProfit();
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\MonthlyGrossProfit.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;
                        case "salespermonth":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalSalesPerCatalog;
                            rptViewerRange.ReportName = "SalesPerMonth";
                            rptViewerRange.Params.AddRange(parameters);
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerMonth.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            break;
                        case "salespercustomer":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalSalesPerCustomer;
                            rptViewerRange.ReportName = "SalesPerCustomer";
                            //rptViewerRange.DataSource = SaleItem.GetTotalSalePerCustomer();
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerCustomer.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;

                        case "salesperformancepermonth":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.SalesPerformancePerMonth;
                            rptViewerRange.ReportName = "SalesPerformancePerMonth";
                            //rptViewerRange.DataSource = SaleItem.GetTotalSalePerCustomer();
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerformancePerMonth.rdlc", reportPath);
                            //rptViewerRange.ShowDialog();
                            rptViewerRange.Params.AddRange(parameters);
                            break;

                        //case "salesperformancepermonth":
                        //    rptViewer = new Report.frmReportViewer();
                        //    rptViewer.ReportName = "SalesPerformancePerMonth";
                        //    rptViewer.DataSource = SaleItem.GetTotalSalePerCatalog();
                        //    rptViewer.ReportPath = string.Format("{0}\\Report\\SalesPerformancePerMonth.rdlc", reportPath);
                        //    rptViewer.ShowDialog();
                        //    break;

                        case "lapstockdetail":
                            //rptViewer = new Report.frmReportViewer();
                            //rptViewer.ReportName = "Stock";
                            //rptViewer.DataSource = CatalogStockItem.GetAll();
                            //rptViewer.ReportPath = string.Format("{0}\\Report\\StockDetail.rdlc", reportPath);
                            //rptViewer.ShowDialog();

                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.StockDetail;
                            rptViewerRange.ReportName = "CstmCatalogStock";
                            //rptViewerRange.DataSource = SaleItem.GetDailyGrossProfit();
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\StockDetails.rdlc", reportPath);
                            rptViewerRange.Params.AddRange(parameters);
                            //rptViewerRange.ShowDialog();
                            break;
                        case "lapstock":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Stock";
                            rptViewer.DataSource = CatalogStockItem.GetStockReport();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Stock.rdlc", reportPath);
                            //rptViewer.ShowDialog();
                            rptViewer.Params.AddRange(parameters);
                            break;

                        case "lapsupplier":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Supplier";
                            rptViewer.DataSource = SupplierItem.GetAll();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Supplier.rdlc", reportPath);
                            //rptViewer.ShowDialog();
                            rptViewer.Params.AddRange(parameters);
                            break;
                        case "lapcustomer":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Customer";
                            rptViewer.DataSource = CustomerItem.GetAll();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Customer.rdlc", reportPath);
                            //rptViewer.ShowDialog();
                            rptViewer.Params.AddRange(parameters);
                            break;
                        default:
                            rptViewer = null;
                            break;
                    }

                    if (rptViewer != null)
                    {
                        rptViewer.StartPosition = FormStartPosition.CenterParent;
                        rptViewer.MdiParent = this;
                        rptViewer.Tag = item.Tag;
                        rptViewer.Text = item.Text.Trim();
                        rptViewer.WindowState = FormWindowState.Maximized;
                        rptViewer.Show();
                        Log.Info(string.Format("{0} opened {1} report", Utilities.Username, rptViewer.ReportName));
                    }
                    else if (rptViewerRange != null)
                    {
                        rptViewerRange.StartPosition = FormStartPosition.CenterParent;
                        rptViewerRange.MdiParent = this;
                        rptViewerRange.Tag = item.Tag;
                        rptViewerRange.Text = item.Text.Trim();
                        rptViewerRange.WindowState = FormWindowState.Maximized;
                        //rptViewerRange.WindowState = FormWindowState.Maximized;
                        rptViewerRange.Show();
                        Log.Info(string.Format("{0} opened {1} report", Utilities.Username, rptViewerRange.ReportName));
                    }
                }
                else
                {
                    //Log.Info(string.Format("{0} opened {1} form", Utilities.Username, item.Text));
                    //form.Text = item.Text;
                    //form.MdiParent = this;
                    //form.Tag = item.Tag;
                    //form.WindowState = FormWindowState.Maximized;
                    //form.Show();


                    Log.Info(string.Format("{0} opened {1} form", Utilities.Username, item.Text));
                    if (item.Name.ToLower() == "profile" || item.Name.ToLower() == "printer")
                    {
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.Text = item.Text.Trim();
                        form.Tag = item.Tag;
                        form.ShowDialog();
                    }
                    else
                    {
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.Text = item.Text.Trim();
                        form.MdiParent = this;
                        form.Tag = item.Tag;
                        form.WindowState = FormWindowState.Maximized;
                        form.Show();
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Utilities.ShowInformation(ex.ToString());
            }
        }

        void LoadMenu(ToolStripMenuItem tMenuItem, List<DataObject.Menu> list, int parentID)
        {
            list.Where(t => t.ParentID == parentID).OrderBy(t => t.Sequence).ToList().ForEach(t =>
            {
                ToolStripMenuItem child = new ToolStripMenuItem();
                child.Name = t.Code;
                child.Text = t.Name;
                child.ToolTipText = t.Description;
                child.Image = GetImage(t.Name);
                child.Enabled = t.Enabled;
                child.Tag = t;
                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(child, list, t.ID);
                else child.Click += new EventHandler(OnClick);
                tMenuItem.DropDownItems.Add(child);
            });
        }

        private Image GetImage(string name)
        {
            Image image = null;
            switch (name.ToLower())
            {
                case "report": image = global::CV.SumberRezeki.Properties.Resources.rpt; break;
                case "user management": image = global::CV.SumberRezeki.Properties.Resources.users1; break;
                case "neraca": image = global::CV.SumberRezeki.Properties.Resources.neraca; break;
                case "user": image = global::CV.SumberRezeki.Properties.Resources.singleuser; break;
                case "menu": image = global::CV.SumberRezeki.Properties.Resources.menu; break;
                case "role": image = global::CV.SumberRezeki.Properties.Resources.role; break;
                case "previllage": image = global::CV.SumberRezeki.Properties.Resources.previllage; break;
                case "transaction": image = global::CV.SumberRezeki.Properties.Resources.transaction; break;
                case "generate hpp": image = global::CV.SumberRezeki.Properties.Resources.hpp; break;
                case "sale": image = global::CV.SumberRezeki.Properties.Resources.sale; break;
                case "list of sale": image = global::CV.SumberRezeki.Properties.Resources.salelist; break;
                case "product reconcile": image = global::CV.SumberRezeki.Properties.Resources.reconcile; break;
                case "stock": image = global::CV.SumberRezeki.Properties.Resources.stock; break;
                case "stock list per supplier": image = global::CV.SumberRezeki.Properties.Resources.supplierlist; break;
                case "input stock": image = global::CV.SumberRezeki.Properties.Resources.input; break;
                case "stock detail/update": image = global::CV.SumberRezeki.Properties.Resources.edit; break;
                case "catalog": image = global::CV.SumberRezeki.Properties.Resources.catalog; break;
                case "customer": image = global::CV.SumberRezeki.Properties.Resources.customers; break;
                case "supplier": image = global::CV.SumberRezeki.Properties.Resources.supplier; break;
                case "master": image = global::CV.SumberRezeki.Properties.Resources.master; break;
                case "logout": image = global::CV.SumberRezeki.Properties.Resources.logout; break;
                case "exit": image = global::CV.SumberRezeki.Properties.Resources.exit; break;
                case "file": image = global::CV.SumberRezeki.Properties.Resources.file; break;
                default: image = global::CV.SumberRezeki.Properties.Resources.graphic_report__2_; break;


            }
            return image;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            menuStrip.Items.Clear();

            //List<MetalHasil.Dto.Menu> list = MetalHasil.Dta.MenuItem.GetMenus();
            //Utilities.PrivilegeList = PrivilegeItem.GetByUsername(Utilities.Username);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    Privilege prev = Utilities.PrivilegeList.Where(t => t.MenuID == list[i].ID).FirstOrDefault();
            //    list[i].Enabled = prev == null ? false : prev.AllowRead;
            //}

            //List<MetalHasil.Dto.Menu> parentList = list.Where(t => t.ParentID == 0).OrderBy(t => t.Sequence).ToList();

            List<DataObject.Menu> list = DataLayer.MenuItem.GetMenus();
            Utilities.PrivilegeList = PrivilegeItem.GetByUsername(Utilities.Username);

            for (int i = 0; i < list.Count; i++)
            {
                Privilege prev = Utilities.PrivilegeList.Where(t => t.MenuID == list[i].ID).FirstOrDefault();
                list[i].Enabled = prev == null ? false : prev.AllowRead;
            }

            List<DataObject.Menu> parentList = list.Where(t => t.ParentID == 0).OrderBy(t => t.Sequence).ToList();
            parentList.ForEach(t =>
            {
                ToolStripMenuItem parent = new ToolStripMenuItem();
                parent.Name = t.Code;
                parent.Text = t.Name;
                parent.Image = GetImage(t.Name);
                parent.Enabled = t.Enabled;
                parent.ToolTipText = t.Description;
                parent.Tag = t;

                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(parent, list, t.ID);
                else parent.Click += new EventHandler(OnClick);
                menuStrip.Items.Add(parent);
            });
        }
    }
}
