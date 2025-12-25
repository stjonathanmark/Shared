namespace Shared;

public interface ICreated<TCreatorId> : IHasCreationDate, IHasCreator<TCreatorId>
    where TCreatorId : struct
{ }
