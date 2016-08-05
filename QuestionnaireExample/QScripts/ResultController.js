app.controller('ResultController', function ($scope, $location, $route, WebService, ShareData) {    

    GetReport();

    // принимаем данные отчета в зависимости от его типа
    function GetReport()
    {
        var type = $route.current.typedata;
        var ReportData;

        switch (type) {
            case 'report':
                ReportData = WebService.getGeneralReport();
                break;
            case 'myReport':
                ReportData = WebService.getMyReport();
                break;
            case 'result':
                ReportData = WebService.getValueUser(ShareData.userkey);
                break;
        }

        GetData(ReportData);
    }

    function GetData(report) {
        report.then(function (pl) {
            // принимаем данные
            $scope.Values = pl.data;
        },
        function (errorPl) {
            $scope.error = errorPl;
        });
    }

});