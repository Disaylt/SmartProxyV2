using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using SmartProxyV2_4._6._2.JsonModels;
using SmartProxyV2_4._6._2.Models;
using SmartProxyV2_4._6._2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_4._6._2
{
    internal class ProxyCityChecker : ProxyChecker
    {
        internal ProxyCityChecker(ProxyModel proxy) : base(proxy)
        {

        }

        internal async Task<bool> ProxyFromCityAsync(string city)
        {
            ProxyJsonModel proxyInfo = await GetProxyInfoAsync();
            if (proxyInfo.City.ToLower() == city.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
