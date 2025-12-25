
namespace Shared
{
    public interface ISoftDeletedEntityWithCreationDate
    {
        DateTime CreationDate { get; set; }
    }
}