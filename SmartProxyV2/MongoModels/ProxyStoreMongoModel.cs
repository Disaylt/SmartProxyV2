using MongoDB.Bson;
using SmartProxyV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class ProxyStoreMongoModel : ProxyDataModel, IMongoCollectionData
    {

        public string ProxyName { get; set; }
        public BsonObjectId _id { get; set; }

        internal ProxyStoreMongoModel() { }
        internal ProxyStoreMongoModel(ProxyDataModel proxyData) : base(proxyData)
        {

        }
    }
}
