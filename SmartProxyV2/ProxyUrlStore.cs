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
    internal static class ProxyUrlStore
    {
        private const string _collectionName = "ProxyStore";
        internal readonly static IMongoCollection<ProxyMongoModel> Collection;

        static ProxyUrlStore()
        {
            Collection = MongoConnector.GetCollection<ProxyMongoModel>(_collectionName);
        }

        internal static async Task<ProxyModel> GetProxyData(string proxyName)
        {
            var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
            var ProxyDataModel = await Collection.Find(filter).FirstOrDefaultAsync();
            return ProxyDataModel;
        }

        internal static async Task AddProxyData(string proxyName, string proxyType, ProxyModel proxyData)
        {
            var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
            var proxyNameExist = Collection.Find(filter).ToList().Any(x => x.ProxyName == proxyName);
            if (!proxyNameExist)
            {
                ProxyMongoModel proxyStoreMongoModel = new ProxyMongoModel(proxyData) 
                {
                    ProxyName = proxyName,
                    Type = proxyType
                };
                await Collection.InsertOneAsync(proxyStoreMongoModel);
            }
        }
    }
}
