namespace Shared;

public abstract class SoftDeletedCreatedEntityWithModifier<TId, TUserId> : SoftDeletedCreatedEntity<TId, TUserId>, ISoftDeletedWithModifier<TUserId>
    where TId : struct
    where TUserId : struct
{
    public TUserId? ModifierId { get; set; }
}
