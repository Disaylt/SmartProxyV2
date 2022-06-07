using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartProxyV2_4._6._2
{
    internal interface IMongoCollectionData
    {
        [BsonId]
        BsonObjectId Id { get; set; }
    }
}
