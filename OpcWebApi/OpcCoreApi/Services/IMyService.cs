using System.ServiceModel;

namespace OpcCoreApi.Services
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string GetData(int value);
    }
}
