using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicLayer
{
    public interface ITransButton
    {
        bool IsValid();
        void Save();
        void Cancel();
        //void Print();
    }
}
