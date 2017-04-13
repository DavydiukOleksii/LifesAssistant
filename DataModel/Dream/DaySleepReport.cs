using System;
using System.Collections.ObjectModel;

namespace DataModel.Dream
{
    public class DaySleepReport
    {
        public DateTime Date { get; set; }
        public int TotalSleepTimeInSecond { get; set; }
        public ObservableCollection<OneSleep> DailySleepTimes { get; set; }
    }
}
