using MongoDB.Driver;
using SmartProxyV2_4._6._2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_4._6._2
{
    public class ProxyPortStore
    {
        private const string _collectionName = "ProxyPort";
        private static IMongoCollection<PortMongoModel> _collection;
        internal static IMongoCollection<PortMongoModel> Collection
        {
            get
            {
                if( _collection == null )
                {
                    _collection = MongoConnector.GetCollection<PortMongoModel>(_collectionName);
                }
                return _collection;
            }
        }

        public static async Task InsertProxyPortAsync(ProxyPort proxyPort)
        {
            if (!proxyPort.PortExists())
            {
                PortMongoModel portMongoModel = new PortMongoModel()
                {
                    IsUse = false,
                    Port = proxyPort.PortNum,
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
