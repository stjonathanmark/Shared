namespace Shared.Dto.Responses;

public class StatusResponse<TStatus> : BaseStatusResponse<TStatus>
    where TStatus : Enum
{
    protected Dictionary<TStatus, string> StatusMessages { get; set; } = [];

    public StatusResponse(Dictionary<TStatus, string> statusMessages)
    {
        StatusMessages = statusMessages;
    }

    public StatusResponse(Dictionary<TStatus, string> statusMessages, TStatus status, string messsage, bool successful = true, Dictionary<string, string>? data = null)
        : base(status, successful, messsage, data)
    {
        StatusMessages = statusMessages;
    }

    public override string StatusName => Status.ToString();

    public override string StatusMessage => StatusMessages.TryGetValue(Status, out string? value) ? value : $"No message was found for the \"{StatusName}\" status";
}
