using NUnit.Framework;
using Rhino.Mocks;
using WeatherProxy;
using WeatherProxy.OpenWeatherAPI.Models;
using WeatherService.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class WeatherControllerTests
    {
        [Test]
        public void ConstructClassUnderTest()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            WeatherController controller = new WeatherController(forecastProxy);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void GetReturnsNullWhenApiReponseIsNull()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 12345;

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(null);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNull(response);
        }


        [Test]
        public void GetReturnsEmptyObjectWhenApiResponseIsBlank()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 12345;
            ForecastResponse apiresponse = new ForecastResponse("");

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
        }

        [Test]
        public void GetReturnsEmptyObjectWhenApiResponseIsEmptyJson()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 12345;
            ForecastResponse apiresponse = new ForecastResponse("{\"cod\":\"0\"}");

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
        }

        [Test]
        public void GetReturnsCityObjectWhenApiResponseContainsCityJson()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 2643743;
            ForecastResponse apiresponse = new ForecastResponse(GetJson());

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
            Assert.AreEqual(2643743, response.City.Id);
            Assert.AreEqual("London", response.City.Name);
            Assert.AreEqual("GB", response.City.CountryCode);
            Assert.AreEqual(51.5085, response.City.Latitude);
            Assert.AreEqual(-0.1258, response.City.Longitude);
        }

        [Test]
        public void GetCapturesAllForecastsWhenApiResponseContainsMultipleForecasts()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 2643743;
            ForecastResponse apiresponse = new ForecastResponse(GetJson());

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
            Assert.AreEqual(7, response.Forecasts.Count);
        }

        [Test]
        public void GetTempratureForTheDayWhenApiResponseContainsDailyTempratureForecast()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 2643743;
            ForecastResponse apiresponse = new ForecastResponse(GetJson());

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
            Assert.AreEqual(17.6, response.Forecasts[0].Temperature.Day);
            Assert.AreEqual(17.6, response.Forecasts[0].Temperature.Evening);
            Assert.AreEqual(17.6, response.Forecasts[0].Temperature.Max);
            Assert.AreEqual(17.6, response.Forecasts[0].Temperature.Max);
            Assert.AreEqual(14.16, response.Forecasts[0].Temperature.Night);
            Assert.AreEqual(17.6, response.Forecasts[0].Temperature.Morning);
        }

        [Test]
        public void GetWeatherDescriptionWhenApiResponseContainsDescription()
        {
            IForecastProxy forecastProxy = MockRepository.GenerateMock<IForecastProxy>();

            int cityId = 2643743;
            ForecastResponse apiresponse = new ForecastResponse(GetJson());

            forecastProxy.Expect(x => x.GetForecastByCityId(cityId)).Return(apiresponse);

            WeatherController controller = new WeatherController(forecastProxy);
            var response = controller.Get(cityId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, apiresponse);
            Assert.AreEqual("Rain", response.Forecasts[0].WeatherList[0].Summary);
            Assert.AreEqual("light rain", response.Forecasts[0].WeatherList[0].Description);
            Assert.AreEqual("10d", response.Forecasts[0].WeatherList[0].Icon);
        }

        public string GetJson()
        {
            return "{\"city\":{\"id\":2643743,\"name\":\"London\",\"coord\":{\"lon\":-0.1258,\"lat\":51.5085},\"country\":\"GB\",\"population\":0},\"cod\":\"200\",\"message\":0.0708082,\"cnt\":7,\"list\":[{\"dt\":1496055600," +
                   "\"temp\":{\"day\":17.6,\"min\":17.6,\"max\":17.6,\"night\":14.16,\"eve\":17.6,\"morn\":17.6},\"pressure\":1016.28,\"humidity\":66,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":4.31,\"deg\":231,\"clouds\":92,\"rain\":1.33},{\"dt\":1496142000,\"temp\":{\"day\":18.92,\"min\":14.89,\"max\":19.75,\"night\":16.48,\"eve\":19.69,\"morn\":14.89},\"pressure\":1020.46,\"humidity\":81,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":4.36,\"deg\":248,\"clouds\":32,\"rain\":0.21},{\"dt\":1496228400,\"temp\":{\"day\":18.79,\"min\":13.6,\"max\":20.96,\"night\":14.32,\"eve\":20.96,\"morn\":13.6},\"pressure\":1029.58,\"humidity\":64,\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"speed\":1.76,\"deg\":146,\"clouds\":56},{\"dt\":1496314800,\"temp\":{\"day\":24.81,\"min\":14.03,\"max\":25.56,\"night\":16.46,\"eve\":24.66,\"morn\":14.03},\"pressure\":1024.98,\"humidity\":54,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":2.86,\"deg\":169,\"clouds\":0},{\"dt\":1496401200,\"temp\":{\"day\":23.24,\"min\":15.98,\"max\":23.41,\"night\":15.98,\"eve\":23.41,\"morn\":16.76},\"pressure\":1018.08,\"humidity\":0,\"weather\":[{\"id\":502,\"main\":\"Rain\",\"description\":\"heavy intensity rain\",\"icon\":\"10d\"}],\"speed\":2.41,\"deg\":128,\"clouds\":64,\"rain\":22.07},{\"dt\":1496487600,\"temp\":{\"day\":14.6,\"min\":11.54,\"max\":14.6,\"night\":11.54,\"eve\":13.69,\"morn\":13.56},\"pressure\":1015.42,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":8.91,\"deg\":224,\"clouds\":61,\"rain\":2.83},{\"dt\":1496574000,\"temp\":{\"day\":15.68,\"min\":10.98,\"max\":16.54,\"night\":10.98,\"eve\":16.54,\"morn\":11.94},\"pressure\":1024.55,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":3.72,\"deg\":236,\"clouds\":59,\"rain\":2.3}]}";
        }

    }
}
