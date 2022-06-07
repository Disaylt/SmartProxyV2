using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_4._6._2.Models
{
    public class ProxyModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public ProxyPort PortData { get; set; }

        internal ProxyModel() { }
        public ProxyModel(string user, string password, string ip)
        {
            User = user;
            Password = password;
            Ip = ip;
        }
        internal ProxyModel(ProxyModel proxyData) : this(proxyData.User, proxyData.Password, proxyData.Ip)
        {
            PortData = proxyData.PortData;
        }
    }
}
