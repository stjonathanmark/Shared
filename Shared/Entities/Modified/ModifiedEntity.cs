namespace Shared;

public abstract class ModifiedEntity<TId, TModifierIdId, TModifierId> : EntityWithModifier<TId, TModifierId>, IHasModificationDate
    where TId : struct
    where TModifierId : struct
{
    public DateTime? LastModificationDate { get; set; }
}
