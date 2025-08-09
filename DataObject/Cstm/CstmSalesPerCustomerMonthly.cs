using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class CstmSalesPerCustomerMonthly : IDataMapper<CstmSalesPerCustomerMonthly>
    {
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalSalesAmount { get; set; }

        public CstmSalesPerCustomerMonthly Map(System.Data.IDataReader reader)
        {
            CstmSalesPerCustomerMonthly obj = new CstmSalesPerCustomerMonthly();
            obj.Item = string.Format("{0}", reader["Item"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.TotalSalesAmount = (reader["TotalSalesAmount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalSalesAmount"]);
            return obj;
        }
    }

}
