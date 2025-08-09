using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sendang.Rejeki.Transaction;
using DataLayer;
using DataObject;

namespace Sendang.Rejeki
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Log log = Log.CreateInstance();
            Login login = new Login();
            try
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    frmMain form = new frmMain();
                    //frmReconcile form = new frmReconcile();
                    Application.Run(form);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
                Log.Error(ex.Message);
            }

        }


    }
}