MainApp.controller('CustEditCtrl', function ($scope, $location, $routeParams, $route, CustService) {
    //透過QueryString來傳遞客戶的ID，$routeParams可以讓我們很方便的去取得參數
    var id = $routeParams.id;
    $scope.IsLoad = true;

    CustService.getCustomer(id).then(function (response) {
        $scope.Customer = response;
        $scope.IsLoad = false;
    }, function () {
        $scope.error = "取得資料錯誤!";
        $scope.IsLoad = false;
    });

    $scope.Update = function () {
        CustService.Update($scope.Customer).then(function (response) {
            alert("Update成功");
            $scope.IsLoad = false;
        }, function () {
            alert("Update失敗");
            $scope.IsLoad = false;
        });
    };

});