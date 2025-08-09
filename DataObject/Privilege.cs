using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class Privilege : IDataMapper<Privilege>
    {

        public int MenuID { get; set; }

        public int RoleID { get; set; }

        public bool AllowCreate { get; set; }

        public bool AllowRead { get; set; }

        public bool AllowUpdate { get; set; }

        public bool AllowDelete { get; set; }

        public bool AllowPrint { get; set; }


        public Privilege Map(System.Data.IDataReader reader)
        {
            Privilege obj = new Privilege();
            obj.MenuID = (reader["MenuID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["MenuID"]);
            obj.RoleID = (reader["RoleID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["RoleID"]);
            obj.AllowCreate = reader["AllowCreate"] is DBNull ? false : Convert.ToBoolean(reader["AllowCreate"]);
            obj.AllowRead = reader["AllowRead"] is DBNull ? false : Convert.ToBoolean(reader["AllowRead"]);
            obj.AllowUpdate = reader["AllowUpdate"] is DBNull ? false : Convert.ToBoolean(reader["AllowUpdate"]);
            obj.AllowDelete = reader["AllowDelete"] is DBNull ? false : Convert.ToBoolean(reader["AllowDelete"]);
            obj.AllowPrint = reader["AllowPrint"] is DBNull ? false : Convert.ToBoolean(reader["AllowPrint"]);

            return obj;
        }
    }

}
