using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Mavis.Core
{
    public class NotifierObject: INotifyPropertyChanged
    {
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new Exception("Invalid property name: " + propertyName);
            }
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}