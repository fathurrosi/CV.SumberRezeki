using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CustomerItem
    {
        public static List<Customer> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT c.*, v.display PaymentTypeDesc FROM Customer c
LEFT JOIN variable v ON v.code = c.PaymentType
";
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<Customer>(context, new Customer());
        }

        public static int Update(int id, string name, string address, string phone, string sales, string paymentType, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
UPDATE customer 
SET	
FullName = @FullName ,  
Address =@Address ,  
Phone = @Phone ,
ModifiedDate = Now() ,
Sales= @Sales ,
PaymentType= @PaymentType ,
ModifiedBy = @ModifiedBy
	WHERE ID = @ID ;

";
            context.AddParameter("@ID", id);
            context.AddParameter("@FullName", name);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@ModifiedBy", Username);
            context.AddParameter("@Sales", sales);

            context.AddParameter("@PaymentType", paymentType);
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteNonQuery(context);
        }

        public static Customer Insert(string name, string address, string sales, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"


INSERT INTO customer 
	(
	
FullName,
Address, 
CreatedDate ,
CreatedBy, Sales
	)
	VALUES
	(

@FullName,
@Address,  
NOw(), 
@CreatedBy, @Sales
	);

Select * from Customer where ID = last_insert_id();

";

            context.AddParameter("@Address", address);
            context.AddParameter("@Sales", sales);
            context.AddParameter("@FullName", name);
            context.AddParameter("@CreatedBy", Username);

            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<Customer>(context, new Customer()).FirstOrDefault();
        }


        public static int Insert(string name, string address, string phone, string sales, string paymentType, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"


INSERT INTO customer 
	(
FullName, 
Address, 
Phone,
CreatedDate,
CreatedBy, Sales,PaymentType
	)
	VALUES
	(
@FullName, 
@Address, 
@Phone,
NOw(), 
@CreatedBy, @Sales,@PaymentType
	);
";

            context.AddParameter("@FullName", name);
            context.AddParameter("@Sales", sales);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CreatedBy", Username);
            context.AddParameter("@PaymentType", paymentType);
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteNonQuery(context);
        }


        public static Customer GetByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT c.*, v.display PaymentTypeDesc FROM Customer c
LEFT JOIN variable v ON v.code = c.PaymentType
Where ID =@ID";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ID", ID);
            return DBUtil.ExecuteMapper<Customer>(context, new Customer()).FirstOrDefault();
        }


        public static List<Customer> GetByName(string fullname)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT c.*, v.display PaymentTypeDesc FROM Customer c
LEFT JOIN variable v ON v.code = c.PaymentType
Where c.FullName=@fullname";
            context.CommandType = CommandType.Text;
            context.AddParameter("@fullname", fullname);
            return DBUtil.ExecuteMapper<Customer>(context, new Customer());
        }

        public static int Delete(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Delete from Customer Where ID =@ID";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ID", ID);
            return DBUtil.ExecuteNonQuery(context);
        }


        public static DataTable GetPaging(string text, int offset, int pageSize)
        {

            string query = @"
SELECT c.*, v.display PaymentTypeDesc FROM Customer c
LEFT JOIN variable v ON v.code = c.PaymentType

WHERE Fullname LIKE concat ('%', @text ,'%') 
OR Address LIKE concat ('%', @text ,'%') 
OR Phone LIKE concat ('%', @text ,'%') 

ORDER BY c.fullname ASC
LIMIT  @pageSize OFFSET @offset
";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            DataSet ds = DBUtil.ExecuteDataSet(context);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" SELECT COUNT(*) FROM Customer 
WHERE Fullname LIKE concat ('%', @text ,'%') 
OR Address LIKE concat ('%', @text ,'%') 
OR Phone LIKE concat ('%', @text ,'%') 

  
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
