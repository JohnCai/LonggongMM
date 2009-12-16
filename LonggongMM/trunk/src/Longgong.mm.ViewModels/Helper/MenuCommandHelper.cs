using System.Collections.Generic;
using Mavis.MVVM;
namespace Longgong.mm.ViewModels
{
    public static class MenuCommandHelper
    {
        /// <summary>
        /// set the command for the menuItem by the key.
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="commandViewModels"></param>
        public static void BindCommand(List<VPFMenuItem> menus, IList<CommandViewModel> commandViewModels)
        {
            if (menus == null || menus.Count == 0)
                return;

            foreach (var menuItem in menus)
            {
                var menu = menuItem;
                CommandViewModel commandViewModel = null;
                foreach (var viewModel in commandViewModels)
                {
                    if (viewModel.Key == menu.Key)
                        commandViewModel = viewModel;
                }
                if (commandViewModel != null)
                {
                    menu.Command = commandViewModel.Command;
                    if (string.IsNullOrEmpty(menu.Text))
                    {
                        menu.Text = commandViewModel.DisplayName;
                    }
                }
                else
                {
                    menu.Command = new DelegateCommand();
                }

                BindCommand(menu.Children, commandViewModels);
            }
            
        }
    }
}