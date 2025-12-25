namespace Shared;

public abstract class SoftDeletedCreatedEntityWithModificationDate<TId, TCreatorId> : SoftDeletedCreatedEntity<TId, TCreatorId>, ISoftDeletedWithModificationDate
    where TId : struct
    where TCreatorId : struct
{
    public DateTime? LastModificationDate { get; set; }
}
