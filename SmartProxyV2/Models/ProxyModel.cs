using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.Models
{
    public class ProxyModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }

        public ProxyModel() { }
        public ProxyModel(ProxyModel proxyData)
        {
            User = proxyData.User;
            Password = proxyData.Password;
            Ip = proxyData.Ip;
            Port = proxyData.Port;
        }
    }
}
