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
            try
            {
                var proxy = await ProxyUrlStore.GetProxyData(proxyName);
                proxy.PortData = GetDataPort(proxy.Type);
                if (proxy.PortData != null)
                {
                    await ClosePort(proxy.PortData);
                    return proxy;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<ProxyModel> GetRussianProxy()
        {
            var proxy = await GetCustomProxy("russian");
            return proxy;
        }

        public static async Task<ProxyModel> GetMoscowProxy()
        {
            var proxy = await GetCustomProxy("moscow");
            return proxy;
        }

        public static async Task<ProxyModel> GetMoscowProxyWithRussianPort()
        {
            SmartProxyPortTypeSettingsStore smartProxyPortTypeSettingsStore = new SmartProxyPortTypeSettingsStore("russian");
            ProxyModel proxy = await ProxyUrlStore.GetProxyData("russian");
            for (int attempt = 0; attempt < 100; attempt++)
            {
                var settingPort = await smartProxyPortTypeSettingsStore.GetSettingsPort();
                ProxyPort proxyPort = new ProxyPort(settingPort.PortType, settingPort.LastUsePort);
                proxy.PortData = proxyPort;
                ProxyCityChecker proxyCityChecker = new ProxyCityChecker(proxy);
                if (await proxyCityChecker.ProxyFromCity("moscow"))
                {
                    await ClosePort(proxy.PortData);
                    return proxy;
                }
            };
            return null;

        }

        private static ProxyPort GetDataPort(string type)
        {
            try
            {
                var ports = ProxyPortStore.GetAvailablePorxyPorts(type);
                if (ports.Count > 0)
                {
                    int indexSelect = _random.Next(0, ports.Count);
                    return ports[indexSelect];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
