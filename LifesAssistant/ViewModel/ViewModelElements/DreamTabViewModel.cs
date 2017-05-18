using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DataModel.Dream;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;
using System.IO;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class DreamTabViewModel: ViewModelBase
    {
        #region Singleton
        protected static DreamTabViewModel m_instance = null;
        public static DreamTabViewModel Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new DreamTabViewModel();
                return m_instance;
            }
        }
        #endregion  

        #region Constructor
        protected DreamTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");

            NewSleep = new OneSleep();
            
            TotalSleepTime = DateTime.Today.Add(TimeSpan.FromSeconds(SleepRepository.Instance.GetByDay(DateTime.Today).TotalSleepTimeInSecond));
            DaySleepTime = SleepRepository.Instance.GetByDay(DateTime.Today).DailySleepTimes;

            _appFolderPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            MoonImgPath = _appFolderPath + "\\Config\\Image\\Sleep\\nightcloud.png";
            WorkImgPath = _appFolderPath + "\\Config\\Image\\Sleep\\sleep.png";
        }
        #endregion

        #region Data
        protected string m_TabName = "Dreams";

        protected double _daySleepNorm = 8;
        protected string _appFolderPath;

        protected string _moonImgPath;
        public string MoonImgPath
        {
            get { return _moonImgPath; }
            set
            {
                _moonImgPath = value;
                OnPropertyChanged("MoonImgPath");
            }
        }

        protected string _workImgPath;
        public string WorkImgPath
        {
            get { return _workImgPath; }
            set
            {
                _workImgPath = value;
                OnPropertyChanged("WorkImgPath");
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

        protected DateTime _totalSleepTime;
        public DateTime TotalSleepTime
        {
            get { return _totalSleepTime; }
            set
            {
                _totalSleepTime = value;
                OnPropertyChanged("TotalSleepTime");
                //CheckTotalSleepTime();
            }
        }

        protected OneSleep _currentSleep;
        public OneSleep CurrentSleep
        {
            get { return _currentSleep; }
            set
            {
                _currentSleep = value;
                OnPropertyChanged("CurrentSleep");
            }
        }

        protected ObservableCollection<OneSleep> _daySleepTime;
        public ObservableCollection<OneSleep> DaySleepTime
        {
            get { return _daySleepTime; }
            set
            {
                _daySleepTime = value;
                OnPropertyChanged("DaySleepTime");
            }
        }

        protected OneSleep _newSleep;
        public OneSleep NewSleep
        {
            get { return _newSleep; }
            set
            {
                _newSleep = value;
                OnPropertyChanged("NewSleep");
            }
        }
        #endregion

        #region Commands

        #region Delete sleep time
        private ICommand _dellCurrentSleepTimeCommand;
        public ICommand DellCurrentSleepTime
        {
            get
            {
                if (_dellCurrentSleepTimeCommand == null)
                {
                    _dellCurrentSleepTimeCommand = new RelayCommand(ExecuteDellCurrentSleepTimeCommand, CanExecuteDellCurrentSleepTimeCommand);
                }
                return _dellCurrentSleepTimeCommand;
            }
        }

        public bool CanExecuteDellCurrentSleepTimeCommand(object parametr)
        {
            if (CurrentSleep != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteDellCurrentSleepTimeCommand(object parametr)
        {
            NewSleep.Time = DateTime.Now;
            TotalSleepTime = TotalSleepTime.Subtract(TimeSpan.FromSeconds(CurrentSleep.GetDurationInSecond()));
            SleepRepository.Instance.DeleteOperation(CurrentSleep);

            DaySleepTime.Remove(CurrentSleep);

            OnPropertyChanged("TotalSleepTime");
            CommandManager.InvalidateRequerySuggested();

            if (DaySleepTime.Count > 0)
                CurrentSleep = DaySleepTime.First();

            CheckTotalSleepTime();
        }
        #endregion

        #region Add new sleep time
        private ICommand _addNewSleepTimeCommand;
        public ICommand AddNewSleepTime
        {
            get
            {
                if (_addNewSleepTimeCommand == null)
                {
                    _addNewSleepTimeCommand = new RelayCommand(ExecuteAddNewSleepTimeCommand, CanExecuteAddNewSleepTimeCommand);
                }
                return _addNewSleepTimeCommand;
            }
        }

        public bool CanExecuteAddNewSleepTimeCommand(object parametr)
        {
            if (NewSleep.Duration.Hour != 0 || NewSleep.Duration.Minute != 0 || NewSleep.Duration.Second != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteAddNewSleepTimeCommand(object parametr)
        {
            NewSleep.Time = DateTime.Now;
            DaySleepTime.Add(new OneSleep(){Duration = NewSleep.Duration, Time = NewSleep.Time});
            TotalSleepTime = TotalSleepTime.Add(TimeSpan.FromSeconds(NewSleep.GetDurationInSecond()));

            SleepRepository.Instance.AddOperation(NewSleep);

            OnPropertyChanged("TotalSleepTime");
            CommandManager.InvalidateRequerySuggested();
            
            NewSleep = new OneSleep();

            CheckTotalSleepTime();
        }
        #endregion

        #endregion

        #region Event
        public delegate void NotificationDelegate(string tabName, bool isNotification);

        public event NotificationDelegate DreamTabNotification;

        public void OnDreamTabNotification(string tabName, bool isNotification)
        {
            if (DreamTabNotification != null)
                DreamTabNotification(tabName, isNotification);
        }

        protected void CheckTotalSleepTime()
        {
            if ((TotalSleepTime.Hour * 3600 + TotalSleepTime.Minute*60  + TotalSleepTime.Second) > 0)
            {
                OnDreamTabNotification(m_TabName, false);
            }
            else
            {
                OnDreamTabNotification(m_TabName, true);
            }
        }

        public void RefreshNotification()
        {
            CheckTotalSleepTime();
        }

        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
