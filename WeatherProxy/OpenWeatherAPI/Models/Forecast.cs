using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherProxy.OpenWeatherAPI.Models
{
    [DataContract(Name = "forecast")]
    public class Forecast
    {
        [DataMember(Name = "dateInUnixFormat")]
        public int DateInUnixFormat { get; set; }

        [DataMember(Name = "temperature")]
        public Temperature Temperature { get; set; }

        [DataMember(Name = "weatherList")]
        public List<Weather> WeatherList { get; set; }

        [DataMember(Name = "pressure")]
        public int Pressure { get; set; }

        [DataMember(Name = "humidity")]
        public int Humidity { get; set; }

        [DataMember(Name = "wind")]
        public Wind Wind { get; set; }

        [DataMember(Name = "clouds")]
        public Clouds Clouds { get; set; }

        [DataMember(Name = "dateInLocalFormat")]
        public DateTime DateInLocalFormat
        {
            get { return UnixTimeStampToDateTime(DateInUnixFormat); }
            set { }
        }

   
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}