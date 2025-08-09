using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject.Cstm
{
    public class CstmSales : IDataMapper<CstmSales>
    {
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public int CatalogID { get; set; }
        public decimal Sales { get; set; }

        public CstmSales Map(System.Data.IDataReader reader)
        {
            CstmSales obj = new CstmSales();
            obj.Bulan = (reader["Bulan"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Bulan"]);
            obj.Tahun = (reader["Tahun"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Tahun"]);
            obj.CatalogID = (reader["CatalogID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["CatalogID"]);
            obj.Sales = (reader["Sales"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Sales"]);
            return obj;
        }
    }
}
