namespace Shared;

public interface IHasCreatorAndModifier<TUserId> : IHasCreator<TUserId>, IHasModifier<TUserId>
    where TUserId : struct
{ }
