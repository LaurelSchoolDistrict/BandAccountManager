using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BandAccountManager.Core.Common
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        ValueTask Delete(string id);
        Task<TAggregateRoot?> Get(string id);
        Task<IReadOnlyCollection<TAggregateRoot>> Query(RepositoryQuery<TAggregateRoot> query, int skip = 0, int take = int.MaxValue);
        ValueTask Save(TAggregateRoot aggregate);
    }

    public static class IRepositoryExtensions
    {
        public static Task<IReadOnlyCollection<TAggregateRoot>> Query<TAggregateRoot>(this IRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> filterExpression, int skip = 0, int take = int.MaxValue)
            where TAggregateRoot : IAggregateRoot
        {
            return repository.Query(new() { FilterExpression = filterExpression }, skip, take);
        }
    }
}
