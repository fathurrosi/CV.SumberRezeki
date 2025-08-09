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
    public partial class ctlTransButton : UserControl
    {
        private bool _IsLookup = false;


        public bool IsLookup
        {
            get { return _IsLookup; }
            set { _IsLookup = value; }
        }

        private bool _SaveButtonVisible = true;

        public bool SaveButtonVisible
        {
            get { return _SaveButtonVisible; }
            set
            {
                _SaveButtonVisible = value;
                btnSave.Visible = _SaveButtonVisible;
            }
        }

        private bool _SaveButtonEnabled = true;

        public bool SaveButtonEnabled
        {
            get { return _SaveButtonEnabled; }
            set { _SaveButtonEnabled = value;
            btnSave.Enabled = _SaveButtonEnabled;
            }
        }

        private bool _CancelButtonEnabled = true;

        public bool CancelButtonEnabled
        {
            get { return _CancelButtonEnabled; }
            set { _CancelButtonEnabled = value;
            btnCancel.Enabled = _CancelButtonEnabled;
            }
        }


        public ctlTransButton()
        {

            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ITransButton master = (ITransButton)this.Parent;
            if (master != null)
            {
                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "btnSave":
                        if (master.IsValid())
                        {
                            master.Save();
                        }
                        break;
                    case "btnCancel":
                        master.Cancel();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ctlTransButton_Load(object sender, EventArgs e)
        {
            btnSave.Text = IsLookup ? "OK" : "Save";
            //if (!SaveButtonEnabled)
            //{
            //    btnSave.Enabled = false;
            //}
            //if (!CancelButtonEnabled)
            //{
            //    btnCancel.Enabled = false;
            //}
        }

        public string SaveButtonText { get; set; }
        public string CancelButtonText { get; set; }

    }
}
