using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using BandAccountManager.Core.Common;
using BandAccountManager.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDBInfrastructure(this IServiceCollection services, string connectionString, string databaseName)
        {
            ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack { new IgnoreExtraElementsConvention(true) }, t => true);
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            return services
                .AddTransient<IMongoClient>(sp => new MongoClient(connectionString))
                .AddTransient(sp => new MongoDBCollectionFactory(sp.GetRequiredService<IMongoClient>(), databaseName))
                .AddTransient(typeof(IRepository<>), typeof(MongoDBRepository<>));
        }
    }
}
