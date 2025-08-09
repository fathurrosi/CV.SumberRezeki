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
using Newtonsoft.Json;

namespace Sendang.Rejeki.Master
{
    public partial class frmUserList : Form, IMasterHeader, IMasterFooter
    {
        static frmUserList _instance;
        public static frmUserList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new frmUserList();
            }
            return _instance;
        }
        public frmUserList()
        {
            InitializeComponent();
        }

        public void Search()
        {
            string textToSearch = ctlHeader2.TextToSearch;
            LoadData(textToSearch, ctlFooter2.Offset, ctlFooter2.PageSize);
        }

        void LoadData(string text, int offset, int pageSize)
        {
            List<User> list = UserItem.GetPaging(text, offset, pageSize);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter2.TotalRows = UserItem.GetRecordCount(text);
        }

        public void Add()
        {
            frmUser f = new frmUser();
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
            frmUser f = new frmUser();
            int Row = grid.CurrentRow.Index;
            f.SelectedUsername = grid[0, Row].Value.ToString();
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
                string username = string.Format("{0}", grid["colCode", Row].Value);
                User item = DataLayer.UserItem.GetUser(username);
                int result = DataLayer.UserItem.Delete(username);
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }

        private void frmUserList_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void frmUserList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
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
