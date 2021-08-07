namespace BandAccountManager.Core.Common
{
    public abstract class BaseEntity : IEntity
    {
        public string Id { get; set; } = string.Empty;
    }
}
