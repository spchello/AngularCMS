MainApp.controller('DashBoardCtrl', function ($scope, $location, $route, DashBoardService) {

    $scope.IsLoad = true;

    DashBoardService.getData()
        .then(function (data) {
            //success
            $scope.datas = data;
            $scope.IsLoad = false;
        }, function (data) {
            //error
            $scope.error = "取得資料錯誤!";
            $scope.IsLoad = false;
        });
});