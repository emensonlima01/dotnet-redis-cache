namespace Application.DTOs;

public record ReceivePaymentRequest(
    decimal Amount,
    string Description
);
