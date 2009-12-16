using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;
using Longgong.mm.ViewModels.Lookups;
using System.Windows.Data;

namespace Longgong.mm.ViewModels
{
    public class RecipeDetailViewModel : EntityDetailViewModel<Recipe>
    {
        private bool _isDescriptionEditable;
        private bool _isNameEditable;

        private ProductsLookupViewModel _productsLookupViewModel;
        private RecipeInput _selectedRecipeInput;
        private ICollectionView _recipeInputsCollectionView;

        public RecipeDetailViewModel(IWorkingProcedureRepository workingProcedureRepository, IShowDialogService showDialogService,
            IContainerFacade containerFacade, IMessageBoxService messageBoxService)
        {
            WorkingProcedureRepository = workingProcedureRepository;
            ShowDialogService = showDialogService;
            MessageBoxService = messageBoxService;
            ContainerFacade = containerFacade;

            //_recipeInputsCollectionView = CollectionViewSource.GetDefaultView(CurrentEntity.RecipeInputs);
            //_recipeInputsCollectionView.CurrentChanged += new EventHandler(_recipeInputsCollectionView_CurrentChanged);

            _productsLookupViewModel = containerFacade.Resolve<ProductsLookupViewModel>();

            LookupOutputProductCommand = new DelegateCommand
                                             {
                                                 CanExecuteDelegate = x => IsOutputProductEditable,
                                                 ExecuteDelegate = x => ExecuteLookupOutputProductCommand()
                                             };
            AddInputCommand = new DelegateCommand
                                  {
                                      CanExecuteDelegate = x => IsAddOrEdit,
                                      ExecuteDelegate = x => ExecuteAddInputCommand()
                                  };
            DeleteInputCommand = new DelegateCommand
                                     {
                                         CanExecuteDelegate = x => IsAddOrEdit,
                                         ExecuteDelegate = x => ExecuteDeleteInputCommand()
                                     };
        }

        //void _recipeInputsCollectionView_CurrentChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        protected IMessageBoxService MessageBoxService { get; private set; }
        protected IShowDialogService ShowDialogService { get; private set; }
        protected IContainerFacade ContainerFacade { get; private set; }

        #region Command Executer

        private void ExecuteDeleteInputCommand()
        {
            if (SelectedRecipeInput != null)
            {
                CurrentEntity.RemoveRecipeInput(SelectedRecipeInput);
            }
        }

        private void ExecuteAddInputCommand()
        {
            var modelResult = ShowDialogService.ShowModel("ProductsLookupPopup", _productsLookupViewModel);
            if (modelResult.GetValueOrDefault(false))
            {
                CurrentEntity.AddRecipeInput(new RecipeInput { InputProduct = _productsLookupViewModel.SelectedProduct , InputQuantity = 1}); ;
            }
        }

        private void ExecuteLookupOutputProductCommand()
        {
            var modelResult = ShowDialogService.ShowModel("ProductsLookupPopup", _productsLookupViewModel);
            if (modelResult.GetValueOrDefault(false))
            {
                CurrentEntity.OutputProduct =  _productsLookupViewModel.SelectedProduct;
            }
        }

        #endregion

        protected IWorkingProcedureRepository WorkingProcedureRepository { get; private set; }

        public DelegateCommand LookupOutputProductCommand { get; private set; }
        public DelegateCommand AddInputCommand { get; private set; }
        public DelegateCommand DeleteInputCommand { get; private set; }


        #region Editable

        public bool IsNameEditable
        {
            get { return _isNameEditable; }
            set
            {
                _isNameEditable = value;
                NotifyPropertyChanged("IsNameEditable");
            }
        }

        public bool IsDescriptionEditable
        {
            get { return _isDescriptionEditable; }
            set
            {
                _isDescriptionEditable = value;
                NotifyPropertyChanged("IsDescriptionEditable");
            }
        }

        private bool _isOutputProductEditable;

        public bool IsOutputProductEditable
        {
            get { return _isOutputProductEditable; }
            set
            {
                _isOutputProductEditable = value;
                NotifyPropertyChanged("IsOutputProductEditable");
            }
        }

        private bool _isWorkingProcedureEditable;

        public bool IsWorkingProcedureEditable
        {
            get { return _isWorkingProcedureEditable; }
            set
            {
                _isWorkingProcedureEditable = value;
                NotifyPropertyChanged("IsWorkingProcedureEditable");
            }
        }

        private bool _isOutputQuantityEditable;

        public bool IsOutputQuantityEditable
        {
            get { return _isOutputQuantityEditable; }
            set
            {
                _isOutputQuantityEditable = value;
                NotifyPropertyChanged("IsOutputQuantityEditable");
            }
        }

        #endregion


        public RecipeInput SelectedRecipeInput
        {
            get { return _selectedRecipeInput; }
            set
            {
                _selectedRecipeInput = value;
                NotifyPropertyChanged("SelectedRecipeInput");
            }
        }

        public IList<WorkingProcedure> AvailableWorkingProcedures
        {
            get
            {
                return WorkingProcedureRepository.GetAll();
            }
        }

        private ObservableCollection<RecipeInput> _recipeInputs;

        public ObservableCollection<RecipeInput> RecipeInputs
        {
            get { return _recipeInputs; }
            set
            {
                _recipeInputs = value;
                NotifyPropertyChanged("RecipeInputs");
            }
        }

        #region Overrides of EntityDetailViewModel<Recipe>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            IsNameEditable = IsAddOrEdit;
            IsDescriptionEditable = IsAddOrEdit;
            //IsOutputProductEditable = CurrentViewMode == ViewMode.AddMode;
            IsOutputProductEditable = IsAddOrEdit;
            IsWorkingProcedureEditable = IsAddOrEdit;
            IsOutputQuantityEditable = IsAddOrEdit;
        }

        public override Recipe CurrentEntity
        {
            get
            {
                var currentEntity = base.CurrentEntity;
                currentEntity.RecipeInputs = RecipeInputs;
                return currentEntity;
            }
            set
            {
                base.CurrentEntity = value;
                if (value != null)
                    RecipeInputs = new ObservableCollection<RecipeInput>(value.RecipeInputs);
            }
        }

        #endregion
    }
}