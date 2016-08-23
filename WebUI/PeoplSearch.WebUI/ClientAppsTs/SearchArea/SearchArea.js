app.controller('searchAreaCtrl', function ($scope, searchQueriesService, $q) {
    $scope.vm = new SearchAreaVm(searchQueriesService, $q);
});
