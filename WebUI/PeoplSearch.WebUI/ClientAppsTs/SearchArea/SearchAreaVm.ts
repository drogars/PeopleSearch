class SearchAreaVm {
    private _searchResults: PeopleSearch.Infrastructure.Dto.PersonDto[];
    private _q: ng.IQService;
    private _searchQueriesService: SearchQueriesService;
    private _isProcessing: boolean;
    private _searchCriteria: string;

    constructor(
        searchQueriesService: SearchQueriesService,
        $q: ng.IQService

    ) {
        this._q = $q;
        this._searchQueriesService = searchQueriesService;

        this._searchResults = [];
        this._isProcessing = false;
    }

    get searchResults(): PeopleSearch.Infrastructure.Dto.PersonDto[] {
        return this._searchResults;
    }

    set searchResults(value: PeopleSearch.Infrastructure.Dto.PersonDto[]) {
        this._searchResults = value;
    }

    get searchCriteria(): string {
        return this._searchCriteria;
    }

    set searchCriteria(value: string) {
        this._searchCriteria = value;
    }

    get isProcessing(): boolean {
        return this._isProcessing;
    }

    set isProcessing(value: boolean) {
        this._isProcessing = value;
    }

    getAddress(person: PeopleSearch.Infrastructure.Dto.PersonDto): string {
        var address: string = "";
        if (person.address1) {
            address += person.address1;
        }
        if (person.address2) {
            address += person.address2;
        }

        return address;
    }
    
    search() {
        this.isProcessing = true;
        this._searchQueriesService.searchPeople(this.searchCriteria)
            .then((dto: PeopleSearch.Infrastructure.Dto.PersonDto[]) => {
                this.searchResults = dto;
                this.isProcessing = false;
            })
            .catch((data) => {
                // TODO: display some error message
                this.isProcessing = false;
            });
    }

    searchSlow() {
        this.isProcessing = true;
        this._searchQueriesService.searchPeopleSlow(this.searchCriteria)
            .then((dto: PeopleSearch.Infrastructure.Dto.PersonDto[]) => {
                this.searchResults = dto;
                this.isProcessing = false;
            })
            .catch((data) => {
                // TODO: display some error message
                this.isProcessing = false;
            });
    }

}