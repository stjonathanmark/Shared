namespace Shared.Location;

public abstract class BaseRegion : BaseEntity<ushort>
{
    public byte CountryId { get; set; }

    public RegionType Type { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;
}
