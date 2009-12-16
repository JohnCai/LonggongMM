using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class LocationMap: ClassMap<Location>
    {
        public LocationMap()
        {
            Id(x => x.ID);
            Map(x => x.LocationType).CustomType<LocationType>();
            Map(x => x.Name).CustomSqlType("nvarchar(100)");
            Map(x => x.Description).CustomSqlType("nvarchar(200)");
        }
    }
}
