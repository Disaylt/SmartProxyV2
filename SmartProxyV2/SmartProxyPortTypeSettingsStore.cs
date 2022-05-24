using MongoDB.Bson;
using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    public class SmartProxyPortTypeSettingsStore : SmartProxySettingsStore
    {
        protected readonly string PortType;
        internal SmartProxyPortTypeSettingsStore(string portType)
        {
            PortType = portType;
        }

        public async Task AddNewSettings(SmartProxySettingsMongoModel settingsModel)
        {
            await Collection.InsertOneAsync(settingsModel);
        }

        internal async Task<SmartProxySettingsMongoModel> GetSettingsPort()
        {
            var filter = Builders<SmartProxySettingsMongoModel>.Filter.Eq("PortType", PortType);
            var portSetting = await Collection.Find(filter).FirstOrDefaultAsync();
            return portSetting;
        }

        internal async Task IncrementLastPort()
        {
            var settingsData = await GetSettingsPort();
            int newNumPort;
            if(settingsData.LastUsePort > settingsData.MaxPort)
            {
                newNumPort = settingsData.MinPort;
            }
            else
            {
                newNumPort = settingsData.LastUsePort += 1;
            }
            await ProxyPortStore.Collection.UpdateOneAsync(
                   new BsonDocument("_id", settingsData.Id),
                   new BsonDocument("LastUsePort", newNumPort));
        }
    }
}
