namespace WeatherProxy
{
    public interface IConfiguration
    {
        string OpenWeatherApiKey { get; set; }
        string OpenWeatherApiForecastUrl { get; set; }
    }
}