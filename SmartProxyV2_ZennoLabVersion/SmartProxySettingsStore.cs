using MongoDB.Driver;
using SmartProxyV2_ZennoLabVersion.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_ZennoLabVersion
{
    public class SmartProxySettingsStore
    {
        private const string _collectionName = "Settings";
        private static IMongoCollection<ProxySettingsMongoModel> _collection;
        internal static IMongoCollection<ProxySettingsMongoModel> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = MongoConnector.GetCollection<ProxySettingsMongoModel>(_collectionName);
                }
                return _collection;
            }
        }
        public static async Task AddNewSettingsAsync(ProxySettingsMongoModel settingsModel)
        {
            await Collection.InsertOneAsync(settingsModel);
        }

    }
}
