using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.Models;
using SmartProxyV2;

namespace TestLib
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ProxyDataModel proxyDataModel = new ProxyDataModel
            {
                Ip = "IpTest",
                Port = "PortTest",
                Password = "PassTest",
                User = "UserTest"
            };
            string proxyName = "testProxy";
            await ProxyStore.AddProxyData(proxyName, proxyDataModel);
            Console.ReadLine();
        }
    }
}
