using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public static class SmartProxyHandler
    {
        public static async Task ClosePort(ProxyPort proxyPort)
        {
            await proxyPort.UpdateUseStatusPort(false);
        }

        public static async Task<> GetCustomProxy(string proxyName)
        {
            
        }
    }
}
