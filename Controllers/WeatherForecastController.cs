using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ASI_Activity_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public List<WeatherForecast> LoadJson()
        {
            using (StreamReader r = new StreamReader("json.json"))
            {
                string json = r.ReadToEnd();
                List<WeatherForecast> items = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);
                return items;
            }
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public WeatherForecast Get()
        {
            WeatherForecast wf = new WeatherForecast
            {
                Date = DateTime.Parse("2020-02-25T16:41:48.1383231+00:00"),
                TemperatureC = 21,
                TemperatureF = 69,
                Summary = "Sunny",
                ZipCode = 124124
            };
            return wf;
        }

        [HttpGet("{zipcode}")]
        public WeatherForecast Get(int zipCode)
        {
            List<WeatherForecast> weatherList = LoadJson();
            WeatherForecast weather = weatherList.Find(weather => weather.ZipCode == zipCode);
            return weather;
        }
    }
}
