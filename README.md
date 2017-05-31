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
Hosted as REST Api which is implemented usin Asp.Net Web Api technology. This service consumes WeatherProxy interface to fetch the weather forecast data and returns the response to client in JSON format. The service use DataContract serialization and DataContractJsonSerializer.

## WeatherSite
This is the UI client layer that consumes the WeatherService for data and uses AngularJS to implement MVC design pattern. The controllers implemented in AngularJS neatly separates the business logic and the UI logic. 
