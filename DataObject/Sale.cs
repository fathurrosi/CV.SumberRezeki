using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class Sale : IDataMapper<Sale>
    {
        public string TransactionID { get; set; }

        public decimal TotalPrice { get; set; }

        public string TotalQty { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Username { get; set; }

        public int? MemberID { get; set; }
        public string CustomerName { get; set; }

        public decimal? Tax { get; set; }

        public string Terminal { get; set; }

        public decimal TotalPayment { get; set; }

        public decimal? TotalPaymentReturn { get; set; }

        public string Notes { get; set; }

        public int PaymentType { get; set; }
        public int Counter { get; set; }
        public List<SaleDetail> Details { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public Sale Map(System.Data.IDataReader reader)
        {
            Sale obj = new Sale();
            obj.TransactionID = reader["TransactionID"].ToString();
            obj.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
            obj.TotalQty = string.Format("{0}", reader["TotalQty"]);
            obj.TransactionDate = Convert.ToDateTime(reader["TransactionDate"]);
            obj.Username = reader["Username"].ToString();
            obj.MemberID = (reader["MemberID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["MemberID"]);
            obj.Terminal = reader["Terminal"].ToString();
            obj.TotalPayment = Convert.ToDecimal(reader["TotalPayment"]);
            obj.TotalPaymentReturn = (reader["TotalPaymentReturn"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPaymentReturn"]);
            obj.ExpiredDate = (reader["ExpiredDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ExpiredDate"]);
            obj.Notes = reader["Notes"].ToString();
            obj.PaymentType = reader["PaymentType"] is System.DBNull ? 0 : Convert.ToInt32(reader["PaymentType"]);
            obj.Counter = reader["Counter"] is System.DBNull ?0: Convert.ToInt32(reader["Counter"]);

            return obj;
        }
    }

}

