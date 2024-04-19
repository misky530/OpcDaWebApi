using Microsoft.AspNetCore.Mvc;
using OpcCoreApi.Services;
using System.ServiceModel;

namespace OpcCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet]
        public string GetOpcVal()
        {
            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8000/MyService"));
            IMyService service = factory.CreateChannel();
            var result = service.GetData(123);
            Console.WriteLine(result);

            return result;
        }
    }
}
