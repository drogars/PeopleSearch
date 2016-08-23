var SearchQueriesService = (function () {
    function SearchQueriesService($http, $q) {
        this._baseApiUrl = '/api/v10/personqueriesapi/';
        this._http = $http;
        this._q = $q;
    }
    SearchQueriesService.prototype.searchPeople = function (searchCriteria) {
        var deferred = this._q.defer();
        var apiUrl = this._baseApiUrl + 'searchPeople?searchCriteria=' + encodeURIComponent(searchCriteria);
        this._http({ method: 'GET', url: apiUrl })
            .success(function (data) {
            deferred.resolve(data);
        })
            .error(function () {
            deferred.reject('An error occurred while searching for people. Please try again.');
        });
        return deferred.promise;
    };
    SearchQueriesService.prototype.searchPeopleSlow = function (searchCriteria) {
        var deferred = this._q.defer();
        var apiUrl = this._baseApiUrl + 'searchPeopleSlow?searchCriteria=' + encodeURIComponent(searchCriteria);
        this._http({ method: 'GET', url: apiUrl })
            .success(function (data) {
            deferred.resolve(data);
        })
            .error(function () {
            deferred.reject('An error occurred while searching for people. Please try again.');
        });
        return deferred.promise;
    };
    return SearchQueriesService;
}());
app.factory('searchQueriesService', function ($http, $q) { return new SearchQueriesService($http, $q); });
