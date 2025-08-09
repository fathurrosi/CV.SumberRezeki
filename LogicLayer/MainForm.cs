using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataObject;

namespace LogicLayer
{

    public class MasterPage : Form
    {
        public MasterPage()
        {
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
        }

        public bool AllowCreate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowPrint { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            Form form = this;            //List<Privilege> prevList = PrivilegeItem.GetByUsername(Utilities.Username);            
            IMasterHeader header = null;
            try
            {
                AllowCreate = true;
                AllowDelete = true;
                AllowEdit = true;
                AllowPrint = true;
                header = (IMasterHeader)form;
                if (header != null && form.Tag != null)
                {
                    DataObject.Menu menu = (DataObject.Menu)form.Tag;
                    Privilege prev = Utilities.PrivilegeList.Where(t => t.MenuID == menu.ID).FirstOrDefault();
                    AllowCreate = prev.AllowCreate;
                    AllowEdit = prev.AllowUpdate;
                    AllowPrint = prev.AllowPrint;
                    AllowDelete = prev.AllowDelete;
                }
            }
            catch (Exception)
            {

            }


            base.OnLoad(e);
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (!AllowEdit)
                {
                    Utilities.ShowValidation("Anda tidak punya akses untuk mengedit data ini");
                    return;
                }
                _Edit();
            }
            
            if (e.Control && e.Shift && e.KeyCode == Keys.Enter) _Bayar();
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (!AllowCreate)
                {
                    Utilities.ShowValidation("Anda tidak punya akses untuk menambah data");
                    return;
                }
                _Add();
            }
            else if (e.Control && e.KeyCode == Keys.S) _Save();
            else if (e.Control && e.KeyCode == Keys.P) _Print();
            else if (e.KeyCode == Keys.Enter) _OK();
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (!AllowDelete)
                {
                    Utilities.ShowValidation("Anda tidak punya akses untuk menghapus data ini");
                    return;
                }
                _Delete();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (this.Modal) DialogResult = System.Windows.Forms.DialogResult.Cancel;
                else
                {
                    if (MessageBox.Show("Anda yakin ingin menutup halaman ini?", "Confirmation", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
        }

        public virtual void _Print() { }
        public virtual void _Bayar() { }
        public virtual void _OK() { }
        public virtual void _Save() { }
        public virtual void _Add() { }
        public virtual void _Edit() { }
        public virtual void _Delete() { }
    }
}
