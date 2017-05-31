using NUnit.Framework;
using WeatherProxy;
using WeatherService;

namespace IntegrationTests
{
    [TestFixture]
    class ConfigurationTests
    {
        [Test]
        public void LoadKey()
        {
            IConfiguration config=new Configuration();
            Assert.AreEqual(config.OpenWeatherApiKey, "This is the key");
        }

        [Test]
        public void LoadUrl()
        {
            IConfiguration config = new Configuration();
            Assert.AreEqual(config.OpenWeatherApiForecastUrl, "This is the url");
        }
    }
}
