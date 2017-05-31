using System;

namespace WeatherProxy.Exceptions
{
    public class InvalidCityException : Exception
    {
        public InvalidCityException(): base("Ensure that a valid city Id is passed")
        {
            
        }
    }
}