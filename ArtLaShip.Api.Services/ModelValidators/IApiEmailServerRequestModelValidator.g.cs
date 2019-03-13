using ArtLaShipNS.Api.Contracts;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiEmailServerRequestModelValidator
	{
		Task<ValidationResult> ValidateCreateAsync(ApiEmailServerRequestModel model);

		Task<ValidationResult> ValidateUpdateAsync(int id, ApiEmailServerRequestModel model);

		Task<ValidationResult> ValidateDeleteAsync(int id);
	}
}

/*<Codenesium>
    <Hash>35bff586e7e11b59f56c4f4c6691177e</Hash>
</Codenesium>*/