using System;

namespace Mavis.MVVM
{
    public class PopupViewModelBase: ViewModelBase
    {
        public PopupViewModelBase()
        {
            ClosePopupCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteClosePopupCommand(),
                                        ExecuteDelegate = x => ExecuteClosePopupCommand()
                                    };
        }

        protected virtual void ExecuteClosePopupCommand()
        {
            RaiseCloseRequest(true);
        }

        protected virtual bool CanExecuteClosePopupCommand()
        {
            return true;
        }

        public DelegateCommand ClosePopupCommand { get; private set; }

        public event EventHandler<CloseRequestEventArgs> CloseRequest;

        public virtual void RaiseCloseRequest()
        {
            RaiseCloseRequest(null);
        }

        public virtual void RaiseCloseRequest(bool? dialogResult)
        {
            var handler = CloseRequest;

            if (handler != null)
            {
                try
                {
                    handler(this, new CloseRequestEventArgs(dialogResult));
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }
        }
    }
}