using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services.Tests
{
	public class ModelValidatorMockFactory
	{
		public Mock<IApiArtistServerRequestModelValidator> ArtistModelValidatorMock { get; set; } = new Mock<IApiArtistServerRequestModelValidator>();

		public Mock<IApiBankAccountServerRequestModelValidator> BankAccountModelValidatorMock { get; set; } = new Mock<IApiBankAccountServerRequestModelValidator>();

		public Mock<IApiTransactionServerRequestModelValidator> TransactionModelValidatorMock { get; set; } = new Mock<IApiTransactionServerRequestModelValidator>();

		public Mock<IApiEmailServerRequestModelValidator> EmailModelValidatorMock { get; set; } = new Mock<IApiEmailServerRequestModelValidator>();

		public ModelValidatorMockFactory()
		{
			this.ArtistModelValidatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiArtistServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.ArtistModelValidatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiArtistServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.ArtistModelValidatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));

			this.BankAccountModelValidatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiBankAccountServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.BankAccountModelValidatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiBankAccountServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.BankAccountModelValidatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));

			this.TransactionModelValidatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiTransactionServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.TransactionModelValidatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiTransactionServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.TransactionModelValidatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));

			this.EmailModelValidatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiEmailServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.EmailModelValidatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiEmailServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
			this.EmailModelValidatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));
		}
	}
}

/*<Codenesium>
    <Hash>0b521e1812daa719c9cf8decec30815f</Hash>
</Codenesium>*/