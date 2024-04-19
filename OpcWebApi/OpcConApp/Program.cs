using OpcConApp.Services;
using System;
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

            //Console.ReadKey();
            //OpcServices.ReleaseInstance();
            ServiceHost host = new ServiceHost(typeof(MyService), new Uri("http://localhost:8001/"));
            host.AddServiceEndpoint(typeof(IMyService), new BasicHttpBinding(), "MyService");
            host.Open();
            Console.WriteLine("Service is running...");
            Console.ReadLine();  // Keep the service running until Enter key is pressed
            host.Close();
        }
    }
}
