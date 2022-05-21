using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace ConversionAPI.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CacheService(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool TryGetValue<TItem>(object key, out TItem value)
        {
            try
            {
                if (_httpContextAccessor != null)
                {
                    bool.TryParse(_httpContextAccessor.HttpContext?.Request?.Query?.ToList()
                                               ?.Where(x => x.Key.ToLower() == "flush_cache")
                                               ?.Select(x => x.Value).FirstOrDefault(), out bool isFlushCache);
                    if (isFlushCache)
                        _cache.Remove(key);
                }

                lock (_cache)
                {
                    value = default;
                    return _cache.TryGetValue(key, out value);
                }
            }
            catch (Exception ex)
            {
                value = default;
                Log.Error(ex, "Error occurred in CacheService.TryGetValue while setting data to cache. Key: {key}", key);
            }

            return false;
        }

        public void Remove(object key)
        {
            _cache.Remove(key);
        }

        public void SetSlidingExpiration<TItem>(string key, TItem value, TimeSpan? slidingExpirationInMinutes = null)
        {
            try
            {
                slidingExpirationInMinutes ??= TimeSpan.FromMinutes(24 * 7 * 60);
                MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration((TimeSpan)slidingExpirationInMinutes);
                _cache.Set(key, value, memoryCacheEntryOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in CacheService.SetSlidingExpiration while setting data to cache. Key: {key}", key);
            }
        }
    }
}
