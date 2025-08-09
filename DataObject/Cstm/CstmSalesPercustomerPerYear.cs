
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    

 
    public class CstmSalesPercustomerPerYear : IDataMapper<CstmSalesPercustomerPerYear>
    {

        public decimal TotalQty { get; set; }
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public DateTime Bulan { get; set; }
        public CstmSalesPercustomerPerYear Map(System.Data.IDataReader reader)
        {
            CstmSalesPercustomerPerYear obj = new CstmSalesPercustomerPerYear();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);

            DateTime bulan = new DateTime(1900, 1, 1);
            DateTime.TryParse(string.Format("{0}", reader["Bulan"]), out bulan);
            obj.Bulan = bulan;
            obj.TotalQty = (reader["TotalQty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalQty"]);
            return obj;
        }
    }

}
