using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Water;

namespace DataRepository
{
    public class WaterRepository: IRepository<WaterDayReport>
    {
        public List<WaterDayReport> GetAll()
        {
            throw new NotImplementedException();
        }

        public WaterDayReport GetByDay(DateTime day)
        {
            throw new NotImplementedException();
        }
    }
}
