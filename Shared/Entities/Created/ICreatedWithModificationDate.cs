namespace Shared;

public interface ICreatedWithModificationDate<TCreatorId> : ICreated<TCreatorId>, IHasModificationDate
    where TCreatorId : struct
{ }
