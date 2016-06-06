MainApp.controller('DashBoardCtrl', function ($scope, $location, $route, DashBoardService, $window) {

    $scope.messages = [];

    var hub = $window.jQuery.hubConnection();

    var proxy = hub.createHubProxy('ChatHub');

    proxy.on('addMessageToPage', function (Name, Message, Time) {
        $scope.messages.push({ Name: Name, Message: Message, Time: Time });
        $scope.$apply();
    });

    hub.satrt();

    $scope.SendMessage = function () {
        proxy.invoke('send', "Tina", $scope.Msg);
    }


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