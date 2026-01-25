using Shared.Payements;

namespace Shared.Payments;

public class Payment<TUserId> : BasePayment<TUserId>
    where TUserId : IEquatable<TUserId>
{
    public PaymentMethod<TUserId>? Method { get; set; } = null;
}
