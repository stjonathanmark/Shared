namespace Shared.Security;

public class SecurityUserProfile : BaseUserProfile<ulong, string>
{
    public string? OrganizationName { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
