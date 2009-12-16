using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class MainProductionPlanViewModel :
        EntityViewModelBase
            <MainProductionPlan, IMainProductionPlanRepository, MainProductionPlanBrowserViewModel,
            MainProductionPlanDetailViewModel>
    {
        public MainProductionPlanViewModel(IMainProductionPlanRepository entityRepository,
                                           IMessageBoxService messageBoxService, IContainerFacade containerFacade)
            : base(entityRepository, messageBoxService, containerFacade)
        {
            DisplayName = "生产下线单";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "查询生产下线单";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "浏览生产下线单";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "新增生产下线单";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "修改生产下线单";
                    break;
            }
        }
    }
}