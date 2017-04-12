using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using DataModel.Credit;
using DataModel.Water;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class data
    {
        public int capacity { get; set; }
        public string name { get; set; }
    }

    class WaterTabViewModel: ViewModelBase
    {
        #region Constructor
        public WaterTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");

            //if(CostsDayReport.Instance.DailyCosts == null)
            //    CostsDayReport.Instance.DailyCosts = new ObservableCollection<OneCashTransaction>();

            //DayCosts = CostsDayReport.Instance.DailyCosts;

            //DailyTotalCosts = CostsDayReport.Instance.TotalCosts;

            //DayCosts = CostsRepository.Instance.GetByDay(DateTime.Today).DailyCosts;
            //DailyTotalCosts = CostsRepository.Instance.GetByDay(DateTime.Today).TotalCosts;

            //NewCashTransaction = new OneCashTransaction();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();

            DayWaterCapacityPercent = new ObservableCollection<data>();
            DayWaterCapacityPercent.Add(new data(){capacity = 5, name = "nevoda"});
            DayWaterCapacityPercent.Add(new data(){capacity = 5, name = "voda"});
        }
        #endregion

        protected void TimerTick(object sender, EventArgs e)
        {
            TimeFromLastDrink = DateTime.Now.ToLongTimeString();
        }

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

        protected string _timeFromLastDrink;
        public string TimeFromLastDrink
        {
            get { return _timeFromLastDrink; }
            set
            {
                _timeFromLastDrink = value;
                OnPropertyChanged("TimeFromLastDrink");
            }
        }

        protected int _dailyTotalWaterCapacity;
        public int DailyTotalWaterCapacity
        {
            get { return _dailyTotalWaterCapacity; }
            set
            {
                _dailyTotalWaterCapacity = value;
                OnPropertyChanged("DailyTotalWaterCapacity");
            }
        }

        protected ObservableCollection<OnceDrink> _dayWaterCapacity;
        public ObservableCollection<OnceDrink> DayWaterCapacity
        {
            get { return _dayWaterCapacity; }
            set
            {
                _dayWaterCapacity = value;
                OnPropertyChanged("DayWaterCapacity");
            }
        }

        protected ObservableCollection<data> _dayWaterCapacityPercent;
        public ObservableCollection<data> DayWaterCapacityPercent
        {
            get { return _dayWaterCapacityPercent; }
            set
            {
                _dayWaterCapacityPercent = value;
                OnPropertyChanged("DayWaterCapacityPercent");
            }
        }


        //protected OneCashTransaction _currentCashTransaction;
        //public OneCashTransaction CurrentCashTransaction
        //{
        //    get { return _currentCashTransaction; }
        //    set
        //    {
        //        _currentCashTransaction = value;
        //        OnPropertyChanged("CurrentCashTransaction");
        //    }
        //}

        //protected OneCashTransaction _newCashTransaction;
        //public OneCashTransaction NewCashTransaction
        //{
        //    get { return _newCashTransaction; }
        //    set
        //    {
        //        _newCashTransaction = value;
        //        OnPropertyChanged("NewCashTransaction");
        //    }
        //}
        #endregion

        #region Commands
        
        //#region Delete curent cash transaction
        //private ICommand _dellCurrentCashTransactionCommand;
        //public ICommand DellCurrentCashTransaction
        //{
        //    get
        //    {
        //        if (_dellCurrentCashTransactionCommand == null)
        //        {
        //            _dellCurrentCashTransactionCommand = new RelayCommand(ExecuteDellCurrentCashTransactionCommand, CanExecuteDellCurrentCashTransactionCommand);
        //        }
        //        return _dellCurrentCashTransactionCommand;
        //    }
        //}

        //public bool CanExecuteDellCurrentCashTransactionCommand(object parametr)
        //{
        //    if (CurrentCashTransaction != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void ExecuteDellCurrentCashTransactionCommand(object parametr)
        //{
        //    DailyTotalCosts -= CurrentCashTransaction.Money;
        //    CostsRepository.Instance.DeleteCashTransaction(CurrentCashTransaction);
        //    DayCosts.Remove(CurrentCashTransaction);
            
        //    if(DayCosts.Count > 0)
        //        CurrentCashTransaction = DayCosts.First();
            
        //}
        //#endregion

        //#region Add new cash transaction
        //private ICommand _addCashTransactionCommand;
        //public ICommand AddCashTransaction
        //{
        //    get
        //    {
        //        if (_addCashTransactionCommand == null)
        //        {
        //            _addCashTransactionCommand = new RelayCommand(ExecuteAddCashTransactionCommand, CanExecuteAddCashTransactionCommand);
        //        }
        //        return _addCashTransactionCommand;
        //    }
        //}

        //public bool CanExecuteAddCashTransactionCommand(object parametr)
        //{
        //    if (NewCashTransaction.Money > 0 && NewCashTransaction.Article != null && NewCashTransaction.Article.Length <= 20)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void ExecuteAddCashTransactionCommand(object parametr)
        //{
        //    DayCosts.Add(new OneCashTransaction(){Article = NewCashTransaction.Article, Money = NewCashTransaction.Money});
        //    DailyTotalCosts += NewCashTransaction.Money;
        //    CostsRepository.Instance.AddNewTransaction(NewCashTransaction);
        //    NewCashTransaction = new OneCashTransaction();
        //}
        //#endregion

        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
