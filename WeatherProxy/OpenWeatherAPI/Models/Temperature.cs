using System.Runtime.Serialization;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "temperature")]
    public class Temperature
    {
        [DataMember(Name = "day")]
        public double Day { get; set; }

        [DataMember(Name = "min")]
        public double Min { get; set; }

        [DataMember(Name = "max")]
        public double Max { get; set; }

        [DataMember(Name = "evening")]
        public double Evening { get; set; }

        [DataMember(Name = "morning")]
        public double Morning { get; set; }

        [DataMember(Name = "night")]
        public double Night { get; set; }
    }
}