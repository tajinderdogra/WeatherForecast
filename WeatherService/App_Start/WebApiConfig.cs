using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Practices.Unity;
using WeatherProxy;
using WeatherProxy.OpenWeatherAPI;
using WeatherService.Controllers;

namespace WeatherService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //this is to allow the cross origin requests as our webapi is hosted outside of the website which gives us a scalable architecture
            config.EnableCors();

            //Dependency configuration
            RegisterDependencyResolver(config);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/city/id/{cityid}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true; 
        }

        private static void RegisterDependencyResolver(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IConfiguration, Configuration>(new ContainerControlledLifetimeManager());
            container.RegisterType<IForecastProxy, ForecastProxy>(new ContainerControlledLifetimeManager());
            container.RegisterInstance(new WeatherController(container.Resolve<IForecastProxy>()), new PerResolveLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
