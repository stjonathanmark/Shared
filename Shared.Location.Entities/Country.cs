namespace Shared.Location;

public class Country : BaseCountry
{
    public List<Region> Regions { get; set; } = [];

    public List<Address> Addresses { get; set; } = [];
}
