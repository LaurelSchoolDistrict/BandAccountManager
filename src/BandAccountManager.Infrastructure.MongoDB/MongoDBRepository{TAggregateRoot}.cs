using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

using BandAccountManager.Core.Common;

namespace BandAccountManager.Infrastructure.MongoDB
{
    public class MongoDBRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        private readonly IMongoCollection<TAggregateRoot> _collection;

        public MongoDBRepository(MongoDBCollectionFactory mongoDbCollectionFactory)
        {
            _collection = mongoDbCollectionFactory.Create<TAggregateRoot>();
        }

        public async ValueTask Delete(string id)
        {
            await _collection.DeleteOneAsync(ar => ar.Id.Equals(id));
        }

        public async Task<bool> Exists(string id)
        {
            var count = await _collection.CountDocumentsAsync(e => e.Id.Equals(id));

            return (count > 0);
        }

        public async Task<TAggregateRoot?> Get(string id)
        {
            return await _collection.Find(ar => ar.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<TAggregateRoot>> Query(RepositoryQuery<TAggregateRoot> query, int skip = 0, int take = int.MaxValue)
        {
            return await _collection
                .Find(query.FilterExpression)
                .Sort((query.SortDirection == QuerySortDirection.Ascending)
                    ? new SortDefinitionBuilder<TAggregateRoot>().Ascending(query.SortExpression)
                    : new SortDefinitionBuilder<TAggregateRoot>().Descending(query.SortExpression))
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        public async ValueTask Save(TAggregateRoot aggregate)
        {
            await _collection.ReplaceOneAsync(ar => ar.Id.Equals(aggregate.Id), aggregate, new ReplaceOptions { IsUpsert = true });
        }
    }
}
