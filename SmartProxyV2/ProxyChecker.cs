using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    internal class ProxyChecker
    {
        private readonly int _port;

        internal ProxyChecker(int port)
        {
            _port = port;
        }

        internal async Task<bool> ProxyFromCity(string city)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "http://ip-api.com/json"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0");
                    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var response = await httpClient.SendAsync(request);
                }
            }
        }
    }
}
