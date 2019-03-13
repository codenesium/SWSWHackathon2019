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
	[Trait("Table", "Email")]
	[Trait("Area", "ModelValidators")]
	public partial class ApiEmailServerRequestModelValidatorTest
	{
		public ApiEmailServerRequestModelValidatorTest()
		{
		}

		[Fact]
		public async void ArtistId_Create_Valid_Reference()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateCreateAsync(new ApiEmailServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Create_Invalid_Reference()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);

			await validator.ValidateCreateAsync(new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Valid_Reference()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(new Artist()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiEmailServerRequestModel());

			validator.ShouldNotHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void ArtistId_Update_Invalid_Reference()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.ArtistByArtistId(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);

			await validator.ValidateUpdateAsync(default(int), new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.ArtistId, 1);
		}

		[Fact]
		public async void Email1_Create_null()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Email()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateCreateAsync(new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Email1, null as string);
		}

		[Fact]
		public async void Email1_Update_null()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Email()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Email1, null as string);
		}

		[Fact]
		public async void Email1_Create_length()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Email()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateCreateAsync(new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Email1, new string('A', 129));
		}

		[Fact]
		public async void Email1_Update_length()
		{
			Mock<IEmailRepository> emailRepository = new Mock<IEmailRepository>();
			emailRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Email()));

			var validator = new ApiEmailServerRequestModelValidator(emailRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiEmailServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Email1, new string('A', 129));
		}
	}
}

/*<Codenesium>
    <Hash>f213f65b1710e023490aad948954a44a</Hash>
</Codenesium>*/