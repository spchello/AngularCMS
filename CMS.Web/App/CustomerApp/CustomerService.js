MainApp.factory("CustService", function($http, $q){
    return {
        //分頁
        getData: function (currentPage, pageSize) {
            var deferred = $q.defer();
            $http.get('/api/Customer', { params: { CurrPage: currentPage, PageSize: pageSize } })
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        },
        //取得單筆
        getCustomer: function (CustomerID) {
            var deferred = $q.defer();
            $http.get('/api/Customer', { params: { CustomerID: CustomerID } })
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        },
        //新增單筆
        AddData: function (Customer) {
            var deferred = $q.defer();
            $http.post('/api/Customer', Customer)
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        },
        //更新單筆
        Update: function (Customer) {
            var deferred = $q.defer();
            $http.put('/api/Customer', Customer)
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        },
        //刪除單筆
        deleteCustomer: function (Customer) {
            var deferred = $q.defer();
            $http.delete('/api/Customer?id=' + Customer.CustomerID)
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        }
    }
});