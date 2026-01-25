
namespace Shared.Payments.Logic;

public class PaymentsOptions
{
    public const string SectionName = "Shared:Payments:";

    public Dictionary<string, string> SecurityCredentials { get; set; } = [];

    public string Currency { get; set; } = "USD";

    public bool Capture { get; set; } = true;

    public string OrgPayerDescription { get; set; } = "{0} - {1}";

    public string PersonPayerDescription { get; set; } = "{0}";
}
