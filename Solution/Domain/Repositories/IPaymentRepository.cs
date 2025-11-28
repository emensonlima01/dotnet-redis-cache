using Domain.Entities;

namespace Domain.Repositories;

public interface IPaymentRepository
{
    Task<Payment> SaveAsync(Payment payment);
    Task<Payment?> GetByIdAsync(Guid id);
    Task<Payment> UpdateAsync(Payment payment);
}
