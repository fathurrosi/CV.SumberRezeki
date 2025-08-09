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
    public partial class frmCustomer : Form, ITransButton
    {
        public frmCustomer()
        {
            InitializeComponent();
        }
        public int ID { get; set; }


        public bool IsValid()
        {
            if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Nama tidak boleh kosong");
                txtName.Focus();
                return false;
            }
            else
            {
                List<Customer> custList = CustomerItem.GetByName(txtName.Text.Trim());
                if (custList.Count > 0 && !(ID > 0))
                {
                    //string message = string.Format("Ada {0} nama yang sama didalam database.", custList.Count);
                    Utilities.ShowValidation(string.Format("Maaf, Nama \"{0}\" sudah ada dalam database kami.\nSilahkan input dengan nama yang berbeda", txtName.Text.Trim()));
                    txtName.Focus();
                    return false;
                }
            }
            //else if (txtDesc.Text.Length == 0) return false;
            //else if (txtNote.Text..cou == 0) return false;
            return true;
        }

        public void Save()
        {
            int result = -1;
            if (ID > 0)
            {
                result = CustomerItem.Update(ID, txtName.Text, txtAddress.Text, txtPhone.Text, cboSales.Text, string.Format("{0}", cboPaymentType.SelectedValue), Utilities.Username);
            }
            else
            {
                result = CustomerItem.Insert(txtName.Text, txtAddress.Text, txtPhone.Text, cboSales.Text, string.Format("{0}", cboPaymentType.SelectedValue), Utilities.Username);
            }
            if (result > 0)
            {

                if (ID > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Customer() { ID = ID, FullName = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));
                else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Customer() { ID = ID, FullName = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            List<Customer> list = CustomerItem.GetAll();

            cboSales.DataSource = list.Select(t => new { code = t.Sales, display = t.Sales }).Distinct().ToList();
            cboSales.ValueMember = "code";
            cboSales.DisplayMember = "display";

            cboPaymentType.DataSource = VariableItem.GetBycategory("PeymentType");
            cboPaymentType.ValueMember = "code";
            cboPaymentType.DisplayMember = "display";


            Customer prd = CustomerItem.GetByID(ID);
            if (prd != null)
            {
                txtAddress.Text = prd.Address;
                txtName.Text = prd.FullName;
                txtPhone.Text = prd.Phone;
                cboSales.SelectedValue = prd.Sales;
                cboPaymentType.SelectedValue = prd.PaymentType;
            }
        }

    }
}
