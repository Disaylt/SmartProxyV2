using SmartProxyV2_4._6._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_4._6._2
{
    public static class SmartProxyHandler
    {
        private readonly static Random _random;

        static SmartProxyHandler()
        {
            _random = new Random();
        }

        public static async Task OpenPortAsync(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPortAsync(false);
        }

        public static async Task ClosePortAsync(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPortAsync(true);
        }

        public static async Task<ProxyModel> GetCustomProxyAsync(string proxyName)
        {
            try
            {
                var proxy = await ProxyUrlStore.GetProxyDataAsync(proxyName);
                proxy.PortData = GetDataPort(proxy.Type);
                if (proxy.PortData != null)
                {
                    await ClosePortAsync(proxy.PortData);
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

        public static async Task<ProxyModel> GetRussianProxyAsync()
        {
            var proxy = await GetCustomProxyAsync("russian");
            return proxy;
        }

        public static async Task<ProxyModel> GetMoscowProxyAsync()
        {
            var proxy = await GetCustomProxyAsync("moscow");
            return proxy;
        }

        public static async Task<ProxyModel> GetMoscowProxyWithRussianPortAsync()
        {
            SmartProxyPortTypeSettingsStore smartProxyPortTypeSettingsStore = new SmartProxyPortTypeSettingsStore("russian");
            ProxyModel proxy = await ProxyUrlStore.GetProxyDataAsync("russian");
            for (int attempt = 0; attempt < 100; attempt++)
            {
                var settingPort = await smartProxyPortTypeSettingsStore.GetSettingsPortAsync();
                ProxyPort proxyPort = new ProxyPort(settingPort.PortType, settingPort.LastUsePort);
                proxy.PortData = proxyPort;
                ProxyCityChecker proxyCityChecker = new ProxyCityChecker(proxy);
                if (await proxyCityChecker.ProxyFromCityAsync("moscow"))
                {
                    await ClosePortAsync(proxy.PortData);
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
