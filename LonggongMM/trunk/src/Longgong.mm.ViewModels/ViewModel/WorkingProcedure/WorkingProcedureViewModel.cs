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
            DisplayName = "�������";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "��ѯ�������";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "����������";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "�����������";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "�޸Ĺ������";
                    break;
            }
        }
    }

    

    
}