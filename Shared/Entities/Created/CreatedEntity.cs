namespace Shared;

public abstract class CreatedEntity<TId, TCreatorId> : EntityWithCreator<TId, TCreatorId>, IHasCreationDate
    where TId : struct
    where TCreatorId : struct
{
    public DateTime CreationDate { get; set; }
}
