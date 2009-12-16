using Longgong.mm.Core.DataInferfaces;
using Mavis.DataAccess;
using Longgong.mm.Core;
using NHibernate;

namespace Longgong.mm.Data
{
    public class RecipeRepository : NHibernateRepository<Recipe>, IRecipeRepository
    {
    }
}