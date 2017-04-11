using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            WindowHeight = 360;
            WindowWidth = _defaultWindowWidth;
            MainWidth = 400;
            WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
            ChartsLabel = Resources.showChartsLabel;

            MainPanel = new CalendarTab();
            MainPanel.DataContext = new CalendarTabViewModel();
        }
        #endregion

        #region Data

        #region Data for controle Windows size, position and state
        //default windows width in diferent states
        protected int _defaultWindowWidth = 430;
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
            if (TabWidth > 0 && TabHeight == 0)
            {
                TabWidth = 0;
                TabHeight = 60;
                WindowWidth = _defaultWindowWidth;
                MainWidth = 400;
                ChartsButtonHeight = 25;
                WindowPosLeft = _currentWinLeftPos;
                WindowPosTop = _currentWinTopPos;
            }
            else
            {
                TabWidth = 42;
                TabHeight = 0;
                WindowWidth = _minimizeWindowWidth;
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
                WindowHeight = 360;
                ChartsHeight = 0;
                ChartsLabel = Resources.showChartsLabel;
            }
            else
            {
                ChartsHeight = 275;
                WindowHeight = 360 + 275;
                ChartsLabel = Resources.hideChartsLabel;
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
            switch (tabName)
            {
                case "Calendar":
                {
                    MainPanel = new CalendarTab();
                    MainPanel.DataContext = new CalendarTabViewModel();
                    break;
                }
                case "Credit":
                {
                    MainPanel = new CreditTab();
                    MainPanel.DataContext = new CreditTabViewModel();
                    break;
                }
                case "Water":
                {
                    MainPanel = new WaterTab();
                    break;
                }
                case "Dream":
                {
                    MainPanel = new DreamTab();
                    break;
                }
                case "Process":
                {
                    MainPanel = new ProcessTab();
                    break;
                }
            }
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

        #endregion

        #region FreeData
        protected override void OnDispose()
        {
            
        }
        #endregion
    }
}
