namespace Shared;

public interface ISoftDeletedWithCreationAndModificationDate : ISoftDeleted, IHasCreationDate, IHasModificationDate
{ }
