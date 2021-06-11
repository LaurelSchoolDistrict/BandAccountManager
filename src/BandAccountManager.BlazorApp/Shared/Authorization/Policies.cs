using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandAccountManager.BlazorApp.Shared.Authorization
{
    public static class Policies
    {
        public const string DistrictUser = "DistrictUser";

        public static class Permissions
        {
            public static class Account
            {
                public const string Delete = "Permission.Account.Delete";
                public const string DeleteTransaction = "Permission.Account.DeleteTransaction";
                public const string Read = "Permission.Account.Read";
                public const string ReadAll = "Permission.Account.ReadAll";
                public const string Write = "Permission.Account.Write";
            }
        }
    }
}
