using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class CstmMenu : IDataMapper<CstmMenu>
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentID { get; set; }

        public int? Sequence { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string ParentDescription { get; set; }


        public CstmMenu Map(System.Data.IDataReader reader)
        {
            CstmMenu obj = new CstmMenu();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.Name = reader["Name"].ToString();
            obj.Code = reader["Code"].ToString();
            obj.Description = reader["Description"].ToString();

            obj.ParentName = reader["ParentName"].ToString();
            obj.ParentCode = reader["ParentCode"].ToString();
            obj.ParentDescription = reader["ParentDescription"].ToString();
            obj.ParentID = Convert.ToInt32(reader["ParentID"]);
            obj.Sequence = (reader["Sequence"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Sequence"]);

            return obj;
        }
    }
}
