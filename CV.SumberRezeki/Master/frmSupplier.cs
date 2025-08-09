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

namespace Sendang.Rejeki.Master
{
    public partial class frmSupplier : Form, ITransButton
    {
        public frmSupplier()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            //if (txtCode.Text.Length == 0)
            //{
            //    Utilities.ShowValidation("Supplier code is empty!");
            //    txtCode.Focus();
            //    return false;
            //}
            //else
            if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Supplier name is empty!");
                txtName.Focus();
                return false;
            }
            else if (txtAddress.Text.Length == 0)
            {
                Utilities.ShowValidation("Supplier address is empty!");
                txtAddress.Focus();
                return false;
            }

            return true;
        }

        public void Save()
        {
            int result = -1;
            if (string.Format("{0}", Code).Length > 0)
            {
                result = SupplierItem.Update(Code, txtName.Text, txtAddress.Text, txtPhone.Text, txtCellPhone.Text, Utilities.Username);
            }
            else
            {
                string name = txtName.Text.Trim().Replace(" ", "");
                string code = Utilities.Crop(name, name.Length > 5 ? 5 : name.Length);
                Code = string.Format("{0}{1}", code, DateTime.Now.ToString(Utilities.FORMAT_DateTime_Flat));
                result = SupplierItem.Insert(Code, txtName.Text, txtAddress.Text, txtPhone.Text, txtCellPhone.Text, Utilities.Username);
            }
            if (result > 0)
            {

                if (string.Format("{0}", Code).Length > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text,JsonConvert.SerializeObject(new Supplier() { Code = Code, Name = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));
                else Log.Insert(string.Format("{0}-{1}", this.Text,JsonConvert.SerializeObject(new Supplier() { Code = Code, Name = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void frm_Load(object sender, EventArgs e)
        {
           // txtCode.Enabled = true;
            if (string.Format("{0}", Code).Length == 0) return;
            Supplier prd = SupplierItem.GetByCode(Code);
            if (prd != null)
            {
                Code = prd.Code;
                //txtCode.Enabled = false;
                txtAddress.Text = prd.Address;
                txtName.Text = prd.Name;
                txtPhone.Text = prd.Phone;
                txtCellPhone.Text = prd.CellPhone;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public string Code { get; set; }
    }
}
