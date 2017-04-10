using System.Collections.ObjectModel;
using System.Data;

namespace DataModel.Credit
{
    public class CostsDayReport
    {
        #region Singleton
        protected static CostsDayReport instance = null;
        public static CostsDayReport Instance
        {
            get
            {
                if (instance == null)
                    instance = new CostsDayReport();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected CostsDayReport() { }
        #endregion

        public DataSetDateTime Date { get; set; }
        public int TotalCosts { get; set; }
        public ObservableCollection<OneCashTransaction> DailyCosts { get; set; } 
    }
}
