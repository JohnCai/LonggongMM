using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels.Lookups
{
    public class ProductsLookupViewModel : PopupViewModelBase
    {
        private readonly ICollectionView _productsView;
        private List<Product> _products;
        private Product _selectedProduct;

        public ProductsLookupViewModel(IProductRepository productRepository)
        {
            Products = (List<Product>) productRepository.GetAll();
            _productsView = CollectionViewSource.GetDefaultView(_products);
            _productsView.CurrentChanged += ProductsViewCurrentChanged;
        }

        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyPropertyChanged("SelectedProduct");
            }
        }

        private void ProductsViewCurrentChanged(object sender, EventArgs e)
        {
            if (_productsView.CurrentItem != null)
            {
                SelectedProduct = _productsView.CurrentItem as Product;
            }
        }

        #region Override

        protected override bool CanExecuteClosePopupCommand()
        {
            return SelectedProduct != null;
        }

        #endregion
    }
}