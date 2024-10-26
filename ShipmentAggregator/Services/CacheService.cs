namespace ShipmentAggregator.Services;
public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CachedKeys = new();

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);
        return cachedValue == null ? null : JsonConvert.DeserializeObject<T>(cachedValue);
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options,
        CancellationToken cancellationToken = default) where T : class
    {
        var serializedValue = JsonConvert.SerializeObject(value);

        var encodedValue = Encoding.UTF8.GetBytes(serializedValue);
        await distributedCache.SetAsync(key, encodedValue, options, cancellationToken);
        CachedKeys.TryAdd(key, false);
    }
}