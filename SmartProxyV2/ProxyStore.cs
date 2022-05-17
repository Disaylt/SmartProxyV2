using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SmartProxyV2.Models;
using SmartProxyV2.MongoModels;

namespace SmartProxyV2
{
    internal class ProxyStore
    {
        private const string _collectionName = "ProxyStore";
        protected readonly IMongoCollection<ProxyMongoModel> _collection;

        internal ProxyStore()
        {
            _collection = MongoConnector.GetCollection<ProxyMongoModel>(_collectionName);
        }

        internal virtual async Task<ProxyDataModel> GetProxyData(string proxyName)
        {
            var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
            var ProxyDataModel = await _collection.Find(filter).FirstOrDefaultAsync();
            return ProxyDataModel;
        }

        internal async Task AddProxyData(string proxyName, string proxyType, ProxyDataModel proxyData)
        {
            var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
            var proxyNameExist = _collection.Find(filter).ToList().Any(x => x.ProxyName == proxyName);
            if (!proxyNameExist)
            {
                ProxyMongoModel proxyStoreMongoModel = new ProxyMongoModel(proxyData) 
                {
                    ProxyName = proxyName,
                    Type = proxyType
                };
                await _collection.InsertOneAsync(proxyStoreMongoModel);
            }
        }
    }
}
