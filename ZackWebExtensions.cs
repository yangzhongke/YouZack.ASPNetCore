using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Memory
{
    public static class ZackWebExtensions
    {
        public static async Task<T> GetFromCacheAsync<T>(this IMemoryCache cache, string key, int cacheSeconds, Func<Task<T>> getDataFunc) where T : class
        {
            T value = cache.Get<T>(key);
            if (value == null)
            {
                Random rand = new Random();
                TimeSpan tsCache = TimeSpan.FromSeconds(cacheSeconds + rand.NextDouble() * 10);//避免缓存刷新集中
                value = await getDataFunc();
                cache.Set(key, value, tsCache);
            }
            return value;
        }
    }
}
