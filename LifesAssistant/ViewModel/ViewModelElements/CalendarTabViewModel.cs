using System;
using System.Windows;
using System.Windows.Input;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CalendarTabViewModel: ViewModelBase
    {
        #region Events

        public delegate void UserDel();

        public event UserDel UserEvent;

        public void OnUserEvent()
        {
            if(null != UserEvent)
                UserEvent();
        }


        #endregion


        #region Constructor

        public CalendarTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");
            ShowTasksLabel = Resources.showTasksLabel;
            FlyoutIsOpen = false;
            TaskHeight = 0;
        }
        #endregion

        #region Data

        protected int _defaultTaskHeight = 275;

        protected string _showTasksLabel;
        public string ShowTasksLabel
        {
            get { return _showTasksLabel; }
            set
            {
                _showTasksLabel = value;
                OnPropertyChanged("ShowTasksLabel");
            }
        }

        protected bool _flyoutIsOpen;
        public bool FlyoutIsOpen
        {
            get { return _flyoutIsOpen; }
            set
            {
                _flyoutIsOpen = value;
                OnPropertyChanged("FlyoutIsOpen");
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

        protected int _taskHeight;
        public int TaskHeight
        {
            get { return _taskHeight; }
            set
            {
                _taskHeight = value;
                OnPropertyChanged("TaskHeight");
            }
        }
        #endregion

        #region Commands

        #region ViewTasks
        private ICommand _viewTasksCommand;
        public ICommand ViewTasks
        {
            get
            {
                if (_viewTasksCommand == null)
                {
                    _viewTasksCommand = new RelayCommand(ExecuteViewTasksCommand, CanExecuteViewTasksCommand);
                }
                return _viewTasksCommand;
            }
        }

        public bool CanExecuteViewTasksCommand(object parametr)
        {
            return true;
        }


        public void ExecuteViewTasksCommand(object parametr)
        {
            if (TaskHeight == 0)
            {
                ShowTasksLabel = Resources.hideTasksLabel;
                TaskHeight = _defaultTaskHeight;
            }
            else
            {
                ShowTasksLabel = Resources.showTasksLabel;
                TaskHeight = 0;
            }
            OnUserEvent();
        }
        #endregion

        #region FlyoutOpen
        private ICommand _viewFlyoutCommand;
        public ICommand ViewFlyout
        {
            get
            {
                if (_viewFlyoutCommand == null)
                {
                    _viewFlyoutCommand = new RelayCommand(ExecuteViewFlyoutCommand, CanExecuteViewFlyoutCommand);
                }
                return _viewFlyoutCommand;
            }
        }

        public bool CanExecuteViewFlyoutCommand(object parametr)
        {
            return true;
        }


        public void ExecuteViewFlyoutCommand(object parametr)
        {
            if (FlyoutIsOpen == false)
            {
                FlyoutIsOpen = true;
            }
            else
            {
                FlyoutIsOpen = false;
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
