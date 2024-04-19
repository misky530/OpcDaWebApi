using OpcWebApi.Models;
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
        [Route("api/opc/GetVal")]
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

        [Route("api/opc/getvals")]
        [HttpPost]
        public HttpResponseMessage PostVals(string itemIds, string groupName)
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

        //[Route("api/opc/batch")]
        ////[HttpPost]
        //public HttpResponseMessage PostBatch(string itemIds, string groupName)
        //{
        //    if (string.IsNullOrEmpty(groupName))
        //    {
        //        groupName = ConfigurationManager.AppSettings["GroupName"];
        //    }

        //    ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8001/MyService"));
        //    IMyService service = factory.CreateChannel();
        //    var result = service.GetVals(itemIds, groupName);
        //    Console.WriteLine(result);

        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
        //    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    return response;
        //}

        [Route("api/opc/batch")]
        [HttpPost]
        public HttpResponseMessage Batch([FromBody] ItemRequest request)
        {
            if (string.IsNullOrEmpty(request.GroupName))
            {
                request.GroupName = ConfigurationManager.AppSettings["GroupName"];
            }

            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8001/MyService"));
            IMyService service = factory.CreateChannel();
            var result = service.GetVals(request.ItemIds, request.GroupName);
            Console.WriteLine(result);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}