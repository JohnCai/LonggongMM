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
            DisplayName = "��Ʒ����";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "��ѯ��Ʒ����";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "�����Ʒ����";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "������Ʒ����";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "�޸Ĳ�Ʒ����";
                    break;
            }
        }
    }
}