using Shared.Payements;

namespace Shared.Payments.Entities;

public class PaymentMethod<TUserId> : BasePaymentMethod<TUserId>
    where TUserId : IEquatable<TUserId>
{ }
