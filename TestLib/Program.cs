using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProxyV2.Models;
using SmartProxyV2;

namespace TestLib
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var asd = await SmartProxyHandler.GetCustomProxy("moscow");
            Console.ReadLine();
            await SmartProxyHandler.OpenPort(asd.PortData);
        }
    }
}
