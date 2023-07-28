using System;
using System.Windows.Input;

namespace WorkersWpfClient.ViewModels.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object p)
        {
            return CanExecute(p);
        }

        void ICommand.Execute(object p)
        {
            Execute(p);
        }

        protected virtual bool CanExecute(object p)
        {
            return true;
        }

        protected abstract void Execute(object p);
    }
}
