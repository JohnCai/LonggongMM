using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class WorkingProcedureMap: ClassMap<WorkingProcedure>
    {
        public WorkingProcedureMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).CustomSqlType("nvarchar(100)");
            Map(x => x.Description).CustomSqlType("nvarchar(200)");
        }
    }
}