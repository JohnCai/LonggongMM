using System.Collections.Generic;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.Core;

namespace Longgong.mm.ViewModels
{
    public class ProductBrowserViewModel: EntityBrowserViewModel<Product, IProductRepository>
    {
        public ProductBrowserViewModel(IProductRepository entityReposity) : base(entityReposity)
        {
        }

        protected override List<SearchCriteria<Product>> SearchCriterias
        {
            get
            {
                var result = new List<SearchCriteria<Product>>();

                if (ProductTypeFilter >= 0)
                    result.Add(new SearchCriteria<Product>(x => x.ProductType, ProductTypeFilter,Operator.Equal));
                if (!string.IsNullOrEmpty(FinishGoodModeFilter))
                    result.Add(new SearchCriteria<Product>(x => x.FinishGoodMode, FinishGoodModeFilter, Operator.Like));
                if (!string.IsNullOrEmpty(NameFilter))
                    result.Add(new SearchCriteria<Product>(x => x.Name, NameFilter, Operator.Like));

                return result;
            }
        }

        private ProductType _productTypeFilter = ProductType.None;

        public ProductType ProductTypeFilter
        {
            get { return _productTypeFilter; }
            set
            {
                _productTypeFilter = value;
                NotifyPropertyChanged("_productTypeFilter");
            }
        }

        private string _finishGoodModeFilter;

        public string FinishGoodModeFilter
        {
            get { return _finishGoodModeFilter; }
            set
            {
                _finishGoodModeFilter = value;
                NotifyPropertyChanged("FinishGoodModeFilter");
            }
        }

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                NotifyPropertyChanged("NameFilter");
            }
        }
    }
}