using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class Purchase : IDataMapper<Purchase>
    {
        public string PurchaseNo { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Notes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string SupplierCode { get; set; }

        public decimal TotalQty { get; set; }

        public decimal TotalPrice { get; set; }
        public int Counter{ get; set; }


        public Purchase Map(System.Data.IDataReader reader)
        {
            Purchase obj = new Purchase();
            obj.PurchaseNo = reader["PurchaseNo"].ToString();
            obj.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
            obj.Notes = reader["Notes"].ToString();
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.SupplierCode = reader["SupplierCode"].ToString();
            obj.TotalQty = (reader["TotalQty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalQty"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.Counter = (reader["Counter"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Counter"]);

            return obj;
        }

        public List<PurchaseDetail> Details { get; set; }
    }


    public class CstmPurchase : IDataMapper<CstmPurchase>
    {
        public string PurchaseNo { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Notes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }

        public decimal TotalQty { get; set; }

        public decimal TotalPrice { get; set; }
        public int Counter{ get; set; }


        public CstmPurchase Map(System.Data.IDataReader reader)
        {
            CstmPurchase obj = new CstmPurchase();
            obj.PurchaseNo = reader["PurchaseNo"].ToString();
            obj.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
            obj.Notes = reader["Notes"].ToString();
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.SupplierCode = reader["SupplierCode"].ToString();
            obj.SupplierName = reader["SupplierName"].ToString();
            obj.TotalQty = (reader["TotalQty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalQty"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.Counter = (reader["Counter"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Counter"]);

            return obj;
        }

    }
}
