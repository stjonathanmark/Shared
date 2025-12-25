namespace Shared;

public abstract class SoftDeletedModifiedEntityWithCreationDate<TId, TModifierId> : SoftDeletedModifiedEntity<TId, TModifierId>, ISoftDeletedModifiedWithCreationDate<TModifierId>
    where TId : struct
    where TModifierId : struct
{
    public DateTime CreationDate { get; set; }
}
