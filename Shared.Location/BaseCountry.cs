namespace Shared.Location;

public abstract class BaseCountry : BaseEntity<byte>
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;
}
