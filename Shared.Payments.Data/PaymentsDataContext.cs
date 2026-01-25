using Microsoft.EntityFrameworkCore;
using Shared.Payements;

namespace Shared.Payments.Data;

public class PaymentsDataContext<TPaymentMethod, TUserId> : DbContext
    where TPaymentMethod : BasePaymentMethod<TUserId>
    where TUserId : IEquatable<TUserId>
{
    public DbSet<TPaymentMethod> PaymentMethod { get; set; }
}
