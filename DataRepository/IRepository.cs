using System;
using System.Collections.Generic;
using System.Data;

namespace DataRepository
{
    public interface IRepository<T, U>
    {
        T GetByDay(DateTime day);
        void AddOperation(U newOperation);
        void DeleteOperation(U dellOperation);
    }
}
