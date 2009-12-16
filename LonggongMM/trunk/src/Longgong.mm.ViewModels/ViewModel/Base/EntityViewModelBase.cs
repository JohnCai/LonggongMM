using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Mavis.Core;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    /// <summary>
    /// Base ViewModel class for Entity
    /// </summary>
    /// <typeparam name="TEntity">type of the entity. eg:Location</typeparam>
    /// <typeparam name="TRepository">type of the IRepository, eg: ILocationRepository</typeparam>
    /// <typeparam name="TBrowser">type of the Browser ViewModel, eg: LocationBrowserViewModel</typeparam>
    /// <typeparam name="TDetail">type of the Detail ViewModel, eg: LocationDetailViewModel</typeparam>
    public class EntityViewModelBase<TEntity, TRepository, TBrowser, TDetail> : ShellViewModelBase
        where TEntity : Entity
        where TRepository : INHibernateRepository<TEntity>
        where TBrowser : EntityBrowserViewModel<TEntity, TRepository>
        where TDetail : EntityDetailViewModel<TEntity>
    {
        private static readonly PropertyChangedEventArgs _currentEntityChangeArgs =
            ObservableHelper.CreateArgs<EntityViewModelBase<TEntity, TRepository, TBrowser, TDetail>>(
                x => x.CurrentEntity);

        private static readonly PropertyChangedEventArgs _currentViewModeChangeArgs =
            ObservableHelper.CreateArgs<EntityViewModelBase<TEntity, TRepository, TBrowser, TDetail>>(
                x => x.CurrentViewMode);

        private static readonly PropertyChangedEventArgs _matchedEntitiesChangeArgs =
            ObservableHelper.CreateArgs<EntityViewModelBase<TEntity, TRepository, TBrowser, TDetail>>(
                x => x.MatchedEntities);

        private static readonly PropertyChangedEventArgs _workspacesChangeArgs =
            ObservableHelper.CreateArgs<EntityViewModelBase<TEntity, TRepository, TBrowser, TDetail>>(x => x.Workspaces);

        private string _currentDisplayLabel;

        private TEntity _currentEntity;
        private ViewMode _currentViewMode;
        private List<TEntity> _matchedEntities;
        private ICollectionView _matchedEntitiesCollectionView;
        protected TEntity _originalCurrentEntity;
        protected ViewMode _originalCurrentViewMode;

        private ObservableCollection<ViewModelBase> _workspaces;

        public EntityViewModelBase(TRepository entityRepository, IMessageBoxService messageBoxService,
                                   IContainerFacade containerFacade)
        {
            EntityRepository = entityRepository;
            MessageBoxService = messageBoxService;
            ContainerFacade = containerFacade;

            //EntityBrowserViewModel = (TBrowser) Activator.CreateInstance(typeof (TBrowser), entityRepository);
            EntityBrowserViewModel = ContainerFacade.Resolve<TBrowser>();
            //EntityDetailViewModel = (TDetail) Activator.CreateInstance(typeof (TDetail));
            EntityDetailViewModel = ContainerFacade.Resolve<TDetail>();

            Workspaces = new ObservableCollection<ViewModelBase> {EntityBrowserViewModel, EntityDetailViewModel};

            CurrentViewMode = ViewMode.BrowseMode;

            AddEntityCommand = new DelegateCommand
                                   {
                                       CanExecuteDelegate = x => CanExecuteAddEntityCommand,
                                       ExecuteDelegate = x => ExecuteAddEntityCommand()
                                   };
            EditEntityCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteEditEntityCommand,
                                        ExecuteDelegate = x => ExecuteEditEntityCommand()
                                    };
            DeleteEntityCommand = new DelegateCommand
                                      {
                                          CanExecuteDelegate = x => CanExecuteDeleteEntityCommand,
                                          ExecuteDelegate = x => ExecuteDeleteEntityCommand()
                                      };
            SaveEntityCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteSaveEntityCommand,
                                        ExecuteDelegate = x => ExecuteSaveEntityCommand()
                                    };

            ReloadEntityCommand = new DelegateCommand
                                      {
                                          CanExecuteDelegate = x => CanExecuteReloadEntityCommand,
                                          ExecuteDelegate = x => ExecuteReloadEntityCommand()
                                      };

            SwitchModeCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteSwitchModeCommand,
                                        ExecuteDelegate = x => ExecuteSwitchModeCommand()
                                    };
            MoveToFirstCommand = new DelegateCommand
                                     {
                                         CanExecuteDelegate = x => CanExecuteMoveItemCommand,
                                         ExecuteDelegate = x => ExecuteMoveToFirstCommand()
                                     };
            MoveToLastCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteMoveItemCommand,
                                        ExecuteDelegate = x => ExecuteMoveToLastCommand()
                                    };
            MoveToNextCommand = new DelegateCommand
                                    {
                                        CanExecuteDelegate = x => CanExecuteMoveItemCommand,
                                        ExecuteDelegate = x => ExecuteMoveToNextCommand()
                                    };
            MoveToPreviousCommand = new DelegateCommand
                                        {
                                            CanExecuteDelegate = x => CanExecuteMoveItemCommand,
                                            ExecuteDelegate = x => ExecuteMoveToPreviousCommand()
                                        };
        }

        public string CurrentDisplayLabel
        {
            get { return _currentDisplayLabel; }
            protected set
            {
                _currentDisplayLabel = value;
                NotifyPropertyChanged("CurrentDisplayLabel");
            }
        }


        protected bool CanExecuteMoveItemCommand
        {
            get { return IsViewMode; }
        }

        protected bool CanExecuteReloadEntityCommand
        {
            get { return !IsViewMode; }
        }

        protected bool CanExecuteSaveEntityCommand
        {
            get { return !IsViewMode; }
        }

        public DelegateCommand SaveEntityCommand { get; private set; }
        public DelegateCommand ReloadEntityCommand { get; private set; }

        private bool IsViewMode
        {
            get
            {
                return
                    (CurrentViewMode == ViewMode.BrowseMode || CurrentViewMode == ViewMode.ViewOnlyMode);
            }
        }

        protected bool CanExecuteSwitchModeCommand
        {
            get { return IsViewMode; }
        }

        protected bool CanExecuteDeleteEntityCommand
        {
            get { return IsViewMode; }
        }

        protected bool CanExecuteEditEntityCommand
        {
            get { return IsViewMode; }
        }


        protected bool CanExecuteAddEntityCommand
        {
            get { return IsViewMode; }
        }

        public TEntity CurrentEntity
        {
            get
            {
                if (CurrentViewMode == ViewMode.BrowseMode)
                    return EntityBrowserViewModel.CurrentEntity;
                return EntityDetailViewModel.CurrentEntity;
            }
            set
            {
                if (CurrentViewMode == ViewMode.BrowseMode)
                    EntityBrowserViewModel.CurrentEntity = value;
                else
                    EntityDetailViewModel.CurrentEntity = value;
                NotifyPropertyChanged(_currentEntityChangeArgs);
            }
        }

        public List<TEntity> MatchedEntities
        {
            get { return _matchedEntities; }
            set
            {
                if (_matchedEntities == null)
                    return;

                _matchedEntities = value;
                NotifyPropertyChanged(_matchedEntitiesChangeArgs);
                if (_matchedEntitiesCollectionView != null)
                {
                    _matchedEntitiesCollectionView.CurrentChanged -= MatchedEntitiesCollectionViewCurrentChanged;
                }
                _matchedEntitiesCollectionView = CollectionViewSource.GetDefaultView(value);
                _matchedEntitiesCollectionView.CurrentChanged += MatchedEntitiesCollectionViewCurrentChanged;
                _matchedEntitiesCollectionView.MoveCurrentTo(-1);
            }
        }

        public ViewMode CurrentViewMode
        {
            get { return _currentViewMode; }
            set
            {
                _currentViewMode = value;
                SwitchTo(value);
                EntityDetailViewModel.CurrentViewMode = value;
                OnCurrentViewModeChanged(value);
                NotifyPropertyChanged(_currentViewModeChangeArgs);
            }
        }

        protected TRepository EntityRepository { get; private set; }
        protected IMessageBoxService MessageBoxService { get; private set; }
        protected IContainerFacade ContainerFacade { get; private set; }

        /// <summary>
        /// the view contains two tabs: browser tab and viewdetail tab, 
        /// so the Workspaces contains EntityBrowserViewModel and EntityDetailViewModel
        /// </summary>
        public ObservableCollection<ViewModelBase> Workspaces
        {
            get { return _workspaces; }
            set
            {
                if (_workspaces == null)
                {
                    _workspaces = value;
                    NotifyPropertyChanged(_workspacesChangeArgs);
                }
            }
        }

        /// <summary>
        /// this is the ViewModel for the first tab, entity list browser tab
        /// </summary>
        public TBrowser EntityBrowserViewModel { get; private set; }

        /// <summary>
        /// this is the ViewModel for the second tab, view single entity tab
        /// </summary>
        public TDetail EntityDetailViewModel { get; private set; }


        public DelegateCommand AddEntityCommand { get; private set; }

        public DelegateCommand EditEntityCommand { get; private set; }

        public DelegateCommand DeleteEntityCommand { get; private set; }

        public DelegateCommand SwitchModeCommand { get; private set; }

        public DelegateCommand MoveToNextCommand { get; private set; }
        public DelegateCommand MoveToFirstCommand { get; private set; }
        public DelegateCommand MoveToPreviousCommand { get; private set; }
        public DelegateCommand MoveToLastCommand { get; private set; }

        protected virtual void OnCurrentViewModeChanged(ViewMode viewMode)
        {
        }


        /// <summary>
        /// update an existing entity
        /// </summary>
        /// <returns></returns>
        protected virtual bool UpdateEntity(ref string errorMessage)
        {
            if (!ValidateCurrentEntity(ref errorMessage))
                return false;

            using (EntityRepository.DbContext.OpenSession())
            using (EntityRepository.DbContext.BeginTransaction())
            {
                try
                {
                    EntityRepository.Update(CurrentEntity);
                    EntityRepository.DbContext.CommitTransaction();
                }
                catch (Exception)
                {
                    errorMessage = "更新时遇到问题，保存失败！";
                    EntityRepository.DbContext.RollbackTransaction();
                    return false;
                }
            }

            EntityBrowserViewModel.CurrentEntity = CurrentEntity;
            return true;
        }

        /// <summary>
        /// save a new(transient) entity
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaveEntity(ref string errorMessage)
        {
            if (!ValidateCurrentEntity(ref errorMessage))
                return false;

            using (EntityRepository.DbContext.OpenSession())
            using (EntityRepository.DbContext.BeginTransaction())
            {
                try
                {
                    EntityRepository.Save(CurrentEntity);
                    EntityRepository.DbContext.CommitTransaction();
                }
                catch (Exception)
                {
                    errorMessage = "保存时遇到问题，保存失败！";
                    EntityRepository.DbContext.RollbackTransaction();
                    return false;
                }
            }

            EntityBrowserViewModel.MatchedEntities.Add(CurrentEntity);
            EntityBrowserViewModel.CurrentEntity = CurrentEntity;
            return true;
        }

        private bool ValidateCurrentEntity(ref string errorMessage)
        {
            if (!CurrentEntity.IsValid)
            {
                errorMessage = "当前资料不可保存，请确认后再尝试！";
                return false;
            }
            return true;
        }


        private void MatchedEntitiesCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            if (_matchedEntitiesCollectionView.CurrentItem != null)
            {
                CurrentEntity = _matchedEntitiesCollectionView.CurrentItem as TEntity;
            }
        }

        /// <summary>
        /// Switch between the Browser mode and ViewDetail mode,that is, in UI,switch the actived Tab.
        /// </summary>
        protected virtual void ExecuteSwitchModeCommand()
        {
            if (CurrentEntity == null)
                ExecuteMoveToNextCommand();
            if (CurrentEntity == null)
                return;
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView == null)
                return;

            switch (CurrentViewMode)
            {
                case ViewMode.BrowseMode:
                    Check.Require(EntityBrowserViewModel.CurrentEntity != null);
                    EntityDetailViewModel.CurrentEntity = EntityBrowserViewModel.CurrentEntity;
                    CurrentViewMode = ViewMode.ViewOnlyMode;

                    break;
                case ViewMode.ViewOnlyMode:
                    Check.Require(EntityDetailViewModel.CurrentEntity != null);
                    Check.Require(EntityBrowserViewModel.MatchedEntities.Contains(EntityDetailViewModel.CurrentEntity));
                    EntityBrowserViewModel.CurrentEntity = EntityDetailViewModel.CurrentEntity;
                    CurrentViewMode = ViewMode.BrowseMode;
                    break;

                default:
                    break;
            }
        }

        protected virtual void ExecuteMoveToFirstCommand()
        {
            EntityBrowserViewModel.MoveToFirst();
            SynchCurrentFromBrowserView();
        }

        private void SynchCurrentFromBrowserView()
        {
            if (CurrentViewMode == ViewMode.ViewOnlyMode)
                EntityDetailViewModel.CurrentEntity = EntityBrowserViewModel.CurrentEntity;
        }

        protected virtual void ExecuteMoveToLastCommand()
        {
            EntityBrowserViewModel.MoveToLast();
            SynchCurrentFromBrowserView();
        }

        protected virtual void ExecuteMoveToNextCommand()
        {
            EntityBrowserViewModel.MoveToNext();
            SynchCurrentFromBrowserView();
        }

        protected virtual void ExecuteMoveToPreviousCommand()
        {
            EntityBrowserViewModel.MoveToPrevious();
            SynchCurrentFromBrowserView();
        }

        protected virtual void ExecuteDeleteEntityCommand()
        {
            if (CurrentEntity == null || !ConfirmDelete())
                return;
            if (DeleteEntity())
                RemoveCurrentEntity();
        }

        private bool DeleteEntity()
        {
            using (EntityRepository.DbContext.OpenSession())
            using (EntityRepository.DbContext.BeginTransaction())
            {
                try
                {
                    EntityRepository.Delete(CurrentEntity);
                    EntityRepository.DbContext.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    EntityRepository.DbContext.RollbackTransaction();
                    MessageBoxService.ShowError("删除资料失败，请检查！");
                    return false;
                    //throw;
                }
            }
        }

        private void RemoveCurrentEntity()
        {
            EntityBrowserViewModel.MatchedEntities.Remove(CurrentEntity);
            ExecuteMoveToNextCommand();
        }

        private bool ConfirmDelete()
        {
            return MessageBoxService.ShowOkCancel("确定删除本笔资料吗？", CustomDialogIcons.Question) == CustomDialogResults.OK;
        }

        protected virtual void ExecuteEditEntityCommand()
        {
            if (CurrentEntity == null)
                ExecuteMoveToNextCommand();
            if (CurrentEntity == null)
                return;

            CurrentEntity = EntityRepository.Get(CurrentEntity.ID);
            _originalCurrentViewMode = CurrentViewMode;
            if (CurrentViewMode == ViewMode.BrowseMode)
            {
                EntityDetailViewModel.CurrentEntity = CurrentEntity;
            }

            CurrentViewMode = ViewMode.EditMode;

            CurrentEntity.BeginEdit();
        }

        protected virtual void ExecuteAddEntityCommand()
        {
            _originalCurrentViewMode = CurrentViewMode;
            _originalCurrentEntity = CurrentEntity;

            CurrentViewMode = ViewMode.AddMode;

            CurrentEntity = Activator.CreateInstance<TEntity>();
        }

        protected virtual void ExecuteReloadEntityCommand()
        {
            switch (CurrentViewMode)
            {
                case ViewMode.EditMode:
                    CurrentEntity.CancelEdit();
                    CurrentViewMode = _originalCurrentViewMode;
                    break;
                case ViewMode.AddMode:
                    CurrentEntity = null;
                    CurrentViewMode = _originalCurrentViewMode;
                    CurrentEntity = _originalCurrentEntity;
                    break;
                default:
                    CurrentEntity.CancelEdit();
                    break;
            }
        }

        protected virtual void ExecuteSaveEntityCommand()
        {
            string errorMessage = string.Empty;
            if (!CurrentEntity.IsValid)
            {
                return;
            }
            switch (CurrentViewMode)
            {
                case ViewMode.AddMode:
                    if (SaveEntity(ref errorMessage))
                    {
                        CurrentViewMode = ViewMode.ViewOnlyMode;
                    }
                    break;

                case ViewMode.EditMode:
                    if (UpdateEntity(ref errorMessage))
                        CurrentViewMode = ViewMode.ViewOnlyMode;
                    break;
            }
        }

        private void SwitchTo(ViewMode viewMode)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);

            collectionView.MoveCurrentTo(viewMode == ViewMode.BrowseMode
                                             ? (ViewModelBase) EntityBrowserViewModel
                                             : EntityDetailViewModel);
        }
    }
}