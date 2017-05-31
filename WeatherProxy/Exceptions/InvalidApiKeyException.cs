using System;

namespace WeatherProxy.Exceptions
{
    public class InvalidApiKeyException : Exception
    {
        public InvalidApiKeyException(): base("OpenWeatherAPI key is either null or blank. Pass a valid api key.")
        {
            
        }
    }
}