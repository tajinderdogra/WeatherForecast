using System.Runtime.Serialization;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "wind")]
    public class Wind
    {
        [DataMember(Name = "speed")]
        public double Speed { get; set; }
        [DataMember(Name = "degrees")]
        public int Degrees { get; set; }
    }
}