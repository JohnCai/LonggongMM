using System;

namespace Mavis.MVVM
{
    public class CloseRequestEventArgs: EventArgs
    {
        public bool? Result { get; private set; }

        public CloseRequestEventArgs(bool? result)
        {
            Result = result;
        }
    }
}