﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DataModel.Credit;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CostsTabViewModel: ViewModelBase
    {
        #region Singleton
        protected static CostsTabViewModel m_instance = null;
        public static CostsTabViewModel Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new CostsTabViewModel();
                return m_instance;
            }
        }
        #endregion  

        #region Constructor
        protected CostsTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");
            DayCosts = CostsRepository.Instance.GetByDay(DateTime.Today).DailyCosts;
            DailyTotalCosts = CostsRepository.Instance.GetByDay(DateTime.Today).TotalCosts;

            NewCashTransaction = new OneCashTransaction();
        }
        #endregion

        #region Data
        protected string m_TabName = "Costs"; 

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
                CheckTotalCosts();
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
            CostsRepository.Instance.DeleteOperation(CurrentCashTransaction);
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
            CostsRepository.Instance.AddOperation(NewCashTransaction);
            NewCashTransaction = new OneCashTransaction();
        }
        #endregion

        #endregion

        #region Events
        public delegate void NotificationDelegate(string tabName, bool isNotification); 

        public event NotificationDelegate TabNotification;

        public void OnTabNotification(string tabName, bool isNotification)
        {
            if (TabNotification != null)
                TabNotification(tabName, isNotification);
        }

        protected void CheckTotalCosts()
        {
            if(DailyTotalCosts > 0)
            {
                OnTabNotification(m_TabName, false);
            }
            else
            {
                OnTabNotification(m_TabName, true);
            }
        } 

        public void RefreshNotification()
        {
            CheckTotalCosts();
        }
        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
