using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    internal static class ProxyPortStore
    {
        private const string _collectionName = "ProxyPort";
        public static IMongoCollection<PortMongoModel> Collection { get; }

        static ProxyPortStore()
        {
            Collection = MongoConnector.GetCollection<PortMongoModel>(_collectionName);
        }
    }
}
