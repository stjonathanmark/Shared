namespace Shared;

public abstract class SoftDeletedEntityWithModifier<TId, TModifierId> : SoftDeletedEntity<TId>, ISoftDeletedWithModifier<TModifierId>
    where TId : struct
    where TModifierId : struct
{
    public TModifierId? ModifierId { get; set; }
}
