using System.Text.RegularExpressions;

namespace Shared.Payements;

public abstract class BasePaymentMethod<TUserId> : BaseEntity<ulong>
    where TUserId : IEquatable<TUserId>
{
    private int expirationMonth;
    private string lastFourDigits = string.Empty;

    public TUserId UserProfileId { get; set; } = default!;

    public string FingerPrint { get; set; } = string.Empty;

    public string CardBrand { get; set; } = string.Empty;

    public string CardId { get; set; } = string.Empty;

    public int ExpirationMonth 
    { 
        get => expirationMonth; 
        set 
        {
            if (value < 1 || value > 12)
                throw new Exception("Expiration month only accepts values from 1 to 12");

            expirationMonth = value; 
        }
    }

    public int ExpirationYear { get; set; }

    public string LastFourDigits
    {
        get => lastFourDigits;
        set
        {
            var regex = new Regex("\\d<4>");

            if (!regex.IsMatch(value))
                throw new Exception("Last four digits can must have length of 4 and can only contain numerical characters");

            lastFourDigits = value;
        }
    }

    public bool Expired => ExpirationYear < DateTime.Now.Year || (ExpirationYear == DateTime.Now.Year && ExpirationMonth < DateTime.Now.Month);

    public bool AlmostExpired => ExpirationYear == DateTime.Now.Year && ExpirationMonth == DateTime.Now.Month;
}
