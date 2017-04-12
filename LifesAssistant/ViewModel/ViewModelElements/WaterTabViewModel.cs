﻿using System;
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
    public class data
    {
        public double capacity { get; set; }
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

            

            DailyTotalWaterCapacity = WaterRepository.Instance.GetByDay(DateTime.Today).TotalCapacity;

            DayWaterCapacity = WaterRepository.Instance.GetByDay(DateTime.Today).DailyWaterOperations;

            NewWaterOperation = new OnceDrink();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();

            DayWaterCapacityPercent = new ObservableCollection<data>();
            DayWaterCapacityPercent.Add(new data(){capacity = DailyTotalWaterCapacity, name = "nevoda"});
            DayWaterCapacityPercent.Add(new data(){capacity = _dayWaterNorm - DailyTotalWaterCapacity, name = "voda"});
        }
        #endregion

        protected void TimerTick(object sender, EventArgs e)
        {
            if (DayWaterCapacity.Count > 0)
                TimeFromLastDrink = DateTime.Now.Subtract(DayWaterCapacity.Last().Time).ToString(@"hh\:mm\:ss");
            else
            {
                TimeFromLastDrink = DateTime.Now.ToLongTimeString();
            }
        }

        #region Data

        protected double _dayWaterNorm = 5;

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
            WaterRepository.Instance.DeleteWaterOperation(CurrentWaterDrink);
            DayWaterCapacity.Remove(CurrentWaterDrink);

            if (DayWaterCapacity.Count > 0)
                CurrentWaterDrink = DayWaterCapacity.First();

            DayWaterCapacityPercent[0].capacity = DailyTotalWaterCapacity;
            DayWaterCapacityPercent[1].capacity = _dayWaterNorm - DailyTotalWaterCapacity;
            OnPropertyChanged("DayWaterCapacityPercent");
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
            if (NewWaterOperation.Capasity > 0 )
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
            WaterRepository.Instance.AddWaterOperation(NewWaterOperation);
            NewWaterOperation = new OnceDrink();

            DayWaterCapacityPercent[0].capacity = DailyTotalWaterCapacity;
            DayWaterCapacityPercent[1].capacity = _dayWaterNorm - DailyTotalWaterCapacity;
            OnPropertyChanged("DayWaterCapacityPercent");
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