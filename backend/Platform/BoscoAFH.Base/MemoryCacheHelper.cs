using Microsoft.Extensions.Caching.Memory;

namespace BoscoAFH.Base
{
    public interface IInMemoryCacheHelper
    {
        T GetOrSet<T>(string key, Func<T> fetchData, TimeSpan? expiration = null);

        void Set<T>(string key, T data, TimeSpan? expiration = null);

        bool TryGet<T>(string key, out T? data);

        void Remove(string key);
    }

    public class InMemoryCacheHelper(IMemoryCache memoryCache): IInMemoryCacheHelper
    {
        private readonly IMemoryCache _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        public T GetOrSet<T>(string key, Func<T> fetchData, TimeSpan? expiration = null)
        {
            if (_memoryCache.TryGetValue<T>(key, out var cached))
            {
                return cached;
            }

            var data = fetchData();
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60)
            };

            _memoryCache.Set(key, data, options);
            return data;
        }

        public void Set<T>(string key, T data, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60)
            };

            _memoryCache.Set(key, data, options);
        }

        public bool TryGet<T>(string key, out T? data)
        {
            return _memoryCache.TryGetValue(key, out data);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
