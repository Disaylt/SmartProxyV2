using MongoDB.Bson;
using SmartProxyV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class ProxyMongoModel : ProxyDataModel, IMongoCollectionData
    {

        public string ProxyName { get; set; }
        public string Type { get; set; }
        public BsonObjectId _id { get; set; }

        internal ProxyMongoModel() { }
        internal ProxyMongoModel(ProxyDataModel proxyData) : base(proxyData)
        {

        }
    }
}
