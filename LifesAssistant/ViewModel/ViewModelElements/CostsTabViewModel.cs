using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using DataModel.Credit;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CostsTabViewModel: ViewModelBase
    {
        #region Constructor
        public CostsTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");

            //if(CostsDayReport.Instance.DailyCosts == null)
            //    CostsDayReport.Instance.DailyCosts = new ObservableCollection<OneCashTransaction>();

            //DayCosts = CostsDayReport.Instance.DailyCosts;

            //DailyTotalCosts = CostsDayReport.Instance.TotalCosts;

            DayCosts = CostsRepository.Instance.GetByDay(DateTime.Today).DailyCosts;
            DailyTotalCosts = CostsRepository.Instance.GetByDay(DateTime.Today).TotalCosts;

            NewCashTransaction = new OneCashTransaction();
        }
        #endregion

        #region Data

        protected string _currentDate;
        public string CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }

        protected OneCashTransaction _currentCashTransaction;
        public OneCashTransaction CurrentCashTransaction
        {
            get { return _currentCashTransaction; }
            set
            {
                _currentCashTransaction = value;
                OnPropertyChanged("CurrentCashTransaction");
            }
        }

        protected OneCashTransaction _newCashTransaction;
        public OneCashTransaction NewCashTransaction
        {
            get { return _newCashTransaction; }
            set
            {
                _newCashTransaction = value;
                OnPropertyChanged("NewCashTransaction");
            }
        }

        protected int _dailyTotalCosts;
        public int DailyTotalCosts
        {
            get { return _dailyTotalCosts; }
            set
            {
                _dailyTotalCosts = value;
                OnPropertyChanged("DailyTotalCosts");
            }
        }

        protected ObservableCollection<OneCashTransaction> _dayCosts;
        public ObservableCollection<OneCashTransaction> DayCosts
        {
            get { return _dayCosts; }
            set
            {
                _dayCosts = value;
                OnPropertyChanged("DayCosts");
            }
        }

        #endregion

        #region Commands
        
        #region Delete curent cash transaction
        private ICommand _dellCurrentCashTransactionCommand;
        public ICommand DellCurrentCashTransaction
        {
            get
            {
                if (_dellCurrentCashTransactionCommand == null)
                {
                    _dellCurrentCashTransactionCommand = new RelayCommand(ExecuteDellCurrentCashTransactionCommand, CanExecuteDellCurrentCashTransactionCommand);
                }
                return _dellCurrentCashTransactionCommand;
            }
        }

        public bool CanExecuteDellCurrentCashTransactionCommand(object parametr)
        {
            if (CurrentCashTransaction != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteDellCurrentCashTransactionCommand(object parametr)
        {
            DailyTotalCosts -= CurrentCashTransaction.Money;
            CostsRepository.Instance.DeleteCashTransaction(CurrentCashTransaction);
            DayCosts.Remove(CurrentCashTransaction);
            
            if(DayCosts.Count > 0)
                CurrentCashTransaction = DayCosts.First();
            
        }
        #endregion

        #region Add new cash transaction
        private ICommand _addCashTransactionCommand;
        public ICommand AddCashTransaction
        {
            get
            {
                if (_addCashTransactionCommand == null)
                {
                    _addCashTransactionCommand = new RelayCommand(ExecuteAddCashTransactionCommand, CanExecuteAddCashTransactionCommand);
                }
                return _addCashTransactionCommand;
            }
        }

        public bool CanExecuteAddCashTransactionCommand(object parametr)
        {
            if (NewCashTransaction.Money > 0 && NewCashTransaction.Article != null && NewCashTransaction.Article.Length <= 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteAddCashTransactionCommand(object parametr)
        {
            DayCosts.Add(new OneCashTransaction(){Article = NewCashTransaction.Article, Money = NewCashTransaction.Money});
            DailyTotalCosts += NewCashTransaction.Money;
            CostsRepository.Instance.AddNewTransaction(NewCashTransaction);
            NewCashTransaction = new OneCashTransaction();
        }
        #endregion

        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
