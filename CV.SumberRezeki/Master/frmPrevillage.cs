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


namespace Sendang.Rejeki.Master
{
    public partial class frmPrevillage : Form, ITransButton
    {
        
        List<Privilege> PrevillageList = new List<Privilege>();
        public enum RoleAccess
        {
            AllowRead,
            AllowCreate,
            AllowUpdate,
            AllowDelete,
            AllowPrint
        }

        public class ItemHelper
        {
            public ItemHelper(RoleAccess roleAccess, int roleID, int menuID, bool check)
            {
                RoleAccess = roleAccess;
                RoleID = roleID;
                MenuID = menuID;
                Check = check;
            }
            public int RoleID { get; set; }
            public int MenuID { get; set; }
            public RoleAccess RoleAccess { get; set; }
            public bool Check { get; set; }
            public override string ToString()
            {
                return RoleAccess.ToString();
            }
        }

        public frmPrevillage()
        {
            InitializeComponent();
        }

        public int SelectedRoleID
        {
            get
            {
                int roleID = 0;
                int.TryParse(cboRole.SelectedValue.ToString(), out roleID);
                return roleID;
            }
        }

        private void frmPrevillage_Load(object sender, EventArgs e)
        {
            cboRole.ValueMember = "ID";
            cboRole.DisplayMember = "Name";
            cboRole.DataSource = RoleItem.GetRoles();
            cboRole.SelectedIndex = 0;

            LoadData();
        }

        void LoadData()
        {
            PrevillageList = PrivilegeItem.GetByRoleID(SelectedRoleID);

            tvPrevillage.Nodes.Clear();
            List<DataObject.Menu> list = DataLayer.MenuItem.GetMenus();
            List<DataObject.Menu> parentList = list.Where(t => t.ParentID == 0).OrderBy(t => t.Sequence).ToList();
            TreeNode root = new TreeNode();
            root.Text = "Root";
            root.Name = "Root";

            parentList.ForEach(t =>
            {
                TreeNode parent = new TreeNode();
                parent.Name = t.ID.ToString();
                parent.Text = t.Name;
                parent.Tag = t;
                parent.ToolTipText = t.Description;
                Privilege item = PrevillageList.Where(p => p.MenuID == t.ID).FirstOrDefault();
                parent.Checked = (item != null) ? item.AllowRead : false;

                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(parent, list, t.ID, PrevillageList);

                root.Nodes.Add(parent);
            });

            tvPrevillage.Nodes.Add(root);
            tvPrevillage.ExpandAll();
        }

        void LoadMenu(TreeNode tMenuItem, List<DataObject.Menu> list, int parentID, List<Privilege> prevList)
        {
            list.Where(t => t.ParentID == parentID).ToList().ForEach(t =>
            {
                TreeNode child = new TreeNode();
                child.Name = t.Code;
                child.Text = t.Name;
                child.Tag = t;
                child.ToolTipText = t.Description;

                Privilege item = prevList.Where(p => p.MenuID == t.ID).FirstOrDefault();
                child.Checked = (item != null) ? item.AllowRead : false;

                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(child, list, t.ID, prevList);
                tMenuItem.Nodes.Add(child);
            });
        }

        void SetRoleAccess(int roleID, int menuID, bool isChecked, RoleAccess roleAccess)
        {
            Privilege item = null;
            if (PrevillageList.Where(t => t.RoleID == roleID && t.MenuID == menuID).Count() == 0)
            {
                item = new Privilege();
                item.MenuID = menuID;
                item.RoleID = roleID;
                if (roleAccess == RoleAccess.AllowRead) item.AllowRead = isChecked;
                else if (roleAccess == RoleAccess.AllowCreate) item.AllowCreate = isChecked;
                else if (roleAccess == RoleAccess.AllowDelete) item.AllowDelete = isChecked;
                else if (roleAccess == RoleAccess.AllowPrint) item.AllowPrint = isChecked;
                else if (roleAccess == RoleAccess.AllowUpdate) item.AllowUpdate = isChecked;
                PrevillageList.Add(item);
            }
            else
            {
                for (int i = 0; i < PrevillageList.Count; i++)
                {
                    item = PrevillageList[i];
                    if (item.MenuID == menuID && item.RoleID == roleID)
                    {
                        if (roleAccess == RoleAccess.AllowRead) PrevillageList[i].AllowRead = isChecked;
                        else if (roleAccess == RoleAccess.AllowCreate) PrevillageList[i].AllowCreate = isChecked;
                        else if (roleAccess == RoleAccess.AllowDelete) PrevillageList[i].AllowDelete = isChecked;
                        else if (roleAccess == RoleAccess.AllowPrint) PrevillageList[i].AllowPrint = isChecked;
                        else if (roleAccess == RoleAccess.AllowUpdate) PrevillageList[i].AllowUpdate = isChecked;
                        break;
                    }
                }
            }
        }

