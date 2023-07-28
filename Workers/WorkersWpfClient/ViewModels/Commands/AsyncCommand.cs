using System;
using System.Threading.Tasks;

namespace WorkersWpfClient.ViewModels.Commands
{
    public class AsyncCommand : AbstractCommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        protected override bool CanExecute(object p)
        {
            return _canExecute?.Invoke(p) ?? true;
        }

        protected override void Execute(object p)
        {
            Task.Run(() => _execute(p));
        }
    }
}
