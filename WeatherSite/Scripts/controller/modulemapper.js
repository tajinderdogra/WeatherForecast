var app = angular
    .module('weatherApp', ['ngMaterial'])
    .controller('cityautocompletecontroller', cityautocompletecontroller)
    .controller('weathercontroller', weathercontroller);

app.service('dataService', function () {
    var selectedCity = {};

    var setSelectedCity = function (cityObj) {
        selectedCity = cityObj;
    };

    var getSelectedCity = function () {
        return selectedCity;
    };

    return {
        setSelectedCity: setSelectedCity,
        getSelectedCity: getSelectedCity
    };

});

app.filter("dateFilter", function ($filter) {
    return function (item) {
        if (item != null) {
            var parsedDate = new Date(parseInt(item.substr(6)));

            var today = new Date();
            if (parsedDate.getDate() === today.getDate()) {
                return "Today";
            }

            var tomorrow = new Date();
            tomorrow.setDate(today.getDate() + 1);
            if (parsedDate.getDate() === tomorrow.getDate()) {
                return "Tomorrow";
            }

            return $filter('date')(parsedDate, 'dd-MMM');
        }
        return "";
    };
});