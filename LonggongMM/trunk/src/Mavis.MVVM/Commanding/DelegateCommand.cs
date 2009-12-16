using System;
using System.Windows.Input;

namespace Mavis.MVVM
{
    public class DelegateCommand: ICommand
    {
        #region data

        public Action<object> ExecuteDelegate { get; set; }
        public Predicate<object> CanExecuteDelegate { get; set; }

        #endregion

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }
            return true;
        }

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
    }

    //public class DelegateCommand<T>: ICommand
    //{
    //    #region data


    //    public Action<T> ExecuteMethod{ get; set;}
    //    public Func<T, bool> CanExecuteMethod{ get; set;}
 

    //    #endregion

    //    #region Implementation of ICommand

    //    public void Execute(object parameter)
    //    {
    //        if (ExecuteMethod == null)
    //        {
    //            return;
    //        }
    //        ExecuteMethod((T) parameter);
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        if (CanExecuteMethod != null)
    //        {
    //            return CanExecuteMethod((T) parameter);
    //        }
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    #endregion
    //}
}