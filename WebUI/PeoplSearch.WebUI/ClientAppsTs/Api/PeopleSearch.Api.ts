// ReSharper disable RedundantQualifier

module PeopleSearch.Infrastructure.Dto {
	export interface IPersonDto   {
		personId: number;
		firstName: string;
		lastName: string;
		age: string;
		address1: string;
		address2: string;
		city: string;
		state: string;
		postalCode: string;
		interests: string[];
		picture: string;
	}

	export class PersonDto  implements IPersonDto {
		personId: number;
		firstName: string;
		lastName: string;
		age: string;
		address1: string;
		address2: string;
		city: string;
		state: string;
		postalCode: string;
		interests: string[];
		picture: string;
		public static fromJSON(json: any) : PersonDto {
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
				city: json.city,
				state: json.state,
				postalCode: json.postalCode,
				interests: json.interests === null ? null : json.interests.map(o => o),
				picture: json.picture
			};
		}
	}
}
module PeopleSearch.Server.Server {
	export interface IQueryErrorResult   {
		message: string;
	}

	export class QueryErrorResult  implements IQueryErrorResult {
		message: string;
		public static fromJSON(json: any) : QueryErrorResult {
			if (json === undefined)
				return undefined;
			if (json === null)
				return null;

			return {
				message: json.message
			};
		}
	}
}

// ReSharper restore RedundantQualifier
