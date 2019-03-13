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
	public abstract class AbstractApiArtistServerRequestModelValidator : AbstractValidator<ApiArtistServerRequestModel>
	{
		private int existingRecordId;

		protected IArtistRepository ArtistRepository { get; private set; }

		public AbstractApiArtistServerRequestModelValidator(IArtistRepository artistRepository)
		{
			this.ArtistRepository = artistRepository;
		}

		public async Task<ValidationResult> ValidateAsync(ApiArtistServerRequestModel model, int id)
		{
			this.existingRecordId = id;
			return await this.ValidateAsync(model);
		}

		public virtual void AspNetUserIdRules()
		{
			this.RuleFor(x => x.AspNetUserId).Length(0, 450).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void BioRules()
		{
			this.RuleFor(x => x.Bio).Length(0, 8000).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void ExternalIdRules()
		{
		}

		public virtual void FacebookRules()
		{
			this.RuleFor(x => x.Facebook).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void NameRules()
		{
			this.RuleFor(x => x.Name).NotNull().WithErrorCode(ValidationErrorCodes.ViolatesShouldNotBeNullRule);
			this.RuleFor(x => x.Name).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void SoundCloudRules()
		{
			this.RuleFor(x => x.SoundCloud).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void TwitterRules()
		{
			this.RuleFor(x => x.Twitter).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}

		public virtual void WebsiteRules()
		{
			this.RuleFor(x => x.Website).Length(0, 128).WithErrorCode(ValidationErrorCodes.ViolatesLengthRule);
		}
	}
}

/*<Codenesium>
    <Hash>0f00b262e7518751f55b45e73c411689</Hash>
</Codenesium>*/