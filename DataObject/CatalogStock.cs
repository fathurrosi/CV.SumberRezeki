using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class CatalogStock : IDataMapper<CatalogStock>
    {
        public int CatalogID { get; set; }

        public decimal Stock { get; set; }

        public DateTime StockDate { get; set; }

        public string CreatedBy { get; set; }
        public decimal Colly { get; set; }

        public CatalogStock Map(System.Data.IDataReader reader)
        {
            CatalogStock obj = new CatalogStock();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Stock = reader["Stock"] is DBNull ? 0 : Convert.ToDecimal(reader["Stock"]);
            obj.Colly = reader["Colly"] is DBNull ? 0 : Convert.ToDecimal(reader["Colly"]);
            obj.StockDate = (reader["StockDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["StockDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();

            return obj;
        }
    }
}
