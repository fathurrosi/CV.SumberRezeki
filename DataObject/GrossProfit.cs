using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class MonthlyGrossProfit
    {
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                string[] temps = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Nopember", "December" };
                if (Month == 0 || Month > 12) return "Unknown";
                return temps[Month];
            }
        }
        public decimal TotalPurchase { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalGP { get; set; }
    }

    public class GrossProfit : IDataMapper<GrossProfit>
    {
        public DateTime TransDate { get; set; }
        public decimal CatalogID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Purchase { get; set; }
        public decimal Sale { get; set; }
        public decimal TotalGrossProfit { get; set; }


        public GrossProfit Map(System.Data.IDataReader reader)
        {
            GrossProfit obj = new GrossProfit();
            obj.TransDate = Convert.ToDateTime(reader["TransDate"]);
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.Purchase = (reader["Purchase"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Purchase"]);
            obj.Sale = (reader["Sale"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Sale"]);
            obj.TotalGrossProfit = (reader["GrossProfit"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["GrossProfit"]);

            return obj;
        }
    }
}
