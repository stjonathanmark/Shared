namespace Shared;

public abstract class BaseResult
{
    public bool Successful { get; set; }

    public string? Message { get; set; }

    public Dictionary<string, string> Data { get; } = [];
}
