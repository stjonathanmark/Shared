namespace Shared;

internal interface ISoftDeletedWithCreatorAndModifier<TUserId> : ISoftDeleted, IHasCreator<TUserId>, IHasModifier<TUserId>
    where TUserId : struct
{ }
