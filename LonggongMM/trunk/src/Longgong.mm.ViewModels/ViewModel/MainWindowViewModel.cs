using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using Mavis.MVVM;
using System.Windows.Data;

namespace Longgong.mm.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Data

        private readonly List<CommandViewModel> _allCommmands;
        private readonly DelegateCommand _exitApplicationCommand;
        private readonly DelegateCommand _loginCommand;

        private ObservableCollection<CommandViewModel> _homePageCommmands;
        private List<VPFMenuItem> _menus;
        private ObservableCollection<CommandViewModel> _menusCommmands;
        private ObservableCollection<ShellViewModelBase> _workspaces;
        private IContainerFacade _containerFacade;

        #endregion

        #region constructor

        public MainWindowViewModel(IContainerFacade containerFacade)
        {
            _containerFacade = containerFacade;

            Workspaces = new ObservableCollection<ShellViewModelBase>();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
            var homePageViewModel = new HomePageViewModel {IsCloseable = false};
            Workspaces.Add(homePageViewModel);

            _allCommmands = CreateAllCommands();
        }

        #endregion

        #region public properties

        public ObservableCollection<CommandViewModel> MenusCommmands
        {
            get
            {
                if (_menusCommmands == null)
                {
                    _menusCommmands =
                        new ObservableCollection<CommandViewModel>(_allCommmands.FindAll(c => c.ShowInMenus));
                }
                return _menusCommmands;
            }
        }

        public ObservableCollection<ShellViewModelBase> Workspaces
        {
            get { return _workspaces; }
            set
            {
                if (_workspaces == null)
                {
                    _workspaces = value;
                    NotifyPropertyChanged("Workspaces");
                }
            }
        }

        public List<VPFMenuItem> Menus
        {
            get
            {
                if (_menus == null)
                {
                    _menus = CreateMenus();
                }
                return _menus;
            }
        }

        public DelegateCommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public DelegateCommand ExitApplicationCommand
        {
            get { return _exitApplicationCommand; }
        }

        #endregion

        #region Private Methods

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (ShellViewModelBase workspace in e.NewItems)
                {
                    ShellViewModelBase @base = workspace;
                    workspace.CloseWorkSpace +=
                         new EventHandler<EventArgs>(OnCloseWorkSpace).
                             MakeWeak(eh => @base.CloseWorkSpace -= eh);
                }
            }
        }

        private void OnCloseWorkSpace(object sender, EventArgs e)
        {
            var workspace = (ShellViewModelBase) sender;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        private List<VPFMenuItem> CreateMenus()
        {
            var menus = new List<VPFMenuItem>();

            var miFile = new VPFMenuItem("File") {Text = "文件"};
            miFile.Children.Add(new VPFMenuItem("login"));
            miFile.Children.Add(new VPFMenuItem("exit") {IconUrl = @"..\Images\Exit.png"});

            menus.Add(miFile);

            var miMaintain = new VPFMenuItem("Maintain"){Text = "基本资料"};
            miMaintain.Children.Add(new VPFMenuItem("location"));
            miMaintain.Children.Add(new VPFMenuItem("workingProcedure"));
            miMaintain.Children.Add(new VPFMenuItem("product"));
            miMaintain.Children.Add(new VPFMenuItem("recipe"));
            menus.Add(miMaintain);

            var miProduction = new VPFMenuItem("Production") { Text = "生产管理" };
            miProduction.Children.Add(new VPFMenuItem("mainProductionPlan"));
            menus.Add(miProduction);

            MenuCommandHelper.BindCommand(menus, MenusCommmands);
            return menus;
        }

        private List<CommandViewModel> CreateAllCommands()
        {
            var commands = new List<CommandViewModel>
                               {
                                   new CommandViewModel("login",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate = x => true,
                                                                ExecuteDelegate = x => ExecuteLoginCommand()
                                                            }) {DisplayName = "登录"},
                                   new CommandViewModel("exit",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate = x => true,
                                                                ExecuteDelegate = x => ExecuteExitApplicationCommand()
                                                            }) {DisplayName = "退出"},
                                   new CommandViewModel("location",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate =  x => true,
                                                                ExecuteDelegate = x => ExecuteOpenLocationCommand()
                                                            }) {DisplayName = "仓库资料"},
                                   new CommandViewModel("workingProcedure",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate =  x => true,
                                                                ExecuteDelegate = x => ExecuteOpenWorkingProcedureCommand()
                                                            }) {DisplayName = "工序种类"},
                                   new CommandViewModel("product",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate =  x => true,
                                                                ExecuteDelegate = x => ExecuteOpenProductCommand()
                                                            }) {DisplayName = "产品资料"},
                                   new CommandViewModel("recipe",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate =  x => true,
                                                                ExecuteDelegate = x => ExecuteOpenRecipeCommand()
                                                            }) {DisplayName = "生产流程"},
                                   new CommandViewModel("mainProductionPlan",
                                                        new DelegateCommand
                                                            {
                                                                CanExecuteDelegate =  x => true,
                                                                ExecuteDelegate = x => ExecuteOpenMainProductionPlanCommand()
                                                            }) {DisplayName = "生产下线单"}
                               };


            return commands;
        }

        private void ExecuteOpenLocationCommand()
        {
            var locationViewModel = CreateWorkSpaceItemIfNull<LocationViewModel>();
            SetActiveWorkspace(locationViewModel);
        }

        private void ExecuteOpenProductCommand()
        {
            var locationViewModel = CreateWorkSpaceItemIfNull<ProductViewModel>();
            SetActiveWorkspace(locationViewModel);
        }

        private void ExecuteOpenRecipeCommand()
        {
            var recipeViewModel = CreateWorkSpaceItemIfNull<RecipeViewModel>();
            SetActiveWorkspace(recipeViewModel);
        }

        private void ExecuteOpenMainProductionPlanCommand()
        {
            SetActiveWorkspace(CreateWorkSpaceItemIfNull<MainProductionPlanViewModel>());
        }


        private void ExecuteOpenWorkingProcedureCommand()
        {
            var workingProcedureViewModel = CreateWorkSpaceItemIfNull<WorkingProcedureViewModel>();
            SetActiveWorkspace(workingProcedureViewModel);
        }

        /// <summary>
        /// Resolve the ShellViewModel if it does not exist in WorkSpace, and add it into WorkSpace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CreateWorkSpaceItemIfNull<T>() where T : ShellViewModelBase
        {
            T result = default(T);
            foreach (var shellViewModelBase in _workspaces)
            {
                if (shellViewModelBase.GetType().Equals(typeof(T)))
                {
                    result = shellViewModelBase as T;
                }
            }
            if (result == default(T))
            {
                result = _containerFacade.Resolve<T>();
                Workspaces.Add(result);
            }
            
            return result;
        }

        /// <summary>
        /// Sets a ShellViewModel to be active, which for the View equates to selected Tab.
        /// </summary>
        /// <param name="workspace"></param>
        private void SetActiveWorkspace(ViewModelBase workspace)
        {
            var collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }


        private void ExecuteLoginCommand()
        {
            //todo
        }

        private void ExecuteExitApplicationCommand()
        {
            //todo:confirm Information

            Application.Current.Shutdown(0);
        }

        #endregion
    }
}