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
    public class ProxyPortController
    {
        public string Type { get; }
        public int Port { get; }
        private readonly FilterDefinition<PortMongoModel> _mainFilter;

        public ProxyPortController(string type, int port)
        {
            Type = type;
            Port = port;
            var filterBuilder = Builders<PortMongoModel>.Filter;
            _mainFilter = filterBuilder.Eq("Port", port) & filterBuilder.Eq("Type", type);
        }

        internal List<PortMongoModel> GetAvailablePorxyPorts()
        {
            var filterBuilder = Builders<PortMongoModel>.Filter;
            var filter = filterBuilder.Eq("Type", Type) & filterBuilder.Eq("IsUse", false);
            var ProxyDataModel =  ProxyPortStore.Collection.Find(filter).ToList();
            return ProxyDataModel;
        }

        internal async Task UpdateUseStatusPort(bool useStatus)
        {
            BsonObjectId id = await GetFirstObjectId();
            await ProxyPortStore.Collection.UpdateOneAsync(
                new BsonDocument("_id", id),
                new BsonDocument("IsUse", useStatus));
        }

        public async Task InsertProxyPort()
        {
            if (!ExistsDataPort())
            {
                PortMongoModel portMongoModel = new PortMongoModel()
                {
                    IsUse = false,
                    Port = Port,
                    Type = Type,
                    LastUse = DateTime.Now
                };
                await ProxyPortStore.Collection.InsertOneAsync(portMongoModel);
            }
        }

        private bool ExistsDataPort()
        {
            var isExist = ProxyPortStore.Collection
                .Find(_mainFilter)
                .CountDocuments() > 0 
                ? true : false;
            return isExist;
        }


        private async Task<BsonObjectId> GetFirstObjectId()
        {
            var bsonDocument = await ProxyPortStore.Collection
                .Find(_mainFilter)
                .FirstOrDefaultAsync();
            BsonObjectId bsonObjectId = bsonDocument._id;
            return bsonObjectId;
        }
    }
}
