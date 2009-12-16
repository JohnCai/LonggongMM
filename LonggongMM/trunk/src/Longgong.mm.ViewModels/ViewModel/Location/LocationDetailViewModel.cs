using System;
using System.ComponentModel;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class LocationDetailViewModel : EntityDetailViewModel<Location>
    {
        private static readonly PropertyChangedEventArgs _isDescriptionEditableChangedArgs =
            ObservableHelper.CreateArgs<LocationDetailViewModel>(x => x.IsDescriptionEditable);

        private static readonly PropertyChangedEventArgs _isLocationIDEditableChangedArgs =
            ObservableHelper.CreateArgs<LocationDetailViewModel>(x => x.IsLocationIDEditable);

        private static readonly PropertyChangedEventArgs _isNameEditableChangedArgs =
            ObservableHelper.CreateArgs<LocationDetailViewModel>(x => x.IsNameEditable);

        private bool _isDescriptionEditable = true;
        private bool _isLocationIDEditable = true;
        private bool _isNameEditable = true;

        public bool IsLocationIDEditable
        {
            get { return _isLocationIDEditable; }
            set
            {
                _isLocationIDEditable = value;
                NotifyPropertyChanged(_isLocationIDEditableChangedArgs);
            }
        }

        public bool IsNameEditable
        {
            get { return _isNameEditable; }
            set
            {
                _isNameEditable = value;
                NotifyPropertyChanged(_isNameEditableChangedArgs);
            }
        }

        public bool IsDescriptionEditable
        {
            get { return _isDescriptionEditable; }
            set
            {
                _isDescriptionEditable = value;
                NotifyPropertyChanged(_isDescriptionEditableChangedArgs);
            }
        }

        #region Overrides of EntityDetailViewModel<Location,ILocationRepository>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            IsNameEditable = IsAddOrEdit;
            IsDescriptionEditable = IsAddOrEdit;
        }

        #endregion
    }
}