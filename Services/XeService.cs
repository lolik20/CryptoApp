using CryptoExchange.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;

namespace CryptoExchange.Services
{
    public class XeService : IXeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationSection _urls;
        private readonly IMemoryCache _memoryCache;

        public XeService(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _urls = configuration.GetSection("Xe:Urls");
            _memoryCache = memoryCache;

        }

        public async Task<decimal> GetRate(string from, string to)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            string pair = $"{from}/{to}";
            Dictionary<string, string> queryString = new Dictionary<string, string> {
                { "currencyPairs",pair },
                };
            decimal resultRate;
            bool isCache = _memoryCache.TryGetValue(pair, out resultRate);
            if (!isCache)
            {
                string path = _urls.GetValue<string>("GetRate") ?? throw new Exception("GetRate path missing");
                string url = QueryHelpers.AddQueryString(path, queryString);
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    List<XeRate> rate = JsonConvert.DeserializeObject<List<XeRate>>(responseString) ?? throw new Exception("Error while parsing xe response");
                    var rateObject = rate.FirstOrDefault() ?? throw new Exception("No rate in collection XE");

                    resultRate = rateObject.rate;
                    if (resultRate == 0m)
                    {
                        throw new Exception("Rate is 0");
                    }
                    _memoryCache.Set(pair, resultRate,TimeSpan.FromSeconds(15));
                    return resultRate;
                }
                
                throw new Exception($"Xe service response is {response.StatusCode}");
                
            }
            return resultRate;
        }
        private class XeRate
        {
            public decimal rate { get; set; }

        }
    }
}
