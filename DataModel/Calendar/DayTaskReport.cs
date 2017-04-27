using System;
using System.Collections.ObjectModel;

namespace DataModel.Calendar
{
    public class DayTaskReport
    {
        public DateTime Date { get; set; }
        public ObservableCollection<OneTask> DailyTasks { get; set; } 
    }
}
