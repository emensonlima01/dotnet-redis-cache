using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases;

public class ReceivePaymentUseCase(IPaymentRepository paymentRepository)
{
    public async Task Handle(ReceivePaymentRequest request)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Status = PaymentStatus.Received
        };

        await paymentRepository.SaveAsync(payment);
    }
}
