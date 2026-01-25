using Shared.Data.Repositories;
using Shared.Payments.Entities;

namespace Shared.Payments.Data.Repositories;

public interface IPaymentRepository<TUserId> : IBaseEntityRepository<Payment<TUserId>, ulong>
    where TUserId : IEquatable<TUserId>
{ }
