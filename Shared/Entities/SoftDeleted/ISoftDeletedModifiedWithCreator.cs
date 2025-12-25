namespace Shared;

public interface ISoftDeletedModifiedWithCreator<TUserId> : ISoftDeletedModified<TUserId>, IHasCreator<TUserId>
    where TUserId : struct
{ }
