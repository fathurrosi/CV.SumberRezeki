
using System;
//using Thunder.Village.DataAccess;
using DataAccessLayer;
namespace DataObject
{
    public class terminal : IDataMapper<terminal>
    {
        #region terminal Properties
        public Int32 id { get; set; }
        public string machine_name { get; set; }
        public string ip_address { get; set; }
        public string disk_id { get; set; }
        public string base_id { get; set; }
        #endregion    
        public terminal Map(System.Data.IDataReader reader)
        {
            terminal obj = new terminal();   
            obj.id = Convert.ToInt32(reader["id"]);
            obj.machine_name = reader["machine_name"] == DBNull.Value ? null : reader["machine_name"].ToString();
            obj.ip_address = reader["ip_address"] == DBNull.Value ? null : reader["ip_address"].ToString();
            obj.disk_id = reader["disk_id"] == DBNull.Value ? null : reader["disk_id"].ToString();
            obj.base_id = reader["base_id"] == DBNull.Value ? null : reader["base_id"].ToString();
            return obj;
        }
    }
}