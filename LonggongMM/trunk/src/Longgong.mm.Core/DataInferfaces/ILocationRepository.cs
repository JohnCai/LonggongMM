using System.Collections.Generic;
using Mavis.Core;

namespace Longgong.mm.Core.DataInferfaces
{
    public interface ILocationRepository: INHibernateRepository<Location>
    {
        List<Location> FindByType(LocationType locationType);
    }
}