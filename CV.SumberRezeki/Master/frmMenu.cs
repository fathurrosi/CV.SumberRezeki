using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using Newtonsoft.Json;


namespace Sendang.Rejeki.Master
{
    public partial class frmMenu : Form, ITransButton
    {
        public frmMenu()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        public void Save()
        {
            try
            {
                int result = -1;
                int seq = 0;
                int parentID = 0;
                int.TryParse(cboParentMenu.SelectedValue.ToString(), out parentID);
                int.TryParse(txtSequence.Text, out seq);
                if (ID > 0)
                {
                    result = DataLayer.MenuItem.Update(ID, txtCode.Text, txtName.Text, txtDesc.Text, parentID, seq, null, Utilities.Username);
                }
                else
                {
                    result = DataLayer.MenuItem.Insert(txtCode.Text, txtName.Text, txtDesc.Text, parentID, seq, null, Utilities.Username);
                }

                if (result > 0)
                {
                    if (ID > 0)
                        Log.Update(string.Format("{0}-{1}", this.Text,JsonConvert.SerializeObject(new DataObject.Menu() { ID = ID, Code = txtCode.Text, Description = txtDesc.Text, ParentID = parentID, Name = txtName.Text, Sequence = seq })));
                    else Log.Insert(string.Format("{0}-{1}", this.Text,(JsonConvert.SerializeObject(new DataObject.Menu() { ID = ID, Code = txtCode.Text, Description = txtDesc.Text, ParentID = parentID, Name = txtName.Text, Sequence = seq }))));

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                MessageBox.Show(ex.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public bool IsValid()
        {
            if (txtCode.Text.Length == 0) return false;

            return true;
        }

        void LoadMenu()
        {
            List<DataObject.Menu> list = DataLayer.MenuItem.GetMenus();

            list.Insert(0, new DataObject.Menu() { ID = 0, Code = "", Name = "", Description = "" });

            cboParentMenu.DisplayMember = "Name";
            cboParentMenu.ValueMember = "ID";
            cboParentMenu.DataSource = list;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            LoadMenu();
            if (ID == 0) return;
            DataObject.Menu mn = DataLayer.MenuItem.GetMenuByID(ID);
            if (mn != null)
            {
                txtCode.Enabled = false;
                txtCode.Text = mn.Code;
                txtDesc.Text = mn.Description;
                txtName.Text = mn.Name;
                txtSequence.Text = mn.Sequence.ToString();
                cboParentMenu.SelectedValue = mn.ParentID;
            }
        }
    }
}
