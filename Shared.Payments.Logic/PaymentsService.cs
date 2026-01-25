using Microsoft.Extensions.Options;
using Shared.Payments.Entities;
using Stripe;

namespace Shared.Payments;

public class PaymentsService<TUserId, TPayerProfile, TPayerAddress> : IPaymentsService<TUserId, TPayerProfile, TPayerAddress>
    where TUserId : IEquatable<TUserId>
    where TPayerAddress : IPayerAddress
    where TPayerProfile : IPayerProfile<TPayerAddress>
{

    private readonly PaymentsOptions payOpts;

    public PaymentsService(IOptions<PaymentsOptions> paymentOptions)
    {
        payOpts = paymentOptions.Value;
        StripeConfiguration.ApiKey = payOpts.SecurityCredentials[CredentialKeys.Stripe.SecretKey];
    }

    public string PublishableKey => payOpts.SecurityCredentials[CredentialKeys.Stripe.PublishableKey];

    #region Payment Transactions

    public async Task<Payment<TUserId>> MakePaymentAsync(string payerId, PaymentMethod<TUserId> method, decimal amount)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = payOpts.Currency,
            Capture = payOpts.Capture,
            Source = method.CardId,
            Customer = payerId
        };

        var service = new ChargeService();
        var response = await service.CreateAsync(options);

        return new Payment<TUserId>()
        {
            ChargeId = response.Id,
            UserId = method.UserProfileId,
            MethodId = method.Id,
            Date = response.Created,
            Amount = amount
        };
    }

    public async Task RefundPaymentAsync(string chargeId)
    {
        var options = new RefundCreateOptions()
        {
            Charge = chargeId
        };

        var service = new RefundService();
        await service.CreateAsync(options);
    }

    #endregion

    #region Payer Management

    public async Task<string> CreatePayerAccountAsync(TPayerProfile payerProfile)
    {
        var opts = new CustomerCreateOptions()
        {
            Description = payerProfile.IsOrganization
                ? string.Format(payOpts.OrgPayerDescription, payerProfile.OrganizationName, payerProfile.FullName)
                : string.Format(payOpts.PersonPayerDescription, payerProfile.FullName),
            Name = payerProfile.FullName,
            BusinessName = payerProfile.OrganizationName,
            Email = payerProfile.EmailAddress,
            Metadata = {
                { "PhoneNumber", payerProfile.PhoneNumber}
            }
        };

        
        if (payerProfile.Address != null)
        {
            opts.Address = new AddressOptions()
            {
                Line1 = payerProfile.Address.StreetOne,
                Line2 = payerProfile.Address.StreetTwo,
                City = payerProfile.Address.City,
                State = payerProfile.Address.Region.Name,
                PostalCode = payerProfile.Address.ZipCode,
                Country = payerProfile.Address.Country.Name
            };
        }

        var service = new CustomerService();
        var response = await service.CreateAsync(opts);

        return response.Id;
    }

    public async Task<bool> UpdatePayerAsync(TPayerProfile payerProfile)
    {
        var opts = new CustomerUpdateOptions()
        {
            Description = payerProfile.IsOrganization
                ? string.Format(payOpts.OrgPayerDescription, payerProfile.OrganizationName, payerProfile.FullName)
                : string.Format(payOpts.PersonPayerDescription, payerProfile.FullName),
            Name = payerProfile.FullName,
            BusinessName = payerProfile.OrganizationName,
            Email = payerProfile.EmailAddress
        };


        if (payerProfile.Address != null)
        {
            opts.Address = new AddressOptions()
            {
                Line1 = payerProfile.Address.StreetOne,
                Line2 = payerProfile.Address.StreetTwo,
                City = payerProfile.Address.City,
                State = payerProfile.Address.Region.Name,
                PostalCode = payerProfile.Address.ZipCode,
                Country = payerProfile.Address.Country.Name
            };
        }

        var service = new CustomerService();
        var response = await service.UpdateAsync(payerProfile.PayerId, opts);

        return response != null;
    }

    public async Task<bool> DeletePayerAsync(string payerId)
    {
        var service = new CustomerService();

        var response = await service.DeleteAsync(payerId);

        return response.Deleted!.Value;
    }

    #endregion

    #region Credit Card Management

    public async Task<PaymentMethod<TUserId>> CreateCreditCardASync(TUserId userId, string payerId, string source)
    {
        var options = new CardCreateOptions()
        {
            Source = source
        };

        var service = new CardService();
        var response = await service.CreateAsync(userId.ToString(), options);

        return new PaymentMethod<TUserId>()
        {
            UserProfileId = userId,
            CardBrand = response.Brand,
            CardId = response.Id,
            LastFourDigits = response.Last4,
            FingerPrint = $"{userId}-{response.Fingerprint}",
            ExpirationMonth = (int)response.ExpMonth,
            ExpirationYear = (int)response.ExpYear
        };
    }

    public async Task<bool> UpdateCreditCardExpirationDateAsyc(string payerId, string cardId, int expirationMonth, int expirationYear)
    {
        var options = new CardUpdateOptions()
        {
            ExpMonth = expirationMonth,
            ExpYear = expirationYear
        };

        var service = new CardService();

        var response = await service.UpdateAsync(payerId, cardId, options);

        return response != null;
    }

    public async Task<bool> DeleteCreditCardAsync(string payerId, string cardId)
    {
        var service = new CardService();

        var response = await service.DeleteAsync(payerId, cardId);

        return response.Deleted!.Value;
    }

    #endregion
}
