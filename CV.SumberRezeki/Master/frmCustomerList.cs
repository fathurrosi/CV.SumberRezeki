using DataLayer;
using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using LogicLayer.Helper;

namespace Sendang.Rejeki.Master
{
    public partial class frmCustomerList : Form, IMasterHeader, IMasterFooter
    {


        public frmCustomerList()
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
            grid.AutoGenerateColumns = false;
            DataTable list = CustomerItem.GetPaging(text, offset, pageSize);

            DataView dv = new DataView(list, "Fullname <> '' ", "Fullname ASC", DataViewRowState.CurrentRows);
            grid.DataSource = dv;

            ctlFooter1.TotalRows = CustomerItem.GetRecordCount(text);
        }

        public void Add()
        {
            frmCustomer f = new frmCustomer();
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
            frmCustomer f = new frmCustomer();
            int rowIndex = grid.CurrentRow.Index;
            int ID = 0;
            int.TryParse(string.Format("{0}", grid.Rows[rowIndex].Cells["colID"].Value), out ID);

            f.ID = ID;
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
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                int ID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[rowIndex].Cells["colID"].Value), out ID);
                Customer item = CustomerItem.GetByID(ID);
                int result = CustomerItem.Delete(ID);
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }


        private void frmCustomerList_Load(object sender, EventArgs e)
        {
            Search();
        }

        public void Print()
        {
            //Excel.GenerateStock(cboCategory.Text, string.Format("STOK GUDANG {0:dd-MMM-yyyy HH-mm-ss}.xls", DateTime.Now).ToUpper()
            //        , string.Format("STOK BARANG {0} JAM :{1:HH:mm}", DateTime.Now.ToString("dddd, dd-MMMM-yyyy", new System.Globalization.CultureInfo("id-ID")), DateTime.Now).ToUpper()
            //        , grid);
            Excel.GenerateExcelFile(string.Format("Daftar_Customer_Per_{0:ddMMMyyyy}.xls", DateTime.Now), "Daftar Customer", grid);
        }
    }
}
