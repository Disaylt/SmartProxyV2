using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public static class SmartProxyHandler
    {
        private readonly static ProxyPortController _portStore;
        private readonly static ProxyStore _proxyStore;

        static SmartProxyHandler()
        {
            _portStore = new ProxyPortController();
            _proxyStore = new ProxyStore();
        }

        public static void ClosePort(int port, string type)
        {
            
        }
    }
}
