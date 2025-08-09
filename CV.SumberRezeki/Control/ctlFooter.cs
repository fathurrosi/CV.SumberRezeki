using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;

namespace Sendang.Rejeki.Control
{
    public partial class ctlFooter : UserControl
    {
        public ctlFooter()
        {
            InitializeComponent();
        }

        public int Offset { get; set; }

        public int PageSize { get { return Config.GetPageSize(); } }
        public int PageIndex
        {
            get
            {
                int pageIndex = 0;
                int.TryParse(txtPageIndex.Text, out pageIndex);
                return pageIndex == 0 ? 1 : pageIndex;
            }
            set { txtPageIndex.Text = (value).ToString(); }
        }

        int _TotalRows = 0;

        private int _PageCount;

        public int PageCount
        {
            get { return _PageCount; }
            private set
            {
                _PageCount = value;
            }
        }

        public int TotalRows
        {
            set
            {
                _TotalRows = value;
                double pageCount = (double)((decimal)_TotalRows / Convert.ToDecimal(PageSize));
                _PageCount = (int)Math.Ceiling(pageCount);
                lblTotalPage.Text = string.Format(" of {0} ", _PageCount);
            }
            get { return _TotalRows; }
        }

        private Form GetActiveForm(System.Windows.Forms.Control ctl)
        {
            if (ctl is Form)
                return (Form)ctl;
            else
                return GetActiveForm(ctl.Parent);
        }


        private void OnClick(string name)
        {
            Form form = GetActiveForm(this.Parent);
            IMasterFooter footer = (IMasterFooter)form;
            if (footer != null)
            {
                switch (name)
                {
                    case "btnNext":
                        if (PageIndex >= PageCount) return;
                        PageIndex += 1;
                        break;
                    case "btnPrev":
                        if (PageIndex <= 1) return;
                        PageIndex -= 1;
                        break;
                    case "btnLast":
                        if (PageCount == 0) return;
                        if (PageIndex == PageCount) return;
                        PageIndex = PageCount;
                        break;
                    case "btnFirst":
                        PageIndex = 1;
                        break;
                    case "btnRefresh":
                        break;
                    default:
                        break;
                }
                Offset = (PageIndex - 1) * PageSize;
                footer.Search();
            }
        }


        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            OnClick(btn.Name);
        }

        private void txtPageIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnClick(string.Empty);
            }
        }

    }
}
