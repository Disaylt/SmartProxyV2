using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    internal class SmartProxySettingsStore
    {
        private const string _collectionName = "Settings";
        private static IMongoCollection<SmartProxySettingsMongoModel> _collection;
        internal static IMongoCollection<SmartProxySettingsMongoModel> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = MongoConnector.GetCollection<SmartProxySettingsMongoModel>(_collectionName);
                }
                return _collection;
            }
        }

        internal async static Task<int> GetLastPort(string portType)
        {
            var filter = Builders<SmartProxySettingsMongoModel>.Filter.Eq("PortType", portType);
            var portSetting = await Collection.Find(filter).FirstOrDefaultAsync();
            int lastPort = portSetting.LastUsePort;
            return lastPort;
        }
    }
}
