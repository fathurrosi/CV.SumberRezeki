using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class Menu : IDataMapper<Menu>
    {
        private bool _Enabled = false;

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }


        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentID { get; set; }

        public int? Sequence { get; set; }

    
        public Menu Map(System.Data.IDataReader reader)
        {
            Menu obj = new Menu();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.Code = reader["Code"].ToString();
            obj.Name = reader["Name"].ToString();
            obj.Description = reader["Description"].ToString();
            obj.ParentID = Convert.ToInt32(reader["ParentID"]);
            obj.Sequence = (reader["Sequence"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Sequence"]);

            return obj;
        }
    }

}
