using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    public class PortMongoModel : IMongoCollectionData
    {
        public string Type { get; set; }
        public int Port { get; set; }
        public bool IsUse { get; set; }
        public DateTime LastUse { get; set; }
        [BsonId]
        public BsonObjectId Id { get; set; }
    }
}
