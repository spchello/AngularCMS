MainApp.controller('CustAddCtrl', function ($scope, $location, $route, CustService) {
    $scope.IsLoad = true;

    $scope.CustomerID = '';
    $scope.CompanyName = '';
    $scope.ContactName = '';

    $scope.Add = function () {

        var Customer = {
            CustomerID: $scope.CustomerID,
            CompanyName: $scope.CompanyName,
            ContactName: $scope.ContactName,
        }


        CustService.AddData(Customer).then(function () {
            alert("add成功");
            $scope.IsLoad = false;
            $scope.CustomerID = '';
            $scope.CompanyName = '';
            $scope.ContactName = '';
        }, function () {
            alert("add失敗");
            $scope.IsLoad = false;
        });

    };
});