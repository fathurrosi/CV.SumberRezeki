using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class Role : IDataMapper<Role>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }

        public Role Map(System.Data.IDataReader reader)
        {
            Role obj = new Role();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.Name = reader["Name"].ToString();
            obj.Description = reader["Description"].ToString();
            obj.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            obj.ModifiedBy = reader["ModifiedBy"].ToString();

            return obj;
        }
    }
}
