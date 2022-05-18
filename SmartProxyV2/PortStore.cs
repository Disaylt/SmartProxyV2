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
    public class PortStore
    {
        private const string _collectionName = "ProxyPort";
        private readonly IMongoCollection<PortMongoModel> _collection;

        public PortStore()
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

        public async Task InsertNewProxyPort(string type, int port)
        {
            if (!PortExists(port))
            {
                PortMongoModel portMongoModel = new PortMongoModel()
                {
                    IsUse = false,
                    Port = port,
                    Type = type,
                    LastUse = DateTime.Now
                };
                await _collection.InsertOneAsync(portMongoModel);
            }
        }

        private bool PortExists(int port)
        {
            var filterBuilder = Builders<PortMongoModel>.Filter;
            var filter = filterBuilder.Eq("Port", port);
            var isExist = _collection.Find(filter).CountDocuments() > 0 ? true : false;
            return isExist;
        }
    }
}
