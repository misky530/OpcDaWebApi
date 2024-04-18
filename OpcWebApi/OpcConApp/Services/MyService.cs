using System.Linq;

namespace OpcConApp.Services
{
    public class MyService : IMyService
    {
        public string GetData(int value)
        {

            OpcServices.GetInstance();
            var result = OpcServices.GetVal(".TestStr", "Group0");

            OpcServices.ReleaseInstance();

            return result.Values.First();
        }
    }
}
