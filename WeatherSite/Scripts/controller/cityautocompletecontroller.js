 

function cityautocompletecontroller($timeout, $q, $log, $http, dataService) {
    var self = this;
    self.simulateQuery = false;
    self.isDisabled    = false;
    // list of states to be displayed
    //self.states        = loadCities();
    self.querySearch   = querySearch;
    self.selectedItemChange = selectedItemChange;
    self.searchTextChange   = searchTextChange;

    loadCities();

    function querySearch (query) {
        var results = query ? self.states.filter( createFilterFor(query) ) : self.states, deferred;
        if (self.simulateQuery) {
            deferred = $q.defer();
            $timeout(function () {
                    deferred.resolve( results );
                },
                Math.random() * 1000, false);
            return deferred.promise;
        } else {
            return results;
        }
    }
    function searchTextChange(text) {
        $log.info('Text changed to ' + text);
    }
    function selectedItemChange(item) {
        $log.info('Item changed to ' + JSON.stringify(item));
        dataService.setSelectedCity(item);
    }
    
    function loadCities() {
        var allCities = [];
        $http({
            method: 'GET',
            url: '/Content/json/city.list.json',
            async: false
        }).then(function successCallback(response) {
            $.each(response.data, function (i, item) {
                var citydisplay = item.name + ", " + item.country;
                var cityvalue = item.id;
                var cityobj = {
                    value: cityvalue,
                    display: citydisplay
                };
                allCities.push(cityobj);
            });
            self.states = allCities;

        }, function errorCallback(response) {
            console.log(response);
        });

        
        
    }
    //filter function for search query
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(state) {
            return (state.display.toLocaleLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }
}
