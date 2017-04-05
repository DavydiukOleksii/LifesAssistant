using System.Collections.ObjectModel;
using DataModel.Credit;

namespace DataModel
{
    public class CreditModel
    {
        public string Name { get; set; }
        public ObservableCollection<DayReport> AllCosts;
    }
}
