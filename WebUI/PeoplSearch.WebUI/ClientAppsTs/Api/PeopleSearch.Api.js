// ReSharper disable RedundantQualifier
var PeopleSearch;
(function (PeopleSearch) {
    var Infrastructure;
    (function (Infrastructure) {
        var Dto;
        (function (Dto) {
            var PersonDto = (function () {
                function PersonDto() {
                }
                PersonDto.fromJSON = function (json) {
                    if (json === undefined)
                        return undefined;
                    if (json === null)
                        return null;
                    return {
                        personId: json.personId,
                        firstName: json.firstName,
                        lastName: json.lastName,
                        age: json.age,
                        address1: json.address1,
                        address2: json.address2,
                        address3: json.address3,
                        city: json.city,
                        state: json.state,
                        postalCode: json.postalCode,
                        interests: json.interests === null ? null : json.interests.map(function (o) { return o; }),
                        picture: json.picture === null ? null : json.picture.map(function (o) { return o; })
                    };
                };
                return PersonDto;
            }());
            Dto.PersonDto = PersonDto;
        })(Dto = Infrastructure.Dto || (Infrastructure.Dto = {}));
    })(Infrastructure = PeopleSearch.Infrastructure || (PeopleSearch.Infrastructure = {}));
})(PeopleSearch || (PeopleSearch = {}));
(function (PeopleSearch) {
    var Server;
    (function (Server_1) {
        var Server;
        (function (Server) {
            var QueryErrorResult = (function () {
                function QueryErrorResult() {
                }
                QueryErrorResult.fromJSON = function (json) {
                    if (json === undefined)
                        return undefined;
                    if (json === null)
                        return null;
                    return {
                        message: json.message
                    };
                };
                return QueryErrorResult;
            }());
            Server.QueryErrorResult = QueryErrorResult;
        })(Server = Server_1.Server || (Server_1.Server = {}));
    })(Server = PeopleSearch.Server || (PeopleSearch.Server = {}));
})(PeopleSearch || (PeopleSearch = {}));
// ReSharper restore RedundantQualifier
//# sourceMappingURL=PeopleSearch.Api.js.map