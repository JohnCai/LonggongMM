using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.DataAccess;
using NHibernate;

namespace Longgong.mm.Data
{
    public class WorkingProcedureRepository : NHibernateRepository<WorkingProcedure>, IWorkingProcedureRepository
    {
    }
}