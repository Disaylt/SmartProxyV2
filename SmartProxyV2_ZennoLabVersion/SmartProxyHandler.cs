using SmartProxyV2_ZennoLabVersion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_ZennoLabVersion
{
    public static class SmartProxyHandler
    {
        private readonly static Random _random;

        static SmartProxyHandler()
        {
            _random = new Random();
        }

        public static void OpenPort(ProxyPort proxyPort)
        {
            proxyPort.UpdateUseStatusPort(false);
        }

        public static async Task OpenPortAsync(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPortAsync(false);
        }

        public static void ClosePort(ProxyPort proxyPort)
        {
            proxyPort.UpdateUseStatusPort(true);
        }

        public static async Task ClosePortAsync(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPortAsync(true);
        }

        public static ProxyModel GetCustomProxy(string proxyName)
        {
            try
            {
                var proxy = ProxyUrlStore.GetProxyData(proxyName);
                proxy.PortData = GetDataPort(proxy.Type);
                if (proxy.PortData != null)
                {
                    ClosePort(proxy.PortData);
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

        public static ProxyModel GetRussianProxy()
        {
            var proxy = GetCustomProxy("russian");
            return proxy;
        }

        public static async Task<ProxyModel> GetRussianProxyAsync()
        {
            var proxy = await GetCustomProxyAsync("russian");
            return proxy;
        }

        public static ProxyModel GetMoscowProxy()
        {
            var proxy = GetCustomProxy("moscow");
            return proxy;
        }

        public static async Task<ProxyModel> GetMoscowProxyAsync()
        {
            var proxy = await GetCustomProxyAsync("moscow");
            return proxy;
        }

        public static ProxyModel GetMoscowProxyWithRussianPort()
        {
            SmartProxyPortTypeSettingsStore smartProxyPortTypeSettingsStore = new SmartProxyPortTypeSettingsStore("russian");
            ProxyModel proxy = ProxyUrlStore.GetProxyData("russian");
            for (int attempt = 0; attempt < 100; attempt++)
            {
                var settingPort = smartProxyPortTypeSettingsStore.GetSettingsPort();
                ProxyPort proxyPort = new ProxyPort(settingPort.PortType, settingPort.LastUsePort);
                proxy.PortData = proxyPort;
                ProxyCityChecker proxyCityChecker = new ProxyCityChecker(proxy);
                if (proxyCityChecker.ProxyFromCity("moscow"))
                {
                    ClosePort(proxy.PortData);
                    return proxy;
                }
            };
            return null;
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
