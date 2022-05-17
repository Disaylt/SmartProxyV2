using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.MongoModels;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SmartProxyV2
{
    internal class PortStore
    {
        private const string _collectionName = "ProxyPort";
        private readonly static IMongoCollection<PortMongoModel> _collection;

        static PortStore()
        {
            _collection = MongoConnector.GetCollection<PortMongoModel>(_collectionName);
        }

        static List<PortMongoModel> GetAvailablePorxyPorts(string type)
        {
            var filterBuilder = Builders<PortMongoModel>.Filter;
            var filter = filterBuilder.Eq("Type", type) & filterBuilder.Eq("IsUse", false);
            var ProxyDataModel =  _collection.Find(filter).ToList();
            return ProxyDataModel;
        }
    }
}
