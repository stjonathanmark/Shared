namespace Shared;

public abstract class EntityWithModificationDate<TId> : BaseEntity<TId>, IHasModificationDate
    where TId : struct
{
    public DateTime? LastModificationDate { get; set; }
}
