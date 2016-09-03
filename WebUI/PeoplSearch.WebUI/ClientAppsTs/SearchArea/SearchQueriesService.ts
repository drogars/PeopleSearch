interface ISearchQueriesService {
    searchPeople: (firstName: string, lastName: string) => ng.IPromise<any>;
    searchPeopleSlow: (firstName: string, lastName: string) => ng.IPromise<any>;
}

class SearchQueriesService implements ISearchQueriesService {
    private _baseApiUrl: string = '/api/v10/personqueriesapi/';
    private _http: ng.IHttpService;
    private _q: ng.IQService;

    constructor($http: ng.IHttpService, $q: ng.IQService) {
        this._http = $http;
        this._q = $q;
    }

    searchPeople(searchCriteria: string): ng.IPromise<PeopleSearch.Infrastructure.Dto.PersonDto[]> {
        var deferred = this._q.defer();
        var apiUrl = this._baseApiUrl + 'searchPeople?searchCriteria=' + encodeURIComponent(searchCriteria);

        this._http({ method: 'GET', url: apiUrl })
            .success((data: any[]) => {
                data = data || [];
                var response = data.map(p => PeopleSearch.Infrastructure.Dto.PersonDto.fromJSON(p));
                deferred.resolve(response);
            })
            .error(() => {
                deferred.reject('An error occurred while searching for people. Please try again.');
            });

        return deferred.promise;
    }

    searchPeopleSlow(searchCriteria: string): ng.IPromise<any> {
        var deferred = this._q.defer();

        var apiUrl = this._baseApiUrl + 'searchPeopleSlow?searchCriteria=' + encodeURIComponent(searchCriteria);

        this._http({ method: 'GET', url: apiUrl })
            .success((data: any[]) => {
                data = data || [];
                var response = data.map(p => PeopleSearch.Infrastructure.Dto.PersonDto.fromJSON(p));
                deferred.resolve(response);
            })
            .error(() => {
                deferred.reject('An error occurred while searching for people. Please try again.');
            });

        return deferred.promise;
    }
}

app.factory('searchQueriesService',
    ($http: ng.IHttpService, $q: ng.IQService) => new SearchQueriesService($http, $q));