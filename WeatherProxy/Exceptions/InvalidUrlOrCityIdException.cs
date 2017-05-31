using System;

namespace WeatherProxy.Exceptions
{
    public class InvalidUrlOrCityIdException : Exception
    {
        public InvalidUrlOrCityIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}