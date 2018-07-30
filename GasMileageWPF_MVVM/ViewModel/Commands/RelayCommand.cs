using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GasMileageWPF_MVVM.ViewModel.Commands
{
    class RelayCommand : ICommand
    {
        #region Fields

        /// <summary>
        /// The can execute function for the command
        /// </summary>
        readonly Predicate<object> _canExecute;

        /// <summary>
        /// The execute function for the command
        /// </summary>
        readonly Action<object> _execute;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
            Console.WriteLine("Instantiating single param RelayCommand() ...");
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            Console.WriteLine("Instantiating double param RelayCommand() ...");

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            Console.WriteLine("Executing CanExecute ...");
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            Console.WriteLine("Executing Execute ...");
            _execute(parameter);
        }

        #endregion Methods
    }
}
