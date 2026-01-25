namespace Shared.Payments.Entities;

public interface IPayerProfile<TPayerAddress>
    where TPayerAddress : IPayerAddress
{
    string PayerId { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string FullName { get; }

    string? OrganizationName { get; set; }

    string EmailAddress { get; set; }

    string PhoneNumber { get; set; }

    bool IsOrganization { get; }

    TPayerAddress Address { get; set; }
}
