using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class ApiArtistServerRequestModelValidator : AbstractApiArtistServerRequestModelValidator, IApiArtistServerRequestModelValidator
	{
		public ApiArtistServerRequestModelValidator(IArtistRepository artistRepository)
			: base(artistRepository)
		{
		}

		public async Task<ValidationResult> ValidateCreateAsync(ApiArtistServerRequestModel model)
		{
			this.AspNetUserIdRules();
			this.BioRules();
			this.ExternalIdRules();
			this.FacebookRules();
			this.NameRules();
			this.SoundCloudRules();
			this.TwitterRules();
			this.VenmoRules();
			this.WebsiteRules();
			return await this.ValidateAsync(model);
		}

		public async Task<ValidationResult> ValidateUpdateAsync(int id, ApiArtistServerRequestModel model)
		{
			this.AspNetUserIdRules();
			this.BioRules();
			this.ExternalIdRules();
			this.FacebookRules();
			this.NameRules();
			this.SoundCloudRules();
			this.TwitterRules();
			this.VenmoRules();
			this.WebsiteRules();
			return await this.ValidateAsync(model, id);
		}

		public async Task<ValidationResult> ValidateDeleteAsync(int id)
		{
			return await Task.FromResult<ValidationResult>(new ValidationResult());
		}
	}
}

/*<Codenesium>
    <Hash>2ad304f635184713e565d9fabf84f2b0</Hash>
</Codenesium>*/