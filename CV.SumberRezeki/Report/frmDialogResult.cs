using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Report
{
    public partial class frmDialogResult : Form
    {
        public string Message { get; set; }
        public string Result { get; set; }
        public string Title { get; set; }
        public frmDialogResult()
        {
            InitializeComponent();
        }

        private void frmDialogResult_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            txtResult.Text = Result;
            lblMessage.Text = Message;
        }
    }
}
