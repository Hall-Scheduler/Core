namespace HallScheduler.Desktop.Client.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class FuncCommand : ICommand
    {
        public FuncCommand(Func<int, bool> funky)
        {
            this.Funky = funky;
        }

        public FuncCommand(Func<int, Task<bool>> funkyAsync)
        {
            this.FunkyAsync = funkyAsync;
        }

        public Func<int, bool> Funky { get; set; }

        public Func<int, Task<bool>> FunkyAsync { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var argument = int.Parse(parameter.ToString());

            this.Funky?.Invoke(argument);
            this.FunkyAsync?.Invoke(argument);
        }
    }
}
