using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;

namespace WebApi.Endpoints;

public static class PaymentsEndpoints
{
    public static void MapPaymentsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/payments")
            .WithTags("Payments");

        group.MapPost("/receive", ReceivePayment)
            .WithName("ReceivePayment");

        group.MapPut("/{id}/cancel", CancelPayment)
            .WithName("CancelPayment");
    }

    private static async Task<IResult> ReceivePayment(
        [FromBody] ReceivePaymentRequest request,
        [FromServices] ReceivePaymentUseCase useCase)
    {
        await useCase.Handle(request);
        return Results.Accepted();
    }

    private static async Task<IResult> CancelPayment(
        [FromRoute] Guid id,
        [FromServices] CancelPaymentUseCase useCase)
    {
        return Results.Accepted();
    }
}
