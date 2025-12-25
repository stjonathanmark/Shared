namespace Shared;

public abstract class EntityWithCreator<TId, TCreatorId> : BaseEntity<TId>, IHasCreator<TCreatorId>
    where TId : struct
    where TCreatorId : struct
{
    public TCreatorId CreatorId { get; set; }
}
