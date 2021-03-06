﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using DataModel.Water;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    public class Data
    {
        public double Capacity { get; set; }
        public string Name { get; set; }
    }

    public class WaterTabViewModel: ViewModelBase
    {
        #region Singleton
        protected static WaterTabViewModel m_instance = null;
        public static WaterTabViewModel Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new WaterTabViewModel();
                return m_instance;
            }
        }
        #endregion  

        #region Constructor
        protected WaterTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");
            
            DailyTotalWaterCapacity = WaterRepository.Instance.GetByDay(DateTime.Today).TotalCapacity;
            DayWaterCapacity = WaterRepository.Instance.GetByDay(DateTime.Today).DailyWaterOperations;

            NewWaterOperation = new OnceDrink();
            NewWaterOperation.Capasity = 0.1;

            SetLastDrincOperationTimer();
            SetNotificationTimer();

            UpdateWaterPercent();
        }
        #endregion

        #region Methods
        protected void TimerTick(object sender, EventArgs e)
        {
            if (DayWaterCapacity.Count > 0)
            {
                TimeFromLastDrink = DateTime.Now.Subtract(DayWaterCapacity.Last().Time).ToString(@"hh\:mm\:ss");   
            }
            else
            {
                TimeFromLastDrink = DateTime.Now.ToLongTimeString();
            }
        }

        protected void UpdateWaterPercent()
        {
            DayWaterCapacityPercent = new ObservableCollection<Data>();
            if (DailyTotalWaterCapacity < _dayWaterNorm)
            {
                DayWaterCapacityPercent.Add(new Data() { Capacity = DailyTotalWaterCapacity, Name = "nevoda" });
                DayWaterCapacityPercent.Add(new Data()
                {
                    Capacity = _dayWaterNorm - DailyTotalWaterCapacity,
                    Name = "voda"
                });

                if (DailyTotalWaterCapacity != 0)
                {
                    WaterPercent = DailyTotalWaterCapacity / _dayWaterNorm * 100;
                }
                else
                {
                    WaterPercent = 0;
                }
            }
            else
            {
                DayWaterCapacityPercent.Add(new Data() { Capacity = _dayWaterNorm, Name = "nevoda" });
                DayWaterCapacityPercent.Add(new Data()
                {
                    Capacity = 0,
                    Name = "voda"
                });
                WaterPercent = 100;
            }
        }

        protected void SetNotificationTimer()
        {
            DispatcherTimer notifyTimer = new DispatcherTimer();
            notifyTimer.Interval = TimeSpan.FromSeconds(600);
            notifyTimer.Tick += NotificationTimerTick;
            notifyTimer.Start();
        }

        protected void SetLastDrincOperationTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected void NotificationTimerTick(object sender, EventArgs e)
        {
            if (DayWaterCapacity.Count > 0)
            {
                CheckTime((int)DateTime.Now.Subtract(DayWaterCapacity.Last().Time).TotalSeconds);
            }
            else
            {
                CheckTime(DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
            }
        }

        public void RefreshNotification()
        {
            NotificationTimerTick(this, null);
        }

        #endregion

        #region Data
        protected string m_TabName = "Water";
        protected bool isNotification = false;
        protected string m_NotifiMassage = Resources.waterTabMessage;

        protected double _dayWaterNorm;
        public double DayWaterNorm
        {
            get { return _dayWaterNorm; }
            set
            {
                _dayWaterNorm = value;
                OnPropertyChanged("DayWaterNorm");
                UpdateWaterPercent();
            }
        }

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
            get
            {
                return _timeFromLastDrink;
            }
            set
            {
                _timeFromLastDrink = value;
                OnPropertyChanged("TimeFromLastDrink");
            }
        }

        protected double _dailyTotalWaterCapacity;
        public double DailyTotalWaterCapacity
        {
            get { return _dailyTotalWaterCapacity; }
            set
            {
                _dailyTotalWaterCapacity = value;
                OnPropertyChanged("DailyTotalWaterCapacity");
            }
        }

        protected double _waterPercent;
        public double WaterPercent
        {
            get { return _waterPercent; }
            set
            {
                _waterPercent = value;
                OnPropertyChanged("WaterPercent");
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

        protected ObservableCollection<Data> _dayWaterCapacityPercent;
        public ObservableCollection<Data> DayWaterCapacityPercent
        {
            get { return _dayWaterCapacityPercent; }
            set
            {
                _dayWaterCapacityPercent = value;
                OnPropertyChanged("DayWaterCapacityPercent");
            }
        }

        protected OnceDrink _currentWaterDrink;
        public OnceDrink CurrentWaterDrink
        {
            get { return _currentWaterDrink; }
            set
            {
                _currentWaterDrink = value;
                OnPropertyChanged("CurrentWaterDrink");
            }
        }

        protected OnceDrink _newWaterOperation;
        public OnceDrink NewWaterOperation
        {
            get { return _newWaterOperation; }
            set
            {
                _newWaterOperation = value;
                OnPropertyChanged("NewWaterOperation");
            }
        }
        #endregion

        #region Commands

        #region Delete curent water operation
        private ICommand _dellCurrentWaterOperationCommand;
        public ICommand DellCurrentWaterOperation
        {
            get
            {
                if (_dellCurrentWaterOperationCommand == null)
                {
                    _dellCurrentWaterOperationCommand = new RelayCommand(ExecuteDellCurrentWaterOperationCommand, CanExecuteDellCurrentWaterOperationCommand);
                }
                return _dellCurrentWaterOperationCommand;
            }
        }

        public bool CanExecuteDellCurrentWaterOperationCommand(object parametr)
        {
            if (CurrentWaterDrink != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteDellCurrentWaterOperationCommand(object parametr)
        {
            DailyTotalWaterCapacity -= CurrentWaterDrink.Capasity;
            WaterRepository.Instance.DeleteOperation(CurrentWaterDrink);
            DayWaterCapacity.Remove(CurrentWaterDrink);

            if (DayWaterCapacity.Count > 0)
                CurrentWaterDrink = DayWaterCapacity.First();

            UpdateWaterPercent();
            RefreshNotification();
        }
        #endregion

        #region Add new water operation
        private ICommand _addNewWaterOperationCommand;
        public ICommand AddNewWaterOperation
        {
            get
            {
                if (_addNewWaterOperationCommand == null)
                {
                    _addNewWaterOperationCommand = new RelayCommand(ExecuteAddNewWaterOperationCommand, CanExecuteAddNewWaterOperationCommand);
                }
                return _addNewWaterOperationCommand;
            }
        }

        public bool CanExecuteAddNewWaterOperationCommand(object parametr)
        {
            if (NewWaterOperation.Capasity > 0  && NewWaterOperation.Capasity < 5.1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteAddNewWaterOperationCommand(object parametr)
        {
            NewWaterOperation.Time = DateTime.Now;
            DayWaterCapacity.Add(new OnceDrink() { Capasity = NewWaterOperation.Capasity, Time = NewWaterOperation.Time});
            DailyTotalWaterCapacity += NewWaterOperation.Capasity;
            WaterRepository.Instance.AddOperation(NewWaterOperation);
            NewWaterOperation = new OnceDrink();

            NewWaterOperation.Capasity = 0.1;

            UpdateWaterPercent();
            RefreshNotification();
        }
        #endregion

        #endregion

        #region Event
        public delegate void NotificationDelegate(string tabName, bool isNotification);
        public delegate void NotificationMessageDelegate(string tabName, string message);

        public event NotificationDelegate WaterTabNotification;
        public event NotificationMessageDelegate WaterTabMessageNotification;

        public void OnWaterTabNotification(string tabName, bool isNotification)
        {
            if (WaterTabNotification != null)
                WaterTabNotification(tabName, isNotification);
        }
        public void OnWaterTabMessageNotification(string tabName, string message)
        {
            if (WaterTabMessageNotification != null)
                WaterTabMessageNotification(tabName, message);
        }

        protected void CheckTime(int time)
        {
            if (time > 7200)
            {
                if (isNotification)
                {
                    if(time % 600 == 0)
                    {
                        OnWaterTabMessageNotification(m_TabName, m_NotifiMassage);
                    }
                }
                else
                {
                    OnWaterTabNotification(m_TabName, true);
                    isNotification = true;
                }
            }
            else
            {
                OnWaterTabNotification(m_TabName, false);
                isNotification = false;
            }
        }

        //public void RefreshNotification()
        //{
        //    CheckTotalSleepTime();
        //}

        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
