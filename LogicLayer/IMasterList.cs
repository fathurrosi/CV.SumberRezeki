using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicLayer
{
    public interface IMasterHeader
    {
        //CRUD
        void Search();
        void Add();
        void Edit();
        void Delete();
        void Print();
    }

    public interface IMasterFooter
    {
        //void First();
        //void Last();
        //void Next();
        //void Prev();
        void Search();
    }
}
