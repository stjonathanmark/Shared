using Shared.Data.Repositories;
using Shared.Payments.Entities;

namespace Shared.Payments.Data.Repositories;

public interface IPaymentMethodRepository<TUserId> : IBaseEntityRepository<PaymentMethod<TUserId>, ulong>
    where TUserId : IEquatable<TUserId>
{
    Task<List<PaymentMethod<TUserId>>> GetByUserAsync(TUserId userId, int? pageNumber, int? pageSize);
}
