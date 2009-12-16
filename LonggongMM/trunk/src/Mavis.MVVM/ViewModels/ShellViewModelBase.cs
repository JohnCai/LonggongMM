using System;

namespace Mavis.MVVM
{
    public class ShellViewModelBase: ViewModelBase
    {
        private readonly DelegateCommand _closeWorkSpaceCommand;
        public string DisplayName { get; set; }

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public DelegateCommand CloseWorkSpaceCommand
        {
            get
            {
                return _closeWorkSpaceCommand;
            }
        }

        public ShellViewModelBase()
        {
            IsCloseable = true;
            _closeWorkSpaceCommand = new DelegateCommand
            {
                CanExecuteDelegate = x => true,
                ExecuteDelegate = x => ExecuteCloseWorkSpaceCommand()
            };
        }

        private void ExecuteCloseWorkSpaceCommand()
        {
            EventHandler<EventArgs> handlers = CloseWorkSpace;

            // Invoke the event handlers
            if (handlers != null)
            {
                try
                {
                    handlers(this, EventArgs.Empty);
                }
                catch
                {
                    //Logger.Log(LogType.Error, "Error firing CloseWorkSpace event");
                }
            }
        }

        #region Event(s)
        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler<EventArgs> CloseWorkSpace;
        #endregion 
    }
}