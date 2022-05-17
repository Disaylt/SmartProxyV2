using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SmartProxyV2
{
    internal static class MongoConnector
    {
        private const string _connectionString = "mongodb://10.100.1.250:27017";
        private const string _databaseName = "SmartProxyV2";
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MongoConnector()
        {
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
        }

        internal static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
