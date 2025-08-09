using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class CstmBuyPrice : IDataMapper<CstmBuyPrice>
    {
        public string Unit { get; set; }
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }

        public decimal TotalBuyPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime TransDate { get; set; }
        public decimal TotalItem { get; set; }
        public decimal BuyPrice { get; set; }


        public CstmBuyPrice Map(System.Data.IDataReader reader)
        {
            CstmBuyPrice obj = new CstmBuyPrice();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);

            //obj.TotalItem = reader["TotalItem"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalItem"]);
            obj.BuyPrice = reader["BuyPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["BuyPrice"]);

            obj.TotalBuyPrice = reader["TotalBuyPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalBuyPrice"]);
            obj.TotalPrice = reader["TotalPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.TransDate = (reader["TransDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TransDate"]);
            obj.TotalQty = reader["TotalQty"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalQty"]);

            obj.Unit = string.Format("{0}", reader["Unit"]);

            return obj;
        }
    }


}
