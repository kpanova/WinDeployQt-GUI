using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Infrastructure.Shared.Commands
{      
    public class RelayCommand : ICommand
    {
        #region Constants and Fields
        
        private readonly Predicate<object> _canExecute;
        
        private readonly Action<object> _execute;

        #endregion

        #region Constructors and Destructors
        
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {            
        }
        
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion

        #region Events
       
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

        #endregion

        #region ICommand members

        /// <summary>
        /// The can execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The can execute.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        #endregion      
    }
}