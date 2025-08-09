using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class Piutang : IDataMapper<Piutang>
    {

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }

        public string CatalogID { get; set; }
        public string CatalogName { get; set; }

        public string TransactionID { get; set; }

        public DateTime TransDate { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal TotalPayment { get; set; }

        public decimal TotalPiutang
        {
            get
            {
                decimal piutang = 0;
                piutang = TotalPrice - TotalPayment;
                return piutang > 0 ? piutang : 0;

            }
        }
        public Piutang Map(System.Data.IDataReader reader)
        {
            Piutang obj = new Piutang();

            obj.CustomerName = string.Format("{0}", reader["CustomerName"]);
            obj.CustomerID = string.Format("{0}", reader["CustomerID"]);
            obj.TransactionID = string.Format("{0}", reader["TransactionID"]);

            //obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            //obj.CatalogID = string.Format("{0}", reader["CatalogID"]);

            obj.TotalPayment = reader["TotalPayment"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPayment"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);

            obj.TransDate = reader["TransDate"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TransDate"]);
            obj.ExpiredDate = reader["ExpiredDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["ExpiredDate"]);

            return obj;
        }
    }
}
