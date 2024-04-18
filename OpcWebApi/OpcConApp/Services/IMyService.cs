using System.ServiceModel;

namespace OpcConApp.Services
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string GetData(int value);
    }
}
