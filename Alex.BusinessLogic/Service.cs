using Alex.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alex.BusinessLogic
{
    public class Service : IService
    {
        private readonly HttpClient _githyHttpClient;
        private readonly string _api_key;

        public Service(HttpClient githyHttpClient)
        {
            _githyHttpClient = githyHttpClient;
            
            _api_key = System.Configuration.ConfigurationManager.AppSettings["api.key"];
        }
        
        public async Task<string> Search(string criteria)
        {
            string responseBody = await _githyHttpClient.GetStringAsync("v1/gifs/search?api_key=" + _api_key  + "&q=" + criteria + "&limit=25&offset=0&rating=g&lang=en");

            return responseBody;
        }

        public async Task<string> Trending()
        {
            var responseBody = await _githyHttpClient.GetStringAsync("v1/gifs/trending?api_key=" + _api_key + "&limit=25&rating=g");
         
            return responseBody;
        }
    }
}