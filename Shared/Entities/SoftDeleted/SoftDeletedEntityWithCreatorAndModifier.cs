namespace Shared;

public abstract class SoftDeletedEntityWithCreatorAndModifier<TId, TUserId> : SoftDeletedEntityWithCreator<TId, TUserId>, ISoftDeletedWithModifier<TUserId>
    where TId : struct
    where TUserId : struct
{
    public TUserId? ModifierId { get; set; }
}
