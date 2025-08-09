using DataLayer;
using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sendang.Rejeki.Master
{
    public partial class frmCatalog : Form, ITransButton
    {
        public frmCatalog()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        private byte[] _ProductImage;

        public bool IsValid()
        { 
            if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Jedis Daging tidak boleh kosong");
                txtName.Focus();
                return false;
            }
            else if (cboUnit.Text.Length == 0)
            {
                Utilities.ShowValidation("Satuan tidak boleh kosong");
                cboUnit.Focus();
                return false;
            }
            else if (cboType.Text.Length == 0)
            {
                Utilities.ShowValidation("Tipe tidak boleh kosong");
                cboType.Focus();
                return false;
            }
            //else if (txtDesc.Text.Length == 0) return false;
            //else if (txtNote.Text.Length == 0) return false;
            return true;
        }

        public void Save()
        {
            if (IsValid())
            {
                Catalog result = null;
                string type = cboType.Text.Length == 0 ? "Item" : cboType.Text;
                if (ID > 0)
                {
                    result = CatalogItem.Update(ID, txtName.Text, cboUnit.Text, string.Empty, string.Empty, _ProductImage, Utilities.Username, type);
                }
                else
                {
                    result = CatalogItem.Insert(txtName.Text, cboUnit.Text, string.Empty, string.Empty, _ProductImage, Utilities.Username, type);
                }
                if (result != null)
                {
                    if (ID > 0)
                        Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Catalog() { ID = ID, Name = txtName.Text, Unit = cboUnit.Text })));
                    else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Catalog() { ID = ID, Name = txtName.Text, Unit = cboUnit.Text })));
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(Config.GetCatalogUnit());
            cboType.Text = "Item";
            if (ID == 0) return;
            Catalog prd = CatalogItem.GetByID(ID);
            if (prd != null)
            {
                //txtDesc.Text = prd.Description;
                txtName.Text = prd.Name;
                //txtNote.Text = prd.Notes;
                cboUnit.Text = prd.Unit;
                cboType.Text = prd.Type;


                //if (prd.Photo != null)
                //{
                //    _ProductImage = prd.Photo;
                //    //ProductImage.Image = Utilities.Resize(prd.Photo, new Size(ProductImage.Width, ProductImage.Height));
                //}
            }
       
        }


        private void ProductImage_DoubleClick(object sender, EventArgs e)
        {
            fileDialog.DefaultExt = ".jpeg";
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = fileDialog.OpenFile();
                    if (stream != null)
                    {
                        _ProductImage = Utilities.StreamToBytes(stream);
                        //ProductImage.Image = Utilities.Resize(stream, ProductImage.Width, ProductImage.Height);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    //Log log = Log.CreateInstance();
                    Log.Error(ex.ToString());
                }
            }
        }
    }
}
