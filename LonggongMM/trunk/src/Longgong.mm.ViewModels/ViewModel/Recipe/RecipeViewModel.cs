using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class RecipeViewModel: EntityViewModelBase<Recipe, IRecipeRepository, RecipeBrowserViewModel, RecipeDetailViewModel>
    {
        public RecipeViewModel(IRecipeRepository entityRepository, IMessageBoxService messageBoxService, IContainerFacade containerFacade) 
            : base(entityRepository, messageBoxService, containerFacade)
        {
            DisplayName = "生产流程资料";
        }

        protected override void OnCurrentViewModeChanged(ViewMode viewMode)
        {
            base.OnCurrentViewModeChanged(viewMode);
            switch (viewMode)
            {
                case ViewMode.BrowseMode:
                    CurrentDisplayLabel = "查询生产流程";
                    break;
                case ViewMode.ViewOnlyMode:
                    CurrentDisplayLabel = "浏览生产流程";
                    break;
                case ViewMode.AddMode:
                    CurrentDisplayLabel = "新增生产流程";
                    break;
                case ViewMode.EditMode:
                    CurrentDisplayLabel = "修改生产流程";
                    break;
            }
        }
    }
}