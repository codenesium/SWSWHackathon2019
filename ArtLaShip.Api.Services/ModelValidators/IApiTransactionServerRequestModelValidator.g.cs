using ArtLaShipNS.Api.Contracts;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiTransactionServerRequestModelValidator
	{
		Task<ValidationResult> ValidateCreateAsync(ApiTransactionServerRequestModel model);

		Task<ValidationResult> ValidateUpdateAsync(int id, ApiTransactionServerRequestModel model);

		Task<ValidationResult> ValidateDeleteAsync(int id);
	}
}

/*<Codenesium>
    <Hash>08a96e6f2c3568d5476a882b27233043</Hash>
</Codenesium>*/