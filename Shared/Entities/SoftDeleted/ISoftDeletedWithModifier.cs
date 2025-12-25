namespace Shared;

public interface ISoftDeletedWithModifier<TModifierId> : ISoftDeleted, IHasModifier<TModifierId>
    where TModifierId : struct
{ }
