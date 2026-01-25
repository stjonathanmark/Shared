namespace Shared.Location;

public class Region : BaseRegion
{
    public Country Country { get; set; } = new();

    public List<Address> Addresses { get; set; } = [];
}
