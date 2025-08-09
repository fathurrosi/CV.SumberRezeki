using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CatalogItem
    {
        //public static void SubtractStock(int id, decimal stock, string subtrackedBy)
        //{
        //    Catalog existing = GetByID(id);
        //    if (existing != null)
        //    {
        //        decimal _stock = existing.Stock - stock;
        //        UpdateStock(existing.ID, _stock);
        //    }
        //}
        //public static void AddStock(int id, decimal stock, string addedBy)
        //{
        //    Catalog existing = GetByID(id);
        //    if (existing != null)
        //    {
        //        decimal _stock = existing.Stock + stock;
        //        UpdateStock(existing.Code, _stock);
        //    }
        //}
        public static List<Catalog> GetAllStockedCatalog()
        {
            //            string query = @"
            //SELECT c.*, cs.Stock
            //  FROM Catalog c
            //  LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
            //  WHERE cs.Stock >0
            //  ";

            string query = @"
SELECT c.*
  FROM Catalog c
  ORDER BY c.Name
  ";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static List<Catalog> GetAll()
        {
            string query = @"
SELECT *
  FROM Catalog
  ORDER BY Name
  ";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }


        public static List<Catalog> GetItems()
        {
            string query = @"
SELECT *
  FROM Catalog
where Type ='Item'
  ORDER BY Name
  ";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static List<DataObject.Catalog> GetPaging(string text, int offset, int pageSize)
        {
            string query = @"
SELECT *
  FROM Catalog 

WHERE NAME LIKE concat ('%', @text ,'%') 
order by name
LIMIT  @pageSize OFFSET @offset
";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Catalog> list = DBUtil.ExecuteMapper<Catalog>(context, new Catalog());

            return list;
        }

        public static List<Catalog> GetByName(string text)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @" SELECT * FROM catalog 
WHERE NAME LIKE concat ('%', @text ,'%') 
Order By Name
";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" SELECT COUNT(*) FROM catalog 
WHERE NAME LIKE concat ('%', @text ,'%') 
            ";
            context.AddParameter("@Text", text);
            //context.AddParameter("@pageSize", pageSize);
            //context.AddParameter("@offset", offset);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static Catalog GetByID(int ID)
        {
            string query = @"
SELECT *
  FROM Catalog
Where ID =@ID
  ";

            IDBHelper context = new DBHelper();
            context.AddParameter("@ID", ID);
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }

        public static int Delete(int ID)
        {




            IDBHelper context = new DBHelper();
            context.CommandText = @"
DELETE FROM Catalog
      Where ID=@ID
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ID", ID);

            return DBUtil.ExecuteNonQuery(context);
        }

        public static Catalog Update(int ID, string name, string unit, string desc, string note, byte[] productImage, string Username, string type)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"

UPDATE Catalog
   SET Name = @Name

        ,Unit =@Unit
      ,ModifiedDate = Now()
      ,ModifiedBy = @ModifiedBy
      ,Type =@Type
       
 WHERE ID=@ID ;

select * from Catalog
WHERE ID=@ID
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ID", ID);
            context.AddParameter("@Name", name);
            context.AddParameter("@Unit", unit);
            context.AddParameter("@ModifiedBy", Username);
            context.AddParameter("@Type", type);

            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }


        public static Catalog GetCatalog(string name, string unit)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"

select * from Catalog
where Name  =@Name
and Unit = @Unit
           
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Name", name);


            context.AddParameter("@Unit", unit);

            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }

        public static Catalog Insert(string name, string unit, string desc, string note, byte[] productImage, string Username, string type)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"

INSERT INTO Catalog
           (Name

           ,CreatedDate
           ,CreatedBy, Unit,Type)
     VALUES
           (@Name

           , NOW()
           ,@CreatedBy,@Unit,@Type);
SELECT * FROM Catalog WHERE id = LAST_INSERT_ID();
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@Name", name.Trim());
            context.AddParameter("@Unit", unit.Trim());
            context.AddParameter("@CreatedBy", Username);
            context.AddParameter("@Type", type);
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }
    }
}
