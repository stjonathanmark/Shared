namespace Shared;

public interface ICreatedAndModified<TUserId> : ICreated<TUserId>, IModified<TUserId>
    where TUserId : struct
{ }
