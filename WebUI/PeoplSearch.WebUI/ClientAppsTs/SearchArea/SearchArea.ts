interface ISearchAreaScope extends ng.IScope {
    vm: SearchAreaVm;
}

app.controller('searchAreaCtrl', function (
    $scope: ISearchAreaScope,
    searchQueriesService: SearchQueriesService,
    $q: ng.IQService) {

    $scope.vm = new SearchAreaVm(searchQueriesService, $q);
});

