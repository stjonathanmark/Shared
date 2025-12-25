namespace Shared;

public interface ISoftDeletedWithCreator<TCreatorId> : ISoftDeleted, IHasCreator<TCreatorId>
    where TCreatorId : struct
{ }
