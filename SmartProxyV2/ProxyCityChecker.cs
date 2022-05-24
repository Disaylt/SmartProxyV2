using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using SmartProxyV2.JsonModels;
using SmartProxyV2.Models;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    internal class ProxyCityChecker
    {
        protected readonly ProxyModel ProxyModel;

        internal ProxyCityChecker(ProxyModel proxy)
        {
            ProxyModel = proxy;
        }

        internal async Task<bool> ProxyFromCity(string city)
        {
            ProxyJsonModel proxyInfo = await GetProxyInfo();
        }

        private async Task<ProxyJsonModel> GetProxyInfo()
        {
            HttpClientHandler httpMessageHandler = new HttpClientHandler();
            using (var httpClient = new HttpClient(httpMessageHandler))
            {
                
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "http://ip-api.com/json"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0");
                    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var response = await httpClient.SendAsync(request).Result.Content.ReadAsStringAsync();
                    ProxyJsonModel proxyInfo = JToken.Parse(response).ToObject<ProxyJsonModel>();
                    return proxyInfo;
                }
            }
        }
    }
}
