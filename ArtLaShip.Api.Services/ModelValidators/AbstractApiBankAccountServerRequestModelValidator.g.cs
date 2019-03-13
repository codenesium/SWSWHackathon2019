using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Codenesium.DataConversionExtensions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractApiBankAccountServerRequestModelValidator : AbstractValidator<ApiBankAccountServerRequestModel>
	{
		private int existingRecordId;

		protected IBankAccountRepository BankAccountRepository { get; private set; }

		public AbstractApiBankAccountServerRequestModelValidator(IBankAccountRepository bankAccountRepository)
		{
			this.BankAccountRepository = bankAccountRepository;
		}

		public async Task<ValidationResult> ValidateAsync(ApiBankAccountServerRequestModel model, int id)
		{
			this.existingRecordId = id;
			return await this.ValidateAsync(model);
		}

		public virtual void AccountNumberRules()
		{
			this.RuleFor(x => x.AccountNumber).NotNull().WithErrorCode(ValidationErrorCodes.ViolatesShouldNotBeNullRule);
			this.RuleFor(x => x.AccountNumber).Length(0, 24).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void ArtistIdRules()
		{
			this.RuleFor(x => x.ArtistId).MustAsync(this.BeValidArtistByArtistId).When(x => !x?.ArtistId.IsEmptyOrZeroOrNull() ?? false).WithMessage("Invalid reference").WithErrorCode(ValidationErrorCodes.ViolatesForeignKeyConstraintRule);
		}

		public virtual void RoutingNumberRules()
		{
			this.RuleFor(x => x.RoutingNumber).NotNull().WithErrorCode(ValidationErrorCodes.ViolatesShouldNotBeNullRule);
			this.RuleFor(x => x.RoutingNumber).Length(0, 24).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		protected async Task<bool> BeValidArtistByArtistId(int id,  CancellationToken cancellationToken)
		{
			var record = await this.BankAccountRepository.ArtistByArtistId(id);

			return record != null;
		}
	}
}

/*<Codenesium>
    <Hash>3847c4d65045d423f08c899708b72807</Hash>
</Codenesium>*/