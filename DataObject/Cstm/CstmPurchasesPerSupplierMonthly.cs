using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class CstmPurchasesPerSupplierMonthly : IDataMapper<CstmPurchasesPerSupplierMonthly>
    {
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPurchasesAmount { get; set; }

        public CstmPurchasesPerSupplierMonthly Map(System.Data.IDataReader reader)
        {
            CstmPurchasesPerSupplierMonthly obj = new CstmPurchasesPerSupplierMonthly();
            obj.Item = string.Format("{0}", reader["Item"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.TotalPurchasesAmount = (reader["TotalPurchasesAmount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPurchasesAmount"]);
            return obj;
        }
    }
}
