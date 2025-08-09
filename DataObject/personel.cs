
using System;
//using Thunder.Village.DataAccess;
using DataAccessLayer;
namespace DataObject
{
    public enum personelType
    {
        NONE = 0,
        DRIVER = 12,
        MANDOR = 13,
        SALES = 15
    }
    public class personel : IDataMapper<personel>
    {
        #region personel Properties
        public Int32 id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public personelType pType { get; set; }
        #endregion
        public personel Map(System.Data.IDataReader reader)
        {
            personel obj = new personel();
            obj.id = Convert.ToInt32(reader["id"]);
            obj.name = string.Format("{0}", reader["name"]);
            obj.address = reader["address"] == DBNull.Value ? null : reader["address"].ToString();
            obj.phone1 = reader["phone1"] == DBNull.Value ? null : reader["phone1"].ToString();
            obj.phone2 = reader["phone2"] == DBNull.Value ? null : reader["phone2"].ToString();

            int _pType = (reader["pType"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pType"]));
            if (Enum.IsDefined(typeof(personelType), _pType))
            {
                obj.pType = (personelType)_pType;
            }
            else obj.pType = personelType.NONE;
            return obj;
        }
    }
}