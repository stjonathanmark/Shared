namespace Shared;

public interface ISoftDeleted
{
    bool Deleted { get; set; }

    DateTime? DeletionDate { get; set; }
}
