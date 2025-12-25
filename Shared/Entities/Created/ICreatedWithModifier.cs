namespace Shared;

public interface ICreatedWithModifier<TUserId> : ICreated<TUserId>, IHasModifier<TUserId>
    where TUserId : struct
{ }
