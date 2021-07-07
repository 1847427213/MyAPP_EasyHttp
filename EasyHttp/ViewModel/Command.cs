using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyHttp.ViewModel
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Command(Action<object> _execute) : this(_execute, null) { }

        public Command(Action<object> _execute, Func<object, bool> _canExecute)
        {
            this.execute = _execute;
            this.canExecute = _canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
        public Action<object> execute { get; set; }
        public Func<object, bool> canExecute { get; set; }
    }
}
