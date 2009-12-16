using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class RecipeMap: ClassMap<Recipe>
    {
        public RecipeMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable().CustomSqlType("nvarchar(100)");
            Map(x => x.Description).CustomSqlType("nvarchar(100)");
            HasMany(x => x.RecipeInputs).Cascade.All().Not.LazyLoad();
            References(x => x.OutputProduct).Not.Nullable().Not.LazyLoad();
            Map(x => x.OutputQuantity).Not.Nullable();
            References(x => x.WorkingProcedure).Not.Nullable().Not.LazyLoad();
        }
    }
}