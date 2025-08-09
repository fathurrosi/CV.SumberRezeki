using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using DataLayer;
using DataObject;
using LogicLayer;

namespace Sendang.Rejeki.Master
{
    public partial class frmCompanyProfile : MasterPage, ITransButton
    //public partial class frmCompanyProfile : Form, ITransButton
    {
        public frmCompanyProfile()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            if (txtNamaPerusahaan.Text.Length == 0)
            {
                Utilities.ShowValidation("Nama Perusahaan harus diisi!");
                txtNamaPerusahaan.Focus();
                return false;
            }
            if (txtBidangPerusahaan.Text.Length == 0)
            {
                Utilities.ShowValidation("Bidang Perusahaan harus diisi!");
                txtBidangPerusahaan.Focus();
                return false;
            }
            else if (txtPhone.Text.Length == 0)
            {
                Utilities.ShowValidation("Telp 1 harus diisi!");
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        public override void _Save()
        {
            if (IsValid())
            {
                Save();
            }
            base._Save();
        }

        public void Save()
        {
            int id = 0;
            int.TryParse(txtID.Text, out id);
            Profile item = ProfileItem.GetByID(id);
            if (item == null) item = new Profile();
            item.tweeter = txtTweeter.Text;
            item.facebook = txtFacebook.Text;
            item.instagram = txtInstagram.Text;
            //update
            item.Nama = txtNamaPerusahaan.Text;
            item.Keterangan = txtBidangPerusahaan.Text;
            item.Alamat = txtAddress.Text;
            item.Alamat2 = txtAddress2.Text;

            item.LabelAlamat = txtAddressName.Text;
            item.LabelAlamat2 = txtAddressName2.Text;
            item.email = txtEmail.Text;
            item.web = txtWeb.Text;
            item.Telp1 = txtPhone.Text;
            item.Telp2 = txtPhone2.Text;
            item.Logo = Utilities.GetBytes(pictureBox1.Image);
            item.LogoExtension = txtLogoExt.Text.ToLower();
            int result = ProfileItem.Update(item);

            if (id > 0)
            {
                Utilities.ShowInformation("Data sudah berhasil disimpan");
                Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item)));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else this.DialogResult = System.Windows.Forms.DialogResult.Retry;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmCompanyProfile_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::CV.SumberRezeki.Properties.Resources.image;
            int id = 0;
            int.TryParse(txtID.Text, out id);
            Profile item = ProfileItem.GetByID(id);
            if (item != null)
            {
                txtFacebook.Text = item.facebook;
                txtTweeter.Text = item.tweeter;
                txtInstagram.Text = item.instagram;
                txtNamaPerusahaan.Text = item.Nama;
                txtBidangPerusahaan.Text = item.Keterangan;
                txtAddressName.Text = item.LabelAlamat;
                txtAddressName2.Text = item.LabelAlamat2;
                txtAddress.Text = item.Alamat;
                txtAddress2.Text = item.Alamat2;
                txtEmail.Text = item.email;
                txtWeb.Text = item.web;
                txtPhone.Text = item.Telp1;
                txtPhone2.Text = item.Telp2;
                if (item.Logo != null)
                {
                    txtLogoExt.Text = item.LogoExtension;
                    pictureBox1.Image = Utilities.BytesToImage(item.Logo);
                }
            }

        }

        //public Image Photo { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files | *.jpg;*.jpeg;*.png;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //txtStudentImage.Text = openFileDialog1.FileName;                                
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                txtLogoExt.Text = System.IO.Path.GetExtension(openFileDialog1.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

    }
}
