using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class CstmDailySale : IDataMapper<CstmDailySale>
    {
        public string CustomerName { get; set; }
        public string CatalogID { get; set; }
        public string CatalogName { get; set; }
        public string Sales { get; set; }
        public decimal TotalSale { get; set; }
        public decimal Qty { get; set; }
        public string MonthYear { get; set; }

        public DateTime TransDate { get; set; }

        public CstmDailySale Map(System.Data.IDataReader reader)
        {
            CstmDailySale obj = new CstmDailySale();
            obj.CustomerName = string.Format("{0}", reader["CUSTOMER_NAME"]);
            obj.CatalogID = string.Format("{0}", reader["CATALOG_ID"]);
            obj.CatalogName = string.Format("{0}", reader["CATALOG_NAME"]);
            obj.Sales = string.Format("{0}", reader["Sales"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.Qty = reader["Qty"] is DBNull ? 0 : Convert.ToDecimal(reader["Qty"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }
}
