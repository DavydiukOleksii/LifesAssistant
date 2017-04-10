using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Credit;

namespace DataRepository
{
    public class CostsRepository: IRepository<CostsModel, CostsDayReport>
    {
        public CostsModel GetAll()
        {
            throw new NotImplementedException();
        }

        public CostsDayReport GetByDay(DataSetDateTime day)
        {
            throw new NotImplementedException();
        }

        public void UpdateToday(CostsDayReport newData)
        {
            throw new NotImplementedException();
        }
    }
}
