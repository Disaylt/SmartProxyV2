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
    }
}
