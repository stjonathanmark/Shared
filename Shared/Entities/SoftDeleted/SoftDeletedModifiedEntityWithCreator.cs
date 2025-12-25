namespace Shared;

public abstract class SoftDeletedModifiedEntityWithCreator<TId, TUserId> : SoftDeletedModifiedEntity<TId, TUserId>, ISoftDeletedModifiedWithCreator<TUserId>
    where TId : struct
    where TUserId : struct
{
    public TUserId CreatorId { get; set; }
}
