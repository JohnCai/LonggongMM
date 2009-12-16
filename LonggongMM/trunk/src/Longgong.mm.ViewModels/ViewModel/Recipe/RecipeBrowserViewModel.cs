using System.Collections.ObjectModel;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;

namespace Longgong.mm.ViewModels
{
    public class RecipeBrowserViewModel : EntityBrowserViewModel<Recipe, IRecipeRepository>
    {
        public RecipeBrowserViewModel(IRecipeRepository entityReposity) : base(entityReposity)
        {
        }

        
    }
}