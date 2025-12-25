namespace Shared;

public abstract class EntityWithCreationDate<TId> : BaseEntity<TId>, IHasCreationDate
    where TId : struct
{
    public DateTime CreationDate { get; set; }
}
