using System;

namespace WorkersWpfClient.ViewModels.Commands
{
    public class ActionCommand : AbstractCommand
    {
        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        public ActionCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            _execute(p);
        }
    }
}
