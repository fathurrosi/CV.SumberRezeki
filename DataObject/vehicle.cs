
using System;
//using Thunder.Village.DataAccess;
using DataAccessLayer;
namespace DataObject
{
    public class vehicle : IDataMapper<vehicle>
    {
        #region vehicle Properties
        public Int32 id { get; set; }
        public string Platnomor { get; set; }
        public string Jenis { get; set; }
        public string PlatDisplay { get; set; }
        public string Kapasitas { get; set; }
        public string Unit { get; set; }
        #endregion
        public vehicle Map(System.Data.IDataReader reader)
        {
            vehicle obj = new vehicle();
            obj.id = Convert.ToInt32(reader["id"]);
            obj.Platnomor = string.Format("{0}", reader["Platnomor"]);
            obj.Jenis = string.Format("{0}", reader["Jenis"]);
            obj.PlatDisplay = string.Format("{0}", reader["PlatDisplay"]);
            obj.Kapasitas = string.Format("{0}", reader["Kapasitas"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            return obj;
        }
    }
}