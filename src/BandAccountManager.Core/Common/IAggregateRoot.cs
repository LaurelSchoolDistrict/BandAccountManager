using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandAccountManager.Core.Common
{
    public interface IAggregateRoot : IEntity
    {
        public DateTimeOffset DateCreated { get; set; }
    }
}
