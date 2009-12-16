using System;
using System.Collections.Generic;
using System.Windows;

namespace Mavis.MVVM
{
    public class VPFShowDialogService : IShowDialogService
    {
        #region Data

        private readonly Dictionary<string, Type> _registeredWindows;

        #endregion

        #region Constructor

        public VPFShowDialogService()
        {
            _registeredWindows = new Dictionary<string, Type>();
        }
        #endregion


        #region Implementation of IShowDialogService

        public void Register(string key, Type winType)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (winType == null)
            {
                throw new ArgumentNullException("winType");
            }
            if (! typeof(Window).IsAssignableFrom(winType))
            {
                throw new ArgumentException("winType must be of type Window");
            }

            lock (_registeredWindows)
            {
                _registeredWindows.Add(key, winType);
            }

        }

        public bool Unregister(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            lock (_registeredWindows)
            {
                return _registeredWindows.Remove(key);
            }
        }

        public bool Show(string key, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedFunc)
        {
            var win = CreateWindow(key, state, setOwner, completedFunc, false);
            if (win != null)
            {
                win.Show();
                return true;
            }
            return false;
        }
       

        public bool? ShowModel(string key, object state)
        {
            Window win = CreateWindow(key, state, true, null, true);
            if (win != null)
                return win.ShowDialog();

            return false;
        }

        #endregion

        public void Register(Dictionary<string, Type> Data)
        {
            foreach (var data in Data)
            {
                Register(data.Key, data.Value);
            }
        }

        private Window CreateWindow(string key, object state, bool setOwner, EventHandler<UICompletedEventArgs> completedFunc, bool isModal)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            Type winType;
            lock (_registeredWindows)
            {
                if (!_registeredWindows.TryGetValue(key, out winType))
                {
                    return null;
                }
            }

            var win = (Window) Activator.CreateInstance(winType);
            win.DataContext = state;
            if (setOwner)
            {
                win.Owner = Application.Current.MainWindow;
            }

            if (state != null)
            {
                var viewModel = state as PopupViewModelBase;
                if (viewModel != null)
                {
                    if (isModal)
                    {
                        viewModel.CloseRequest += ((s, e) =>
                                                       {
                                                           try
                                                           {
                                                               win.DialogResult = e.Result;
                                                           }
                                                           catch (InvalidOperationException)
                                                           {
                                                               win.Close();
                                                           }
                                                       });
                    }
                    else
                    {
                        viewModel.CloseRequest += ((s, e) => win.Close());
                    }
                    //viewModel.ActivateRequest += ((s, e) => win.Activate());
                }
            }

            if (completedFunc != null)
            {
                win.Closed +=
                    (s, e) =>
                    completedFunc(this,
                                  new UICompletedEventArgs()
                                      {State = state, Result = (isModal) ? win.DialogResult : null});
            }

            return win;
        }
    }
}