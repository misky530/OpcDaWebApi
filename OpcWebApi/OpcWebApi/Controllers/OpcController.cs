using OpcWebApi.Services;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace OpcWebApi.Controllers
{
    public class OpcController : ApiController
    {
        // GET api/values
        public HttpResponseMessage GetVal(string itemId, string groupName)
        {
            //var result = Services.OpcServicesTest.GetVal(itemId);
            //var result = OpcConApp.Services.OpcServices.GetVal(itemId);

            if (string.IsNullOrEmpty(groupName))
            {
                groupName = ConfigurationManager.AppSettings["GroupName"];
            }

            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8001/MyService"));
            IMyService service = factory.CreateChannel();
            var result = service.GetVal(itemId, groupName);
            Console.WriteLine(result);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }

        public HttpResponseMessage GetVals(string itemIds, string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                groupName = ConfigurationManager.AppSettings["GroupName"];
            }

            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8001/MyService"));
            IMyService service = factory.CreateChannel();
            var result = service.GetVals(itemIds, groupName);
            Console.WriteLine(result);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}