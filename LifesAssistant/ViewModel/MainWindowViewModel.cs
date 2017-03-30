using System.Windows;
using System.Windows.Input;
using LifesAssistant.Infrastructure;

namespace LifesAssistant.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel()
        {
            TabWidth = 0;
            TabHeight = 60;
            WindowWidth = _defaultWindowWidth;
            MainWidth = 400;
            WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
        }
        #endregion

        #region Data
        //
        protected int _defaultWindowWidth = 430;
        protected int _minimizeWindowWidth = 72;
        //
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

        //protected double _windowPosTop;
        //public double WindowPosTop
        //{
        //    get
        //    {
        //        return _windowPosTop;
        //    }
        //    set
        //    {
        //        _windowPosTop = value;
        //        OnPropertyChanged("WindowPosTop");
        //    }
        //}

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
                WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
            }
            else
            {
                TabWidth = 42;
                TabHeight = 0;
                WindowWidth = _minimizeWindowWidth;
                MainWidth = 0;
                WindowPosLeft = SystemParameters.WorkArea.Right - WindowWidth;
            }
        }

        public bool CanExecuteChangeWSCommand(object parameter)
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

        #region CloseWindow
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

        #endregion

        #region FreeData
        protected override void OnDispose()
        {
            
        }
        #endregion
    }
}
