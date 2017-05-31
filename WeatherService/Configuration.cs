using System.Configuration;
using WeatherProxy;

namespace WeatherService
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            OpenWeatherApiKey = ConfigurationManager.AppSettings.Get("OpenWeatherApiKey");
            OpenWeatherApiForecastUrl = ConfigurationManager.AppSettings.Get("OpenWeatherApiForecastUrl");
        }
        public string OpenWeatherApiKey { get; set; }
        public string OpenWeatherApiForecastUrl { get; set; }
    }
}