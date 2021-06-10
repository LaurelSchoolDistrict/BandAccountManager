using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandAccountManager.Core.Common
{
    public abstract class AggregateRootBase : IAggregateRoot
    {
        public string Id { get; set; } = string.Empty;
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}
