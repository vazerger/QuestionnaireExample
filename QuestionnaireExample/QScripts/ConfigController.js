app.controller('ConfigController', function ($scope, $location, WebService, ShareData) {    
    $scope.Remove = function () {
        $scope.msg = null;
        if (confirm("Вы уверенны, что нужно удалить все данные отчетов?")) {

            var removeAllValues = WebService.delete();

            removeAllValues.then(function (pl) {
                    $scope.msg = 'Удалено!';
            },
                  function (errorPl) {
                      $scope.error = errorPl;
                  });
        }
    }
});