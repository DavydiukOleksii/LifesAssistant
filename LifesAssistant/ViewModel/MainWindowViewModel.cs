using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataModel.Charts;
using DataRepository;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;
using LifesAssistant.View.ViewElements;
using LifesAssistant.ViewModel.ViewModelElements;

namespace LifesAssistant.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel()
        {
            TabWidth = 0;
            TabHeight = 60;
            ChartsButtonHeight = 25;
            WindowHeight = _defaultWindowsHeight;
            WindowWidth = _defaultWindowWidth;
            MainWidth = 400;
            WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
            ChartsLabel = Resources.showChartsLabel;

            Charts = new ObservableCollection<ChartsElement>();

            ExecuteTabChangedCommand("CalendarRepository");
        }
        #endregion

        #region Data

        #region Data for controle Windows size, position and state
        //default windows width in diferent states
        protected int _defoultChartsHeight = 275;
        protected int _defaultWindowWidth = 430;
        protected int _defaultWindowsHeight = 385;
        protected int _minimizeWindowWidth = 72;
        //current windows position
        protected int _currentWinTopPos;
        protected int _currentWinLeftPos;
        //property
        protected int _windowWidth;
        public int WindowWidth
        {
            get
            {
                return _windowWidth;
            }
            set
            {
                _windowWidth = value;
                OnPropertyChanged("WindowWidth");
            }
        }

        protected int _windowHeight;
        public int WindowHeight
        {
            get
            {
                return _windowHeight;
            }
            set
            {
                _windowHeight = value;
                OnPropertyChanged("WindowHeight");
            }
        }

        protected int _tabHeight;
        public int TabHeight
        {
            get
            {
                return _tabHeight;
            }
            set
            {
                _tabHeight = value;
                OnPropertyChanged("TabHeight");
            }
        }

        protected int _chartsButtonHeight;
        public int ChartsButtonHeight
        {
            get
            {
                return _chartsButtonHeight;
            }
            set
            {
                _chartsButtonHeight = value;
                OnPropertyChanged("ChartsButtonHeight");
            }
        }

        protected int _chartsHeight;
        public int ChartsHeight
        {
            get
            {
                return _chartsHeight;
            }
            set
            {
                _chartsHeight = value;
                OnPropertyChanged("ChartsHeight");
            }
        }

        protected int _tabWidth;
        public int TabWidth
        {
            get
            {
                return _tabWidth;
            }
            set
            {
                _tabWidth = value;
                OnPropertyChanged("TabWidth");
            }
        }

        protected int _mainWidth;
        public int MainWidth
        {
            get { return _mainWidth; }
            set
            {
                _mainWidth = value;
                OnPropertyChanged("MainWidth");
            }
        }

        protected double _windowPosLeft;
        public double WindowPosLeft
        {
            get
            {
                return _windowPosLeft;
            }
            set
            {
                _windowPosLeft = value;
                OnPropertyChanged("WindowPosLeft");
            }
        }

        protected WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        protected double _windowPosTop;
        public double WindowPosTop
        {
            get
            {
                return _windowPosTop;
            }
            set
            {
                _windowPosTop = value;
                OnPropertyChanged("WindowPosTop");
            }
        }

        protected string _chartsLabel;
        public string ChartsLabel
        {
            get
            {
                return _chartsLabel;
            }
            set
            {
                _chartsLabel = value;
                OnPropertyChanged("ChartsLabel");
            }
        }
        #endregion

        #region Data for tabControle

        protected string _currentTab;
        public string CurrentTab
        {
            get { return _currentTab; }
            set
            {
                _currentTab = value;
                OnPropertyChanged("CurrentTab");
            }
        }

        protected ObservableCollection<ChartsElement> _charts;
        public ObservableCollection<ChartsElement> Charts
        {
            get { return _charts; }
            set
            {
                _charts = value;
                OnPropertyChanged("Charts");
            }
        } 

        protected UserControl _mainPanel;
        public UserControl MainPanel
        {
            get { return _mainPanel; }
            set
            {
                _mainPanel = value;
                OnPropertyChanged("MainPanel");
            }
        }

        #endregion

        #endregion

        #region Command

        #region ChangeWindowSize
        RelayCommand _changeWindowSizeCommand;
        public ICommand ChangeWindowSize
        {
            get
            {
                if (_changeWindowSizeCommand == null)
                {
                    _changeWindowSizeCommand = new RelayCommand(ExecuteChangeWSCommand, CanExecuteChangeWSCommand);
                }
                return _changeWindowSizeCommand;
            }
        }

        public void ExecuteChangeWSCommand(object parameter)
        {
            WindowHeight = _defaultWindowsHeight;
            ChartsHeight = 0;
            ChartsLabel = Resources.showChartsLabel;

            if (TabWidth > 0 && TabHeight == 0)
            {
                TabWidth = 0;
                TabHeight = 60;
                WindowWidth = _defaultWindowWidth;
                MainWidth = 400;
                if(!(MainPanel is CalendarTab))
                    ChartsButtonHeight = 25;
                WindowPosLeft = _currentWinLeftPos;
                WindowPosTop = _currentWinTopPos;
            }
            else
            {
                if ((MainPanel is CalendarTab) && (MainPanel.DataContext as CalendarTabViewModel).TaskHeight > 0)
                    (MainPanel.DataContext as CalendarTabViewModel).ViewTasks.Execute(this);
                TabWidth = 42;
                TabHeight = 0;
                WindowWidth = _minimizeWindowWidth;
                WindowHeight = _defaultWindowsHeight;
                MainWidth = 0;
                ChartsButtonHeight = 0;
                _currentWinLeftPos = (int)WindowPosLeft;
                WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
                _currentWinTopPos = (int)WindowPosTop;
                WindowPosTop = 0;
            }
        }

        public bool CanExecuteChangeWSCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region ShowCharts
        RelayCommand _showChartsCommand;
        public ICommand ShowCharts
        {
            get
            {
                if (_showChartsCommand == null)
                {
                    _showChartsCommand = new RelayCommand(ExecuteShowChartsCommand, CanExecuteShowChartsCommand);
                }
                return _showChartsCommand;
            }
        }

        public void ExecuteShowChartsCommand(object parameter)
        {
            if (ChartsHeight > 0)
            {
                WindowHeight = _defaultWindowsHeight;
                ChartsHeight = 0;
                ChartsLabel = Resources.showChartsLabel;
            }
            else
            {
                ChartsHeight = _defoultChartsHeight;
                WindowHeight = _defaultWindowsHeight + _defoultChartsHeight;
                ChartsLabel = Resources.hideChartsLabel;

                switch (CurrentTab)
                {
                    case "Credit":
                        {
                            Charts = CostsRepository.Instance.GetTotalByDay();
                            break;
                        }
                    case "Water":
                        {
                            Charts = WaterRepository.Instance.GetTotalByDay();
                            break;
                        }
                    case "Dream":
                        {
                            Charts = SleepRepository.Instance.GetTotalByDay();
                            break;
                        }
                }
            }
        }

        public bool CanExecuteShowChartsCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region CloseWindow
        RelayCommand _closeWindowComand;
        public ICommand CloseWindow
        {
            get
            {
                if (_closeWindowComand == null)
                {
                    _closeWindowComand = new RelayCommand(ExecuteCloseWindowCommand, CanExecuteCloseWindowCommand);
                }
                return _closeWindowComand;
            }
        }

        public void ExecuteCloseWindowCommand(object parameter)
        {
            this.OnDispose();
            Application.Current.Shutdown(0);
        }

        public bool CanExecuteCloseWindowCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region MinimizeWindow
        RelayCommand _minimizeWindowComand;
        public ICommand MinimizeWindow
        {
            get
            {
                if (_minimizeWindowComand == null)
                {
                    _minimizeWindowComand = new RelayCommand(ExecuteMinimizeWindowCommand, CanExecuteMinimizeWindowCommand);
                }
                return _minimizeWindowComand;
            }
        }

        public void ExecuteMinimizeWindowCommand(object parameter)
        {
            this.WindowState = WindowState.Minimized;
        }

        public bool CanExecuteMinimizeWindowCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region TabChanged
        private ICommand _tabChangedCommand;
        public ICommand TabChanged
            {
            get
            {
                if (_tabChangedCommand == null)
                {
                    _tabChangedCommand = new RelayCommand(ExecuteTabChangedCommand, CanExecuteTabChangedCommand);
                }
                return _tabChangedCommand;
            }
        }

        public void ExecuteTabChangedCommand(object parameter)
        {
            string tabName = parameter.ToString();
            CurrentTab = parameter.ToString();
            ChartsButtonHeight = 25;
            WindowHeight = _defaultWindowsHeight;
            if (ChartsHeight > 0)
                ExecuteShowChartsCommand(this);
            switch (tabName)
            {
                case "CalendarRepository":
                {
                    MainPanel = new CalendarTab();
                    MainPanel.DataContext = new CalendarTabViewModel();
                    (MainPanel.DataContext as CalendarTabViewModel).OpenTaskEvent += ChangeWindowSizeEventsHandler;
                    ChartsButtonHeight = 0;
                    
                    break;
                }
                case "Credit":
                {
                    MainPanel = new CostsTab();
                    MainPanel.DataContext = new CostsTabViewModel();
                    break;
                }
                case "Water":
                {
                    MainPanel = new WaterTab();
                    MainPanel.DataContext = new WaterTabViewModel();
                    break;
                }
                case "Dream":
                {
                    MainPanel = new DreamTab();
                    MainPanel.DataContext = new DreamTabViewModel();
                    break;
                }
            }
            if(WindowWidth < _defaultWindowWidth)
                ChangeWindowSize.Execute(this);
        }

        public bool CanExecuteTabChangedCommand(object parameter)
        {
            if(parameter != null && parameter.ToString().Length > 0)
                return true;
            else
            {
                return false;
            }
        }
      
        #endregion

        #region ChartGroupChange
        private ICommand _chartGroupChangeCommand;
        public ICommand ChartGroupChange
        {
            get
            {
                if (_chartGroupChangeCommand == null)
                {
                    _chartGroupChangeCommand = new RelayCommand(ExecuteChartGroupChangeCommand, CanExecuteChartGroupChangeCommand);
                }
                return _chartGroupChangeCommand;
            }
        }

        public void ExecuteChartGroupChangeCommand(object parameter)
        {
            string group = parameter.ToString();

            switch (CurrentTab)
            {
                case "Credit":
                    {
                        switch (group)
                        {
                            case "day":
                                {
                                    Charts = CostsRepository.Instance.GetTotalByDay();
                                    break;
                                }
                            case "month":
                                {
                                    Charts = CostsRepository.Instance.GetTotalByMonth();
                                    break;
                                }
                            case "year":
                                {
                                    Charts = CostsRepository.Instance.GetTotalByYear();
                                    break;
                                }
                        }
                        break;
                    }
                case "Water":
                    {
                        switch (group)
                        {
                            case "day":
                                {
                                    Charts = WaterRepository.Instance.GetTotalByDay();
                                    break;
                                }
                            case "month":
                                {
                                    Charts = WaterRepository.Instance.GetTotalByMonth();
                                    break;
                                }
                            case "year":
                                {
                                    Charts = WaterRepository.Instance.GetTotalByYear();
                                    break;
                                }
                        }
                        break;
                    }
                case "Dream":
                    {
                        switch (group)
                        {
                            case "day":
                                {
                                    Charts = SleepRepository.Instance.GetTotalByDay();
                                    break;
                                }
                            case "month":
                                {
                                    Charts = SleepRepository.Instance.GetTotalByMonth();
                                    break;
                                }
                            case "year":
                                {
                                    Charts = SleepRepository.Instance.GetTotalByYear();
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public bool CanExecuteChartGroupChangeCommand(object parameter)
        {
            return true;
        }

        #endregion

        #endregion

        //todo: rewrite
        #region Events

        public void ChangeWindowSizeEventsHandler()
        {
            if (WindowHeight == _defaultWindowsHeight)
            {
                WindowHeight = _defoultChartsHeight + _defaultWindowsHeight;
            }
            else
            {
                WindowHeight = _defaultWindowsHeight;
            }
            
        }

        #endregion

        #region FreeData
        protected override void OnDispose()
        {
            
        }
        #endregion
    }
}
