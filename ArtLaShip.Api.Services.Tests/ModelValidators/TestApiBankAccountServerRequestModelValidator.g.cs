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
	[Trait("Table", "BankAccount")]
	[Trait("Area", "ModelValidators")]
	public partial class ApiBankAccountServerRequestModelValidatorTest
	{
		public ApiBankAccountServerRequestModelValidatorTest()
		{
		}

		[Fact]
		public async void AccountNumber_Create_null()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, null as string);
		}

		[Fact]
		public async void AccountNumber_Update_null()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, null as string);
		}

		[Fact]
		public async void AccountNumber_Create_length()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, new string('A', 25));
		}

		[Fact]
		public async void AccountNumber_Update_length()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, new string('A', 25));
		}

		[Fact]
		public async void ArtistId_Create_Valid_Reference()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Create_Invalid_Reference()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);

			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Valid_Reference()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Invalid_Reference()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);

			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void RoutingNumber_Create_null()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.RoutingNumber, null as string);
		}

		[Fact]
		public async void RoutingNumber_Update_null()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.RoutingNumber, null as string);
		}

		[Fact]
		public async void RoutingNumber_Create_length()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateCreateAsync(new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.RoutingNumber, new string('A', 25));
		}

		[Fact]
		public async void RoutingNumber_Update_length()
		{
			Mock<IBankAccountRepository> bankAccountRepository = new Mock<IBankAccountRepository>();
			bankAccountRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));

			var validator = new ApiBankAccountServerRequestModelValidator(bankAccountRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiBankAccountServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.RoutingNumber, new string('A', 25));
		}
	}
}

/*<Codenesium>
    <Hash>0123022e23cb49d7b98bddd0330038d7</Hash>
</Codenesium>*/