using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class ColiPerCatalog : IDataMapper<ColiPerCatalog>
    {
        public int CatalogID { get; set; }
        public decimal TotalColi { get; set; }

        public ColiPerCatalog Map(System.Data.IDataReader reader)
        {
            ColiPerCatalog obj = new ColiPerCatalog();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.TotalColi = reader["TotalColi"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalColi"]);

            return obj;
        }
    }
    public class CurrentStock : IDataMapper<CurrentStock>
    {
        public int CatalogID { get; set; }

        public decimal Stock { get; set; }

        public decimal Coli { get; set; }

        public string CatalogName { get; set; }
        public string Unit { get; set; }

        public decimal TotalSales { get; set; }
        public DateTime? StockDate { get; set; }

        public CurrentStock Map(System.Data.IDataReader reader)
        {
            CurrentStock obj = new CurrentStock();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Stock = reader["Stock"] is DBNull ? 0 : Convert.ToDecimal(reader["Stock"]);
            obj.Coli = reader["Coli"] is DBNull ? 0 : Convert.ToDecimal(reader["Coli"]);
            obj.StockDate = reader["StockDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["StockDate"]);
            obj.Unit = reader["Unit"].ToString();
            obj.CatalogName = reader["CatalogName"].ToString();

            return obj;
        }
    }
}


