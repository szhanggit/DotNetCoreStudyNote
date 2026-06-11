using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Lib;

namespace WorkerService_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICalService _calService;
        private readonly ICal2Service _cal2Service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , ICalService calService
            , ICal2Service cal2Service)
        {
            this._logger = logger;
            this._calService = calService;
            this._cal2Service = cal2Service;
        }

        /*
         http://localhost:35155/api/WeatherForecast
         http://localhost:5000/api/WeatherForecast
         */
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _calService.Add(11);
            _cal2Service.Scan(11);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
