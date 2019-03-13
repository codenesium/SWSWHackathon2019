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
	[Trait("Table", "Artist")]
	[Trait("Area", "ModelValidators")]
	public partial class ApiArtistServerRequestModelValidatorTest
	{
		public ApiArtistServerRequestModelValidatorTest()
		{
		}

		[Fact]
		public async void AspNetUserId_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AspNetUserId, new string('A', 451));
		}

		[Fact]
		public async void AspNetUserId_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.AspNetUserId, new string('A', 451));
		}

		[Fact]
		public async void Bio_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Bio, new string('A', 8001));
		}

		[Fact]
		public async void Bio_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Bio, new string('A', 8001));
		}

		[Fact]
		public async void Facebook_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Facebook, new string('A', 129));
		}

		[Fact]
		public async void Facebook_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Facebook, new string('A', 129));
		}

		[Fact]
		public async void Name_Create_null()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
		}

		[Fact]
		public async void Name_Update_null()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
		}

		[Fact]
		public async void Name_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Name, new string('A', 129));
		}

		[Fact]
		public async void Name_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Name, new string('A', 129));
		}

		[Fact]
		public async void SoundCloud_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.SoundCloud, new string('A', 129));
		}

		[Fact]
		public async void SoundCloud_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.SoundCloud, new string('A', 129));
		}

		[Fact]
		public async void Twitter_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Twitter, new string('A', 129));
		}

		[Fact]
		public async void Twitter_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Twitter, new string('A', 129));
		}

		[Fact]
		public async void Venmo_Create_null()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Venmo, null as string);
		}

		[Fact]
		public async void Venmo_Update_null()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Venmo, null as string);
		}

		[Fact]
		public async void Venmo_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Venmo, new string('A', 129));
		}

		[Fact]
		public async void Venmo_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Venmo, new string('A', 129));
		}

		[Fact]
		public async void Website_Create_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateCreateAsync(new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Website, new string('A', 129));
		}

		[Fact]
		public async void Website_Update_length()
		{
			Mock<IArtistRepository> artistRepository = new Mock<IArtistRepository>();
			artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));

			var validator = new ApiArtistServerRequestModelValidator(artistRepository.Object);
			await validator.ValidateUpdateAsync(default(int), new ApiArtistServerRequestModel());

			validator.ShouldHaveValidationErrorFor(x => x.Website, new string('A', 129));
		}
	}
}

/*<Codenesium>
    <Hash>0739887d0ea62aed9d0dc3e75c794d45</Hash>
</Codenesium>*/