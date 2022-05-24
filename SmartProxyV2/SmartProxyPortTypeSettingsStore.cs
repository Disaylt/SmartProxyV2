using MongoDB.Driver;
using SmartProxyV2.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2
{
    internal class SmartProxyPortTypeSettingsStore : SmartProxySettingsStore
    {
        protected readonly string PortType;
        internal SmartProxyPortTypeSettingsStore(string portType)
        {
            PortType = portType;
        }

        internal async Task<SmartProxySettingsMongoModel> GetSettingsPort()
        {
            var filter = Builders<SmartProxySettingsMongoModel>.Filter.Eq("PortType", PortType);
            var portSetting = await Collection.Find(filter).FirstOrDefaultAsync();
            return portSetting;
        }



        internal async Task IncrementLastPort()
        {
            int lastPort = await GetLastPort();
            int newLastPort = lastPort += 1;
        }
    }
}
