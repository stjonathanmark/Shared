namespace Shared.Dto.Requests;

public abstract class CreateEntityRequest<TEntity> : Request
    where TEntity : class, new()
{
    public TEntity Entity { get; set; } = new();
}
