using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.Models;
using SmartProxyV2;
using SmartProxyV2.MongoModels;

namespace TestLib
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var proxy = await SmartProxyHandler.GetMoscowProxyWithRussianPort();
            Console.ReadLine();
        }
    }
}
