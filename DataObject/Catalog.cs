using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class Catalog : IDataMapper<Catalog>
    {
        public override string ToString()
        {
            return Name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }

        public Catalog Map(System.Data.IDataReader reader)
        {
            Catalog obj = new Catalog();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.Name = reader["Name"].ToString();
            obj.Unit = reader["Unit"].ToString();
            obj.Type = string.Format("{0}", reader["Type"]);
            obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
            obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            obj.ModifiedBy = reader["ModifiedBy"].ToString();
            return obj;
        }
    }

}
