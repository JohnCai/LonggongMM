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
            DisplayName = "�������ߵ�";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "��ѯ�������ߵ�";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "����������ߵ�";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "�����������ߵ�";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "�޸��������ߵ�";
                    break;
            }
        }
    }
}