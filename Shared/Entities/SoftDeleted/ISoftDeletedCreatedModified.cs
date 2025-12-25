namespace Shared;

public interface ISoftDeletedCreatedAndModified<TUserId> : ISoftDeletedCreatedWithModificationDate<TUserId>, IHasModifier<TUserId>
    where TUserId : struct
{ }
