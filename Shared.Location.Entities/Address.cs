namespace Shared.Location;

public class Address : BaseAddress 
{
    public Region Region { get; set; } = new();

    public Country Country { get; set; } = new();
}
