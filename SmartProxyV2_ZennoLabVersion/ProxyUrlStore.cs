using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SmartProxyV2_ZennoLabVersion.Models;
using SmartProxyV2_ZennoLabVersion.MongoModels;

namespace SmartProxyV2_ZennoLabVersion
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

        internal static ProxyMongoModel GetProxyData(string proxyName)
        {
            try
            {
                var filter = Builders<ProxyMongoModel>.Filter.Eq("ProxyName", proxyName);
                var ProxyDataModel = Collection.Find(filter).FirstOrDefault();
                return ProxyDataModel;
            }
            catch
            {
                return null;
            }
        }

        internal static async Task<ProxyMongoModel> GetProxyDataAsync(string proxyName)
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

        public static async Task InsertProxyDataAsync(string proxyName, string proxyType, ProxyModel proxyData)
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
