namespace Shared;

public interface IModifiedWithCreationDate<TModifierId> : IModified<TModifierId>, IHasCreationDate
    where TModifierId : struct
{ }
