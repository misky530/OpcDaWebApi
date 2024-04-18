using System.ServiceModel;

namespace OpcWebApi.Services
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string GetData(int value);
    }
}