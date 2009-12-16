using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class ProductViewModel : EntityViewModelBase<Product, IProductRepository, ProductBrowserViewModel, ProductDetailViewModel>
    {
        public ProductViewModel(IProductRepository entityRepository, IMessageBoxService messageBoxService, IContainerFacade containerFacade) 
            : base(entityRepository, messageBoxService, containerFacade)
        {
            DisplayName = "产品资料";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "查询产品资料";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "浏览产品资料";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "新增产品资料";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "修改产品资料";
                    break;
            }
        }
    }
}