using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class RecipeOutputMap: ClassMap<RecipeOutput>
    {
        public RecipeOutputMap()
        {
            Id(x => x.ID).GeneratedBy.Foreign("Recipe");
            HasOne(x => x.Recipe).Cascade.All().Constrained();
            References(x => x.OutputProduct);
            Map(x => x.OutputQuantity);
        }
    }
}