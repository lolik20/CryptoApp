using CryptoCalculator.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;

namespace CryptoCalculator.Services
{
    public class XeService : IXeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationSection _urls;
        public XeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urls = configuration.GetSection("Xe:Urls");

        }

        public async Task<decimal> GetRate(string from, string to)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            Dictionary<string, string> queryString = new Dictionary<string, string> {
                { "currencyPairs",$"{from}/{to}" },
                };
            string path = _urls.GetValue<string>("GetRate") ?? throw new Exception("GetRate path missing");
            string url = QueryHelpers.AddQueryString(path, queryString);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                List<XeRate> rate = JsonConvert.DeserializeObject<List<XeRate>>(responseString) ?? throw new Exception("Error while parsing xe response");
                var rateObject= rate.FirstOrDefault() ?? throw new Exception("No rate in collection XE");
                
                decimal resultRate = rateObject.rate;
                if (resultRate == 0m)
                {
                    throw new Exception("Rate is 0");
                }
                return resultRate;
            }

            throw new Exception($"Xe service response is {response.StatusCode}");
        }
        private class XeRate
        {
            public decimal rate { get; set; }

        }
    }
}
