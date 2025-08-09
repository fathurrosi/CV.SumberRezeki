using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class PurchaseReturn : IDataMapper<PurchaseReturn>
    {
        public int Counter { get; set; }
        public string TransactionID { get; set; }
        public string ReturnNo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }


        public string Supplier { get; set; }
        public string SupplierName { get; set; }

        public string Terminal { get; set; }
        public string NoStruk { get; set; }

        public string Notes { get; set; }

        public List<PurchaseReturnDetail> Details { get; set; }

        public PurchaseReturn Map(System.Data.IDataReader reader)
        {
            PurchaseReturn obj = new PurchaseReturn();
            obj.NoStruk = string.Format("{0}", reader["NoStruk"]);
            obj.Counter = (reader["Counter"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Counter"]);
            obj.TransactionID = reader["TransactionID"].ToString();
            obj.ReturnNo = reader["ReturnNo"].ToString();
            obj.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
            obj.TotalQty = Convert.ToDecimal(reader["TotalQty"]);
            obj.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
            obj.Created = Convert.ToDateTime(reader["Created"]);
            obj.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
            obj.Terminal = string.Format("{0}", reader["Terminal"]);
            obj.Supplier = string.Format("{0}", reader["Supplier"]);
            obj.SupplierName = string.Format("{0}", reader["Supplier"]);
            obj.Terminal = string.Format("{0}", reader["Terminal"]);

            obj.Notes = string.Format("{0}", reader["Notes"]);


            return obj;
        }

    }

}
