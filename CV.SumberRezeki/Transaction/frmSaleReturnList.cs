using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using LogicLayer;
using DataObject;
using DataLayer;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmSaleReturnList : MasterPage, IMasterHeader, IMasterFooter
    {
        public frmSaleReturnList()
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
            List<SaleReturn> list = SaleReturnItem.GetCstmPaging(text, offset, pageSize);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = SaleReturnItem.GetCstmRecordCount(text);
        }

        public override void _Add()
        {
            Add();
        }

        public void Add()
        {
            frmSaleReturn f = new frmSaleReturn();
            f.Text = this.Text;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.ControlBox = false;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            f.SaleReturnNo = string.Empty;
            f.ShowDialog();
            Search();
        }

        public override void _Edit()
        {
            Edit();
        }
        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmSaleReturn f = new frmSaleReturn();
            f.Text = this.Text;
            f.ControlBox = false;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            int Row = grid.CurrentRow.Index;
            f.SaleReturnNo = string.Format("{0}", grid["colReturnNo", Row].Value);
            f.IsView = true;
            //f.Username = Utilities.Username;
            f.ShowDialog();
            Search();
        }

        public override void _Delete()
        {
            Delete();
        }

        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Doing this will delete data permanently\nApakah anda yakin ingin menghapus data ini??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string returnNo = string.Format("{0}", grid["colReturnNo", rowIndex].Value);
                SaleReturn item = SaleReturnItem.GetByCode(returnNo);

                int result = SaleReturnItem.Delete(returnNo);
                if (result >0)
                {
                    Log.Delete(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item)));
                    Search();
                }
            }
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        private void frm_Load(object sender, EventArgs e)
        {
            Search();
        }
    }
}
