var app = angular.module("ApplicationModule", ["ngRoute"]);

app.factory("ShareData", function () {
    return {
        userkey: 0 // id пользователя
    };
});

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    debugger;

    $routeProvider.when('/',
                    {
                        templateUrl: 'Home/YourName',
                        controller: 'ShowController'
                    });
    $routeProvider.when('/result',
                        {
                            templateUrl: 'Home/Result',
                            controller: 'ResultController',
                            typedata: 'result'
                        });
    $routeProvider.when('/report',
                    {
                        templateUrl: 'Home/Report',
                        controller: 'ResultController',
                        typedata: 'report'
                    });

    $routeProvider.when('/myReport',
                    {
                        templateUrl: 'Home/myReport',
                        controller: 'ResultController',
                        typedata: 'myReport'
                    });

    $routeProvider.when('/config',
                    {
                        templateUrl: 'Home/Config',
                        controller: 'ConfigController'
                    });

    $routeProvider.otherwise(
                        {
                          redirectTo: '/'
                        });

    $locationProvider.html5Mode(true).hashPrefix('!');
}]);