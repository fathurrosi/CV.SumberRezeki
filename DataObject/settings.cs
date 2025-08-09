
using System;
//using Thunder.Village.DataAccess;
using DataAccessLayer;
namespace DataObject
{
    public class settings : IDataMapper<settings>
    {
        #region settings Properties
        public Int32 id { get; set; }
        public Int32 ukuran_struk { get; set; }
        public Int32 terminal_id { get; set; }
        #endregion
        public settings Map(System.Data.IDataReader reader)
        {
            settings obj = new settings();
            obj.id = Convert.ToInt32(reader["id"]);
            obj.ukuran_struk = reader["ukuran_struk"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ukuran_struk"]);
            obj.terminal_id = reader["terminal_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["terminal_id"]);
            return obj;
        }
    }
}