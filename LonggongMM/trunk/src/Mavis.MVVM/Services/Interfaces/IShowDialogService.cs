using System;

namespace Mavis.MVVM
{
    public interface IShowDialogService
    {
        /// <summary>
        /// Registers a window type through a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="winType"></param>
        void Register(string key, Type winType);

        /// <summary>
        /// unregisters a window type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Unregister(string key);

        /// <summary>
        /// displays a modeless dialog
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>
        /// <param name="setOwner"></param>
        /// <param name="completedFunc"></param>
        /// <returns></returns>
        bool Show(string key, object state, bool setOwner,
                  EventHandler<UICompletedEventArgs> completedFunc);

        /// <summary>
        /// displays a modal dialog
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool? ShowModel(string key, object state);

    }
}