namespace Shared.Location;

public class Address : BaseAddress 
{
    public Region Region { get; set; } = new();

    public Country Country { get; set; } = new();
}

public class Address<TAddresseeId>
    where TAddresseeId : struct, IEquatable<TAddresseeId>
{
    public TAddresseeId AddresseeId { get; set; }
}
