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

namespace Sendang.Rejeki.Lookup
{
    public partial class frmSupplierLookup : Form, ITransButton, IMasterHeader, IMasterFooter
    {
        public Supplier SelectedSupplier { get; set; }
        public frmSupplierLookup()
        {
            InitializeComponent();
            Search();

        }

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        }

        void LoadData(string text, int offset, int pageSize)
        {
            int totalRecord = SupplierItem.GetRecordCount(text);
            List<Supplier> list = SupplierItem.GetPaging(text, offset, pageSize);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            return;
        }

        public void Edit()
        {
            return;
        }

        public void Delete()
        {
            return;
        }

        public bool IsValid()
        {
            return true;
        }

        public void Save()
        {
            DataGridViewRow vRow = grid.CurrentRow;
            if (vRow == null) return;
            int Row = grid.CurrentRow.Index;
            SelectedSupplier = SupplierItem.GetByCode(string.Format("{0}", grid[0, Row].Value));
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Save();
        }


        public void Print()
        {  //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            //Report.frmReportViewer f = new Report.frmReportViewer();
            //f.ReportName = "Catalog";
            //f.ReportPath = reportPath;
            //f.DataSource = CatalogItem.GetAll();
            //f.ShowDialog();
        }
    }
}
