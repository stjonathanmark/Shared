namespace Shared;

public interface ISoftDeletedCreatedWithModificationDate<TCreatorId> : ISoftDeletedCreated<TCreatorId>, IHasModificationDate
    where TCreatorId : struct
{ }
