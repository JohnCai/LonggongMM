using System;
using System.Collections.Generic;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.Core;
using Mavis.DataAccess;
using NHibernate;
using NHibernate.Criterion;

namespace Longgong.mm.Data
{
    public class LocationRepository : NHibernateRepository<Location>, ILocationRepository
    {
        #region Implementation of ILocationRepository

        public List<Location> FindByType(LocationType locationType)
        {
            var criteria = Session.CreateCriteria(typeof (Location))
                .Add(Restrictions.Eq("LocationType", locationType));

            return criteria.List<Location>() as List<Location>;
        }

       
        #endregion
        
    }

    
}