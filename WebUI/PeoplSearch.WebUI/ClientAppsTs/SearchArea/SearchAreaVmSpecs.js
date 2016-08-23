/// <reference path="../_references.ts" />
describe("VideModel: SearchAreaVm", function () {
    var q;
    var searchQueriesService;
    beforeEach(function () {
        angular.module("app")
            .service("searchAreaVm", SearchAreaVm);
        module("app");
    });
    beforeEach(function () {
        inject(function (_searchQueriesService_, $q) {
            q = $q;
            searchQueriesService = _searchQueriesService_;
        });
    });
    describe("constructor", function () {
        it("should initialize searchResults to empty array", function () {
            // Arrange
            inject(function (searchAreaVm) {
                // Act // Assert
                expect(searchAreaVm.searchResults.length).toBe(0);
            });
        });
    });
    describe("search", function () {
        var searchPeopleSpy;
        var deferred;
        beforeEach(function () {
            inject(function ($httpBackend) {
                deferred = q.defer();
                searchPeopleSpy = spyOn(searchQueriesService, "searchPeople")
                    .andCallFake(function () {
                    return deferred.promise;
                });
            });
        });
        it("should set isProcessing to true then back to false after server response", function () {
            inject(function (searchAreaVm, $rootScope) {
                // Arrange
                expect(searchAreaVm.isProcessing).toBeFalsy();
                searchAreaVm.searchCriteria = "Darth Vader";
                // Act
                searchAreaVm.search();
                // Assert
                expect(searchPeopleSpy).toHaveBeenCalled();
                expect(searchAreaVm.isProcessing).toBeTruthy();
                // Act
                deferred.resolve({});
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.isProcessing).toBeFalsy();
            });
        });
        it("should set properties from the dto", function () {
            inject(function (searchAreaVm, $rootScope) {
                // Arrange
                searchAreaVm.searchCriteria = "Darth Vader";
                // this is not the exact shape that comes back from the server, but it will
                // suffice to verify that things are working correctly
                var searchResults = [{ FirstName: "Darth", LastName: "Vader" }];
                // Act
                searchAreaVm.search();
                deferred.resolve(searchResults);
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.searchResults.length).toBe(1);
                expect(searchAreaVm.searchResults[0].FirstName).toBe("Darth");
                expect(searchAreaVm.searchResults[0].LastName).toBe("Vader");
            });
        });
    });
    describe("searchSlow", function () {
        var searchPeopleSpy;
        var deferred;
        beforeEach(function () {
            inject(function ($httpBackend) {
                deferred = q.defer();
                searchPeopleSpy = spyOn(searchQueriesService, "searchPeopleSlow")
                    .andCallFake(function () {
                    return deferred.promise;
                });
            });
        });
        it("should set isProcessing to true then back to false after server response", function () {
            inject(function (searchAreaVm, $rootScope) {
                // Arrange
                expect(searchAreaVm.isProcessing).toBeFalsy();
                searchAreaVm.searchCriteria = "Darth Vader";
                // Act
                searchAreaVm.searchSlow();
                // Assert
                expect(searchPeopleSpy).toHaveBeenCalled();
                expect(searchAreaVm.isProcessing).toBeTruthy();
                // Act
                deferred.resolve({});
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.isProcessing).toBeFalsy();
            });
        });
        it("should set properties from the dto", function () {
            inject(function (searchAreaVm, $rootScope) {
                // Arrange
                searchAreaVm.searchCriteria = "Darth Vader";
                // this is not the exact shape that comes back from the server, but it will
                // suffice to verify that things are working correctly
                var searchResults = [{ FirstName: "Darth", LastName: "Vader" }];
                // Act
                searchAreaVm.searchSlow();
                deferred.resolve(searchResults);
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.searchResults.length).toBe(1);
                expect(searchAreaVm.searchResults[0].FirstName).toBe("Darth");
                expect(searchAreaVm.searchResults[0].LastName).toBe("Vader");
            });
        });
    });
});
