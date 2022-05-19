using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public static class ProxyPortStore
    {
        private const string _collectionName = "ProxyPort";
        internal static IMongoCollection<PortMongoModel> Collection { get; }

        static ProxyPortStore()
        {
            Collection = MongoConnector.GetCollection<PortMongoModel>(_collectionName);
        }

        public static async Task InsertProxyPort(ProxyPort proxyPort)
        {
            if (!proxyPort.PortExists())
            {
                PortMongoModel portMongoModel = new PortMongoModel()
                {
                    IsUse = false,
                    Port = proxyPort.Port,
                    Type = proxyPort.Type,
                    LastUse = DateTime.Now
                };
                await Collection.InsertOneAsync(portMongoModel);
            }
        }

        internal static List<PortMongoModel> GetAvailablePorxyPorts(string type)
        {
            var filterBuilder = Builders<PortMongoModel>.Filter;
            var filter = filterBuilder.Eq("Type", type) & filterBuilder.Eq("IsUse", false);
            var ProxyDataModel = Collection.Find(filter).ToList();
            return ProxyDataModel;
        }
    }
}
