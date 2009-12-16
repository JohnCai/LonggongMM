using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class RecipeInputMap: ClassMap<RecipeInput>
    {
        public RecipeInputMap()
        {
            Id(x => x.ID);
            References(x => x.InputProduct);
            Map(x => x.InputQuantity).Not.Nullable().Default("1");
            References(x => x.Recipe).Not.LazyLoad();
        }
    }
}