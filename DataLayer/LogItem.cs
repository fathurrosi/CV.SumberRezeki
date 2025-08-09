
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class LogItem
    {

        public static int Insert(string ComputerName
             , string IPAddress
             , string LogType
             , string LogMessage
             , string Username)
        {
            string query = @"  
                            INSERT INTO Log
                                       (LogDate
                                       ,ComputerName
                                       ,IPAddress
                                       ,LogType
                                       ,LogMessage
                                       ,Username)
                                 VALUES
                                       ( NOW()
                                       ,@ComputerName
                                       ,@IPAddress
                                       ,@LogType
                                       ,@LogMessage
                                       ,@Username) ";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@ComputerName", ComputerName);
            context.AddParameter("@IPAddress", IPAddress);
            context.AddParameter("@LogType", LogType);
            context.AddParameter("@LogMessage", LogMessage);
            context.AddParameter("@Username", Username);
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
