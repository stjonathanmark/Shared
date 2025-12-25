namespace Shared;

public interface ISoftDeletedModified<TModifierId> : ISoftDeleted, IModified<TModifierId>
    where TModifierId : struct
{ }
