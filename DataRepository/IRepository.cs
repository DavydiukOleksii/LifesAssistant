using System.Data;

namespace DataRepository
{
    public interface IRepository<T, U>
    {
        T GetAll();
        U GetByDay(DataSetDateTime day);
        void UpdateToday(U newData);
    }
}
