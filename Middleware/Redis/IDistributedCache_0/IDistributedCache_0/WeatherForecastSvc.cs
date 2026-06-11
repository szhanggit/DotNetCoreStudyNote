using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace IDistributedCache_0
{
    public interface IWeatherForecast
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([NotNull] string requestKey);
        Task SetWeatherforecasts([NotNull] string requestKey);
    }
    public class WeatherForecastSvc : IWeatherForecast
    {
        private readonly ILogger<WeatherForecastSvc> _logger;
        private readonly IDistributedCache _distributedCache;

        public WeatherForecastSvc(IDistributedCache distributedCache, ILogger<WeatherForecastSvc> logger)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([NotNull] string requestKey)
        {
            try
            {
                var forecastsObject = await _distributedCache.GetStringAsync(requestKey);
                if (forecastsObject != null)
                {
                    var lists = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(forecastsObject);
                    return lists;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured in GetWeatherForecasts {ex.ToString()}");
                return null;
            }
        }

        public async Task SetWeatherforecasts([NotNull] string requestKey)
        {
            try {
                var forecast = new WeatherForecast()
                { 
                    Date = DateTime.Now,
                    Summary = "Sunny",
                    TemperatureC = 70
                };
                var summaries = new List<WeatherForecast>();
                summaries.Add(forecast);
                var summariesString = JsonConvert.SerializeObject(summaries);
                var expiration = new DistributedCacheEntryOptions()
                { 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };
                await _distributedCache.SetStringAsync(requestKey, summariesString, expiration);
            } 
            catch (Exception ex) {
                _logger.LogError($"An error occured in SetWeatherForecasts {ex.ToString()}");
            }
        }
    }
}
