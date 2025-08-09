using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class PrivilegeItem
    {
        public static List<Privilege> GetByRoleID(int roleID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT *
  FROM Privilege
  where RoleID =@RoleID";
            context.AddParameter("@RoleID", roleID);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Privilege>(context, new Privilege());
        }

        public static List<Privilege> GetByRoleAndMenuID(int roleID, int menuID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT *
  FROM Privilege
  where RoleID =@RoleID
  and MenuID =@MenuID
";
            context.AddParameter("@RoleID", roleID);
            context.AddParameter("@MenuID", menuID);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Privilege>(context, new Privilege());
        }
        
        public static int Update(List<Privilege> list)
        {
            int result = -1;
            IDBHelper ctx = new DBHelper();
            ctx.BeginTransaction();
            ctx.CommandType = CommandType.Text;
            try
            {

                foreach (Privilege item in list)
                {
                    ctx.CommandText = @"
select * from Privilege
where   MenuID = @MenuID and  RoleID = @RoleID ";

                    ctx.AddParameter("@RoleID", item.RoleID);
                    ctx.AddParameter("@MenuID", item.MenuID);
                    Privilege tempItem = DBUtil.ExecuteMapper<Privilege>(ctx, new Privilege()).FirstOrDefault();
                    if (tempItem != null)
                    {
                        //Update
                        ctx.CommandText = @"
UPDATE Privilege
   SET AllowCreate = @AllowCreate
      ,AllowRead = @AllowRead
      ,AllowUpdate = @AllowUpdate
      ,AllowDelete = @AllowDelete
      ,AllowPrint = @AllowPrint
  WHERE  
   MenuID = @MenuID and  RoleID = @RoleID
";              
                    }
                    else
                    {
                        ctx.CommandText = @"
INSERT INTO Privilege
           (MenuID
           ,RoleID
           ,AllowCreate
           ,AllowRead
           ,AllowUpdate
           ,AllowDelete
           ,AllowPrint)
     VALUES
           (@MenuID
           ,@RoleID
           ,@AllowCreate
           ,@AllowRead
           ,@AllowUpdate
           ,@AllowDelete
           ,@AllowPrint)
";
                        item.AllowCreate = true;
                        item.AllowUpdate = true;
                        item.AllowDelete = true;
                        item.AllowPrint = true;
                        item.AllowRead = true;
                    }
                    ctx.AddParameter("@RoleID", item.RoleID);
                    ctx.AddParameter("@MenuID", item.MenuID);
                    ctx.AddParameter("@AllowCreate", item.AllowCreate);
                    ctx.AddParameter("@AllowRead", item.AllowRead);
                    ctx.AddParameter("@AllowUpdate", item.AllowUpdate);
                    ctx.AddParameter("@AllowDelete", item.AllowDelete);
                    ctx.AddParameter("@AllowPrint", item.AllowPrint);

                    result = DBUtil.ExecuteNonQuery(ctx);
                    if (result == -1)
                    {
                        ctx.RollbackTransaction();
                        break;
                    }

                  
                }
                ctx.CommitTransaction();
            }
            catch (Exception)
            {
                ctx.RollbackTransaction();
            }
            return result;
        }
        
        public static List<Privilege> GetByUsername(string username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
        SELECT p.* FROM Privilege p 
INNER JOIN UserRole ur ON ur.RoleID =p.ROleID
WHERE ur.Username =@Username
";
            context.AddParameter("@Username", username);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Privilege>(context, new Privilege());
        }

        public static List<Privilege> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Select * from Privilege";
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Privilege>(context, new Privilege());
        }
    }
}
