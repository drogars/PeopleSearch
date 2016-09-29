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
                var searchResults = [{
                        personId: 1,
                        firstName: "Darth",
                        lastName: "Vader",
                        age: "400",
                        address1: "address2",
                        address2: "address2",
                        city: "Smallville",
                        state: "UT",
                        postalCode: "",
                        interests: ["bob"],
                        picture: "VGhpcyBpcyBhIHRlc3Qu"
                    }
                ];
                // Act
                searchAreaVm.search();
                deferred.resolve(searchResults);
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.searchResults.length).toBe(1);
                expect(searchAreaVm.searchResults[0].personId).toBe(searchResults[0].personId);
                expect(searchAreaVm.searchResults[0].firstName).toBe(searchResults[0].firstName);
                expect(searchAreaVm.searchResults[0].lastName).toBe(searchResults[0].lastName);
                expect(searchAreaVm.searchResults[0].age).toBe(searchResults[0].age);
                expect(searchAreaVm.searchResults[0].address1).toBe(searchResults[0].address1);
                expect(searchAreaVm.searchResults[0].address2).toBe(searchResults[0].address2);
                expect(searchAreaVm.searchResults[0].city).toBe(searchResults[0].city);
                expect(searchAreaVm.searchResults[0].state).toBe(searchResults[0].state);
                expect(searchAreaVm.searchResults[0].postalCode).toBe(searchResults[0].postalCode);
                expect(searchAreaVm.searchResults[0].interests).toBe(searchResults[0].interests);
                expect(searchAreaVm.searchResults[0].picture).toBe(searchResults[0].picture);
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
                var searchResults = [{
                        personId: 1,
                        firstName: "Darth",
                        lastName: "Vader",
                        age: "400",
                        address1: "address2",
                        address2: "address2",
                        city: "Smallville",
                        state: "UT",
                        postalCode: "",
                        interests: ["bob"],
                        picture: "VGhpcyBpcyBhIHRlc3Qu"
                    }
                ];
                // Act
                searchAreaVm.searchSlow();
                deferred.resolve(searchResults);
                $rootScope.$digest();
                // Assert
                expect(searchAreaVm.searchResults.length).toBe(1);
                expect(searchAreaVm.searchResults[0].personId).toBe(searchResults[0].personId);
                expect(searchAreaVm.searchResults[0].firstName).toBe(searchResults[0].firstName);
                expect(searchAreaVm.searchResults[0].lastName).toBe(searchResults[0].lastName);
                expect(searchAreaVm.searchResults[0].age).toBe(searchResults[0].age);
                expect(searchAreaVm.searchResults[0].address1).toBe(searchResults[0].address1);
                expect(searchAreaVm.searchResults[0].address2).toBe(searchResults[0].address2);
                expect(searchAreaVm.searchResults[0].city).toBe(searchResults[0].city);
                expect(searchAreaVm.searchResults[0].state).toBe(searchResults[0].state);
                expect(searchAreaVm.searchResults[0].postalCode).toBe(searchResults[0].postalCode);
                expect(searchAreaVm.searchResults[0].interests).toBe(searchResults[0].interests);
                expect(searchAreaVm.searchResults[0].picture).toBe(searchResults[0].picture);
            });
        });
    });
});
//# sourceMappingURL=SearchAreaVmSpecs.js.map