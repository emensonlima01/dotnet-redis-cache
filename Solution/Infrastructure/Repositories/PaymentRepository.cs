using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private static readonly Dictionary<Guid, Payment> _payments = new();

    public Task<Payment> SaveAsync(Payment payment)
    {
        _payments[payment.Id] = payment;
        return Task.FromResult(payment);
    }

    public Task<Payment?> GetByIdAsync(Guid id)
    {
        _payments.TryGetValue(id, out var payment);
        return Task.FromResult(payment);
    }

    public Task<Payment> UpdateAsync(Payment payment)
    {
        _payments[payment.Id] = payment;
        return Task.FromResult(payment);
    }
}
