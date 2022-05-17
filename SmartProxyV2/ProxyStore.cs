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
    internal static class ProxyStore
    {
        private const string collectionName = "ProxyStore";
        internal async static Task<ProxyDataModel> GetProxyData(string proxyName)
        {
            var collection = MongoConnector.GetCollection<ProxyStoreMongoModel>(collectionName);
            var filter = Builders<ProxyStoreMongoModel>.Filter.Eq("ProxyName", proxyName);
            var ProxyDataModel = await collection.Find(filter).FirstOrDefaultAsync();
            return ProxyDataModel;
        }
    }
}
