namespace Shared;

public interface ISoftDeletedCreatedWithModifier<TUserId> : ISoftDeletedCreated<TUserId>, IHasModifier<TUserId>
    where TUserId : struct
{ }
