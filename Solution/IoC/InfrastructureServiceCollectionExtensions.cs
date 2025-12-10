using Domain.Repositories;
using Domain.Services;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IoC;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRedisCache(configuration)
            .AddRepositories()
            .AddDomainServices();

        return services;
    }

    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisSettings>(configuration.GetSection(RedisSettings.SectionName));

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisSettings = configuration.GetSection(RedisSettings.SectionName).Get<RedisSettings>()
                ?? throw new InvalidOperationException("Redis configuration not found");

            if (string.IsNullOrWhiteSpace(redisSettings.ConnectionString))
                throw new InvalidOperationException("Redis connection string not found");

            return ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
        });

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentRepository, PaymentRepository>();

        return services;
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}
