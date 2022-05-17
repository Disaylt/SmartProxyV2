using SmartProxyV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class ProxyStoreMongoModel : ProxyDataModel
    {
        internal string ProxyName { get; set; }

        internal ProxyStoreMongoModel() { }
        internal ProxyStoreMongoModel(ProxyDataModel proxyData) : base(proxyData)
        {

        }
    }
}
