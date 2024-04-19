using System.Collections.Generic;
using System.ServiceModel;

namespace OpcWebApi.Services
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        IEnumerable<Dictionary<string, string>> GetVals(string itemIds, string groupName);

        [OperationContract]
        Dictionary<string, string> GetVal(string itemId, string groupName);
    }
}