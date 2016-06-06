var MainApp = angular.module('MainApp', ['ngRoute', 'ui.bootstrap']);

MainApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    var viewBase = '/App/';

    $routeProvider
    //客戶列表　        
    .when('/Customer', { // href
        controller: 'CustCtrl', //對應的controller
        templateUrl: viewBase + 'CustomerApp/List.html' //對應的頁面
    })
    //新增客戶
    .when('/Customer/Add', {
        controller: 'CustAddCtrl',
        templateUrl: viewBase + 'CustomerApp/AddCustomer.html'
    })
    //編輯客戶
    .when('/Customer/Edit/:id', {
        controller: 'CustEditCtrl',
        templateUrl: viewBase + 'CustomerApp/EditCustomer.html'
    })
    .otherwise({
        controller: 'DashBoardCtrl',
        templateUrl: viewBase + 'DashBoardApp/Index.html'
    });
    
    $locationProvider.html5Mode(
       {
           enabled: true,
           requireBase: false
       });
}]);