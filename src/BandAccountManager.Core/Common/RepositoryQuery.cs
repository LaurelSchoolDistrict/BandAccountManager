using System;
using System.Linq.Expressions;

namespace BandAccountManager.Core.Common
{
    public class RepositoryQuery<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        public Expression<Func<TAggregateRoot, bool>> FilterExpression { get; init; } = (TAggregateRoot ar) => true;
        public Expression<Func<TAggregateRoot, object>> SortExpression { get; init; } = (TAggregateRoot ar) => ar.Id;
        public QuerySortDirection SortDirection { get; init; } = QuerySortDirection.Ascending;
    }
}
