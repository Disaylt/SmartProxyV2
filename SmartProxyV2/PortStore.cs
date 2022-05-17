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
        private readonly IMongoCollection<PortMongoModel> _collection;

        internal PortStore()
        {
            _collection = MongoConnector.GetCollection<PortMongoModel>(_collectionName);
        }

        internal List<PortMongoModel> GetAvailablePorxyPorts(string type)
        {
            var filterBuilder = Builders<PortMongoModel>.Filter;
            var filter = filterBuilder.Eq("Type", type) & filterBuilder.Eq("IsUse", false);
            var ProxyDataModel =  _collection.Find(filter).ToList();
            return ProxyDataModel;
        }
    }
}
