namespace Shared;

public interface IModified<TModifierId> : IHasModificationDate, IHasModifier<TModifierId>
    where TModifierId : struct
{ }
