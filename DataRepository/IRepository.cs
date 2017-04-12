using System;
using System.Collections.Generic;
using System.Data;

namespace DataRepository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetByDay(DateTime day);
        //void UpdateDay(U newData);
    }
}
