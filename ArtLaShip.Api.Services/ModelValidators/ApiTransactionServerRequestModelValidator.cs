using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class ApiTransactionServerRequestModelValidator : AbstractApiTransactionServerRequestModelValidator, IApiTransactionServerRequestModelValidator
	{
		public ApiTransactionServerRequestModelValidator(ITransactionRepository transactionRepository)
			: base(transactionRepository)
		{
		}

		public async Task<ValidationResult> ValidateCreateAsync(ApiTransactionServerRequestModel model)
		{
			this.AmountRules();
			this.ArtistIdRules();
			this.DateCreatedRules();
			this.StripeTransactionIdRules();
			return await this.ValidateAsync(model);
		}

		public async Task<ValidationResult> ValidateUpdateAsync(int id, ApiTransactionServerRequestModel model)
		{
			this.AmountRules();
			this.ArtistIdRules();
			this.DateCreatedRules();
			this.StripeTransactionIdRules();
			return await this.ValidateAsync(model, id);
		}

		public async Task<ValidationResult> ValidateDeleteAsync(int id)
		{
			return await Task.FromResult<ValidationResult>(new ValidationResult());
		}
	}
}

/*<Codenesium>
    <Hash>a980bdf01a9ce47e288cd7f10b4db130</Hash>
</Codenesium>*/