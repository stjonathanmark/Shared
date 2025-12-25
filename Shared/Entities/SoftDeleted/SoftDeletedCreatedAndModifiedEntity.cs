namespace Shared;

public abstract class SoftDeletedCreatedAndModifiedEntity<TId, TUserId> : SoftDeletedCreatedEntityWithModifier<TId, TUserId>, ISoftDeletedCreatedAndModified<TUserId>
    where TId : struct
    where TUserId : struct
{
    public DateTime? LastModificationDate { get; set; }
}
