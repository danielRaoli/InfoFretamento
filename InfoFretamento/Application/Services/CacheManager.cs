using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace InfoFretamento.Application.Services
{
    public class CacheManager(IMemoryCache cache)
    {
        private readonly IMemoryCache _cache = cache;
        private readonly ConcurrentDictionary<string, byte> _cacheKeys = new();

        public void AddKey(string key)
        {
            _cacheKeys.TryAdd(key, 0);
        }

        public void ClearAll(string cacheKey)
        {
            var keysToRemove = _cacheKeys.Keys.Where(k => k.Contains(cacheKey)).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _cacheKeys.TryRemove(key, out _);
            }
        }
    }
}
