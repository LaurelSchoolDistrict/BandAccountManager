namespace BandAccountManager.Shared.Users
{
    public static class UserIdHelper
    {
        public static string FromEmailAddress(string emailAddress)
        {
            return emailAddress.Contains('@') ? emailAddress.ToLowerInvariant().Substring(0, emailAddress.IndexOf('@')) : emailAddress.ToLowerInvariant();
        }
    }
}
