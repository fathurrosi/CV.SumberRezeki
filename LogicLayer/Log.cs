using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DataLayer;

namespace LogicLayer
{
    public class Log
    {
        //private static Log _log;
        //public static Log CreateInstance()
        //{
        //    if (_log == null)
        //        _log = new Log();            
        //    return _log;
        //}

        public static void Error(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.ERROR.ToString(), message, Utilities.Username);
        }

        public static void Info(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.INFROMATION.ToString(), message, Utilities.Username);
        }

        public static void Warning(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.WARNING.ToString(), message, Utilities.Username);
        }

        public static void Delete(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.DELETE.ToString(), message, Utilities.Username);
        }

        public static void Insert(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.INSERT.ToString(), message, Utilities.Username);
        }

        public static void Update(string message)
        {
            LogItem.Insert(Utilities.GetComputerName(), Utilities.GetIpAddress(), LogType.UPDATE.ToString(), message, Utilities.Username);
        }
    }
}
