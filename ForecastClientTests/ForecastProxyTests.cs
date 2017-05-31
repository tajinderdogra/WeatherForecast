using System;
using NUnit.Framework;
using Rhino.Mocks;
using WeatherProxy;
using WeatherProxy.Exceptions;
using WeatherProxy.OpenWeatherAPI;

namespace IntegrationTests
{
    [TestFixture]
    public class ForecastProxyTests
    {
        private string _openWeatherApiKey = "6b5fd304d35d18b72d120f3496b6783e";
        private string _forecastApiUrl = "http://api.openweathermap.org/data/2.5/forecast/daily";

        [Test]
        public void GetForecastThrowsExceptionWhenApiUrlIsInvalid()
        {
            IConfiguration configuration = MockRepository.GenerateMock<IConfiguration>();

            configuration.Expect(x => x.OpenWeatherApiKey).Return(_openWeatherApiKey);
            configuration.Expect(x => x.OpenWeatherApiForecastUrl).Return("http://invalid");

            IForecastProxy forecastProxy = new ForecastProxy(configuration);

            Assert.That(() => forecastProxy.GetForecastByCityId(2643743),
                Throws.Exception.TypeOf<InvalidUrlOrCityIdException>()
                    .With.Message.Contains("The API url or the city id passed may be invalid"));
        }

        [Test]
        public void GetForecastThrowsExceptionWhenCityIdIsZero()
        {
            IConfiguration configuration = MockRepository.GenerateMock<IConfiguration>();

            configuration.Expect(x => x.OpenWeatherApiKey).Return(_openWeatherApiKey);
            configuration.Expect(x => x.OpenWeatherApiForecastUrl).Return(_forecastApiUrl);

            IForecastProxy forecastProxy = new ForecastProxy(configuration);

            Assert.That(() => forecastProxy.GetForecastByCityId(0), 
                Throws.Exception.TypeOf<InvalidCityException>()
                .With.Message.Contains("Ensure that a valid city Id is passed"));
        }


        [Test]
        public void GetForecastThrowsExceptionWhenCityIdIsNegative()
        {
            IConfiguration configuration = MockRepository.GenerateMock<IConfiguration>();

            configuration.Expect(x => x.OpenWeatherApiKey).Return(_openWeatherApiKey);
            configuration.Expect(x => x.OpenWeatherApiForecastUrl).Return(_forecastApiUrl);

            IForecastProxy forecastProxy = new ForecastProxy(configuration);

            Assert.That(() => forecastProxy.GetForecastByCityId(-1),
                Throws.Exception.TypeOf<InvalidCityException>()
                    .With.Message.Contains("Ensure that a valid city Id is passed"));
        }

        [Test]
        [Ignore("This test is unpredictable as the open weather API intelligently returns empty object after throwing exception for 3-4 times")]
        public void GetForecastThrowsExceptionWhenCityIdIsInvalid()
        {
            IConfiguration configuration = MockRepository.GenerateMock<IConfiguration>();

            configuration.Expect(x => x.OpenWeatherApiKey).Return(_openWeatherApiKey);
            configuration.Expect(x => x.OpenWeatherApiForecastUrl).Return(_forecastApiUrl);

            IForecastProxy forecastProxy = new ForecastProxy(configuration);

            Assert.That(() => forecastProxy.GetForecastByCityId(999999999999999999),
                Throws.Exception.TypeOf<InvalidUrlOrCityIdException>()
                    .With.Message.Contains("The API url or the city id passed may be invalid"));
        }

        [Test]
        public void GetForecastForLondon()
        {
            IConfiguration configuration = MockRepository.GenerateMock<IConfiguration>();
           
            configuration.Expect(x => x.OpenWeatherApiKey).Return(_openWeatherApiKey);
            configuration.Expect(x => x.OpenWeatherApiForecastUrl).Return(_forecastApiUrl);

            IForecastProxy forecastProxy = new ForecastProxy(configuration);
            var response = forecastProxy.GetForecastByCityId(2643743);

            Assert.IsNotNull(response.Forecasts, "response.Forecasts != null");
            Assert.IsTrue(response.Forecasts.Count > 0, "response.Forecasts.Count greater than 0");
            Assert.IsNotNull(response.Forecasts[0].Clouds);
            Assert.IsNotNull(response.Forecasts[0].WeatherList);
            Assert.IsNotNull(response.Forecasts[0].Wind);
            Assert.IsNotNull(response.Forecasts[0].DateInUnixFormat);
            Assert.IsNotNull(response.Forecasts[0].Temperature);

            Assert.Greater(response.Forecasts[1].DateInLocalFormat, DateTime.Now);
        }
    }
}
