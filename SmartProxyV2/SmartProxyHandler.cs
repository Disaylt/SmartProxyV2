using SmartProxyV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public static class SmartProxyHandler
    {
        private readonly static Random _random;

        static SmartProxyHandler()
        {
            _random = new Random();
        }

        public static async Task OpenPort(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPort(true);
        }

        public static async Task ClosePort(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPort(false);
        }

        public static async Task<ProxyModel> GetCustomProxy(string proxyName)
        {
            var proxy = await ProxyUrlStore.GetProxyData(proxyName);
            proxy.Port = GetNumPort(proxyName);
            await ClosePort(proxy.Port);
            return proxy;
        }

        private static ProxyPort GetNumPort(string type)
        {
            return ProxyPortStore.GetAvailablePorxyPorts(type)
                .FirstOrDefault();
        }
    }
}
