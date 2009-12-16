using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class LocationViewModel : EntityViewModelBase<Location, ILocationRepository, LocationBrowserViewModel, LocationDetailViewModel>
    {
        public LocationViewModel(ILocationRepository entityRepository, IMessageBoxService messageBoxService, IContainerFacade containerFacade) 
            : base(entityRepository, messageBoxService, containerFacade)
        {
            DisplayName = "仓库资料";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "查询仓库资料";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "浏览仓库资料";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "新增仓库资料";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "修改仓库资料";
                    break;
            }
        }
    }
}