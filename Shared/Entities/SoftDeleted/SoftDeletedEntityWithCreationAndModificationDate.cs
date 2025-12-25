namespace Shared;

public abstract class SoftDeletedEntityWithCreationAndModificationDate<TId> : SoftDeletedEntityWithCreationDate<TId>, ISoftDeletedWithCreationAndModificationDate
    where TId : struct
{
    public DateTime? LastModificationDate { get; set; }
}
