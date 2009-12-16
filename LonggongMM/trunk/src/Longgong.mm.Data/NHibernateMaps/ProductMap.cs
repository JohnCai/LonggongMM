using FluentNHibernate.Mapping;
using Longgong.mm.Core;

namespace Longgong.mm.Data
{
    public class ProductMap: ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.ID);
            Map(x => x.ProductType).CustomType<ProductType>();
            Map(x => x.Name).CustomSqlType("nvarchar(100)");
            Map(x => x.DrawingNumber).CustomSqlType("varchar(100)");
            Map(x => x.FinishGoodMode).CustomSqlType("nvarchar(100)");
            Map(x => x.FinishGoodConfigure).CustomSqlType("nvarchar(100)");
            Map(x => x.NetWeight);
            Map(x => x.Spec).CustomSqlType("nvarchar(100)");
            Map(x => x.Description).CustomSqlType("nvarchar(200)");
        }
    }
}