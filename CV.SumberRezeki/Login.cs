using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

using LogicLayer;
using DataLayer;
using DataObject;

namespace Sendang.Rejeki
{
    public partial class Login : MetroFramework.Forms.MetroForm// Form
    {
        public Login()
        {
            InitializeComponent();
#if DEBUG
            txtPassword.Text = "admin";
            txtUsername.Text = "admin";
#endif
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            //LogicLayer.Log log = LogicLayer.Log.CreateInstance();
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("User Name is empty, please fill User Name!");
                txtUsername.Focus();
                return;
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Password is empty, please fill Password!");
                txtPassword.Focus();
                return;
            }

            try
            {
                User user = UserItem.GetUser(txtUsername.Text);
                if (user.Password == Security.Encrypt(txtPassword.Text.Trim()))
                {
                    Utilities.Username = user.Username;
                    Utilities.CurrentUser = user;
                    UserItem.UpdateLogin(Utilities.Username, Utilities.GetComputerName(), Utilities.GetIpAddress());

                    Log.Info(string.Format("{0} logged in", user.Username));
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    Log.Info(string.Format("{0} tried to login", txtUsername.Text));
                    MessageBox.Show("Username or Password not match!");
                    //this.DialogResult = System.Windows.Forms.DialogResult.Retry;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ups...! System unable to connect to database\nJangan lupa udah dulu seting di App.Config!");
                //this.DialogResult = System.Windows.Forms.DialogResult.Retry;               
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnOK_Click(sender, null);
        }
    }
}
