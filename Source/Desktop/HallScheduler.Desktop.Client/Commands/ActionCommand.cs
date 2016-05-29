namespace HallScheduler.Desktop.Client.Commands
{
    using System;
    using System.Windows.Input;

    public class ActionCommand : ICommand
    {
        public ActionCommand(Action action)
        {
            this.Action = action;
        }

        private Action Action { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.Action?.Invoke();
        }
    }
}
