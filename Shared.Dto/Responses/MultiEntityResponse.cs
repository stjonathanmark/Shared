namespace Shared.Dto.Responses;

public abstract class MultiEntityResponse<TEntity> : Response
    where TEntity : class
{
    public MultiEntityResponse() { }

    public MultiEntityResponse(IEnumerable<TEntity> entities, string message, bool successful = true, Dictionary<string, string>? data = null) : base(successful, message, data)
    {
        Entities = entities;
    }

    protected MultiEntityResponse(MultiEntityResponse<TEntity> response) : base(response)
    {
        Entities = response.Entities;
        PagingInfo = response.PagingInfo;
    }

    public IEnumerable<TEntity> Entities { get; set; } = [];

    public IPagingInfo? PagingInfo { get; set; }
}
