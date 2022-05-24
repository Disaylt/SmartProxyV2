using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2.MongoModels
{
    public class SmartProxySettingsMongoModel : IMongoCollectionData
    {
        public BsonObjectId Id { get; set; }
        public string PortType { get; set; }
        public int LastUsePort { get; set; }
        public int MaxPort { get; set; }
        public int MinPort { get; set; }
    }
}
