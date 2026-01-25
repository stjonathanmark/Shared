namespace Shared.Security;

public abstract class BaseUserProfile<TUserId> : BaseEntity<TUserId>
    where TUserId : struct, IEquatable<TUserId>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string? OrganizationName { get; set; }

    public bool IsOrganization => !string.IsNullOrWhiteSpace(OrganizationName);

    public virtual string EmailAddress { get; set; } = string.Empty;

    public virtual string PhoneNumber { get; set; } = string.Empty;
}
