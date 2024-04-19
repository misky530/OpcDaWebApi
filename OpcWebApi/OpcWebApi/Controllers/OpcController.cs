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
        private string ServerUrl
        {
            get
            {
                string serverPort = ConfigurationManager.AppSettings["ServerPort"];
                if (string.IsNullOrEmpty(serverPort))
                {
                    return "http://localhost:18001/MyService";
                }
                else
                {

                    return $"http://localhost:{serverPort}/MyService";
                }
            }
        }

        private IMyService ServiceInstance
        {
            get
            {
                ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress(ServerUrl));
                return factory.CreateChannel();
            }
        }

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

            try
            {
                IMyService service = this.ServiceInstance;
                var result = service.GetVal(itemId, groupName);

                return BuildResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ErrorResult(ex.Message);
            }
        }



        [Route("api/opc/batch")]
        [HttpPost]
        public HttpResponseMessage Batch([FromBody] ItemRequest request)
        {
            if (string.IsNullOrEmpty(request.GroupName))
            {
                request.GroupName = ConfigurationManager.AppSettings["GroupName"];
            }

            try
            {
                IMyService service = this.ServiceInstance;
                var result = service.GetVals(request.ItemIds, request.GroupName);
                Console.WriteLine(result);

                return BuildResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ErrorResult(ex.Message);
            }


        }

        private HttpResponseMessage BuildResult<T>(T result)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }

        private HttpResponseMessage ErrorResult(string errMsg)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, errMsg);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}