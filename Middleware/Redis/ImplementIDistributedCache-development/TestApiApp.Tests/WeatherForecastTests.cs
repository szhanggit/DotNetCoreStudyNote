using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApiApp.Services;

namespace TestApiApp.Tests
{
    public class Tests
    {
        private static Mock<IDistributedCache> _cache;
        private WeatherForecastSvc _forecastSvc;

        [SetUp]
        public void Setup()
        {
            _cache = new Mock<IDistributedCache>();
            _forecastSvc = new WeatherForecastSvc(NullLogger<WeatherForecastSvc>.Instance, _cache.Object);
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
            var subject = new WeatherForecastSvc(NullLogger<WeatherForecastSvc>.Instance, cache.Object);

            //Setup the mocks to behave as expected.
            var moqData = new WeatherForecast()
            {
                Date = DateTime.Now,
                Summary = "Sunny",
                TemperatureC = 90,
                RequestId = "10"
            };

            //Act
            //...call the method under test

            await Task.Run(async () =>
            {
                await subject.SetWeatherForecasts(moqData);
                // Actual test code here.
            });

        }
    }
}