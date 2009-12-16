using System;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    /// <summary>
    /// Represents an actionable item displayed by a view.
    /// </summary>
    public class CommandViewModel: ViewModelBase
    {
        public CommandViewModel(string key, DelegateCommand command)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            Key = key;
            Command = command;
            ShowInMenus = true;
            ShowInHomepage = true;
        }

        public DelegateCommand Command { get; private set; }

        public string DisplayName { get; set; }

        public string Key { get; private set; }

        public bool ShowInMenus { get; set; }

        public bool ShowInHomepage { get; set; }
    }
}