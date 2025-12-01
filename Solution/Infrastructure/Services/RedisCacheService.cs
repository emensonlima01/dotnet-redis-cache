using Domain.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var value = await _database.StringGetAsync(key);

        if (!value.HasValue)
            return null;

        return JsonSerializer.Deserialize<T>(value.ToString(), _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        var serializedValue = JsonSerializer.Serialize(value, _jsonOptions);

        if (expiration.HasValue)
            await _database.StringSetAsync(key, serializedValue, expiration.Value);
        else
            await _database.StringSetAsync(key, serializedValue);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }
}
