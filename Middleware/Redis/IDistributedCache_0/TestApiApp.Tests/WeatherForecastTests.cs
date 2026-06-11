using IDistributedCache_0;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApiApp.Tests
{
    public class WeatherForecastTests
    {
        private static Mock<IDistributedCache> _cache;
        private WeatherForecastSvc _forecastSvc;

        [SetUp]
        public void Setup()
        {
            _cache = new Mock<IDistributedCache>();
            _forecastSvc = new WeatherForecastSvc(_cache.Object, NullLogger<WeatherForecastSvc>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _cache = null;
            _forecastSvc = null;
        }

        [Test]
        public async Task SetWeatherForecasts_WeatherSummary_IsStored()
        {
            //Arrange
            var cache = new Mock<IDistributedCache>();
            var subject = new WeatherForecastSvc(cache.Object, NullLogger<WeatherForecastSvc>.Instance);

            //Setup the mocks to behave as expected.
            var moqData = new WeatherForecast()
            {
                Date = DateTime.Now,
                Summary = "Sunny",
                TemperatureC = 90,
            };

            //Act
            //...call the method under test

            await Task.Run(async () =>
            {
                await subject.SetWeatherforecasts("requestKey");
                // Actual test code here.
            });

        }
    }
}
