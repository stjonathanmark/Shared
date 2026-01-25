using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;
using Shared.Payments.Entities;

namespace Shared.Payments.Data.Repositories;

public class PaymentRepository<TUserId> : BaseEntityRepository<Payment<TUserId>, ulong>, IPaymentRepository<TUserId>
    where TUserId : IEquatable<TUserId>
{
    public PaymentRepository(DbContext context) 
        : base(context)
    { }
}
