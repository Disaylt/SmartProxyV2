using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    internal class SmartProxySettingsMongoModel : IMongoCollectionData
    {
        public BsonObjectId Id { get; set; }
        public string PortType { get; set; }
        public int LastUsePort { get; set; }
    }
}
