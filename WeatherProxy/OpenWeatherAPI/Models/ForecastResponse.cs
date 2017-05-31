using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "forecastResponse")]
    public class ForecastResponse
    {
        private readonly string _apiResponse;

        [DataMember(Name = "responseCode")]
        public int ResponseCode { get; set; }

        [DataMember(Name = "city")]
        public City City { get; set; }

        [DataMember(Name = "forecasts")]
        public IList<Forecast> Forecasts { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        public ForecastResponse(string apiResponse)
        {
            _apiResponse = apiResponse;
            ParseJson();
        }

        
        public void ParseJson()
        {
            if (string.IsNullOrEmpty(_apiResponse)) return;

            JToken root = JObject.Parse(_apiResponse);

            var responseCode = (int)root.SelectToken("cod");
            if (responseCode != 200)
            {
                Error = (string)root.SelectToken("message");
            }
            else
            {
                var cityToken = root.SelectToken("city");
                if (cityToken != null)
                {
                    City = new City
                    {
                        Id = (int)cityToken.SelectToken("id"),
                        Name = (string)cityToken.SelectToken("name"),
                        CountryCode = (string)cityToken.SelectToken("country"),
                        Longitude = (double)cityToken.SelectToken("coord").SelectToken("lon"),
                        Latitude = (double)cityToken.SelectToken("coord").SelectToken("lat")
                    };
                }

                var list = root.SelectTokens("list");

                foreach (var child in list.Children())
                {
                    Forecast forecast = new Forecast();

                    forecast.DateInUnixFormat = (int)child.SelectToken("dt");

                    var tempToken = child.SelectToken("temp");
                    Temperature temperatureObj = new Temperature
                    {
                        Day = (double)tempToken.SelectToken("day"),
                        Min = (double)tempToken.SelectToken("min"),
                        Max = (double)tempToken.SelectToken("max"),
                        Evening = (double)tempToken.SelectToken("eve"),
                        Night = (double)tempToken.SelectToken("night"),
                        Morning = (double)tempToken.SelectToken("morn")
                    };
                    forecast.Temperature = temperatureObj;
                    forecast.Pressure = (int) child.SelectToken("pressure");
                    forecast.Humidity = (int) child.SelectToken("humidity");

                    Clouds cloudsObj = new Clouds { All = (int)child.SelectToken("clouds") };
                    forecast.Clouds = cloudsObj;

                    Wind windObj = new Wind
                    {
                        Speed = (double)child.SelectToken("speed"),
                        Degrees = (int)child.SelectToken("deg")
                    };
                    forecast.Wind = windObj;

                    var weatherToken = child.SelectToken("weather");
                    forecast.WeatherList = new List<Weather>();
                    foreach (var weatherChild in weatherToken.Children())
                    {
                        Weather weatherObj = new Weather
                        {
                            Summary = (string)weatherChild.SelectToken("main"),
                            Description = (string)weatherChild.SelectToken("description"),
                            Icon = (string)weatherChild.SelectToken("icon")
                        };
                        forecast.WeatherList.Add(weatherObj);
                    }

                    if (Forecasts == null) Forecasts = new List<Forecast>();
                    Forecasts.Add(forecast);
                }
            }
           
        }
    }
}