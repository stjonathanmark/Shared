namespace Shared;

public abstract class SoftDeletedCreatedEntity<TId, TCreatorId> : SoftDeletedEntity<TId>, ISoftDeletedCreated<TCreatorId>
    where TId : struct
    where TCreatorId : struct
{
    public DateTime CreationDate { get; set; }

    public TCreatorId CreatorId { get; set; }
}
