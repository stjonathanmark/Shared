namespace Shared;

public abstract class SoftDeletedEntity<TId> : BaseEntity<TId>, ISoftDeleted
	where TId : struct 
{
	public bool Deleted { get; set; }

	public DateTime? DeletionDate { get; set; }
}
