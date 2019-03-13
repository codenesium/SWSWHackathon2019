using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class ApiEmailServerRequestModelValidator : AbstractApiEmailServerRequestModelValidator, IApiEmailServerRequestModelValidator
	{
		public ApiEmailServerRequestModelValidator(IEmailRepository emailRepository)
			: base(emailRepository)
		{
		}

		public async Task<ValidationResult> ValidateCreateAsync(ApiEmailServerRequestModel model)
		{
			this.ArtistIdRules();
			this.DateCreatedRules();
			this.Email1Rules();
			return await this.ValidateAsync(model);
		}

		public async Task<ValidationResult> ValidateUpdateAsync(int id, ApiEmailServerRequestModel model)
		{
			this.ArtistIdRules();
			this.DateCreatedRules();
			this.Email1Rules();
			return await this.ValidateAsync(model, id);
		}

		public async Task<ValidationResult> ValidateDeleteAsync(int id)
		{
			return await Task.FromResult<ValidationResult>(new ValidationResult());
		}
	}
}

/*<Codenesium>
    <Hash>5319be6a0480fe5b8fb22a61d5eebebb</Hash>
</Codenesium>*/