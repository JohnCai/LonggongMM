using System;
using Longgong.mm.Core;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class ProductDetailViewModel: EntityDetailViewModel<Product>
    {
        private bool _isNameEditable;
        public bool IsNameEditable
        {
            get { return _isNameEditable; }
            set
            {
                _isNameEditable = value;
                NotifyPropertyChanged("IsNameEditable");
            }
        }

        private bool _isDescriptionEditable;
        public bool IsDescriptionEditable
        {
            get { return _isDescriptionEditable; }
            set
            {
                _isDescriptionEditable = value;
                NotifyPropertyChanged("IsDescriptionEditable");
            }
        }

        private bool _isDrawingNumberEditable;
        public bool IsDrawingNumberEditable
        {
            get { return _isDrawingNumberEditable; }
            set
            {
                _isDrawingNumberEditable = value;
                NotifyPropertyChanged("IsDrawingNumberEditable");
            }
        }

        private bool _isNetWeightEditable;
        public bool IsNetWeightEditable
        {
            get { return _isNetWeightEditable; }
            set
            {
                _isNetWeightEditable = value;
                NotifyPropertyChanged("IsNetWeightEditable");
            }
        }

        private bool _isProductTypeEditable;
        public bool IsProductTypeEditable
        {
            get { return _isProductTypeEditable; }
            set
            {
                _isProductTypeEditable = value;
                NotifyPropertyChanged("IsProductTypeEditable");
            }
        }

        private bool _isSpecEditable;
        public bool IsSpecEditable
        {
            get { return _isSpecEditable; }
            set
            {
                _isSpecEditable = value;
                NotifyPropertyChanged("IsSpecEditable");
            }
        }

        private bool _isFinishGoodConfigureEditable;
        public bool IsFinishGoodConfigureEditable
        {
            get { return _isFinishGoodConfigureEditable; }
            set
            {
                _isFinishGoodConfigureEditable = value;
                NotifyPropertyChanged("IsFinishGoodConfigureEditable");
            }
        }

        private bool _isFinishGoodModeEditable;
        public bool IsFinishGoodModeEditable
        {
            get { return _isFinishGoodModeEditable; }
            set
            {
                _isFinishGoodModeEditable = value;
                NotifyPropertyChanged("IsFinishGoodModeEditable");
            }
        }

        #region Overrides of EntityDetailViewModel<Product>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            IsNameEditable = IsAddOrEdit;
            IsDescriptionEditable = IsAddOrEdit;
            IsDrawingNumberEditable = IsAddOrEdit;
            IsNetWeightEditable = IsAddOrEdit;
            IsProductTypeEditable = _currentViewMode == ViewMode.AddMode;
            IsSpecEditable = IsAddOrEdit;
            IsFinishGoodModeEditable = IsAddOrEdit;
            IsFinishGoodConfigureEditable = IsAddOrEdit;
        }

        #endregion
    }
}