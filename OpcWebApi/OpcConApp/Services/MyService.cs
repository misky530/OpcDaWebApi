using System.Collections.Generic;

namespace OpcConApp.Services
{
    public class MyService : IMyService
    {
        public string GetData(int value)
        {

            return "data:" + value;
        }

        public IEnumerable<Dictionary<string, string>> GetVals(string itemIds)
        {
            return OpcServices.GetVals(itemIds, "Group0");
        }

        public Dictionary<string, string> GetVal(string itemId)
        {
            return OpcServices.GetVal(itemId, "Group0");
        }
    }
}
