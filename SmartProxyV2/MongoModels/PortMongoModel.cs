using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class PortMongoModel : IMongoCollectionData
    {
        public BsonObjectId _id { get; set; }
        public string Type { get; set; }
        public int Port { get; set; }
        public bool IsUse { get; set; }
        public DateTime LastUse { get; set; }
    }
}
