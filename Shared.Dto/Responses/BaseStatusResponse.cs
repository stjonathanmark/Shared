namespace Shared.Dto.Responses;

public abstract class BaseStatusResponse<TStatus> : Response
    where TStatus : Enum
{

    public BaseStatusResponse()
    { }

    public BaseStatusResponse(TStatus status, bool successful, string message, Dictionary<string, string>? data = null)
        : base(successful, message, data)
    { 
        Status = status;
    }

    protected BaseStatusResponse(BaseStatusResponse<TStatus> response)
        : base(response)
    {
        Status = response.Status;
        StatusName = response.StatusName;
        StatusMessage = response.StatusMessage;
    }

    public TStatus Status { get; set; } = default!;

    public virtual string StatusName { get; set; } = string.Empty;

    public virtual string StatusMessage { get; set; } = string.Empty;
}
