using System;
using System.Collections.ObjectModel;

namespace DataModel.Water
{
    public class WaterDayReport
    {
        public DateTime Date { get; set; }
        public double TotalCapacity { get; set; }
        public ObservableCollection<OnceDrink> DailyWaterOperations { get; set; } 
    }
}
