using ArtLaShipNS.Api.Contracts;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiBankAccountServerRequestModelValidator
	{
		Task<ValidationResult> ValidateCreateAsync(ApiBankAccountServerRequestModel model);

		Task<ValidationResult> ValidateUpdateAsync(int id, ApiBankAccountServerRequestModel model);

		Task<ValidationResult> ValidateDeleteAsync(int id);
	}
}

/*<Codenesium>
    <Hash>65bad63d3c9a195d81beadcd7b125487</Hash>
</Codenesium>*/