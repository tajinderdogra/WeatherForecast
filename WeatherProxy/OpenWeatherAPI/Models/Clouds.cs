using System.Runtime.Serialization;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "clouds")]
    public class Clouds
    {
        [DataMember(Name = "all")]
        public int All { get; set; }
    }
}