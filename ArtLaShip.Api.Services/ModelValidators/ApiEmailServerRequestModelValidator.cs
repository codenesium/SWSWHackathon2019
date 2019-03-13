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
			this.EmailValueRules();
			return await this.ValidateAsync(model);
		}

		public async Task<ValidationResult> ValidateUpdateAsync(int id, ApiEmailServerRequestModel model)
		{
			this.ArtistIdRules();
			this.DateCreatedRules();
			this.EmailValueRules();
			return await this.ValidateAsync(model, id);
		}

		public async Task<ValidationResult> ValidateDeleteAsync(int id)
		{
			return await Task.FromResult<ValidationResult>(new ValidationResult());
		}
	}
}

/*<Codenesium>
    <Hash>4ff014df6a36d673ef358c338e80b8b3</Hash>
</Codenesium>*/