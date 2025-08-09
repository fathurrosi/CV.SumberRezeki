using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class Supplier : IDataMapper<Supplier>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }


        public Supplier Map(System.Data.IDataReader reader)
        {
            Supplier obj = new Supplier();
            obj.Code = reader["Code"].ToString();
            obj.Name = reader["Name"].ToString();
            obj.Address = reader["Address"].ToString();
            obj.Phone = reader["Phone"].ToString();
            obj.CellPhone = reader["CellPhone"].ToString();
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["CreatedDate"]);
            //Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            obj.ModifiedBy = reader["ModifiedBy"].ToString();

            return obj;
        }
    }
}
