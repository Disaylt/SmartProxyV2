using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_4._6._2.MongoModels
{
    public class ProxySettingsMongoModel : IMongoCollectionData
    {
        public BsonObjectId Id { get; set; }
        public string PortType { get; set; }
        public int LastUsePort { get; set; }
        public int MaxPort { get; set; }
        public int MinPort { get; set; }
    }
}
