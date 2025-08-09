using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class SaleReturn : IDataMapper<SaleReturn>
    {
        public int Counter { get; set; }
        public string TransactionID { get; set; }
        public string ReturnNo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }


        public string Customer { get; set; }
        public string CustomerName { get; set; }

        public string Terminal { get; set; }
        public string NoStruk { get; set; }

        public string Notes { get; set; }

        public List<SaleReturnDetail> Details { get; set; }

        public SaleReturn Map(System.Data.IDataReader reader)
        {
            SaleReturn obj = new SaleReturn();
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
            obj.Customer = string.Format("{0}", reader["Customer"]);
            obj.CustomerName = string.Format("{0}", reader["CustomerName"]);
            obj.Terminal = string.Format("{0}", reader["Terminal"]);

            obj.Notes = string.Format("{0}", reader["Notes"]);


            return obj;
        }

    }

}
