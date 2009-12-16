using System;
using System.ComponentModel;
using Longgong.mm.Core;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class WorkingProcedureDetailViewModel : EntityDetailViewModel<WorkingProcedure>
    {
        private static readonly PropertyChangedEventArgs _isNameEditableChangedArgs =
            ObservableHelper.CreateArgs<WorkingProcedureDetailViewModel>(x => x.IsNameEditable);
        private static readonly PropertyChangedEventArgs _isDescriptionEditableChangedArgs =
           ObservableHelper.CreateArgs<WorkingProcedureDetailViewModel>(x => x.IsDescriptionEditable);

        private bool _isNameEditable;
        private bool _isDescriptionEditable;

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

        #region Overrides of EntityDetailViewModel<WorkingProcedure>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            IsNameEditable = IsAddOrEdit;
            IsDescriptionEditable = IsAddOrEdit;
        }

        #endregion
    }
}