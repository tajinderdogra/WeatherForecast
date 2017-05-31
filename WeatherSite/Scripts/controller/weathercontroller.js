
function weathercontroller($timeout, $q, $log, $http, $scope, dataService) {
    $scope.getWeather = function () {
        if (dataService.getSelectedCity()) {
            $http({
                    method: 'GET',
                    url: 'http://localhost:58295/api/weather/city/id/' + dataService.getSelectedCity().value,
                    dataType: "json"
                }).success(function(result) {
                    console.log(result);

                    $scope.weatherresult = result;
                    //$scope.weatherresult.forecasts[0]
                })
                .error(function(error) {
                    console.log('failed');
                });
        } else {
            $scope.weatherresult = null;
        }
        
    }

    $scope.hasWeatherResult = function () {
        return $scope.weatherresult != null;
    };
}