using Domain.Entities;

namespace Application.DTOs;

public record PaymentResponse(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime CreatedAt,
    PaymentStatus Status
);
