using DataLayer;
using DataObject;
using LogicLayer;
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
    public partial class frmCatalogPriceList : Form, IMasterHeader, IMasterFooter
    {

        public frmCatalogPriceList()
        {
            InitializeComponent();
        }

        public void Search()
        {
            //Supplier supplier = cboSupplier.SelectedItem as Supplier;
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(null, textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        }

        void LoadData(Supplier selectedSupplier, string text, int offset, int pageSize)
        {
            List<CstmCatalogPrice> list = CatalogPriceItem.GetBySupplierCode(string.Empty, text, offset, pageSize);
            grid.DataSource = list;
            ctlFooter1.TotalRows = CatalogPriceItem.GetRecordCount(string.Empty, text);
        }


        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {

            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\POPriceList.rdlc";
            Report.frmReportViewer f = new Report.frmReportViewer();
            f.ReportName = "CstmCatalogPrice";
            f.ReportPath = reportPath;
            f.DataSource = CatalogPriceItem.GetAll();
            f.ShowDialog();
        }

        private void frmCatalogPriceList_Load(object sender, EventArgs e)
        {
            grid.AutoGenerateColumns = false;
            //List<Supplier> list = SupplierItem.GetAll();
            //list.Insert(0, new Supplier() { Name = "Select All" });
            //cboSupplier.DataSource = list;
            //cboSupplier.DisplayMember = "Name";
            //cboSupplier.ValueMember = "Code";
            //cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            //cboSupplier.SelectedIndex = 0;
            Search();
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
