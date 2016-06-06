MainApp.controller('CustCtrl', function ($scope, $location, $route, CustService) {
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