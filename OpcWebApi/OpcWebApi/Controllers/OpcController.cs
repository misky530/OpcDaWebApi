using OpcWebApi.Services;
using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace OpcWebApi.Controllers
{
    public class OpcController : ApiController
    {
        // GET api/values
        public HttpResponseMessage GetVal(string itemId)
        {
            //var result = Services.OpcServicesTest.GetVal(itemId);
            //var result = OpcConApp.Services.OpcServices.GetVal(itemId);

            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8001/MyService"));
            IMyService service = factory.CreateChannel();
            var result = service.GetData(123);
            Console.WriteLine(result);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }

        public HttpResponseMessage GetVals(string ids)
        {
            var values = new string[] { "value1", "value2" };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, values);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}