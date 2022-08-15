using Business.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //Example of distributed and in memory cache using Redis
    public class CacheExample: ICacheExample
    {
        private IDistributedCache _distributedcache;
        private IMemoryCache _memoryCache;
        public CacheExample(IDistributedCache distributedcache, IMemoryCache memoryCache)
        {
            _distributedcache = distributedcache;
            _memoryCache = memoryCache;
        }
        public void DistSetString(string key, string value)
        {
            _distributedcache.SetString(key, value);
        }

        public void DistSetList(string key)
        {
            var arrayNumber = new int[] { 1, 2, 4, 6 };
            var strArrayNumber = System.Text.Json.JsonSerializer.Serialize(arrayNumber);
            _distributedcache.SetString(key, strArrayNumber);
        }
        public string DistGetValue(string key)
        {
            return _distributedcache.GetString(key);
        }

        public void InMemSetString(string key, string value)
        {
            _memoryCache.Set(key, value);
        }

        public void InMemSetObject(string key, object value)
        {
            _memoryCache.Set(key, value);
        }

        public T InMemGetValue<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
    }
}
