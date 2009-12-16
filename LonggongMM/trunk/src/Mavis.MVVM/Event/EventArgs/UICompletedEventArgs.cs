using System;

namespace Mavis.MVVM
{
    public class UICompletedEventArgs : EventArgs
    {
        public object State { get; set; }
        public bool? Result { get; set; }
    }
}