using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Transaction")]
	[Trait("Area", "ModelValidators")]
	public partial class ApiTransactionServerRequestModelValidatorTest
	{
		public ApiTransactionServerRequestModelValidatorTest()
		{
		}

		[Fact]
		public async void ArtistId_Create_Valid_Reference()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateCreateAsync(new ApiTransactionServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Create_Invalid_Reference()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);

			await validator.ValidateCreateAsync(new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Valid_Reference()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiTransactionServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Invalid_Reference()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);

			await validator.ValidateUpdateAsync(default(int), new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void StripeTransactionId_Create_null()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Transaction()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateCreateAsync(new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.StripeTransactionId, null as string);
		}

		[Fact]
		public async void StripeTransactionId_Update_null()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Transaction()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.StripeTransactionId, null as string);
		}

		[Fact]
		public async void StripeTransactionId_Create_length()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Transaction()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateCreateAsync(new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.StripeTransactionId, new string('A', 129));
		}

		[Fact]
		public async void StripeTransactionId_Update_length()
		{
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			transactionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Transaction()));

			var validator = new ApiTransactionServerRequestModelValidator(transactionRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiTransactionServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.StripeTransactionId, new string('A', 129));
		}
	}
}

/*<Codenesium>
    <Hash>a5b1f34d5c705b94e90d0d477705aa5d</Hash>
</Codenesium>*/