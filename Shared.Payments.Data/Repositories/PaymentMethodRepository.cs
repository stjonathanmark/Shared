using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;
using Shared.Payments.Entities;

namespace Shared.Payments.Data.Repositories;

public class PaymentMethodRepository<TUserId> : BaseEntityRepository<PaymentMethod<TUserId>, ulong>, IPaymentMethodRepository<TUserId>
    where TUserId : IEquatable<TUserId>
{
    public PaymentMethodRepository(DbContext context) 
        : base(context)
    { }

    public async Task<List<PaymentMethod<TUserId>>> GetByUserAsync(TUserId userId, int? pageNumber, int? pageSize)
    {
        return await GetEntitiesAsync(p => p.Id.Equals(userId), null, pageNumber, pageSize, null, null, false);
    }
}
