using System;
using System.Collections.ObjectModel;
using System.Data;

namespace DataModel.Credit
{
    public class CostsDayReport
    {
        public DateTime Date { get; set; }
        public int TotalCosts { get; set; }
        public ObservableCollection<OneCashTransaction> DailyCosts { get; set; } 
    }
}
