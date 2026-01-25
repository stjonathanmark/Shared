using Shared.Location;

namespace Shared.Payments.Entities;

public interface IPayerAddress
{
    string? StreetOne { get; set; }

    string? StreetTwo { get; set; }

    string? City { get; set; }

    Region Region { get; set; }

    string? ZipCode { get; set; }

    Country Country { get; set; }
}
