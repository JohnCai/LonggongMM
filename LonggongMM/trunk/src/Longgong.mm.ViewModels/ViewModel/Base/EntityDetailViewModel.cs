using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Mavis.Core;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public abstract class EntityDetailViewModel<TEntity> : ViewModelBase
        where TEntity: Entity
    {
        private static readonly PropertyChangedEventArgs _currentEntityChangeArgs =
            ObservableHelper.CreateArgs<EntityDetailViewModel<TEntity>>(x => x.CurrentEntity);

        private static readonly PropertyChangedEventArgs _currentViewModeChangeArgs =
            ObservableHelper.CreateArgs<EntityDetailViewModel<TEntity>>(x => x.CurrentViewMode);

        private TEntity _currentEntity;
        protected ViewMode _currentViewMode;

        public virtual TEntity CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                _currentEntity = value;
                NotifyPropertyChanged(_currentEntityChangeArgs);
            }
        }

        public ViewMode CurrentViewMode
        {
            get { return _currentViewMode; }
            set
            {
                _currentViewMode = value;
                SetPropertiesEditableViaCurrentViewMode();
                NotifyPropertyChanged(_currentViewModeChangeArgs);
            }
        }

        protected bool IsAddOrEdit
        { 
            get { return _currentViewMode == ViewMode.AddMode || _currentViewMode == ViewMode.EditMode; }
        }

        protected abstract void SetPropertiesEditableViaCurrentViewMode();

    }
}