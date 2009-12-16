using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class MainProductionPlanMap : ClassMap<MainProductionPlan>
    {
        public MainProductionPlanMap()
        {
            Id(x => x.ID);
            Map(x => x.PlanDate).Not.Nullable();
            Map(x => x.PlanQuantity).Not.Nullable();
            Map(x => x.Notes).CustomSqlType("nvarchar(500)");
            References(x => x.PlanProduct).Not.Nullable().Not.LazyLoad();
        }
    }
}