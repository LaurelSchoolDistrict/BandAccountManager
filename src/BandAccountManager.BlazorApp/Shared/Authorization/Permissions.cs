using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandAccountManager.BlazorApp.Shared.Authorization
{
    public static class Permissions
    {
        public static class Account
        {
            public const string Delete = "Account.Delete";
            public const string DeleteTransaction = "Account.DeleteTransaction";
            public const string Read = "Account.Read";
            public const string ReadAll = "Account.ReadAll";
            public const string Write = "Account.Write";
        }
    }
}
