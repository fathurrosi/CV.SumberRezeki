using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class StockSummary : IDataMapper<StockSummary>
    {
        public int CatalogID { get; set; }

        public string CatalogName { get; set; }

        public decimal Stock { get; set; }

        public string Unit { get; set; }

        public StockSummary Map(System.Data.IDataReader reader)
        {
            StockSummary obj = new StockSummary();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Stock = 0;

            obj.CatalogName = reader["CatalogName"].ToString();
            obj.Unit = reader["Unit"].ToString();

            return obj;
        }
    }

}
