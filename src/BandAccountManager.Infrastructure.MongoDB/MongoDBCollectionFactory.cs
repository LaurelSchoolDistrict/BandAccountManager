using MongoDB.Driver;

namespace BandAccountManager.Infrastructure.MongoDB
{
    public class MongoDBCollectionFactory
    {
        private readonly IMongoClient _mongoClient;
        private readonly string _databaseName;

        public MongoDBCollectionFactory(IMongoClient mongoClient, string databaseName)
        {
            _mongoClient = mongoClient;
            _databaseName = databaseName;
        }

        public IMongoCollection<T> Create<T>()
        {
            return _mongoClient.GetDatabase(_databaseName).GetCollection<T>(typeof(T).Name);
        }
    }
}
