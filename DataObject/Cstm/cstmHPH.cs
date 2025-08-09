using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class cstmHPP : IDataMapper<cstmHPP>
    {
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public DateTime TransDate { get; set; }
        public decimal? HPP { get; set; }
        public decimal? PrevStock { get; set; }
        public decimal? PrevHPP { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalQty { get; set; }
        public cstmHPP Map(System.Data.IDataReader reader)
        {
            cstmHPP obj = new cstmHPP();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.TransDate = Convert.ToDateTime(reader["TransDate"]);
            obj.HPP = reader["TotalHPP"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["TotalHPP"]);
            obj.PrevStock = reader["PrevStock"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["PrevStock"]);
            obj.PrevHPP = reader["PrevHPP"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["PrevHPP"]);
            obj.TotalPrice = reader["TotalPrice"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["TotalPrice"]);
            obj.TotalQty = reader["TotalQty"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["TotalQty"]);
            return obj;
        }
    }
}
