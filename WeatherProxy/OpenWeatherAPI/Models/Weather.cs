using System.Runtime.Serialization;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "weather")]
    public class Weather
    {
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }
}