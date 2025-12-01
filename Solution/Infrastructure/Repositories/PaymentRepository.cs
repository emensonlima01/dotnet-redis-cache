using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Repositories;

public class PaymentRepository(ICacheService cacheService) : IPaymentRepository
{
    private static readonly Dictionary<Guid, Payment> _payments = [];
    private const string CacheKeyPrefix = "payment:";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

    public async Task<Payment> SaveAsync(Payment payment)
    {
        _payments[payment.Id] = payment;
        var cacheKey = GetCacheKey(payment.Id);
        await cacheService.SetAsync(cacheKey, payment, CacheExpiration);
        return payment;
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        var cacheKey = GetCacheKey(id);
        var cachedPayment = await cacheService.GetAsync<Payment>(cacheKey);

        if (cachedPayment != null)
            return cachedPayment;

        _payments.TryGetValue(id, out var payment);

        if (payment != null)
            await cacheService.SetAsync(cacheKey, payment, CacheExpiration);

        return payment;
    }

    public async Task<Payment> UpdateAsync(Payment payment)
    {
        _payments[payment.Id] = payment;
        var cacheKey = GetCacheKey(payment.Id);
        await cacheService.SetAsync(cacheKey, payment, CacheExpiration);
        return payment;
    }

    private static string GetCacheKey(Guid id) => $"{CacheKeyPrefix}{id}";
}
