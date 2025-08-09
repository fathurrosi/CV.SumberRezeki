using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{


    public class CstmSellPrice : IDataMapper<CstmSellPrice>
    {
        public string Unit { get; set; }
        //public int CustomerID { get; set; }
        public int CatalogID { get; set; }
        //public string CustomerName { get; set; }
        public string CatalogName { get; set; }

        public decimal TotalSellPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime TransDate { get; set; }
        public decimal TotalItem { get; set; }
        public decimal SellPrice { get; set; }


        public CstmSellPrice Map(System.Data.IDataReader reader)
        {
            CstmSellPrice obj = new CstmSellPrice();
            //obj.CustomerID = Convert.ToInt32(reader["CustomerID"]);
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            //obj.CustomerName = string.Format("{0}", reader["CustomerName"]);

            obj.TotalItem = reader["TotalItem"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalItem"]);
            obj.SellPrice = reader["SellPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["SellPrice"]);

            obj.TotalSellPrice = reader["TotalSellPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSellPrice"]);
            obj.TotalPrice = reader["TotalPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.TransDate = (reader["TransDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TransDate"]);
            obj.TotalQty = reader["TotalQty"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalQty"]);

            obj.Unit = string.Format("{0}", reader["Unit"]);

            return obj;
        }
    }

}
