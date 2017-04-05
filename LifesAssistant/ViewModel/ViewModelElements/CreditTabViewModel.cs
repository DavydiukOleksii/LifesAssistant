using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using DataModel.Credit;
using LifesAssistant.Infrastructure;
using LifesAssistant.Properties.Language;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CreditTabViewModel: ViewModelBase
    {
        #region Constructor
        public CreditTabViewModel()
        {
            CurrentDate = Resources.todayLabel + DateTime.Today.ToString("d");
            DayCosts = new ObservableCollection<OneCashTransaction>();
            DayCosts.Add(new OneCashTransaction(){Article = "tea", Money = 1});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "coffee", Money = 2});
            DayCosts.Add(new OneCashTransaction(){Article = "nesquike", Money = 3});
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
