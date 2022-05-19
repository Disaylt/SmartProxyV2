﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.MongoModels;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SmartProxyV2
{
    public class ProxyPort
    {
        public string Type { get; }
        public int Port { get; }
        private readonly FilterDefinition<PortMongoModel> _mainFilter;

        public ProxyPort(string type, int port)
        {
            Type = type;
            Port = port;
            var filterBuilder = Builders<PortMongoModel>.Filter;
            _mainFilter = filterBuilder.Eq("Port", port) & filterBuilder.Eq("Type", type);
        }

        internal bool PortExists()
        {
            var isExist = ProxyPortStore.Collection
                .Find(_mainFilter)
                .CountDocuments() > 0
                ? true : false;
            return isExist;
        }

        internal async Task UpdateUseStatusPort(bool useStatus)
        {
            BsonObjectId id = await GetFirstObjectId();
            if(id != null)
            {
                await ProxyPortStore.Collection.UpdateOneAsync(
                    new BsonDocument("_id", id),
                    new BsonDocument("IsUse", useStatus));
            }
        }

        protected async Task<BsonObjectId> GetFirstObjectId()
        {
            var bsonDocument = await ProxyPortStore.Collection
                .Find(_mainFilter)
                .FirstOrDefaultAsync();
            BsonObjectId bsonObjectId = bsonDocument._id;
            return bsonObjectId;
        }
    }
}
