using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicLayer
{
    public enum TransType
    {
        Unknown = 0,
        Active = 1,
        Deleted = 2
    }
    public enum LogType
    {
        ERROR,
        WARNING,
        INFROMATION,
        UPDATE,
        INSERT,
        DELETE
    }
}
