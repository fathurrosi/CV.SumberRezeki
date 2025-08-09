
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    /// <summary>
    /// Dta Class of TABLE personel
    /// </summary>    
    public partial class personelItem
    {

        #region Data Access

        /// <summary>
        /// Execute Insert to TABLE personel
        /// </summary>        
        public static personel Insert(personel obj, string createdBy)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"


INSERT INTO personel(name, address, phone1, phone2, createdBy, created, pType) 
VALUES      (@name, @address, @phone1, @phone2,@createdBy, @created,@pType);

SELECT  *
FROM    personel
WHERE   id  = LAST_INSERT_ID();
";
            context.AddParameter("@name", string.Format("{0}", obj.name));
            context.AddParameter("@address", string.Format("{0}", obj.address));
            context.AddParameter("@phone1", string.Format("{0}", obj.phone1));
            context.AddParameter("@phone2", string.Format("{0}", obj.phone2));
            context.AddParameter("@createdBy", string.Format("{0}", createdBy));
            context.AddParameter("@created", DateTime.Now);
            context.AddParameter("@pType", (int)obj.pType);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<personel>(context, new personel()).FirstOrDefault();
        }

        /// <summary>
        /// Execute Update to TABLE personel
        /// </summary>        
        public static personel Update(personel obj, string editedBy, DateTime edited)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"

UPDATE      personel
SET         name = @name,
            address = @address,
            phone1 = @phone1,
            phone2 = @phone2,
            edited = @edited,
            editedBy = @editedBy, pType=@pType
WHERE       id  = @id;


SELECT *
FROM    personel
WHERE   id  = @id";
            context.AddParameter("@name", string.Format("{0}", obj.name));
            context.AddParameter("@address", string.Format("{0}", obj.address));
            context.AddParameter("@phone1", string.Format("{0}", obj.phone1));
            context.AddParameter("@phone2", string.Format("{0}", obj.phone2));
            context.AddParameter("@edited", edited);
            context.AddParameter("@editedBy", string.Format("{0}", editedBy));
            context.AddParameter("@id", obj.id);
            context.AddParameter("@pType", (int)obj.pType);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<personel>(context, new personel()).FirstOrDefault();
        }

        /// <summary>
        /// Execute Delete to TABLE personel
        /// </summary>        
        public static int Delete(Int32 id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"DELETE FROM personel 
WHERE   id  = @id";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }


        /// <summary>
        /// Get All records from TABLE personel
        /// </summary>        
        public static List<personel> GetAll()
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT * FROM personel ";
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<personel>(context, new personel());
        }



        public static List<personel> GetPaging(string text, int offset, int pageSize, personelType pType)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT * from personel 
WHERE pType=@pType
AND Name LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            context.AddParameter("@pType", (int)pType);
            List<personel> result = DBUtil.ExecuteMapper<personel>(context, new personel());

            return result;
        }



        public static int GetRecordCount(string text, personelType pType)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT count(*) from personel 
WHERE pType=@pType
AND Name LIKE concat ('%', @text ,'%') 
            ";
            context.AddParameter("@text", text);
            context.AddParameter("@pType", (int)pType);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        /// <summary>
        /// Get a single record of TABLE personel by Primary Key
        /// </summary>        
        public static personel GetByPK(Int32 id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"SELECT * FROM personel
            WHERE id  = @id";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<personel>(context, new personel()).FirstOrDefault();
        }

        #endregion

    }
}