using Shared.Payments.Entities;

namespace Shared.Payments;

public interface IPaymentsService<TUserId, TPayerProfile, TPayerAddress>
    where TUserId : IEquatable<TUserId>
    where TPayerAddress : IPayerAddress
    where TPayerProfile : IPayerProfile<TPayerAddress>
{
    Task<string> CreatePayerAccountAsync(TPayerProfile payerProfile);

    Task<PaymentMethod<TUserId>> CreateCreditCardASync(TUserId userId, string payerId, string source);

    Task<Payment<TUserId>> MakePaymentAsync(string payerId, PaymentMethod<TUserId> method, decimal amount);

    Task RefundPaymentAsync(string chargeId);

    Task<bool> UpdateCreditCardExpirationDateAsyc(string payerId, string cardId, int expirationMonth, int expirationYear);

    Task<bool> DeleteCreditCardAsync(string payerId, string cardId);

    Task<bool> DeletePayerAsync(string payerId);

    Task<bool> UpdatePayerAsync(TPayerProfile payerProfile);
}
