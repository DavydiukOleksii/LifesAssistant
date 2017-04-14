using System;
using System.Windows.Input;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CalendarTabViewModel: ViewModelBase
    {
        #region Constructor

        public CalendarTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");
        }
        #endregion

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
        #endregion

        #region Commands

        //private ICommand _viewTasksCommand;
        //public ICommand ViewTasks
        //{
        //    get
        //    {
        //        if (_viewTasksCommand == null)
        //        {
        //            _viewTasksCommand = new RelayCommand(ExecuteViewTasksCommand, CanExecuteViewTasksCommand);
        //        }
        //        return _viewTasksCommand;
        //    }
        //}

        //public bool CanExecuteViewTasksCommand(object parametr)
        //{
        //    return true;
        //}


        //public void ExecuteViewTasksCommand(object parametr)
        //{
        //    TextBox = "Click";
        //}
        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
