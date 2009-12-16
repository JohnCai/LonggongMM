using System;
using System.Windows;
using Longgong.mm.app.Views;
using Mavis.Core;

namespace Longgong.mm.app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoadResources();
            var shellWindow = new Window()
                                 {
                                     //Content = mainWindowViewModel,
                                     Content = new MainWindow {DataContext = new LonggongMMShellBuilder().BuildShell()},
                                     WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                     
                                     Title = "中国龙工生产管理系统",
                                     MinHeight =768,
                                     Height =768,
                                     MinWidth =1024,
                                     Width =1024
                                 };
            shellWindow.ShowDialog();
        }

        /// <summary>
        /// Including the ViewModelTemplat in design time will cause some designtime problem,
        /// so load it in run time.
        /// </summary>
        private static void LoadResources()
        {
            var viewModelResource =
                LoadComponent(new Uri("Resources/ViewModelTemplate.xaml", UriKind.Relative)) as ResourceDictionary;

            Check.Assert(viewModelResource != null);

            Current.Resources.MergedDictionaries.Add(viewModelResource);
        }
    }
}
