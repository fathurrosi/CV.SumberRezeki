using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataLayer;
using DataObject;
using Newtonsoft.Json;


namespace Sendang.Rejeki.Master
{
    public partial class frmRole : Form, ITransButton
    {
        public frmRole()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Role is empty!");
                txtName.Focus();
                return false;
            }
            else if (txtDesc.Text.Length == 0)
            {
                Utilities.ShowValidation("Decription is empty!");
                txtDesc.Focus();
                return false;
            }
            return true;
        }

        public void Save()
        {
            int result = -1;
            if (role != null)
            {
                //update
                result = RoleItem.Update(role.ID, txtName.Text, txtDesc.Text, Utilities.Username);
            }
            else
            {
                //insert
                result = RoleItem.Insert(txtName.Text, txtDesc.Text, Utilities.Username);
            }


            if (result > 0)
            {
                if (role != null)
                    Log.Update(string.Format("{0}-{1}", this.Text,JsonConvert.SerializeObject(new DataObject.Role() { ID = ID, Description = txtDesc.Text, Name = txtName.Text })));
                else Log.Insert(string.Format("{0}-{1}", this.Text,JsonConvert.SerializeObject(new DataObject.Role() { ID = ID, Description = txtDesc.Text, Name = txtName.Text })));

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else this.DialogResult = System.Windows.Forms.DialogResult.Retry;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public int ID { get; set; }
        public Role role { get; set; }
        private void frmRole_Load(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                role = RoleItem.GetRoleByID(ID);
                if (role != null)
                {
                    txtName.Text = role.Name;
                    txtDesc.Text = role.Description;
                }
            }
        }
    }
}
