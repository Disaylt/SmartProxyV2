using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.Models;
using SmartProxyV2;
using SmartProxyV2.MongoModels;

namespace TestLib
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ProxySettingsMongoModel proxyMongo = new ProxySettingsMongoModel()
            {
                PortType = "russian",
                MaxPort = 49999,
                MinPort = 40001,
                LastUsePort = 40001
            };
            await SmartProxySettingsStore.AddNewSettings(proxyMongo);
        }
    }
}
