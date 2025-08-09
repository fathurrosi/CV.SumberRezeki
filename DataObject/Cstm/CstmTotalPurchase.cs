using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class CstmTotalPurchase : IDataMapper<CstmTotalPurchase>
    {
        public string Unit { get; set; }
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Quantity { get; set; }

        public DateTime PurchaseDate { get; set; }



        public CstmTotalPurchase Map(System.Data.IDataReader reader)
        {
            CstmTotalPurchase obj = new CstmTotalPurchase();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.TotalPrice = reader["TotalPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.PricePerUnit = reader["PricePerUnit"] is DBNull ? 0 : Convert.ToDecimal(reader["PricePerUnit"]);
            obj.PurchaseDate = (reader["PurchaseDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["PurchaseDate"]);
            obj.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToDecimal(reader["Quantity"]);
            return obj;
        }
    }

}
