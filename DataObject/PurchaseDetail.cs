using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class CstmPurchasePriceRate : IDataMapper<CstmPurchasePriceRate>
    {
        public decimal Price { get; set; }
        public DateTime? TransDate { get; set; }

        public CstmPurchasePriceRate Map(System.Data.IDataReader reader)
        {
            CstmPurchasePriceRate obj = new CstmPurchasePriceRate();
            obj.Price = Convert.ToDecimal(reader["PricePerUnit"]);
            obj.TransDate = (reader["PurchaseDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["PurchaseDate"]);
            return obj;
        }
    }
    public class CstmPurchaseDetail : IDataMapper<CstmPurchaseDetail>
    {
        public int ID { get; set; }

        public string PurchaseNo { get; set; }

        public int CatalogID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public decimal Qty { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }

        public string Unit { get; set; }

        public Guid UniqueID { get; set; }
        public DateTime? PurchaseDate { get; set; }

        public CstmPurchaseDetail Map(System.Data.IDataReader reader)
        {
            CstmPurchaseDetail obj = new CstmPurchaseDetail();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.PurchaseNo = reader["PurchaseNo"].ToString();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.Qty = (reader["Qty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Qty"]);
            obj.PricePerUnit = (reader["PricePerUnit"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["PricePerUnit"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.Unit = reader["Unit"].ToString();
            obj.CatalogName = reader["CatalogName"].ToString();
            obj.UniqueID = Guid.NewGuid();
            obj.PurchaseDate = (reader["PurchaseDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["PurchaseDate"]);
            return obj;
        }

        public string CatalogName { get; set; }
    }

    public class PurchaseDetail : IDataMapper<PurchaseDetail>
    {
        public int ID { get; set; }

        public string PurchaseNo { get; set; }

        public int CatalogID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public decimal Qty { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }

        public string Unit { get; set; }
        public decimal Coli { get; set; }
        public Guid UniqueID { get; set; }

        public PurchaseDetail Map(System.Data.IDataReader reader)
        {
            PurchaseDetail obj = new PurchaseDetail();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.PurchaseNo = reader["PurchaseNo"].ToString();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.Qty = (reader["Qty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Qty"]);
            obj.PricePerUnit = (reader["PricePerUnit"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["PricePerUnit"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.Unit = reader["Unit"].ToString();
            obj.CatalogName = reader["CatalogName"].ToString();
            obj.UniqueID = Guid.NewGuid();
            obj.Coli = (reader["Coli"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Coli"]);
            return obj;
        }

        public string CatalogName { get; set; }
    }


}
