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
using System.IO;
using Microsoft.Reporting.WinForms;

namespace Sendang.Rejeki.Report
{
    public partial class frmRptItemPurchasedPerCustomer : Form
    {

        public frmRptItemPurchasedPerCustomer()
        {
            InitializeComponent();

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
        private void frmRptItemPurchasedPerCustomer_Load(object sender, EventArgs e)
        {
            this.Text = "Item Purchased Per Month";
            dtYear.Format = DateTimePickerFormat.Custom;
            dtYear.CustomFormat = "yyyy";
            dtYear.ShowUpDown = true;
            dtYear.Value = DateTime.Now;

            cboCustomer.DataSource = CustomerItem.GetAll().OrderBy(t => t.FullName).ToList();
            cboCustomer.ValueMember = "ID";
            cboCustomer.DisplayMember = "FullName";

            //cboBulan.DataSource = Utilities.GetAllMonth();
            //cboBulan.ValueMember = "ID";
            //cboBulan.DisplayMember = "Name";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int selectedYear = dtYear.Value.Year;

            dataGridView1.Columns.Clear();
            DataTable tbl = new DataTable();
            DataGridViewTextBoxColumn col = CreateColumn("Item", "CatalogName", "CatalogName", true);
            dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));
            col = CreateColumn("Catalog ID", "CatalogID", "CatalogID", false);
            dataGridView1.Columns.Add(col);
            tbl.Columns.Add(col.Name, typeof(string));

            int customerID = (int)cboCustomer.SelectedValue;
            DateTime thisMonth = new DateTime(selectedYear, 1, 1);
            List<Catalog> list = SaleDetailItem.GetCatalog(customerID, thisMonth);
            string[] monthList = new string[]{
                "Januari",
                "Februari",
                "Maret",
                "April",
                "Mei",
                "Juni",
                "Juli",
                "Agustus",
                "September",
                "Oktober",
                "November",
                "Desember"};

            if (list.Count > 0)
            {
                for (int i = 0; i < monthList.Length; i++)
                {

                    col = CreateColumn(monthList[i], monthList[i], string.Format("{0}", monthList[i]), true);
                    dataGridView1.Columns.Add(col);
                    tbl.Columns.Add(col.Name, typeof(decimal));
                }


                foreach (Catalog catalog in list)
                {
                    DataTable tempTable = SaleDetailItem.GetPerCustomerPerYear(customerID, catalog.ID, thisMonth);
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
                for (int i = 0; i < monthList.Length; i++)
                {

                    dataGridView1.Columns.Add(string.Format("{0}", monthList[0]), monthList[i]);
                }

                Utilities.ShowInformation("No record found!");
            }

            dataGridView1.DataSource = tbl;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedYear = dtYear.Value.Year;
            int customerID = (int)cboCustomer.SelectedValue;
            DateTime thisMonth = new DateTime(selectedYear, 1, 1);

            List<CstmSalesPercustomerPerYear> list = SaleDetailItem.GetTotalSalesPerCustomer(customerID, thisMonth);

            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            string title = string.Format("Item purchased by \"{0}\" per month", cboCustomer.Text);

            rptViewer.Params.Add(new ReportParameter("customer", title, false));
            rptViewer.ReportName = "SalesPercustomerPeryear";
            rptViewer.ReportPath = string.Format("{0}\\Report\\SalesPercustomerPeryear.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.Show();
        }
    }
}
