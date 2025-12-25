namespace Shared;

public interface ISoftDeletedModifiedWithCreationDate<TModifierId> : ISoftDeletedModified<TModifierId>, IHasCreationDate
    where TModifierId : struct
{ }
