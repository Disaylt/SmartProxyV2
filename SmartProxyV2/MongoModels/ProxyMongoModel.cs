using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SmartProxyV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class ProxyMongoModel : ProxyModel, IMongoCollectionData
    {

        public string ProxyName { get; set; }
        public string Type { get; set; }
        [BsonId]
        public BsonObjectId Id { get; set; }
        internal ProxyMongoModel() { }
        internal ProxyMongoModel(ProxyModel proxyData) : base(proxyData)
        {

        }
    }
}
