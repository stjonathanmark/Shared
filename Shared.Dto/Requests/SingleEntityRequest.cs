namespace Shared.Dto.Requests;

public abstract class SingleEntityRequest<TId> : Request
    where TId : struct
{
    public TId EntityId { get; set; }
}
