using Alex.Model;
using System.Web.Http;
using System.Runtime.Caching;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Alex.Application.Controllers
{
    public class GiphyController : ApiController
    {
        private readonly IService _service;
        private readonly IInMemoryCache _cache;

        public GiphyController(IService service, IInMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        
        [HttpGet]
        public List<String> Trending()
        {            
            var gifsUrls = _cache.GetOrFetch("key.trending.gifs", () => _service.Trending().Result);         
            return gifsUrls;
        }
        [HttpGet]
        public List<String> Search(string criteria)
        {          
            var gifsUrls = _cache.GetOrFetch("key.search.gifs." + criteria, () => _service.Search(criteria).Result);
            return gifsUrls;
        }
    }
}