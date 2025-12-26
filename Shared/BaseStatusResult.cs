namespace Shared;

public abstract class BaseStatusResult<TStatus> : BaseResult
    where TStatus : Enum
{
    public TStatus Status { get; set; } = default!;
}
