using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class VariableItem
    {
        public static List<Variable> GetBycategory(string category)
        {
            string query = @"
select * from  variable where category=@category
Order by Sequence asc;
";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@category", category);
            return DBUtil.ExecuteMapper<Variable>(context, new Variable());
        }
    }
}
