using System;
using MongoDB.Driver;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var client = new MongoClient();
                IMongoDatabase database = client.GetDatabase(
                    "db_" + Guid.NewGuid().ToString("N"));

                IMongoCollection<City> collection
                    = database.GetCollection<City>("col");
                collection.InsertMany(new[]
                {
                    new City(1, "Amsterdam", "nl", true),
                    new City(2, "Berlin", "de", true),
                    new City(3, "Paris", "fr", true),
                    new City(4, "Zürich", "ch", false)
                });

                return collection;
            });

            services.AddGraphQL(s =>
            {
                ISchemaBuilder builder = SchemaBuilder.New()
                    .AddQueryType<QueryType>()
                    .AddServices(s);

                return builder.Create();
            }, QueryOptions);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGraphQL();

        }

        protected QueryExecutionOptions QueryOptions =>
            new QueryExecutionOptions
            {
                TracingPreference = TracingPreference.Always
            };
    }
}