        void CheckAllChildNodes(TreeNode tNode, bool nodeChecked)
        {
            foreach (TreeNode node in tNode.Nodes)
            {
                node.Checked = nodeChecked;
                DataObject.Menu menu = (DataObject.Menu)node.Tag;
                if (menu != null) SetRoleAccess(SelectedRoleID, menu.ID, node.Checked, RoleAccess.AllowRead);

                if (node.Nodes.Count > 0)
                    CheckAllChildNodes(node, nodeChecked);
            }
        }

        public bool IsValid()
        {
            if (cboRole.SelectedIndex == -1)
            {
                cboRole.Focus();
                return false;
            }
            return true;
        }

        public void Save()
        {
            try
            {
                PrivilegeItem.Update(PrevillageList);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Utilities.ShowInformation("Data saved!");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Utilities.ShowInformation(ex.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        List<Privilege> IterateTreeNode(TreeNode node)
        {
            List<Privilege> pList = new List<Privilege>();
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Checked)
                {
                    DataObject.Menu menu = (DataObject.Menu)childNode.Tag;
                    Privilege p = new Privilege();
                    p.MenuID = menu.ID;
                    p.RoleID = SelectedRoleID;
                    pList.Add(p);
                }

                if (childNode.Nodes.Count > 0)
                {
                    List<Privilege> result = IterateTreeNode(childNode);
                    pList.AddRange(result);
                }
            }
            return pList;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                DataObject.Menu menu = (DataObject.Menu)e.Node.Tag;
                if (menu != null) SetRoleAccess(SelectedRoleID, menu.ID, e.Node.Checked, RoleAccess.AllowRead);

                if (e.Node.Nodes.Count > 0)
                    CheckAllChildNodes(e.Node, e.Node.Checked);
            }
        }

        private void tvPrevillage_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var temp = e.Action;
            //clbPrevillage.Items.Clear();
            TreeNode node = e.Node;
            DataObject.Menu m = (DataObject.Menu)node.Tag;
            if (m == null) return;
            int roleID = 0;
            int.TryParse(string.Format("{0}", cboRole.SelectedValue), out roleID);

            List<Privilege> list = DataLayer.PrivilegeItem.GetByRoleAndMenuID(roleID, m.ID);
            Privilege item = list.FirstOrDefault();

            bool allowCreate = (item == null) ? false : item.AllowCreate;
            bool allowUpdate = (item == null) ? false : item.AllowUpdate;
            bool allowDelete = (item == null) ? false : item.AllowDelete;
            bool allowPrint = (item == null) ? false : item.AllowPrint;
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowCreate, roleID, m.ID, allowCreate), allowCreate);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowUpdate, roleID, m.ID, allowUpdate), allowUpdate);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowDelete, roleID, m.ID, allowDelete), allowDelete);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowPrint, roleID, m.ID, allowPrint), allowPrint); ;
        }

        private void clbPrevillage_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //ItemHelper itemHelper = (ItemHelper)clbPrevillage.SelectedItem;
            //if (itemHelper != null)
            //{
            //    bool check = e.NewValue.ToString().ToLower() == "checked" ? true : false;
            //    SetRoleAccess(itemHelper.RoleID, itemHelper.MenuID, check, itemHelper.RoleAccess);
            //}
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

 
    }
}
