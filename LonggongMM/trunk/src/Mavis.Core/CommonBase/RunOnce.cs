using System;

namespace Mavis.Core
{
    public class RunOnce: Singleton<RunOnce>
    {
        private bool _haveRun;

        public void Run(Action action)
        {
            lock (_syncLock)
            {
                if (! _haveRun)
                {
                    action();
                    _haveRun = true;
                }
            }
        }
    }
}