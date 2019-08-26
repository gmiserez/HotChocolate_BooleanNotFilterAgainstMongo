using HotChocolate.Types;
using MongoDB.Driver;

namespace Demo
{
    public class QueryType: ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field("cities")
                .Resolver(ctx => ctx.Service<IMongoCollection<City>>().AsQueryable())
                .UseFiltering<CityFilterType>();
        }
    }
}
