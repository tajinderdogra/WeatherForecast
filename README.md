# WeatherForecast
Weather App - data provided by OpenWeatherMap

# Instructions for running the application
1. Clone the repository or download the code 
2. Open the solution file WeatherApp.sln in Visual Studio 2015 or above
3. Rebuild the solution and make sure that the Nuget packages are restored
4. Make sure that the solution has built successfully 
5. Check startup project configuration by right clicking the solution and select "Set Startup Projects"
6. Make sure "Multiple Startup Projects" option is selected and WeatherService and WeatherSite action is set to "Start".
7. Hit the start button to launch the website and webservice.

# Architecture

## WeatherProxy
Implements remote proxy pattern and is responsible for getting weather forecast data from OpenWeatherMap API. Response is received in JSON which is then transformed into .NET Objects.

## WeatherService
Hosted as REST Api which is implemented using Asp.Net Web Api technology. This service consumes WeatherProxy interface to fetch the weather forecast data and returns the response to client in JSON format. The service use DataContract serialization and DataContractJsonSerializer.

## WeatherSite
This is the UI client layer that consumes the WeatherService for data and uses AngularJS to implement MVC design pattern. The controllers implemented in AngularJS neatly separates the business logic and the UI logic. 

# Cross Origin Request
CORS is enabled for Web Api to allow the website to send cross origin requests.
The details of how to enable CORS is shown in the url below
https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

*Caution* : The EnableCORS attribute is set to allow any origin however this is not recommended for production code.
