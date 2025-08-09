using DataAccessLayer;
using Custom.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    //public class Log : IDataMapper<Log>
    //{
    //    public DateTime? LogDate { get; set; }

    //    public string ComputerName { get; set; }

    //    public string IPAddress { get; set; }

    //    public string LogType { get; set; }

    //    public string LogMessage { get; set; }

    //    public string Username { get; set; }


    //    public Log Map(System.Data.IDataReader reader)
    //    {
    //        Log obj = new Log();
    //        obj.LogDate = (reader["LogDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["LogDate"]);
    //        obj.ComputerName = reader["ComputerName"].ToString();
    //        obj.IPAddress = reader["IPAddress"].ToString();
    //        obj.LogType = reader["LogType"].ToString();
    //        obj.LogMessage = reader["LogMessage"].ToString();
    //        obj.Username = reader["Username"].ToString();

    //        return obj;
    //    }
    //}

}
