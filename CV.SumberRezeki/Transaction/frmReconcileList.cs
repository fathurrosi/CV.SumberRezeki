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

namespace Sendang.Rejeki.Transaction
{
    public partial class frmReconcileList : Form, IMasterHeader, IMasterFooter
    {
        public frmReconcileList()
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
            List<Reconcile> list = ReconcileItem.GetPaging(text, offset, pageSize);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list.OrderByDescending(t => t.ProccessDate).ToList();
            ctlFooter1.TotalRows = ReconcileItem.GetRecordCount(text);
        }

        public void Add()
        {
            frmReconcile f = new frmReconcile();
            //f.Username = Utilities.Username;
            f.ReconcileID = string.Empty;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            Form f = null;
            //if (!Utilities.IsSuperAdmin())
            //    f = new frmPosView() { TransactionID = string.Format("{0}", grid[2, Row].Value) };
            //else f = new frmPos() { TransactionID = string.Format("{0}", grid[2, Row].Value) };

            f = new frmReconcile() { ReconcileID = string.Format("{0}", grid["colID", Row].Value) };
            if (f != null && f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }


        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?\nDeleting this would update current stock", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string ReconcileID = string.Format("{0}", grid.Rows[rowIndex].Cells["colID"].Value);
                int result = ReconcileItem.Delete(ReconcileID);
                if (result > 0) Search();
            }
        }

        public void Print()
        {
            //throw new NotImplementedException();
        }

        private void frmReconcileList_Load(object sender, EventArgs e)
        {
            Search();
        }
    }
}
