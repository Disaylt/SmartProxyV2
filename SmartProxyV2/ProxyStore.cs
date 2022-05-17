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
    public static class ProxyStore
    {
        private const string _collectionName = "ProxyStore";
        private readonly static IMongoCollection<MongoModel> _collection;

        static ProxyStore()
        {
            _collection = MongoConnector.GetCollection<MongoModel>(_collectionName);
        }

        public async static Task<ProxyDataModel> GetProxyData(string proxyName)
        {
            var filter = Builders<MongoModel>.Filter.Eq("ProxyName", proxyName);
            var ProxyDataModel = await _collection.Find(filter).FirstOrDefaultAsync();
            return ProxyDataModel;
        }

        public async static Task AddProxyData(string proxyName, ProxyDataModel proxyData)
        {
            var filter = Builders<MongoModel>.Filter.Eq("ProxyName", proxyName);
            var proxyNameExist = _collection.Find(filter).ToList().Any(x => x.ProxyName == proxyName);
            if (!proxyNameExist)
            {
                MongoModel proxyStoreMongoModel = new MongoModel(proxyData) { ProxyName = proxyName };
                await _collection.InsertOneAsync(proxyStoreMongoModel);
            }
        }
    }
}
