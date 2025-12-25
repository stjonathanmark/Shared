namespace Shared;

public abstract class SoftDeletedModifiedEntity<TId, TModifierId> : SoftDeletedEntity<TId>, ISoftDeletedModified<TModifierId>
    where TId : struct
    where TModifierId : struct
{
    public DateTime? LastModificationDate { get; set; }

    public TModifierId? ModifierId { get; set; }
}
