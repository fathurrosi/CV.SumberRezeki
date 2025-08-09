
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer;
using DataObject;

namespace DataLayer
{
    /// <summary>
    /// Dta Class of TABLE terminal
    /// </summary>    
    public partial class terminalItem
    {

        #region Data Access

        /// <summary>
        /// Execute Insert to TABLE terminal
        /// </summary>        
        public static terminal Insert(terminal obj)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"



INSERT INTO terminal(machine_name, ip_address, disk_id, base_id) 
VALUES      (@machine_name, @ip_address, @disk_id, @base_id);



SELECT  *
FROM    terminal
WHERE   base_id  = @base_id
            AND disk_id = @disk_id";
            context.AddParameter("@id", obj.id);
            context.AddParameter("@machine_name", string.Format("{0}", obj.machine_name));
            context.AddParameter("@ip_address", string.Format("{0}", obj.ip_address));
            context.AddParameter("@disk_id", string.Format("{0}", obj.disk_id));
            context.AddParameter("@base_id", string.Format("{0}", obj.base_id));
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<terminal>(context, new terminal()).FirstOrDefault();
        }

        /// <summary>
        /// Execute Update to TABLE terminal
        /// </summary>        
        public static terminal Update(terminal obj)
        {

            terminal item = GetByPK(obj.base_id, obj.disk_id);
            if (item == null)
            {
                return Insert(obj);
            }
            else
            {

                IDBHelper context = new DBHelper();
                string sqlQuery = @"

UPDATE      terminal
SET         id = @id,
            machine_name = @machine_name,
            ip_address = @ip_address
WHERE       base_id  = @base_id
            AND disk_id = @disk_id;



SELECT  id, machine_name, ip_address, disk_id, base_id 
FROM    terminal
WHERE   base_id  = @base_id
        AND disk_id = @disk_id";
                context.AddParameter("@id", obj.id);
                context.AddParameter("@machine_name", string.Format("{0}", obj.machine_name));
                context.AddParameter("@ip_address", string.Format("{0}", obj.ip_address));
                context.AddParameter("@base_id", string.Format("{0}", obj.base_id));
                context.AddParameter("@disk_id", string.Format("{0}", obj.disk_id));
                context.CommandText = sqlQuery;
                context.CommandType = System.Data.CommandType.Text;
                return DBUtil.ExecuteMapper<terminal>(context, new terminal()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Execute Delete to TABLE terminal
        /// </summary>        
        public static int Delete(string base_id, string disk_id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"DELETE FROM terminal 
WHERE   base_id  = @base_id
        AND disk_id = @disk_id";
            context.AddParameter("@base_id", string.Format("{0}", base_id));
            context.AddParameter("@disk_id", string.Format("{0}", disk_id));
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }
        public static int GetCount(int PageSize, int PageIndex)
        {
            return GetTotalRecord();
        }
        /// <summary>
        /// Get Total records from terminal
        /// </summary>        
        public static int GetTotalRecord()
        {
            int result = -1;
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT Count(*) as Total FROM terminal ";
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;

        }

        /// <summary>
        /// Get All records from TABLE terminal
        /// </summary>        
        public static List<terminal> GetAll()
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT id, machine_name, ip_address, disk_id, base_id FROM terminal ";
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<terminal>(context, new terminal());
        }

        /// <summary>
        /// Get All records from TABLE terminal
        /// </summary>        
        public static List<terminal> GetPaging(int PageSize, int PageIndex)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"
            WITH Paging_terminal AS
            (
                SELECT  ROW_NUMBER() OVER (ORDER BY terminal.base_id, terminal.disk_id DESC ) AS PAGING_ROW_NUMBER,
                        terminal.*
                FROM    terminal
                
            )

            SELECT      Paging_terminal.*
            FROM        Paging_terminal
            ORDER BY PAGING_ROW_NUMBER           
            OFFSET @PageIndex ROWS 
            FETCH Next @PageSize ROWS ONLY
";

            context.AddParameter("@PageIndex", PageIndex);
            context.AddParameter("@PageSize", PageSize);
            context.CommandType = System.Data.CommandType.Text;
            context.CommandText = sqlQuery;
            return DBUtil.ExecuteMapper<terminal>(context, new terminal());
        }

        /// <summary>
        /// Get a single record of TABLE terminal by Primary Key
        /// </summary>        
        public static terminal GetByPK(string base_id, string disk_id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"SELECT id, machine_name, ip_address, disk_id, base_id FROM terminal
            WHERE base_id  = @base_id AND disk_id = @disk_id";
            context.AddParameter("@base_id", base_id);
            context.AddParameter("@disk_id", disk_id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<terminal>(context, new terminal()).FirstOrDefault();
        }

        #endregion

    }
}