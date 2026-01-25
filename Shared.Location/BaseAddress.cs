namespace Shared.Location;

public abstract class BaseAddress : BaseEntity<ulong>
{
    public ushort? RegionId { get; set; }

    public byte? CountryId { get; set; }

    public string? StreetOne { get; set; }

    public string? StreetTwo { get; set; }

    public string? City { get; set; } 

    public string? ZipCode { get; set; }
}
