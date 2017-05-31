using System;
using System.IO;
using System.Net;
using System.Web;
using WeatherProxy.Exceptions;
using WeatherProxy.OpenWeatherAPI.Models;

namespace WeatherProxy.OpenWeatherAPI
{
    public class ForecastProxy : IForecastProxy
    {
        private readonly string _apiKey;
        private readonly string _forecastApiEndpointUrl;

        public ForecastProxy(IConfiguration config)
        {
            _apiKey = config.OpenWeatherApiKey;
            _forecastApiEndpointUrl = config.OpenWeatherApiForecastUrl;
        }

        public ForecastResponse GetForecastByCityId(long cityId)
        {
            try
            {
                string apiResponse = null;
                using (HttpWebResponse response = GetWebRequest(cityId).GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        apiResponse = reader.ReadToEnd();
                    }
                }

                return new ForecastResponse(apiResponse);
            }
            catch (WebException e)
            {
                throw new InvalidUrlOrCityIdException("The API url or the city id passed may be invalid", e);
            }
        }

        private HttpWebRequest GetWebRequest(long cityId)
        {
            if (string.IsNullOrEmpty(_apiKey)) throw new InvalidApiKeyException();
            if (cityId <= 0) throw new InvalidCityException();

            var uriBuilder = new UriBuilder(_forecastApiEndpointUrl);
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["appid"] = _apiKey;
            parameters["id"] = cityId.ToString();
            parameters["units"] = "metric";
            uriBuilder.Query = parameters.ToString();

            return WebRequest.Create(uriBuilder.Uri) as HttpWebRequest;
        }
        
    }
}
