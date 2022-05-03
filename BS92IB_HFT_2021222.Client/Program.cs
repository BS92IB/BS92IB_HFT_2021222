using BS92IB_HFT_2021222.Client;
using System;

namespace BS92IB_HFT_2021222
{
    class Program
    {
        static void Main(string[] args)
        {
            IRestService restService;
            if (args.Length > 0 && args[0] == "offline")
            {
                restService = new OfflineRestService();
            }
            else
            {
                Console.WriteLine(@" _                     _ _                   ");
                Console.WriteLine(@"| |                   | (_)                  ");
                Console.WriteLine(@"| |     ___   __ _  __| |_ _ __   __ _       ");
                Console.WriteLine(@"| |    / _ \ / _` |/ _` | | '_ \ / _` |      ");
                Console.WriteLine(@"| |___| (_) | (_| | (_| | | | | | (_| |_ _ _ ");
                Console.WriteLine(@"\_____/\___/ \__,_|\__,_|_|_| |_|\__, (_|_|_)");
                Console.WriteLine(@"                                  __/ |      ");
                Console.WriteLine(@"                                 |___/       ");
                Console.WriteLine();
                Console.WriteLine(@"           NSXHF1HBNE - 2021/22/2            ");
                Console.WriteLine();
                Console.WriteLine(@"        Simó Csongor Zoltán - BS92IB         ");
                System.Threading.Thread.Sleep(5000);
                restService = new RestService("http://localhost:21990");
            }

            var app = new ClientApplication(restService);
            app.Start();
        }
    }
}
