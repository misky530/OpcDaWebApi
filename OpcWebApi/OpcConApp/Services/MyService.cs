using System.Collections.Generic;

namespace OpcConApp.Services
{
    public class MyService : IMyService
    {
        public string GetData(int value)
        {

            return "data:" + value;
        }

        public IEnumerable<Dictionary<string, string>> GetVals(string itemIds, string groupName)
        {
            return OpcServices.GetVals(itemIds, groupName);
        }

        public Dictionary<string, string> GetVal(string itemId, string groupName)
        {
            return OpcServices.GetVal(itemId, groupName);
        }
    }
}
