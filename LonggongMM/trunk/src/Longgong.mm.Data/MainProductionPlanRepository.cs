using Longgong.mm.Core.DataInferfaces;
using Longgong.mm.Core;
using Mavis.DataAccess;

namespace Longgong.mm.Data
{
    public class MainProductionPlanRepository : NHibernateRepository<MainProductionPlan>,IMainProductionPlanRepository
    {
    }
}