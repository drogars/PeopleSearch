class SearchAreaVm {
    private _searchResults: any[];
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

    get searchResults(): any[] {
        return this._searchResults;
    }

    set searchResults(value: any[]) {
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

    getAddress(person: any): string {
        var address: string = "";
        if (person.Address1) {
            address += person.Address1;
        }
        if (person.Address2) {
            address += person.Address2;
        }

        return address;
    }
    
    search() {
        this.isProcessing = true;
        this._searchQueriesService.searchPeople(this.searchCriteria)
            .then((dto: any[]) => {
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
            .then((dto: any[]) => {
                this.searchResults = dto;
                this.isProcessing = false;
            })
            .catch((data) => {
                // TODO: display some error message
                this.isProcessing = false;
            });
    }

}