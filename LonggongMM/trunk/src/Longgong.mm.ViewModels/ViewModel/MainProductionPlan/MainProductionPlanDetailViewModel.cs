using System;
using Longgong.mm.Core;
using Longgong.mm.ViewModels.Lookups;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class MainProductionPlanDetailViewModel : EntityDetailViewModel<MainProductionPlan>
    {
        private bool _isPlanDateEditable;
        private bool _isPlanProductEditable;
        private bool _isPlanQuantityEditable;

        //private IContainerFacade _containerFacade;
        private readonly ProductsLookupViewModel _productsLookupViewModel;
        private readonly IShowDialogService _showDialogService;

        public MainProductionPlanDetailViewModel(IContainerFacade containerFacade)
        {
            _showDialogService = containerFacade.Resolve<IShowDialogService>();
            _productsLookupViewModel = containerFacade.Resolve<ProductsLookupViewModel>();

            LookupProductCommand = new DelegateCommand
                                       {
                                           CanExecuteDelegate = x => true,
                                           ExecuteDelegate = x => ExecuteLookupProductCommand()
                                       };
        }

        private void ExecuteLookupProductCommand()
        {
            var modelResult = _showDialogService.ShowModel("ProductsLookupPopup", _productsLookupViewModel);
            if (modelResult.GetValueOrDefault(false))
            {
                CurrentEntity.PlanProduct = _productsLookupViewModel.SelectedProduct;
            }
        }

        public DelegateCommand LookupProductCommand { get; private set; }

        public bool IsPlanProductEditable
        {
            get { return _isPlanProductEditable; }
            set
            {
                _isPlanProductEditable = value;
                NotifyPropertyChanged("IsPlanProductEditable");
            }
        }

        public bool IsPlanQuantityEditable
        {
            get { return _isPlanQuantityEditable; }
            set
            {
                _isPlanQuantityEditable = value;
                NotifyPropertyChanged("IsPlanQuantityEditable");
            }
        }

        public bool IsPlanDateEditable
        {
            get { return _isPlanDateEditable; }
            set
            {
                _isPlanDateEditable = value;
                NotifyPropertyChanged("IsPlanDateEditable");
            }
        }

        #region Overrides of EntityDetailViewModel<MainProductionPlan>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            IsPlanDateEditable = false;
            IsPlanProductEditable = IsAddOrEdit;
            IsPlanQuantityEditable = IsAddOrEdit;
        }

        #endregion
    }
}