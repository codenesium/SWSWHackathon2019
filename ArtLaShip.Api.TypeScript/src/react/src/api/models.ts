export class ArtistClientRequestModel {
				aspNetUserId:string;
bio:string;
externalId:any;
facebook:string;
id:number;
name:string;
soundCloud:string;
twitter:string;
website:string;

	
				constructor() {
					this.aspNetUserId = '';
this.bio = '';
this.externalId = undefined;
this.facebook = '';
this.id = 0;
this.name = '';
this.soundCloud = '';
this.twitter = '';
this.website = '';

				}

				setProperties(aspNetUserId : string,bio : string,externalId : any,facebook : string,id : number,name : string,soundCloud : string,twitter : string,website : string) : void
				{
					this.aspNetUserId = aspNetUserId;
this.bio = bio;
this.externalId = externalId;
this.facebook = facebook;
this.id = id;
this.name = name;
this.soundCloud = soundCloud;
this.twitter = twitter;
this.website = website;

				}
			}

			export class ArtistClientResponseModel {
				aspNetUserId:string;
bio:string;
externalId:any;
facebook:string;
id:number;
name:string;
soundCloud:string;
twitter:string;
website:string;

	
				constructor() {
					this.aspNetUserId = '';
this.bio = '';
this.externalId = undefined;
this.facebook = '';
this.id = 0;
this.name = '';
this.soundCloud = '';
this.twitter = '';
this.website = '';

				}

				setProperties(aspNetUserId : string,bio : string,externalId : any,facebook : string,id : number,name : string,soundCloud : string,twitter : string,website : string) : void
				{
					this.aspNetUserId = aspNetUserId;
this.bio = bio;
this.externalId = externalId;
this.facebook = facebook;
this.id = id;
this.name = name;
this.soundCloud = soundCloud;
this.twitter = twitter;
this.website = website;

				}
			}
			export class BankAccountClientRequestModel {
				accountNumber:string;
artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
id:number;
routingNumber:string;

	
				constructor() {
					this.accountNumber = '';
this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.id = 0;
this.routingNumber = '';

				}

				setProperties(accountNumber : string,artistId : number,id : number,routingNumber : string) : void
				{
					this.accountNumber = accountNumber;
this.artistId = artistId;
this.id = id;
this.routingNumber = routingNumber;

				}
			}

			export class BankAccountClientResponseModel {
				accountNumber:string;
artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
id:number;
routingNumber:string;

	
				constructor() {
					this.accountNumber = '';
this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.id = 0;
this.routingNumber = '';

				}

				setProperties(accountNumber : string,artistId : number,id : number,routingNumber : string) : void
				{
					this.accountNumber = accountNumber;
this.artistId = artistId;
this.id = id;
this.routingNumber = routingNumber;

				}
			}
			export class TransactionClientRequestModel {
				amount:number;
artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
dateCreated:any;
id:number;
stripeTransactionId:string;

	
				constructor() {
					this.amount = 0;
this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.dateCreated = undefined;
this.id = 0;
this.stripeTransactionId = '';

				}

				setProperties(amount : number,artistId : number,dateCreated : any,id : number,stripeTransactionId : string) : void
				{
					this.amount = amount;
this.artistId = artistId;
this.dateCreated = dateCreated;
this.id = id;
this.stripeTransactionId = stripeTransactionId;

				}
			}

			export class TransactionClientResponseModel {
				amount:number;
artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
dateCreated:any;
id:number;
stripeTransactionId:string;

	
				constructor() {
					this.amount = 0;
this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.dateCreated = undefined;
this.id = 0;
this.stripeTransactionId = '';

				}

				setProperties(amount : number,artistId : number,dateCreated : any,id : number,stripeTransactionId : string) : void
				{
					this.amount = amount;
this.artistId = artistId;
this.dateCreated = dateCreated;
this.id = id;
this.stripeTransactionId = stripeTransactionId;

				}
			}
			export class EmailClientRequestModel {
				artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
dateCreated:any;
emailValue:string;
id:number;

	
				constructor() {
					this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.dateCreated = undefined;
this.emailValue = '';
this.id = 0;

				}

				setProperties(artistId : number,dateCreated : any,emailValue : string,id : number) : void
				{
					this.artistId = artistId;
this.dateCreated = dateCreated;
this.emailValue = emailValue;
this.id = id;

				}
			}

			export class EmailClientResponseModel {
				artistId:number;
artistIdEntity : string;
artistIdNavigation? : ArtistClientResponseModel;
dateCreated:any;
emailValue:string;
id:number;

	
				constructor() {
					this.artistId = 0;
this.artistIdEntity = '';
this.artistIdNavigation = undefined;
this.dateCreated = undefined;
this.emailValue = '';
this.id = 0;

				}

				setProperties(artistId : number,dateCreated : any,emailValue : string,id : number) : void
				{
					this.artistId = artistId;
this.dateCreated = dateCreated;
this.emailValue = emailValue;
this.id = id;

				}
			}

/*<Codenesium>
    <Hash>fdb6ab2a9043bfab451e6a864185122b</Hash>
</Codenesium>*/