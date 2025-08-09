using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{

    public class RoleItem
    {
        public static List<Role> GetPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT * from Role WHERE Name LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Role> result = DBUtil.ExecuteMapper<Role>(context, new Role());

            return result;
        }



        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT count(*) from Role WHERE Name LIKE concat ('%', @text ,'%')   
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }



        public static List<Role> GetByUsername(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT r.*
  FROM UserRole
  INNER JOIN Role r ON r.ID = RoleID
  WHERE UserRole.Username =@Username
";
            context.AddParameter("@Username", Username);
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<Role>(context, new Role());
        }

        public static List<Role> GetRoles()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "SELECT *  FROM Role";
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Role>(context, new Role());
        }


        public static Role GetRoleByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT *
  FROM Role
  where ID =@ID
";
            context.AddParameter("@ID", ID);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Role>(context, new Role()).FirstOrDefault();
        }



        public static int Insert(string Name, string Description, string Username)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@CreatedBy", Username);
            context.CommandText = @"
            INSERT INTO Role
           (Name
           ,Description
           ,CreatedDate
           ,CreatedBy)
     VALUES
           (@Name
           ,@Description
           ,Now()
           ,@CreatedBy)
            ";
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Update(int ID, string Name, string Description, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
UPDATE Role
   SET Name = @Name
      ,Description = @Description      
      ,ModifiedDate = NOW()
      ,ModifiedBy = @ModifiedBy
 WHERE ID=@ID
";
            context.AddParameter("@ID", ID);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ModifiedBy", Username);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Delete(int id)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
DELETE FROM Role
      WHERE ID=@ID
"; context.AddParameter("@ID", id);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
