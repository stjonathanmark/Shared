using Shared.Data;
using Shared.Payments.Data.Repositories;

namespace Shared.Payments.Data;

public interface IPaymentsUnitOfWork<TUserId> : IBaseUnitOfWork
    where TUserId : IEquatable<TUserId>
{
    IPaymentMethodRepository<TUserId> PaymentMethods { get; }

    IPaymentRepository<TUserId> Payments { get; }
}
