﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2_ZennoLabVersion.MongoModels;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SmartProxyV2_ZennoLabVersion
{
    public class ProxyPort
    {
        public string Type { get; }
        public int PortNum { get; }
        private readonly FilterDefinition<PortMongoModel> _mainFilter;

        public ProxyPort(string type, int port)
        {
            Type = type;
            PortNum = port;
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

        internal void UpdateUseStatusPort(bool useStatus)
        {
            BsonObjectId id = GetFirstObjectId();
            if (id != null)
            {
                var updater = Builders<PortMongoModel>.Update.Set("IsUse", useStatus);
                ProxyPortStore.Collection.UpdateOne(
                    new BsonDocument("_id", id), updater);
                UpdateUseDateTime();
            }
        }

        internal async Task UpdateUseStatusPortAsync(bool useStatus)
        {
            BsonObjectId id = await GetFirstObjectIdAsync();
            if (id != null)
            {
                var updater = Builders<PortMongoModel>.Update.Set("IsUse", useStatus);
                await ProxyPortStore.Collection.UpdateOneAsync(
                    new BsonDocument("_id", id), updater);
                await UpdateUseDateTimeAsync();
            }
        }

        internal void UpdateUseDateTime()
        {
            BsonObjectId id = GetFirstObjectId();
            if (id != null)
            {
                var updater = Builders<PortMongoModel>.Update.Set("LastUse", DateTime.Now);
                ProxyPortStore.Collection.UpdateOne(
                    new BsonDocument("_id", id), updater);
            }
        }

        internal async Task UpdateUseDateTimeAsync()
        {
            BsonObjectId id = await GetFirstObjectIdAsync();
            if (id != null)
            {
                var updater = Builders<PortMongoModel>.Update.Set("LastUse", DateTime.Now);
                await ProxyPortStore.Collection.UpdateOneAsync(
                    new BsonDocument("_id", id), updater);
            }
        }

        protected BsonObjectId GetFirstObjectId()
        {
            var bsonDocument = ProxyPortStore.Collection
                .Find(_mainFilter)
                .FirstOrDefault();
            BsonObjectId bsonObjectId = bsonDocument.Id;
            return bsonObjectId;
        }

        protected async Task<BsonObjectId> GetFirstObjectIdAsync()
        {
            var bsonDocument = await ProxyPortStore.Collection
                .Find(_mainFilter)
                .FirstOrDefaultAsync();
            BsonObjectId bsonObjectId = bsonDocument.Id;
            return bsonObjectId;
        }

        public static implicit operator ProxyPort(PortMongoModel portMongoModel)
        {
            ProxyPort proxyPort = new ProxyPort(portMongoModel.Type, portMongoModel.Port);
            return proxyPort;
        }
    }
}
