MainApp.controller('CustCtrl', function ($scope, $location, $route, CustService) {
    //$location, $route -->SPA網站 ng-view不用重新整理
    //CustService       -->WebAPI Client邏輯拆開

    $scope.IsLoad = true;

    //分頁所需參數
    $scope.totalRecords = 0;
    $scope.currentPage = 1;
    $scope.pageSize = 10;

    //點選換頁時觸發
    $scope.pageChanged = function () {
        $scope.IsLoad = true;
        GetData();
    };

    //點選編輯時 轉導頁面
    $scope.Edit = function (CustomerID) {
        $location.path('/Customer/Edit/' + CustomerID);
    };

    //點選刪除時 給Service ID 並呼叫 web API
    $scope.Delete = function (Customer) {
        CustService.deleteCustomer(Customer).then(function () {
            alert("刪除成功");
            $scope.currentPage = 1;
            GetData();
            $scope.IsLoad = false;
        }, function () {
            alert("刪除失敗");
            $scope.IsLoad = false;
        });

    };

    //載入CustService參數來使用 再.then裡面來接回Service成功/錯誤回傳的值。
    var GetData = function () {
        CustService.getData($scope.currentPage, $scope.pageSize)
        .then(function (data) {
            //success
            $scope.Customers = data.Data;
            $scope.totalRecords = data.Total;
            $scope.IsLoad = false;
        }, function (data) {
            //error
            $scope.error = "取得資料錯誤!";
            $scope.IsLoad = true;
        });

    };

    //Page Load時去要資料
    GetData();
});