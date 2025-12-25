namespace Shared;

public abstract class SoftDeletedEntityWithCreator<TId, TCreatorId> : SoftDeletedEntity<TId>, ISoftDeletedWithCreator<TCreatorId>
    where TId : struct
    where TCreatorId : struct
{ 
    public TCreatorId CreatorId { get; set; }
}
