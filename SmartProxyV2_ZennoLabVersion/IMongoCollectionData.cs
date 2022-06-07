using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartProxyV2_ZennoLabVersion
{
    internal interface IMongoCollectionData
    {
        [BsonId]
        BsonObjectId Id { get; set; }
    }
}
