namespace Shared;

public abstract class EntityWithModifier<TId, TModifierId> : BaseEntity<TId>, IHasModifier<TModifierId>
    where TId : struct
    where TModifierId : struct
{
    public TModifierId? ModifierId { get; set; }
}
