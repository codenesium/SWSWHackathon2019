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
	public abstract class AbstractApiTransactionServerRequestModelValidator : AbstractValidator<ApiTransactionServerRequestModel>
	{
		private int existingRecordId;

		protected ITransactionRepository TransactionRepository { get; private set; }

		public AbstractApiTransactionServerRequestModelValidator(ITransactionRepository transactionRepository)
		{
			this.TransactionRepository = transactionRepository;
		}

		public async Task<ValidationResult> ValidateAsync(ApiTransactionServerRequestModel model, int id)
		{
			this.existingRecordId = id;
			return await this.ValidateAsync(model);
		}

		public virtual void AmountRules()
		{
		}

		public virtual void ArtistIdRules()
		{
			this.RuleFor(x => x.ArtistId).MustAsync(this.BeValidArtistByArtistId).When(x => !x?.ArtistId.IsEmptyOrZeroOrNull() ?? false).WithMessage("Invalid reference").WithErrorCode(ValidationErrorCodes.ViolatesForeignKeyConstraintRule);
		}

		public virtual void DateCreatedRules()
		{
		}

		public virtual void StripeTransactionIdRules()
		{
			this.RuleFor(x => x.StripeTransactionId).NotNull().WithErrorCode(ValidationErrorCodes.ViolatesShouldNotBeNullRule);
			this.RuleFor(x => x.StripeTransactionId).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		protected async Task<bool> BeValidArtistByArtistId(int id,  CancellationToken cancellationToken)
		{
			var record = await this.TransactionRepository.ArtistByArtistId(id);

			return record != null;
		}
	}
}

/*<Codenesium>
    <Hash>371e9f2371522ef12577a9e0d330859c</Hash>
</Codenesium>*/