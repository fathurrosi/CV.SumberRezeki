using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class Variable : IDataMapper<Variable>
    {
        public string code { get; set; }
        public string display { get; set; }
        public string category { get; set; }
        public int sequence { get; set; }


        public Variable Map(System.Data.IDataReader reader)
        {
            Variable obj = new Variable();
            obj.code = string.Format("{0}", reader["code"]);
            obj.display = string.Format("{0}", reader["display"]);
            obj.category = string.Format("{0}", reader["category"]);
            obj.sequence = reader["sequence"] is System.DBNull ? 0 : Convert.ToInt32(reader["sequence"]);

            return obj;
        }
    }
}
