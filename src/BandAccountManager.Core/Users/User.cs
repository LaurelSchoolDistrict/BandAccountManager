using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BandAccountManager.Core.Common;

namespace BandAccountManager.Core.Users
{
    public class User : AggregateRootBase
    {
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
