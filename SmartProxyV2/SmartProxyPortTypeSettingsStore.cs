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
        public SmartProxyPortTypeSettingsStore(string portType)
        {
            PortType = portType;
        }

        internal async Task<ProxySettingsMongoModel> GetSettingsPortAsync()
        {
            var filter = Builders<ProxySettingsMongoModel>.Filter.Eq("PortType", PortType);
            var portSetting = await Collection.Find(filter).FirstOrDefaultAsync();
            await IncrementLastPortAsync(portSetting);
            return portSetting;
        }

        private async Task IncrementLastPortAsync(ProxySettingsMongoModel settingsData)
        {
            int newNumPort;
            if(settingsData.LastUsePort > settingsData.MaxPort)
            {
                newNumPort = settingsData.MinPort;
            }
            else
            {
                newNumPort = settingsData.LastUsePort += 1;
            }
            var update = Builders<ProxySettingsMongoModel>.Update.Set("LastUsePort", newNumPort);
            await Collection.UpdateOneAsync(
                   new BsonDocument("_id", settingsData.Id), update);
        }
    }
}
