using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class UserItem
    {
        public static User GetUser(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"	SELECT * from User WHERE Username = @Username ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Username", Username);
            List<User> result = DBUtil.ExecuteMapper<User>(context, new User());
            User user = result.FirstOrDefault();
            user.Roles = RoleItem.GetByUsername(user.Username);
            return user;
        }

        public static void UpdateLogin(string Username, string machine, string ipAddress)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"UPDATE User
   SET LastLogin =  NOW()
      ,IsLogin = 1
      ,IPAddress = @IPAddress
      ,MachineName = @MachineName      
 WHERE  Username = @Username";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Username", Username);
            context.AddParameter("@MachineName", machine);
            context.AddParameter("@IPAddress", ipAddress);
            DBUtil.ExecuteNonQuery(context);
        }



        public static int Insert(string Username, string password, List<Role> roles)
        {
            int result = -1;
            try
            {

                IDBHelper context = new DBHelper();
                context.BeginTransaction();
                context.CommandText = @"
INSERT INTO User
           (Username
           ,Password
          , IsActive)
     VALUES
           (@Username
           ,@Password
           ,1)
";
                context.CommandType =  CommandType.Text;
                context.AddParameter("@Username", Username);
                context.AddParameter("@Password", password);
                result = DBUtil.ExecuteNonQuery(context);
                if (result > 0)
                {
                    roles.ForEach(t =>
                    {
                        context.Clear();
                        context.AddParameter("Username", Username);
                        context.AddParameter("@RoleID", t.ID);
                        context.CommandText = @"
INSERT INTO UserRole
           (Username
           ,RoleID)
     VALUES
           (@Username
           ,@RoleID)
                        ";
                        context.CommandType =  CommandType.Text;
                        DBUtil.ExecuteNonQuery(context);
                    });
                    context.CommitTransaction();
                }
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }

        //
        public static int Update(string Username, string password, List<Role> roles)
        {
            int result = -1;
            try
            {
                IDBHelper context = new DBHelper();
                context.BeginTransaction();
                context.CommandText = @"

update User
           set Password =@Password
          , IsActive=1
		  where Username=@Username

";
                context.CommandType =  CommandType.Text;
                context.AddParameter("@Username", Username);
                context.AddParameter("@Password", password);
                result = DBUtil.ExecuteNonQuery(context);
                if (result > 0)
                {
                    context.Clear();
                    context.AddParameter("Username", Username);
                    context.CommandText = @"                    
DELETE FROM UserRole
      WHERE Username =@Username
";
                    context.CommandType =  CommandType.Text;
                    DBUtil.ExecuteNonQuery(context);
                    roles.ForEach(t =>
                    {
                        context.Clear();
                        context.AddParameter("Username", Username);
                        context.AddParameter("@RoleID", t.ID);
                        context.CommandText = @"
                        INSERT INTO UserRole
           (Username
           ,RoleID)
     VALUES
           (@Username
           ,@RoleID)
                        ";
                        context.CommandType =  CommandType.Text;
                        DBUtil.ExecuteNonQuery(context);
                    });
                    context.CommitTransaction();
                }
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        public static int UpdatePassword(string Username, string password)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
update User
           set Password =@Password
          , IsActive=1
		  where Username=@Username
";
            context.CommandType =  CommandType.Text;
            context.AddParameter("@Username", Username);
            context.AddParameter("@Password", password);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Delete(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"

DELETE FROM User
      WHERE Username =@Username
";
            context.CommandType =  CommandType.Text;
            context.AddParameter("@Username", Username);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static List<User> GetPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"

SELECT * from User WHERE Username LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType =  CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<User> result = DBUtil.ExecuteMapper<User>(context, new User());
            result.ForEach(t => { t.Roles = RoleItem.GetByUsername(t.Username); });
            return result;
        }

        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" 

SELECT count(*) from User WHERE Username LIKE concat ('%', @text ,'%') 
  
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

    }
}
