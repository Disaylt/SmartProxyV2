using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SmartProxyV2
{
    internal interface IMongoCollectionData
    {
        BsonObjectId _id { get; set; }
    }
}
