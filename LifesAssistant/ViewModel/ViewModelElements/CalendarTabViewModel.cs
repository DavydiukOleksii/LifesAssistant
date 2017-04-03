using System.Windows.Input;
using LifesAssistant.Infrastructure;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class CalendarTabViewModel: ViewModelBase
    {
        #region Constructor

        public CalendarTabViewModel()
        {
            TextBox = "Null;";
        }
        #endregion

        #region Data

        protected string _textBox;

        public string TextBox
        {
            get { return _textBox; }
            set
            {
                _textBox = value;
                OnPropertyChanged("TextBox");
            }
        }
        #endregion

        #region Commands

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
            TextBox = "Click";
        }
        #endregion

        #region FreeData
        protected override void OnDispose()
        {

        }
        #endregion
    }
}
