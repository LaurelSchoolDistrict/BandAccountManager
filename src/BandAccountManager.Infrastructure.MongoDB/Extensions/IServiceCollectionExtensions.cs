
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using BandAccountManager.Core.Common;
using BandAccountManager.Infrastructure.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDBInfrastructure(this IServiceCollection services, string connectionString)
        {
            ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack { new IgnoreExtraElementsConvention(true) }, t => true);
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            return services.AddTransient(typeof(IRepository<>), typeof(MongoDBRepository<>));
        }
    }
}
