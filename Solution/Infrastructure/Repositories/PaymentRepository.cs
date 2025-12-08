using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Repositories;

public class PaymentRepository(ICacheService cacheService) : IPaymentRepository
{
    private const string CacheKeyPrefix = "payment:";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

    public async Task<Payment> SaveAsync(Payment payment)
    {
        var cacheKey = GetCacheKey(payment.Id);
        await cacheService.SetAsync(cacheKey, payment, CacheExpiration);
        return payment;
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        var cacheKey = GetCacheKey(id);
        return await cacheService.GetAsync<Payment>(cacheKey);
    }

    public async Task<Payment> UpdateAsync(Payment payment)
    {
        var cacheKey = GetCacheKey(payment.Id);
        await cacheService.SetAsync(cacheKey, payment, CacheExpiration);
        return payment;
    }

    private static string GetCacheKey(Guid id) => $"{CacheKeyPrefix}{id}";
}
