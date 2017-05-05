using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using DataModel.Calendar;
using DataRepository;
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

            NewTask = new OneTask();

            SearchDate = DateTime.Today;
            DayTasks = CalendarRepository.Instance.GetByDay(DateTime.Today).DailyTasks;

            DaysWithTask = CalendarRepository.Instance.GetListDaysWithTask();

            
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

        protected string _taskDate;
        public string TaskDate
        {
            get { return _taskDate; }
            set
            {
                _taskDate = value;
                OnPropertyChanged("TaskDate");
            }
        }

        protected DateTime _searchDate;
        public DateTime SearchDate
        {
            get { return _searchDate; }
            set
            {
                _searchDate = value;
                OnPropertyChanged("SearchDate");
            }
        }

        protected List<CalendarDateRange> _daysWithTask;
        public List<CalendarDateRange> DaysWithTask
        {
            get { return _daysWithTask; }
            set
            {
                _daysWithTask = value;
                OnPropertyChanged("DaysWithTask");
            }
        }

        protected DateTime _currentSelectedDate;
        public DateTime CurrentSelectedDate
        {
            get { return _currentSelectedDate; }
            set
            {
                _currentSelectedDate = value;
                SearchDate = value;
                OnPropertyChanged("CurrentSelectedDate");
                OnPropertyChanged("SearchDate");
            }
        }

        #region Tasks panel data

        protected OneTask _newTask;
        public OneTask NewTask
        {
            get { return _newTask; }
            set
            {
                _newTask = value;
                OnPropertyChanged("NewTask");
            }
        }

        protected DateTime _tasksDate;
        public DateTime TasksDate
        {
            get { return _tasksDate; }
            set
            {
                _tasksDate = value;
                OnPropertyChanged("TasksDate");
            }
        }

        protected OneTask _searchTask;
        public OneTask SearchTask
        {
            get { return _searchTask; }
            set
            {
                _searchTask = value;
                OnPropertyChanged("SearchTask");
            }
        }

        protected OneTask _selectedTask;
        public OneTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        protected ObservableCollection<OneTask> _dayTasks;
        public ObservableCollection<OneTask> DayTasks
        {
            get { return _dayTasks; }
            set
            {
                _dayTasks = value;
                OnPropertyChanged("DayTasks");
            }
        }

        #endregion

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
            FlyoutIsOpen = false;

            if (TaskHeight == 0)
            {
                DayTasks = CalendarRepository.Instance.GetByDay(SearchDate).DailyTasks;
                ShowTasksLabel = Resources.hideTasksLabel;
                TaskHeight = _defaultTaskHeight;
                TaskDate = SearchDate.ToShortDateString();
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

        #region FindDate
        private ICommand _findSelectedDateCommand;
        public ICommand FindSelectedDate
        {
            get
            {
                if (_findSelectedDateCommand == null)
                {
                    _findSelectedDateCommand = new RelayCommand(ExecuteFindSelectedDateCommand, CanExecuteFindSelectedDateCommand);
                }
                return _findSelectedDateCommand;
            }
        }

        public bool CanExecuteFindSelectedDateCommand(object parametr)
        {
            return true;
        }

        public void ExecuteFindSelectedDateCommand(object parametr)
        {
            if (SearchDate != null)
            {
                CurrentSelectedDate = SearchDate;
                DayTasks = CalendarRepository.Instance.GetByDay(CurrentSelectedDate).DailyTasks;
                if(_taskHeight == 0)
                    ViewTasks.Execute(null);

                TaskDate = SearchDate.ToShortDateString();
            }
        }
        #endregion

        #region Add Task
        private ICommand _addTaskCommand;
        public ICommand AddTask
        {
            get
            {
                if (_addTaskCommand == null)
                {
                    _addTaskCommand = new RelayCommand(ExecuteAddTaskCommand, CanExecuteAddTaskCommand);
                }
                return _addTaskCommand;
            }
        }

        public bool CanExecuteAddTaskCommand(object parametr)
        {
            if (NewTask != null && NewTask.Time != null && NewTask.Descriptions != null && NewTask.Descriptions.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteAddTaskCommand(object parametr)
        {
            NewTask.Time = SearchDate + NewTask.Time.TimeOfDay;
            CalendarRepository.Instance.AddOperation(NewTask);
            NewTask = new OneTask();
            DayTasks = CalendarRepository.Instance.GetByDay(SearchDate).DailyTasks;
            FlyoutIsOpen = false;
        }
        #endregion

        #region Dell Task
        private ICommand _dellCurrentTaskCommand;
        public ICommand DellCurrentTask
        {
            get
            {
                if (_dellCurrentTaskCommand == null)
                {
                    _dellCurrentTaskCommand = new RelayCommand(ExecuteDellCurrentTaskCommand, CanExecuteDellCurrentTaskCommand);
                }
                return _dellCurrentTaskCommand;
            }
        }

        public bool CanExecuteDellCurrentTaskCommand(object parametr)
        {
            if (SelectedTask != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteDellCurrentTaskCommand(object parametr)
        {
            SelectedTask.Time = SearchDate + SelectedTask.Time.TimeOfDay;
            CalendarRepository.Instance.DeleteOperation(SelectedTask);
            SelectedTask = new OneTask();
            DayTasks = CalendarRepository.Instance.GetByDay(SearchDate).DailyTasks;
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
