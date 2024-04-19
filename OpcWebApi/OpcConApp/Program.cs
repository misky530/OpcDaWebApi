using OpcConApp.Services;
using System;
using System.Configuration;
using System.ServiceModel;

namespace OpcConApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var result = OpcServices.GetVal(".TestStr", "Group0");
            Console.WriteLine("result:" + result);

            string serverPort = ConfigurationManager.AppSettings["ServerPort"];

            if (string.IsNullOrEmpty(serverPort))
            {
                serverPort = "18001";
            }

            //Console.ReadKey();
            //OpcServices.ReleaseInstance();
            ServiceHost host = new ServiceHost(typeof(MyService), new Uri($"http://localhost:{serverPort}/"));
            host.AddServiceEndpoint(typeof(IMyService), new BasicHttpBinding(), "MyService");
            host.Open();
            Console.WriteLine($"Service is running on {serverPort}...");
            Console.ReadLine();  // Keep the service running until Enter key is pressed
            host.Close();
        }
    }
}
