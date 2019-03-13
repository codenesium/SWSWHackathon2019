using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class ApiBankAccountServerRequestModelValidator : AbstractApiBankAccountServerRequestModelValidator, IApiBankAccountServerRequestModelValidator
	{
		public ApiBankAccountServerRequestModelValidator(IBankAccountRepository bankAccountRepository)
			: base(bankAccountRepository)
		{
		}

		public async Task<ValidationResult> ValidateCreateAsync(ApiBankAccountServerRequestModel model)
		{
			this.AccountNumberRules();
			this.ArtistIdRules();
			this.RoutingNumberRules();
			return await this.ValidateAsync(model);
		}

		public async Task<ValidationResult> ValidateUpdateAsync(int id, ApiBankAccountServerRequestModel model)
		{
			this.AccountNumberRules();
			this.ArtistIdRules();
			this.RoutingNumberRules();
			return await this.ValidateAsync(model, id);
		}

		public async Task<ValidationResult> ValidateDeleteAsync(int id)
		{
			return await Task.FromResult<ValidationResult>(new ValidationResult());
		}
	}
}

/*<Codenesium>
    <Hash>efed83705e1c7c6dbc07e6fcb3937205</Hash>
</Codenesium>*/