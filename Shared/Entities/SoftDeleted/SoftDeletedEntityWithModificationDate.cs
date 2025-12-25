namespace Shared;

public abstract class SoftDeletedEntityWithModificationDate<TId> : SoftDeletedEntity<TId>, ISoftDeletedWithModificationDate
    where TId : struct 
{
	public DateTime? LastModificationDate { get; set; }
}
