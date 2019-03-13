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
	public abstract class AbstractApiEmailServerRequestModelValidator : AbstractValidator<ApiEmailServerRequestModel>
	{
		private int existingRecordId;

		protected IEmailRepository EmailRepository { get; private set; }

		public AbstractApiEmailServerRequestModelValidator(IEmailRepository emailRepository)
		{
			this.EmailRepository = emailRepository;
		}

		public async Task<ValidationResult> ValidateAsync(ApiEmailServerRequestModel model, int id)
		{
			this.existingRecordId = id;
			return await this.ValidateAsync(model);
		}

		public virtual void ArtistIdRules()
		{
			this.RuleFor(x => x.ArtistId).MustAsync(this.BeValidArtistByArtistId).When(x => !x?.ArtistId.IsEmptyOrZeroOrNull() ?? false).WithMessage("Invalid reference").WithErrorCode(ValidationErrorCodes.ViolatesForeignKeyConstraintRule);
		}

		public virtual void DateCreatedRules()
		{
		}

		public virtual void Email1Rules()
		{
			this.RuleFor(x => x.Email1).NotNull().WithErrorCode(ValidationErrorCodes.ViolatesShouldNotBeNullRule);
			this.RuleFor(x => x.Email1).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		protected async Task<bool> BeValidArtistByArtistId(int id,  CancellationToken cancellationToken)
		{
			var record = await this.EmailRepository.ArtistByArtistId(id);

			return record != null;
		}
	}
}

/*<Codenesium>
    <Hash>0e10e86aac27f04daeb93b22efa82e9d</Hash>
</Codenesium>*/