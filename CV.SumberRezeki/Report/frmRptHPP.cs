using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using DataObject;
using LogicLayer;
using LogicLayer.Helper;

namespace Sendang.Rejeki.Report
{
    public partial class frmRptHPP : Form
    {
        public frmRptHPP()
        {
            InitializeComponent();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            DateTime selected = dateTimePicker1.Value;
            List<cstmHPP> list = HPPItem.GetPerMonth(selected);
            //dataGridView1.Columns.Clear();
            DataTable tbl = new DataTable();
            DataGridViewTextBoxColumn col = CreateColumn("Item", "CatalogName", "CatalogName", true);
            dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));
            col = CreateColumn("Catalog ID", "CatalogID", "CatalogID", false);
            //dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));

            List<int> catalogList = new List<int>();
            catalogList = list.Select(t => t.CatalogID).Distinct().ToList();


            int lastDay = DateTime.DaysInMonth(selected.Year, selected.Month);
            if (catalogList.Count > 0)
            {
                int maxDay = list.Select(t => t.TransDate).Max().Day;
                for (int i = 1; i <= lastDay; i++)
                {
                    DateTime dt = new DateTime(selected.Year, selected.Month, i);
                    //col = CreateColumn(string.Format("{0:ddMMM}", dt), string.Format("{0:ddMMM}", dt), string.Format("{0:ddMMM}", dt), maxDay >= i);
                    //dataGridView1.Columns.Add(col);
                    tbl.Columns.Add(string.Format("col{0}", dt.Day), typeof(decimal));
                }


                foreach (int catalogID in catalogList)
                {
                    DataTable tempTable = HPPItem.GetHPPTablePerMonth(selected, catalogID);
                    DataRow newRow = tbl.NewRow();
                    foreach (DataRow dr in tempTable.Rows)
                    {
                        for (int i = 0; i < tbl.Columns.Count; i++)
                        {
                            newRow[tbl.Columns[i].ColumnName] = dr[tbl.Columns[i].ColumnName];
                        }
                    }

                    tbl.Rows.Add(newRow);
                }
            }
            else
            {
                //for (int i = 1; i <= lastDay; i++)
                //{
                //    DateTime dt = new DateTime(selected.Year, selected.Month, i);
                //    dataGridView1.Columns.Add(string.Format("col{0}", dt.Day), string.Format("{0:dd-MMM}", dt));
                //}

                Utilities.ShowInformation("No record found!");
            }


            string path = Excel.WriteExcel("xlsx", tbl, selected);

            frmDialogResult f = new frmDialogResult();
            f.Message = "Excel sudah di buat di :";
            f.Result = path;
            f.Title = "Daily Gross Profit";
            f.ShowDialog();
        }
        private void frmRptHPP_Load(object sender, EventArgs e)
        {
            this.Text = "Monthly HPP Report";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM-yyyy";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Value = DateTime.Now;
        }

        private DataGridViewTextBoxColumn CreateColumn(string headerText, string propName, string colName, bool isVisible)
        {
            DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            cellStyle.Format = "N2";
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DefaultCellStyle = cellStyle;
            col.HeaderText = headerText;// string.Format("{0:dd-MMM}", dt);
            col.Name = colName;// string.Format("col{0}", dt.Day);
            col.DataPropertyName = propName;// string.Format("col{0}", dt.Day);
            col.ReadOnly = true;
            col.Visible = isVisible;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            return col;
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            DateTime selected = dateTimePicker1.Value;
            List<cstmHPP> list = HPPItem.GetPerMonth(selected);
            dataGridView1.Columns.Clear();
            DataTable tbl = new DataTable();
            DataGridViewTextBoxColumn col = CreateColumn("Item", "CatalogName", "CatalogName", true);
            dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));
            col = CreateColumn("Catalog ID", "CatalogID", "CatalogID", false);
            dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));

            List<int> catalogList = new List<int>();
            catalogList = list.Select(t => t.CatalogID).Distinct().ToList();


            int lastDay = DateTime.DaysInMonth(selected.Year, selected.Month);
            if (catalogList.Count > 0)
            {
                int maxDay = list.Select(t => t.TransDate).Max().Day;
                for (int i = 1; i <= lastDay; i++)
                {
                    DateTime dt = new DateTime(selected.Year, selected.Month, i);
                    col = CreateColumn(string.Format("{0:dd-MMM}", dt), string.Format("col{0}", dt.Day), string.Format("col{0}", dt.Day), maxDay >= i);
                    dataGridView1.Columns.Add(col);
                    tbl.Columns.Add(col.Name, typeof(decimal));
                }


                foreach (int catalogID in catalogList)
                {
                    DataTable tempTable = HPPItem.GetHPPTablePerMonth(selected, catalogID);
                    DataRow newRow = tbl.NewRow();
                    foreach (DataRow dr in tempTable.Rows)
                    {
                        for (int i = 0; i < tbl.Columns.Count; i++)
                        {
                            newRow[tbl.Columns[i].ColumnName] = dr[tbl.Columns[i].ColumnName];
                        }
                    }

                    tbl.Rows.Add(newRow);
                }
            }
            else
            {
                for (int i = 1; i <= lastDay; i++)
                {
                    DateTime dt = new DateTime(selected.Year, selected.Month, i);
                    dataGridView1.Columns.Add(string.Format("col{0}", dt.Day), string.Format("{0:dd-MMM}", dt));
                }

                Utilities.ShowInformation("No record found!");
            }

            dataGridView1.DataSource = tbl;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DateTime selected = dateTimePicker1.Value;
            List<cstmHPP> list = HPPItem.GetPerMonth(selected);
            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            rptViewer.ReportName = "HPPChart";
            rptViewer.ReportPath = string.Format("{0}\\Report\\HPPChart.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.MdiParent = this.ParentForm;
            rptViewer.Show();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
