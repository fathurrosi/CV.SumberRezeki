using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class CstmStock : IDataMapper<CstmStock>
    {

        public int CatalogID { get; set; }

        public string Catalog { get; set; }

        public decimal Sisa { get; set; }

        public string Satuan { get; set; }

        public decimal Input { get; set; }


        public decimal Output { get; set; }

        public CstmStock Map(System.Data.IDataReader reader)
        {
            CstmStock obj = new CstmStock();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Sisa = reader["Sisa"] is DBNull ? 0 : Convert.ToDecimal(reader["Sisa"]);
            try
            {
                obj.Input = reader["Input"] is DBNull ? 0 : Convert.ToDecimal(reader["Input"]);
                obj.Output = reader["Output"] is DBNull ? 0 : Convert.ToDecimal(reader["Output"]);
            }
            catch (Exception)
            {

            }


            obj.Catalog = reader["Catalog"].ToString();
            obj.Satuan = reader["Satuan"].ToString();

            return obj;
        }
    }

}
