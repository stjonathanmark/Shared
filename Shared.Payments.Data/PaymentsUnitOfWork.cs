using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Payments.Data.Repositories;
using Shared.Payments.Entities;

namespace Shared.Payments.Data;

public class PaymentsUnitOfWork<TUserId> : BaseUnitOfWork, IPaymentsUnitOfWork<TUserId>
    where TUserId : IEquatable<TUserId>
{
    public PaymentsUnitOfWork(DbContext context) : base(context)
    { }

    public IPaymentMethodRepository<TUserId> PaymentMethods => GetRepository<PaymentMethodRepository<TUserId>, PaymentMethod<TUserId>>();

    public IPaymentRepository<TUserId> Payments => GetRepository<PaymentRepository<TUserId>, Payment<TUserId>>();
}
