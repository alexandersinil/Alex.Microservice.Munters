using Alex.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Alex.Application
{
    public class InMemoryCache : IInMemoryCache
    {
        private readonly ObjectCache _cache;
        private readonly IService _service;

        public ObjectCache MemoryCache { get; set; }
        public IService Service { get; set; }

        public InMemoryCache(IService service, ObjectCache cache)
        {
            _service = service;
            _cache = cache;
        }
        public List<string> GetOrFetch(string key, Func<string> action)
        {
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(86400.0)
            };
            string trendingGifs = (string)_cache.Get(key);
            if (string.IsNullOrEmpty(trendingGifs))
            {
                trendingGifs = action();
            }
            _cache.AddOrGetExisting(key, trendingGifs, cacheItemPolicy);

            var gifsUrls = JObject.Parse(trendingGifs)
                .SelectTokens("$..original.url")
                .Select(x => (string)x)
                .ToList();

            return gifsUrls;
        }
    }
    public interface IInMemoryCache 
    {
        List<string> GetOrFetch(string key, Func<string> action);
    }
}
