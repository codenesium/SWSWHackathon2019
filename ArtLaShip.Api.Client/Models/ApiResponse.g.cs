using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiResponse
	{
		public ApiResponse()
		{
		}

		public void Merge(ApiResponse from)
		{
			from.Artists.ForEach(x => this.AddArtist(x));
			from.BankAccounts.ForEach(x => this.AddBankAccount(x));
			from.Transactions.ForEach(x => this.AddTransaction(x));
			from.Emails.ForEach(x => this.AddEmail(x));
		}

		public List<ApiArtistClientResponseModel> Artists { get; private set; } = new List<ApiArtistClientResponseModel>();

		public List<ApiBankAccountClientResponseModel> BankAccounts { get; private set; } = new List<ApiBankAccountClientResponseModel>();

		public List<ApiTransactionClientResponseModel> Transactions { get; private set; } = new List<ApiTransactionClientResponseModel>();

		public List<ApiEmailClientResponseModel> Emails { get; private set; } = new List<ApiEmailClientResponseModel>();

		public void AddArtist(ApiArtistClientResponseModel item)
		{
			if (!this.Artists.Any(x => x.Id == item.Id))
			{
				this.Artists.Add(item);
			}
		}

		public void AddBankAccount(ApiBankAccountClientResponseModel item)
		{
			if (!this.BankAccounts.Any(x => x.Id == item.Id))
			{
				this.BankAccounts.Add(item);
			}
		}

		public void AddTransaction(ApiTransactionClientResponseModel item)
		{
			if (!this.Transactions.Any(x => x.Id == item.Id))
			{
				this.Transactions.Add(item);
			}
		}

		public void AddEmail(ApiEmailClientResponseModel item)
		{
			if (!this.Emails.Any(x => x.Id == item.Id))
			{
				this.Emails.Add(item);
			}
		}
	}
}

/*<Codenesium>
    <Hash>8b51d75d5c465211ed12519fb4d9a75e</Hash>
</Codenesium>*/