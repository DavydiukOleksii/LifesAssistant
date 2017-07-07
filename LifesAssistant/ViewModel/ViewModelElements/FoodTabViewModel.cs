namespace LifesAssistant.ViewModel.ViewModelElements
{
    class FoodTabViewModel: ViewModelBase
    {
        #region Singleton
        protected static FoodTabViewModel instance = null;
        public static FoodTabViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodTabViewModel();
                return instance;
            }
        }
        #endregion
    }
}
