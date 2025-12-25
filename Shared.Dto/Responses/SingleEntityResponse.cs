namespace Shared.Dto.Responses;

public abstract class SingleEntityResponse<TEntity> : Response
    where TEntity : class
{
    public SingleEntityResponse() { }

    public SingleEntityResponse(TEntity entity, string message, bool successful = true, Dictionary<string, string>? data = null) : base(successful, message, data) 
    {
        Entity = entity;
    }

    protected SingleEntityResponse(SingleEntityResponse<TEntity> response) : base(response) 
    { 
        Entity = response.Entity;
    }

    public SingleEntityResponse(bool isSuccess, string message) : base(isSuccess, message) { }

    public TEntity? Entity { get; set; }    
}
