using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public static class SmartProxyHandler
    {
        private readonly static ProxyPort _portStore;
        private readonly static ProxyUrlStore _proxyStore;

        static SmartProxyHandler()
        {
            _proxyStore = new ProxyUrlStore();
        }

        public static void ClosePort()
        {
            
        }
    }
}
