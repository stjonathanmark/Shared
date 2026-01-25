using Shared.Payements;

namespace Shared.Payments;

public class PaymentMethod<TUserId> : BasePaymentMethod<TUserId>
    where TUserId : IEquatable<TUserId>
{ }
