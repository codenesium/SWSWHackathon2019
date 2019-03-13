using ArtLaShipNS.Api.Contracts;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiArtistServerRequestModelValidator
	{
		Task<ValidationResult> ValidateCreateAsync(ApiArtistServerRequestModel model);

		Task<ValidationResult> ValidateUpdateAsync(int id, ApiArtistServerRequestModel model);

		Task<ValidationResult> ValidateDeleteAsync(int id);
	}
}

/*<Codenesium>
    <Hash>e75b311987478175287e532ce0f5a37b</Hash>
</Codenesium>*/