using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataObject;
using DataLayer;
using System.IO;

namespace Sendang.Rejeki.Control
{
    public partial class ctlHeader : UserControl
    {
        private bool _VisibleSearch = true;
        public bool EnableSearch
        {
            get { return _VisibleSearch; }
            set
            {
                _VisibleSearch = value;
                txtSearch.Enabled = _VisibleSearch;
                btnSearch.Enabled = _VisibleSearch;
                lblSearch.Enabled = _VisibleSearch;
            }
        }
        public string NewButtonText
        {
            get { return btnAdd.Text; }
            set { btnAdd.Text = string.Format("{0}", value).Length > 0 ? value : "Add"; }
        }

        public string PrintButtonText
        {
            get { return btnPrint.Text; }
            set { btnPrint.Text = string.Format("{0}", value).Length > 0 ? value : "Report"; }
        }

        public string DeleteButtonText
        {
            get { return btnDelete.Text; }
            set { btnDelete.Text = string.Format("{0}", value).Length > 0 ? value : "Delete"; }
        }


        public string EditButtonText
        {
            get { return btnEdit.Text; }
            set
            {
                btnEdit.Image = global::CV.SumberRezeki.Properties.Resources.pencil;
                btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
                if (string.Format("{0}", value).ToLower() == "view" || string.Format("{0}", value).ToLower() == "lihat")
                {
                    btnEdit.Image = global::CV.SumberRezeki.Properties.Resources.application_view_list;
                }
                btnEdit.Text = string.Format("{0}", value).Length > 0 ? value : "Edit";
            }
        }

        public bool NewButtonEnabled
        {
            get { return btnAdd.Enabled; }
            set { btnAdd.Enabled = value; }
        }

        public bool DeleteButtonEnabled
        {
            get { return btnDelete.Enabled; }
            set { btnDelete.Enabled = value; }
        }

        public bool EditButtonEnabled
        {
            get { return btnEdit.Enabled; }
            set
            {
                btnEdit.Enabled = value;
                separatorEdit.Visible = btnEdit.Visible;
            }
        }

        public bool PrintButtonVisible
        {
            get { return btnPrint.Visible; }
            set
            {
                btnPrint.Visible = value;
                separatorReport.Visible = btnPrint.Visible;
            }
        }



        public bool NewButtonVisible
        {
            get
            {
                return btnAdd.Visible;
            }
            set
            {
                btnAdd.Visible = value;
                separatorAdd.Visible = btnAdd.Visible;
            }
        }




        public bool DeleteButtonVisible
        {
            get { return btnDelete.Visible; }
            set
            {
                btnDelete.Visible = value;
                separatorDelete.Visible = btnDelete.Visible;
            }
        }


        public bool EditButtonVisible
        {
            get { return btnEdit.Visible; }
            set
            {
                btnEdit.Visible = value;
                separatorEdit.Visible = btnEdit.Visible;
            }
        }

        public Button DeleteButton { get; set; }
        public Button EditButton { get; set; }
        public Button NewButton { get; set; }
        private bool _IsLookup;

        public bool IsLookup
        {
            get { return _IsLookup; }
            set
            {
                _IsLookup = value;
                //btnAdd.Enabled = !value;
                //btnEdit.Enabled = !value;
                //btnDelete.Enabled = !value;
            }
        }

        public ctlHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (string.Format("{0}", PrintButtonText).ToLower() == "Print to Excel".ToLower() ||
                string.Format("{0}", PrintButtonText).ToLower() == "Export to Excel".ToLower())
            {
                this.btnPrint.Image = global::CV.SumberRezeki.Properties.Resources.excel;
            }
            Form form = GetActiveForm(this.Parent);
            IMasterHeader header = (IMasterHeader)form;
            if (header != null && form.Tag != null)
            {
                DataObject.Menu menu = (DataObject.Menu)form.Tag;
                Privilege prev = Utilities.PrivilegeList.Where(t => t.MenuID == menu.ID).FirstOrDefault();
                btnAdd.Enabled = prev.AllowCreate;
                btnEdit.Enabled = prev.AllowUpdate;
                btnPrint.Enabled = prev.AllowPrint;
                btnDelete.Enabled = prev.AllowDelete;

                if (!NewButtonVisible) separatorAdd.Visible = false;
                if (!EditButtonVisible) separatorEdit.Visible = false;
                if (!DeleteButtonVisible) separatorDelete.Visible = false;
                if (!PrintButtonVisible) separatorReport.Visible = false;

                this.ActiveControl = toolStripMaterGrid;

            }
            base.OnLoad(e);
        }

        public string TextToSearch
        {
            get { return txtSearch.Text; }
            set { value = txtSearch.Text; }
        }

        private Form GetActiveForm(System.Windows.Forms.Control ctl)
        {
            if (ctl is Form)
                return (Form)ctl;
            else
                return GetActiveForm(ctl.Parent);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            Execute(btn.Name);
        }

        void Execute(string name)
        {
            Form form = GetActiveForm(this.Parent);
            IMasterHeader header = (IMasterHeader)form;
            if (header != null)
            {
                switch (name)
                {
                    case "btnAdd":
                        header.Add();
                        break;
                    case "btnEdit":
                        header.Edit();
                        break;
                    case "btnSearch":
                        header.Search();
                        break;
                    case "btnDelete":
                        header.Delete();
                        break;
                    case "btnPrint":
                        header.Print();
                        break;
                    default:
                        header.Search();
                        break;
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Execute(string.Empty);
            }
        }
    }
}
