using Microsoft.Extensions.Caching.Distributed;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Newtonsoft.Json;

namespace Marketplace.Services.DocumentsAPI.Redis
{
    public static class CacheHelper
    {
        /// <summary>
        /// Метод записывает данные в кеш
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="recordId"></param>
        /// <param name="data"></param>
        /// <param name="absoluteExpireTime"></param>
        /// <param name="slidingExpireTime"></param>
        /// <returns></returns>
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                              string recordId,
                                              T data,
                                              TimeSpan? absoluteExpireTime = null,
                                              TimeSpan? slidingExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = slidingExpireTime
            };


            var jsonData = JsonConvert.SerializeObject(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        /// <summary>
        /// Метод возвращает данные из кеша
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache,
                                                       string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}