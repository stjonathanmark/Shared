namespace Shared.Payements;

public abstract class BasePayment<TUserId> : BaseEntity<ulong>
    where TUserId : IEquatable<TUserId>
{
    public TUserId UserId { get; set; } = default!;

    public ulong MethodId { get; set; }

    public DateTime Date { get; set; }

    public string PaymentCode { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string ChargeId { get; set; } = string.Empty;
}
