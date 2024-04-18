using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpcWebApi.Controllers
{
    public class OpcController : ApiController
    {
        // GET api/values
        public HttpResponseMessage GetVal(string id)
        {
            var values = new string[] { "value1", "value2" };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, values);
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