using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WeatherProxy;
using WeatherProxy.Exceptions;
using WeatherProxy.OpenWeatherAPI.Models;

namespace WeatherService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] //Caution: this is not a great idea to allow all origins but I am doing it for demo purposes only. This should not be set for prod.
    public class WeatherController : ApiController
    {
        private readonly IForecastProxy _forecastProxy;

        public WeatherController(IForecastProxy forecastProxy)
        {
            _forecastProxy = forecastProxy;
        }

        public ForecastResponse Get(int cityid)
        {
            try
            {
                return _forecastProxy.GetForecastByCityId(cityid);
            }
            catch (InvalidApiKeyException e)
            {
                return new ForecastResponse(null) {Error = "API key is invalid.", ResponseCode = -1 };
            }
            catch (InvalidCityException e)
            {
                return new ForecastResponse(null) { Error = "City Id passed in invalid", ResponseCode = -2 };
            }
            catch (InvalidUrlOrCityIdException e)
            {
                return new ForecastResponse(null) { Error = "Either the City Id passed in invalid or the web API url is invalid.", ResponseCode = -3};
            }
        }
    }
}
