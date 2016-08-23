var SearchAreaVm = (function () {
    function SearchAreaVm(searchQueriesService, $q) {
        this._q = $q;
        this._searchQueriesService = searchQueriesService;
        this._searchResults = [];
        this._isProcessing = false;
    }
    Object.defineProperty(SearchAreaVm.prototype, "searchResults", {
        get: function () {
            return this._searchResults;
        },
        set: function (value) {
            this._searchResults = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SearchAreaVm.prototype, "searchCriteria", {
        get: function () {
            return this._searchCriteria;
        },
        set: function (value) {
            this._searchCriteria = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SearchAreaVm.prototype, "isProcessing", {
        get: function () {
            return this._isProcessing;
        },
        set: function (value) {
            this._isProcessing = value;
        },
        enumerable: true,
        configurable: true
    });
    SearchAreaVm.prototype.getAddress = function (person) {
        var address = "";
        if (person.Address1) {
            address += person.Address1;
        }
        if (person.Address2) {
            address += person.Address2;
        }
        return address;
    };
    SearchAreaVm.prototype.search = function () {
        var _this = this;
        this.isProcessing = true;
        this._searchQueriesService.searchPeople(this.searchCriteria)
            .then(function (dto) {
            _this.searchResults = dto;
            _this.isProcessing = false;
        })
            .catch(function (data) {
            // TODO: display some error message
            _this.isProcessing = false;
        });
    };
    SearchAreaVm.prototype.searchSlow = function () {
        var _this = this;
        this.isProcessing = true;
        this._searchQueriesService.searchPeopleSlow(this.searchCriteria)
            .then(function (dto) {
            _this.searchResults = dto;
            _this.isProcessing = false;
        })
            .catch(function (data) {
            // TODO: display some error message
            _this.isProcessing = false;
        });
    };
    return SearchAreaVm;
}());
