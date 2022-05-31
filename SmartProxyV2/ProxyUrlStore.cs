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
    public class ProxyUrlStore
    {
        private const string _collectionName = "ProxyStore";
        private static IMongoCollection<ProxyMongoModel> _collection;
        internal static IMongoCollection<ProxyMongoModel> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = MongoConnector.GetCollection<ProxyMongoModel>(_collectionName);
                }
                return _collection;
            }
        }

        internal static async Task<ProxyMongoModel> GetProxyData(string proxyName)
        {
            try
            {
                var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
                var ProxyDataModel = await Collection.Find(filter).FirstOrDefaultAsync();
                return ProxyDataModel;
            }
            catch
            {
                return null;
            }
        }

        public static async Task InsertProxyData(string proxyName, string proxyType, ProxyModel proxyData)
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
