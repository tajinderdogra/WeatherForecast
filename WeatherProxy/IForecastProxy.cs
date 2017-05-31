using WeatherProxy.OpenWeatherAPI.Models;

namespace WeatherProxy
{
    public interface IForecastProxy
    {
        ForecastResponse GetForecastByCityId(long cityId);
    }
}