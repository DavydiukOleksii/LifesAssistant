using System.Collections.ObjectModel;

namespace DataModel.Credit
{
    public class CostsModel
    {
        #region Singleton
        protected static CostsModel instance = null;
        public static CostsModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new CostsModel();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected CostsModel() { }
        #endregion

        public string Name { get; set; }
        public ObservableCollection<CostsDayReport> AllCosts;
    }
}
