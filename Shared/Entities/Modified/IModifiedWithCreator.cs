namespace Shared;

public interface IModifiedWithCreator<TUserId> : IModified<TUserId>, IHasCreator<TUserId>
    where TUserId : struct
{ }
