using System;
using System.Windows.Input;

namespace EscapePod.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;

        public DelegateCommand(Action<object> action) : this(action, _ => true)
        {
        }

        public DelegateCommand(Action<object> action, Predicate<object> predicate )
        {
            _action = action;
            _predicate = predicate; 
        }

        public bool CanExecute(object parameter)
        {
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
