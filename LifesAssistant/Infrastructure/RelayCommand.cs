using System;
using System.Windows.Input;

namespace LifesAssistant.Infrastructure
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #region Constructor
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        //determines when the command can execute
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter);
        }

        //event, when the state canExecute been changed, the elements check it event
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        //start logic of command
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
