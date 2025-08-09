
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    /// <summary>
    /// Dta Class of TABLE vehicle
    /// </summary>    
    public partial class vehicleItem
    {
        public static List<vehicle> GetPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT * from vehicle WHERE 
PlatDisplay LIKE concat ('%', @text ,'%')  OR Jenis LIKE concat ('%', @text ,'%') 
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<vehicle> result = DBUtil.ExecuteMapper<vehicle>(context, new vehicle());

            return result;
        }



        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT count(*) from vehicle WHERE PlatDisplay LIKE concat ('%', @text ,'%')     OR Jenis LIKE concat ('%', @text ,'%') 
            ";
            context.AddParameter("@text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }


        #region Data Access

        /// <summary>
        /// Execute Insert to TABLE vehicle
        /// </summary>        
        public static vehicle Insert(vehicle obj, string createdBy)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"
INSERT INTO vehicle(Platnomor, Jenis, PlatDisplay, Kapasitas, Unit, created, createdBy) 
VALUES      (@Platnomor, @Jenis, @PlatDisplay, @Kapasitas, @Unit, @created, @createdBy);



SELECT  *
FROM    vehicle
WHERE   ID  = LAST_INSERT_ID();";
            context.AddParameter("@id", obj.id);
            context.AddParameter("@Platnomor", string.Format("{0}", obj.Platnomor));
            context.AddParameter("@Jenis", string.Format("{0}", obj.Jenis));
            context.AddParameter("@PlatDisplay", string.Format("{0}", obj.PlatDisplay));
            context.AddParameter("@Kapasitas", obj.Kapasitas);
            context.AddParameter("@Unit", string.Format("{0}", obj.Unit));
            context.AddParameter("@created", DateTime.Now);
            context.AddParameter("@createdBy", string.Format("{0}", createdBy));
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<vehicle>(context, new vehicle()).FirstOrDefault();
        }

        /// <summary>
        /// Execute Update to TABLE vehicle
        /// </summary>        
        public static vehicle Update(vehicle obj, string editedBy)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"
UPDATE      vehicle
SET      
            Jenis = @Jenis,
            PlatDisplay = @PlatDisplay,
            Kapasitas = @Kapasitas,
            Unit = @Unit,
Platnomor  = @Platnomor,
edited =@edited,
editedBy =@editedBy
WHERE    id  = @id    

SELECT  *
FROM    vehicle
WHERE   id  = @id";
            context.AddParameter("@id", obj.id);
            context.AddParameter("@Jenis", string.Format("{0}", obj.Jenis));
            context.AddParameter("@PlatDisplay", string.Format("{0}", obj.PlatDisplay));
            context.AddParameter("@Kapasitas", obj.Kapasitas);
            context.AddParameter("@Unit", string.Format("{0}", obj.Unit));
            context.AddParameter("@Platnomor", string.Format("{0}", obj.Platnomor));
            context.AddParameter("@edited", DateTime.Now);
            context.AddParameter("@editedBy", string.Format("{0}", editedBy));
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<vehicle>(context, new vehicle()).FirstOrDefault();
        }

        /// <summary>
        /// Execute Delete to TABLE vehicle
        /// </summary>        
        public static int Delete(int id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"DELETE FROM vehicle 
WHERE   id  = @id";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }


        /// <summary>
        /// Get All records from TABLE vehicle
        /// </summary>        
        public static List<vehicle> GetAll()
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = "SELECT * FROM vehicle ";
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<vehicle>(context, new vehicle());
        }


        /// <summary>
        /// Get a single record of TABLE vehicle by Primary Key
        /// </summary>        
        public static vehicle GetByPK(int id)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"SELECT * FROM vehicle
            WHERE id  = @id ";
            context.AddParameter("@id", id);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<vehicle>(context, new vehicle()).FirstOrDefault();
        }



        public static vehicle GetByPlatnomor(string Platnomor)
        {
            IDBHelper context = new DBHelper();
            string sqlQuery = @"SELECT * FROM vehicle
            WHERE Platnomor  = @Platnomor ";
            context.AddParameter("@Platnomor", Platnomor);
            context.CommandText = sqlQuery;
            context.CommandType = System.Data.CommandType.Text;
            return DBUtil.ExecuteMapper<vehicle>(context, new vehicle()).FirstOrDefault();
        }

        #endregion

    }
}