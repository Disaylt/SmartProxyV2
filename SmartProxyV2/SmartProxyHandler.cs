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
            await proxyPort.UpdateUseStatusPort(false);
        }

        public static async Task ClosePort(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPort(true);
        }

        public static async Task<ProxyModel> GetCustomProxy(string proxyName)
        {
            var proxy = await ProxyUrlStore.GetProxyData(proxyName);
            proxy.Port = GetDataPort(proxy.Type);
            if(proxy.Port != null)
            {
                await ClosePort(proxy.Port);
                return proxy;
            }
            else
            {
                return null;
            }
        }

        private static ProxyPort GetDataPort(string type)
        {
            var ports = ProxyPortStore.GetAvailablePorxyPorts(type);
            if(ports.Count > 0)
            {
                int indexSelect = _random.Next(0, ports.Count);
                return ports[indexSelect];
            }
            else
            {
                return null;
            }
        }
    }
}
