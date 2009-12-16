using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;

namespace Longgong.mm.ViewModels
{
    public class WorkingProcedureBrowserViewModel : EntityBrowserViewModel<WorkingProcedure, IWorkingProcedureRepository>
    {
        public WorkingProcedureBrowserViewModel(IWorkingProcedureRepository entityReposity) : base(entityReposity)
        {
        }
    }
}