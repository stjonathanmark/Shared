namespace Shared;

public interface ISoftDeletedCreated<TCreatorId> : ISoftDeleted, ICreated<TCreatorId>
    where TCreatorId : struct
{ }
