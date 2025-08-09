using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class SupplierItem
    {
        public static List<Supplier> GetPaging(string text, int offset, int pageSize)
        {
            string query = @"
SELECT *
  FROM Supplier 

WHERE NAME LIKE concat ('%', @text ,'%') 
OR Phone LIKE concat ('%', @text ,'%') 
OR CellPhone LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Supplier> list = DBUtil.ExecuteMapper<Supplier>(context, new Supplier());

            return list;
        }


        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" SELECT count(*)
  FROM Supplier 

WHERE NAME LIKE concat ('%', @text ,'%') 
OR Phone LIKE concat ('%', @text ,'%') 
OR CellPhone LIKE concat ('%', @text ,'%') 
  
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static Supplier GetByCode(string Code)
        {
            string query = @"
SELECT *
  FROM Supplier
Where Code =@Code
  ";

            IDBHelper context = new DBHelper();
            context.AddParameter("@Code", Code);
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier()).FirstOrDefault();
        }

        public static int Delete(string Code)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
DELETE FROM Supplier
      Where Code=@Code
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Code", Code);

            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Insert(string code, string name, string address, string phone, string cellPhone, string Username)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"

INSERT INTO Supplier
           (Code,Name
           ,Address
           ,Phone
           ,CellPhone
           ,CreatedDate
           ,CreatedBy)
     VALUES
           (@Code,@Name
           ,@Address
           ,@Phone
           ,@CellPhone
           , NOW()
           ,@CreatedBy)";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Name", name);
            context.AddParameter("@Code", code);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CellPhone", cellPhone);
            context.AddParameter("@CreatedBy", Username);
            return DBUtil.ExecuteNonQuery(context);
        }


        public static Supplier Insert(string code, string name, string Username)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
INSERT INTO Supplier
           (Code,Name,CreatedDate,CreatedBy)
     VALUES
           (@Code,@Name,NOW(),@CreatedBy) ;

Select * from Supplier where Code = @Code;
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Name", name);
            context.AddParameter("@Code", code);
            context.AddParameter("@CreatedBy", Username);


            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier()).FirstOrDefault();
        }

        public static int Update(string Code, string name, string address, string phone, string cellPhone, string Username)
        {


            IDBHelper context = new DBHelper();
            context.CommandText = @"

UPDATE Supplier
   SET Name = @Name
      ,Address = @Address
      ,Phone= @Phone
      ,CellPhone= @CellPhone
      ,ModifiedDate = Now()
      ,ModifiedBy = @ModifiedBy
 WHERE Code=@Code
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Code", Code);
            context.AddParameter("@Name", name);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CellPhone", cellPhone);
            context.AddParameter("@ModifiedBy", Username);
            return DBUtil.ExecuteNonQuery(context);
        }


        public static List<Supplier> GetAll()
        {
            string query = @"
SELECT *  FROM Supplier
  ";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier());
        }
    }
}
