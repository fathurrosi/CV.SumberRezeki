using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class ReconcileDetail : IDataMapper<ReconcileDetail>
    {
        public int ID { get; set; }
        public int CatalogID { get; set; }
        public decimal CatalogQty { get; set; }
        public decimal CatalogPrice { get; set; }
        public string CatalogUnit { get; set; }
        public string ProductUnit { get; set; }
        public int ProductID { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductQty { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ReconcileID { get; set; }
        public DateTime? CatalogPriceDate { get; set; }
        public ReconcileDetail Map(System.Data.IDataReader reader)
        {
            ReconcileDetail obj = new ReconcileDetail();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogQty = reader["CatalogQty"] is DBNull ? 0 : Convert.ToDecimal(reader["CatalogQty"]);
            obj.CatalogPrice = reader["CatalogPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["CatalogPrice"]);
            obj.ProductID = Convert.ToInt32(reader["ProductID"]);
            obj.ProductPrice = reader["ProductPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["ProductPrice"]);
            obj.ProductQty = reader["ProductQty"] is DBNull ? 0 : Convert.ToDecimal(reader["ProductQty"]);
            obj.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
            obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
            obj.ModifiedBy = string.Format("{0}", reader["ModifiedBy"]);
            obj.ModifiedDate = reader["ModifiedDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            obj.CatalogPriceDate = reader["CatalogPriceDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["CatalogPriceDate"]);
            obj.ReconcileID = Convert.ToInt32(reader["ReconcileID"]);
            return obj;
        }
    }

    public class Reconcile : IDataMapper<Reconcile>
    {
        public int ID { get; set; }
        public DateTime ProccessDate { get; set; }
        public string Description { get; set; }
        public string PurchaseNo { get; set; }
        public string TransactionID{ get; set; }

        public Reconcile Map(System.Data.IDataReader reader)
        {
            Reconcile obj = new Reconcile();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.ProccessDate = Convert.ToDateTime(reader["ProccessDate"]);
            obj.Description = string.Format("{0}", reader["Description"]);
            obj.PurchaseNo = string.Format("{0}", reader["PurchaseNo"]);
            obj.TransactionID = string.Format("{0}", reader["TransactionID"]);
            return obj;
        }
    }
}
