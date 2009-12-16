using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Mavis.Core;
using Mavis.MVVM;

namespace Mavis.MVVM
{
    public class ViewModelBase : NotifierObject, IDisposable
    {
        private bool _isCloseable;

        #region public properties

        private static readonly PropertyChangedEventArgs _isCloseableChangedArgs =
            ObservableHelper.CreateArgs<ViewModelBase>(x => x.IsCloseable);

        

        public bool IsCloseable
        {
            get { return _isCloseable; }
            set
            {
                _isCloseable = value;
                NotifyPropertyChanged(_isCloseableChangedArgs);
            }
        }

        #endregion

        



        #region Implementation of IDisposable

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            
        }

        #endregion
    }
}
