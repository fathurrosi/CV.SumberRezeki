using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class MenuItem
    {

        public static List<Menu> GetMenus()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT * FROM MENU";
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<Menu>(context, new Menu());
        }


        public static List<CstmMenu> GetPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT m.*,
			parent.Code ParentCode,
			parent.Name ParentName,
			parent.Description ParentDescription 	  
FROM Menu m
left join Menu parent on parent.ID = m.ParentID
WHERE
m.Name LIKE concat ('%', @text ,'%') 
OR m.Description LIKE concat ('%', @text ,'%') 
or parent.Name LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<CstmMenu> result = DBUtil.ExecuteMapper<CstmMenu>(context, new CstmMenu());

            return result;
        }



        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT count(1) from Menu m
left join Menu parent on parent.ID = m.ParentID
WHERE 
m.Name LIKE concat ('%', @text ,'%') 
OR m.Description LIKE concat ('%', @text ,'%')  
or parent.Name LIKE concat ('%', @text ,'%') 
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }




        public static Menu GetMenuByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT * FROM Menu
where ID=@ID
";
            context.AddParameter("@ID", ID);
            context.CommandType =  CommandType.Text;

            return DBUtil.ExecuteMapper<Menu>(context, new Menu()).FirstOrDefault();
        }



        public static int Insert(string Code, string Name, string Description, int ParentID, int Sequence, byte[] Ico, string Username)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Code", Code);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ParentID", ParentID);
            context.AddParameter("@Sequence", Sequence);
            context.AddParameter("@Ico", Ico);
            context.AddParameter("@CreatedBy", Username);
            context.CommandText = @"
INSERT INTO Menu
           (Code
           ,Name
           ,Description
           ,ParentID
           ,Sequence
           ,Ico,CreatedBy, CreatedDate  )
     VALUES
           (@Code
           ,@Name
           ,@Description
           ,@ParentID
           ,@Sequence
           ,@Ico,@CreatedBy, Now() )
            
            ";
            context.CommandType =  CommandType.Text;

            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Update(int ID, string Code, string Name, string Description, int ParentID, int Sequence, byte[] Ico, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
UPDATE Menu
   SET Code = @Code
      ,Name = @Name
      ,Description = @Description
      ,ParentID = @ParentID
      ,Sequence = @Sequence
      ,Ico = @Ico
	  ,ModifiedDate = Now()
	  ,ModifiedBy =@ModifiedBy
 WHERE ID=@ID

";

            context.AddParameter("@ID", ID);
            context.AddParameter("@Code", Code);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ParentID", ParentID);
            context.AddParameter("@Sequence", Sequence);
            context.AddParameter("@Ico", Ico);
            context.AddParameter("@ModifiedBy", Username);
            context.CommandType =  CommandType.Text;

            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Delete(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
Delete from Menu
 WHERE ID=@ID
";
            context.AddParameter("@ID", ID);
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteNonQuery(context);
        }

    }
}
