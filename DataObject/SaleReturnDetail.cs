using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class SaleReturnDetail : IDataMapper<SaleReturnDetail>
    {
        public Guid UniqueID { get; set; }
        public int ID { get; set; }
        public string ReturnNo { get; set; }
        public string CatalogName { get; set; }
        public string Catalog { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Qty { get; set; }
        public decimal Colly { get; set; }
        public decimal TotalPrice { get; set; }
        public string Unit { get; set; }
        //public string UnitName { get; set; }
        public int Sequence { get; set; }

        public SaleReturnDetail Map(System.Data.IDataReader reader)
        {
            SaleReturnDetail obj = new SaleReturnDetail();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.CatalogName = reader["CatalogName"].ToString();
            obj.Unit = reader["Unit"].ToString();
            //obj.UnitName = reader["UnitName"].ToString();
            obj.ReturnNo = reader["ReturnNo"].ToString();
            obj.Catalog = string.Format("{0}", reader["Catalog"]);
            obj.Price = (reader["Price"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Price"]);
            obj.Discount = (reader["Discount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Discount"]);
            obj.Qty = (reader["Qty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Qty"]);
            obj.Colly = (reader["Colly"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Colly"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.Sequence = (reader["Sequence"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Sequence"]);

            return obj;
        }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Catalog Item { get; set; }
    }
}