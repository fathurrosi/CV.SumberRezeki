using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class CstmCatalogPrice : IDataMapper<CstmCatalogPrice>
    {

        public int ID { get; set; }

        public int CatalogID { get; set; }

        public decimal BuyPricePerunit { get; set; }

        public decimal SellPrice { get; set; }

        public DateTime? PriceDate { get; set; }

        public string CreatedBy { get; set; }

        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string CatalogName { get; set; }


        public CstmCatalogPrice Map(System.Data.IDataReader reader)
        {
            CstmCatalogPrice obj = new CstmCatalogPrice();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.BuyPricePerunit = (reader["BuyPricePerunit"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["BuyPricePerunit"]);
            obj.SellPrice = (reader["SellPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["SellPrice"]);
            obj.PriceDate = (reader["PriceDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["PriceDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.SupplierCode = reader["SupplierCode"].ToString();
            obj.SupplierName = reader["SupplierName"].ToString();
            obj.CatalogName = reader["CatalogName"].ToString();

            return obj;
        }
    }

    public class CatalogPrice : IDataMapper<CatalogPrice>
    {


        public int ID { get; set; }

        public int CatalogID { get; set; }

        public decimal BuyPricePerunit { get; set; }

        public decimal SellPrice { get; set; }

        public DateTime PriceDate { get; set; }

        public string CreatedBy { get; set; }

        public string SupplierCode { get; set; }


        public CatalogPrice Map(System.Data.IDataReader reader)
        {
            CatalogPrice obj = new CatalogPrice();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.BuyPricePerunit = (reader["BuyPricePerunit"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["BuyPricePerunit"]);
            obj.SellPrice = (reader["SellPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["SellPrice"]);
            obj.PriceDate = (reader["PriceDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["PriceDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.SupplierCode = string.Format("{0}", reader["SupplierCode"]);

            return obj;
        }
    }
}
