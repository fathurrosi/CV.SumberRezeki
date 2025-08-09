
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer;
using DataObject;

namespace DataLayer
{
    /// <summary>
    /// Dta Class of TABLE settings
    /// </summary>    
    public partial class settingsItem
    {
       
        #region Data Access

        /// <summary>
        /// Execute Update to TABLE settings
        /// </summary>        
        public static settings Update(settings obj)
        {
             IDBHelper context = new DBHelper();
            string sqlQuery = @"

UPDATE      settings
SET         ukuran_struk = @ukuran_struk,
            terminal_id = @terminal_id
WHERE       id  = @id;



SELECT  id, ukuran_struk, terminal_id 
FROM    settings
WHERE   id  = @id";
            context.AddParameter("@ukuran_struk", obj.ukuran_struk);
            context.AddParameter("@terminal_id", obj.terminal_id);
            context.AddParameter("@id", obj.id);            
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<settings>(context, new settings()).FirstOrDefault(); 
        }

        /// <summary>
        /// Execute Delete to TABLE settings
        /// </summary>        
        public static int Delete(Int32 id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery =@"DELETE FROM settings 
WHERE   id  = @id";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }
        public static int GetCount(int PageSize, int PageIndex)
        {
            return GetTotalRecord();
        }
        /// <summary>
        /// Get Total records from settings
        /// </summary>        
        public static int GetTotalRecord()
        {
            int result = -1;
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT Count(*) as Total FROM settings ";
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
            
        }

        /// <summary>
        /// Get All records from TABLE settings
        /// </summary>        
        public static List<settings> GetAll()
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT id, ukuran_struk, terminal_id FROM settings ";
            context.CommandText = sqlQuery;
            context.CommandType =  System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<settings>(context, new settings());
        }

        /// <summary>
        /// Get All records from TABLE settings
        /// </summary>        
        public static List<settings> GetPaging(int PageSize, int PageIndex)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"
            WITH Paging_settings AS
            (
                SELECT  ROW_NUMBER() OVER (ORDER BY settings.id DESC ) AS PAGING_ROW_NUMBER,
                        settings.*
                FROM    settings
                
            )

            SELECT      Paging_settings.*
            FROM        Paging_settings
            ORDER BY PAGING_ROW_NUMBER           
            OFFSET @PageIndex ROWS 
            FETCH Next @PageSize ROWS ONLY
";
        
            context.AddParameter("@PageIndex", PageIndex);
            context.AddParameter("@PageSize", PageSize);
            context.CommandType = System.Data.CommandType.Text;
            context.CommandText = sqlQuery;
            return DBUtil.ExecuteMapper<settings>(context, new settings());
        }

        /// <summary>
        /// Get a single record of TABLE settings by Primary Key
        /// </summary>        
        public static settings GetByPK(Int32 id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"SELECT id, ukuran_struk, terminal_id FROM settings
            WHERE id  = @id";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<settings>(context, new settings()).FirstOrDefault();
        }

        #endregion

    }
}