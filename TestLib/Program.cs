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
            ProxyPortController portStore = new ProxyPortController();
            for(int i = 40001; i <= 49999; i++)
            {
                await portStore.InsertNewProxyPort("russian", i);
            }
            Console.ReadLine();
        }
    }
}
