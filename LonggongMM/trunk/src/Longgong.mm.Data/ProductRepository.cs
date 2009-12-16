using Longgong.mm.Core.DataInferfaces;
using Mavis.DataAccess;
using Longgong.mm.Core;
using NHibernate;

namespace Longgong.mm.Data
{
    public class ProductRepository : NHibernateRepository<Product>, IProductRepository
    {
    }
}