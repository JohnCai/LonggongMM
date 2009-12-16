using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Mavis.Core;
using Mavis.MVVM;
using System.Collections.ObjectModel;

namespace Longgong.mm.ViewModels
{
    /// <summary>
    /// base ViewModel for Entity browser, the default UI for this ViewModel is composed of a readonly Grid,
    /// a Search button, a Add button, a Edit button, a delete button and a switch button(switch to View single entity detail.)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public class EntityBrowserViewModel<TEntity, TRepository> : ViewModelBase
        where TEntity : Entity
        where TRepository : INHibernateRepository<TEntity>
    {
        private static readonly PropertyChangedEventArgs _currentEntityChangeArgs =
            ObservableHelper.CreateArgs<EntityBrowserViewModel<TEntity, TRepository>>(x => x.CurrentEntity);

        private static readonly PropertyChangedEventArgs _matchedEntitiesChangeArgs =
            ObservableHelper.CreateArgs<EntityBrowserViewModel<TEntity, TRepository>>(x => x.MatchedEntities);

        private TEntity _currentEntity;
        private ObservableCollection<TEntity> _matchedEntities = new ObservableCollection<TEntity>();
        private ICollectionView _matchedEntitiesCollectionView;

        public EntityBrowserViewModel(TRepository entityReposity)
        {
            EntityRepository = entityReposity;

            MatchedEntities = new ObservableCollection<TEntity>();

            SearchCriterias = new List<SearchCriteria<TEntity>>();

            SearchEntityCommand = new DelegateCommand
                                      {
                                          CanExecuteDelegate = x => CanExecuteSearchEntityCommand,
                                          ExecuteDelegate = x => ExecuteSearchEntityCommand()
                                      };
        }

        protected bool CanExecuteSearchEntityCommand
        {
            get { return true; }
        }

        public TEntity CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                _currentEntity = value;
                _matchedEntitiesCollectionView.MoveCurrentTo(value);
                NotifyPropertyChanged(_currentEntityChangeArgs);
            }
        }

        public ObservableCollection<TEntity> MatchedEntities
        {
            get { return _matchedEntities; }
            set
            {
                _matchedEntities = value;
                NotifyPropertyChanged(_matchedEntitiesChangeArgs);
                if (_matchedEntitiesCollectionView != null)
                {
                    _matchedEntitiesCollectionView.CurrentChanged -= MatchedEntitiesCollectionViewCurrentChanged;
                }
                _matchedEntitiesCollectionView = CollectionViewSource.GetDefaultView(value);
                _matchedEntitiesCollectionView.CurrentChanged += MatchedEntitiesCollectionViewCurrentChanged;
                _matchedEntitiesCollectionView.MoveCurrentTo(value.Count == 0 ? -1: 0);
            }
        }

        protected virtual List<SearchCriteria<TEntity>> SearchCriterias
        {
            get; private set;
        }

        public TRepository EntityRepository { get; protected set; }

        public DelegateCommand SearchEntityCommand { get; private set; }

        /// <summary>
        /// default Search for all entities matched by SearchCriterias
        /// </summary>
        protected virtual void ExecuteSearchEntityCommand()
        {
            //todo
            using (EntityRepository.DbContext.OpenSession())
            using (EntityRepository.DbContext.BeginTransaction())
            {
                MatchedEntities = new ObservableCollection<TEntity>(FindAllBySearchCriterias());
            }
            
        }

        protected virtual IList<TEntity> FindAllBySearchCriterias()
        {
            return EntityRepository.FindByCriterias(SearchCriterias);
        }

        private void MatchedEntitiesCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            CurrentEntity = _matchedEntitiesCollectionView.CurrentItem as TEntity;
        }

        public virtual void MoveToFirst()
        {
            _matchedEntitiesCollectionView.MoveCurrentToFirst();
        }

        public virtual void MoveToLast()
        {
            _matchedEntitiesCollectionView.MoveCurrentToLast();
        }

        public virtual void MoveToNext()
        {
            _matchedEntitiesCollectionView.MoveCurrentToNext();
            if (_matchedEntitiesCollectionView.IsCurrentAfterLast)
                _matchedEntitiesCollectionView.MoveCurrentToFirst();
        }

        public virtual void MoveToPrevious()
        {
            _matchedEntitiesCollectionView.MoveCurrentToPrevious();
            if (_matchedEntitiesCollectionView.IsCurrentBeforeFirst)
                _matchedEntitiesCollectionView.MoveCurrentToLast();
        }
    }
}