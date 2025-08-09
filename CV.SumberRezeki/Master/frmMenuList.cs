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
using Newtonsoft.Json;


namespace Sendang.Rejeki.Master
{
    public partial class frmMenuList : Form, IMasterHeader, IMasterFooter
    {
        public frmMenuList()
        {
            InitializeComponent();
        }

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        }

        void LoadData(string text, int offset, int pageSize)
        {
            List<DataObject.CstmMenu> list = DataLayer.MenuItem.GetPaging(text, offset, pageSize);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = DataLayer.MenuItem.GetRecordCount(text);
        }

        public void Add()
        {
            frmMenu f = new frmMenu();
            //f.Username = Utilities.Username;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            DataGridViewRow vRow = grid.CurrentRow;
            if (vRow == null) return;
            frmMenu f = new frmMenu();
            int Row = grid.CurrentRow.Index;
            int id = 0;
            int.TryParse(grid[0, Row].Value.ToString(), out id);
            f.ID = id;
            //f.Username = Utilities.Username;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Delete()
        {
            DataGridViewRow vRow = grid.CurrentRow;
            if (vRow == null) return;
            int Row = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                int id = 0;
                int.TryParse(grid[0, Row].Value.ToString(), out id);
                DataObject.Menu item = DataLayer.MenuItem.GetMenuByID(id);
                int result = DataLayer.MenuItem.Delete(id);
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }

        private void frmMenuList_Load(object sender, EventArgs e)
        {
            Search();
        }

        static frmMenuList _instance;
        public static frmMenuList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new frmMenuList();

            }
            return _instance;
        }

        private void frmMenuList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }


        public void Print()
        {
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            //Report.frmReportViewer f = new Report.frmReportViewer();
            //f.ReportName = "Catalog";
            //f.ReportPath = reportPath;
            //f.DataSource = CatalogItem.GetAll();
            //f.ShowDialog();
        }
    }
}
