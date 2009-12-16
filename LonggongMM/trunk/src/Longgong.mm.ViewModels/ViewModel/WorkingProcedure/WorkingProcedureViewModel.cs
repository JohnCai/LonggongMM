using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class WorkingProcedureViewModel : EntityViewModelBase<WorkingProcedure, IWorkingProcedureRepository, WorkingProcedureBrowserViewModel, WorkingProcedureDetailViewModel>
    {
        public WorkingProcedureViewModel(IWorkingProcedureRepository entityRepository, IMessageBoxService messageBoxService, IContainerFacade containerFacade) 
            : base(entityRepository, messageBoxService, containerFacade)
        {
            DisplayName = "工序类别";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "查询工序类别";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "浏览工序类别";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "新增工序类别";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "修改工序类别";
                    break;
            }
        }
    }

    

    
}