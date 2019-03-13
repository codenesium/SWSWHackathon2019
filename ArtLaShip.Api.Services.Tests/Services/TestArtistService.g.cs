using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Artist")]
	[Trait("Area", "Services")]
	public partial class ArtistServiceTests
	{
		[Fact]
		public async void All()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var records = new List<Artist>();
			records.Add(new Artist());
			mock.RepositoryMock.Setup(x => x.All(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(records));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiArtistServerResponseModel> response = await service.All();

			response.Should().HaveCount(1);
			mock.RepositoryMock.Verify(x => x.All(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()));
		}

		[Fact]
		public async void Get()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var record = new Artist();
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(record));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			ApiArtistServerResponseModel response = await service.Get(default(int));

			response.Should().NotBeNull();
			mock.RepositoryMock.Verify(x => x.Get(It.IsAny<int>()));
		}

		[Fact]
		public async void Get_null_record()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<Artist>(null));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			ApiArtistServerResponseModel response = await service.Get(default(int));

			response.Should().BeNull();
			mock.RepositoryMock.Verify(x => x.Get(It.IsAny<int>()));
		}

		[Fact]
		public async void Create_NoErrors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Create(It.IsAny<Artist>())).Returns(Task.FromResult(new Artist()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			CreateResponse<ApiArtistServerResponseModel> response = await service.Create(model);

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Verify(x => x.ValidateCreateAsync(It.IsAny<ApiArtistServerRequestModel>()));
			mock.RepositoryMock.Verify(x => x.Create(It.IsAny<Artist>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistCreatedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Create_Errors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			var validatorMock = new Mock<IApiArtistServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiArtistServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                validatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			CreateResponse<ApiArtistServerResponseModel> response = await service.Create(model);

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateCreateAsync(It.IsAny<ApiArtistServerRequestModel>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void Update_NoErrors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Create(It.IsAny<Artist>())).Returns(Task.FromResult(new Artist()));
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			UpdateResponse<ApiArtistServerResponseModel> response = await service.Update(default(int), model);

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Verify(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiArtistServerRequestModel>()));
			mock.RepositoryMock.Verify(x => x.Update(It.IsAny<Artist>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistUpdatedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Update_Errors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			var validatorMock = new Mock<IApiArtistServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiArtistServerRequestModel>())).Returns(Task.FromResult(new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new Artist()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                validatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			UpdateResponse<ApiArtistServerResponseModel> response = await service.Update(default(int), model);

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiArtistServerRequestModel>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistUpdatedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void Delete_NoErrors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			ActionResponse response = await service.Delete(default(int));

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.RepositoryMock.Verify(x => x.Delete(It.IsAny<int>()));
			mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Verify(x => x.ValidateDeleteAsync(It.IsAny<int>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistDeletedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Delete_Errors()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var model = new ApiArtistServerRequestModel();
			var validatorMock = new Mock<IApiArtistServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                validatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			ActionResponse response = await service.Delete(default(int));

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateDeleteAsync(It.IsAny<int>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<ArtistDeletedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void BankAccountsByArtistId_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var records = new List<BankAccount>();
			records.Add(new BankAccount());
			mock.RepositoryMock.Setup(x => x.BankAccountsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(records));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiBankAccountServerResponseModel> response = await service.BankAccountsByArtistId(default(int));

			response.Should().NotBeEmpty();
			mock.RepositoryMock.Verify(x => x.BankAccountsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void BankAccountsByArtistId_Not_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			mock.RepositoryMock.Setup(x => x.BankAccountsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult<List<BankAccount>>(new List<BankAccount>()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiBankAccountServerResponseModel> response = await service.BankAccountsByArtistId(default(int));

			response.Should().BeEmpty();
			mock.RepositoryMock.Verify(x => x.BankAccountsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void EmailsByArtistId_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var records = new List<Email>();
			records.Add(new Email());
			mock.RepositoryMock.Setup(x => x.EmailsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(records));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiEmailServerResponseModel> response = await service.EmailsByArtistId(default(int));

			response.Should().NotBeEmpty();
			mock.RepositoryMock.Verify(x => x.EmailsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void EmailsByArtistId_Not_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			mock.RepositoryMock.Setup(x => x.EmailsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult<List<Email>>(new List<Email>()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiEmailServerResponseModel> response = await service.EmailsByArtistId(default(int));

			response.Should().BeEmpty();
			mock.RepositoryMock.Verify(x => x.EmailsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void TransactionsByArtistId_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			var records = new List<Transaction>();
			records.Add(new Transaction());
			mock.RepositoryMock.Setup(x => x.TransactionsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(records));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiTransactionServerResponseModel> response = await service.TransactionsByArtistId(default(int));

			response.Should().NotBeEmpty();
			mock.RepositoryMock.Verify(x => x.TransactionsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void TransactionsByArtistId_Not_Exists()
		{
			var mock = new ServiceMockFacade<IArtistRepository>();
			mock.RepositoryMock.Setup(x => x.TransactionsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult<List<Transaction>>(new List<Transaction>()));
			var service = new ArtistService(mock.LoggerMock.Object,
			                                mock.MediatorMock.Object,
			                                mock.RepositoryMock.Object,
			                                mock.ModelValidatorMockFactory.ArtistModelValidatorMock.Object,
			                                mock.DALMapperMockFactory.DALArtistMapperMock,
			                                mock.DALMapperMockFactory.DALBankAccountMapperMock,
			                                mock.DALMapperMockFactory.DALEmailMapperMock,
			                                mock.DALMapperMockFactory.DALTransactionMapperMock);

			List<ApiTransactionServerResponseModel> response = await service.TransactionsByArtistId(default(int));

			response.Should().BeEmpty();
			mock.RepositoryMock.Verify(x => x.TransactionsByArtistId(default(int), It.IsAny<int>(), It.IsAny<int>()));
		}
	}
}

/*<Codenesium>
    <Hash>f45d7eb1c1cc9902985008ee52841dd5</Hash>
</Codenesium>*/