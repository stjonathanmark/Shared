namespace Shared;

public abstract class SoftDeletedEntityWithCreationDate<TId> : SoftDeletedEntity<TId>, ISoftDeletedWithCreationDate
    where TId : struct
{ 
    public DateTime CreationDate { get; set; }
}
 