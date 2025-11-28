using Application.UseCases;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ReceivePaymentUseCase>();
        services.AddScoped<CancelPaymentUseCase>();

        return services;
    }
}
