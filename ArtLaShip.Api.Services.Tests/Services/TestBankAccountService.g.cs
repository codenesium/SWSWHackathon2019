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
	[Trait("Table", "BankAccount")]
	[Trait("Area", "Services")]
	public partial class BankAccountServiceTests
	{
		[Fact]
		public async void All()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var records = new List<BankAccount>();
			records.Add(new BankAccount());
			mock.RepositoryMock.Setup(x => x.All(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(records));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			List<ApiBankAccountServerResponseModel> response = await service.All();

			response.Should().HaveCount(1);
			mock.RepositoryMock.Verify(x => x.All(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()));
		}

		[Fact]
		public async void Get()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var record = new BankAccount();
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(record));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			ApiBankAccountServerResponseModel response = await service.Get(default(int));

			response.Should().NotBeNull();
			mock.RepositoryMock.Verify(x => x.Get(It.IsAny<int>()));
		}

		[Fact]
		public async void Get_null_record()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<BankAccount>(null));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			ApiBankAccountServerResponseModel response = await service.Get(default(int));

			response.Should().BeNull();
			mock.RepositoryMock.Verify(x => x.Get(It.IsAny<int>()));
		}

		[Fact]
		public async void Create_NoErrors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Create(It.IsAny<BankAccount>())).Returns(Task.FromResult(new BankAccount()));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			CreateResponse<ApiBankAccountServerResponseModel> response = await service.Create(model);

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Verify(x => x.ValidateCreateAsync(It.IsAny<ApiBankAccountServerRequestModel>()));
			mock.RepositoryMock.Verify(x => x.Create(It.IsAny<BankAccount>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountCreatedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Create_Errors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			var validatorMock = new Mock<IApiBankAccountServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateCreateAsync(It.IsAny<ApiBankAccountServerRequestModel>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     validatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			CreateResponse<ApiBankAccountServerResponseModel> response = await service.Create(model);

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateCreateAsync(It.IsAny<ApiBankAccountServerRequestModel>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void Update_NoErrors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Create(It.IsAny<BankAccount>())).Returns(Task.FromResult(new BankAccount()));
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			UpdateResponse<ApiBankAccountServerResponseModel> response = await service.Update(default(int), model);

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Verify(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiBankAccountServerRequestModel>()));
			mock.RepositoryMock.Verify(x => x.Update(It.IsAny<BankAccount>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountUpdatedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Update_Errors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			var validatorMock = new Mock<IApiBankAccountServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiBankAccountServerRequestModel>())).Returns(Task.FromResult(new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			mock.RepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(new BankAccount()));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     validatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			UpdateResponse<ApiBankAccountServerResponseModel> response = await service.Update(default(int), model);

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateUpdateAsync(It.IsAny<int>(), It.IsAny<ApiBankAccountServerRequestModel>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountUpdatedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void Delete_NoErrors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			mock.RepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			ActionResponse response = await service.Delete(default(int));

			response.Should().NotBeNull();
			response.Success.Should().BeTrue();
			mock.RepositoryMock.Verify(x => x.Delete(It.IsAny<int>()));
			mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Verify(x => x.ValidateDeleteAsync(It.IsAny<int>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountDeletedNotification>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async void Delete_Errors()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var model = new ApiBankAccountServerRequestModel();
			var validatorMock = new Mock<IApiBankAccountServerRequestModelValidator>();
			validatorMock.Setup(x => x.ValidateDeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>() { new ValidationFailure("text", "test") })));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     validatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			ActionResponse response = await service.Delete(default(int));

			response.Should().NotBeNull();
			response.Success.Should().BeFalse();
			validatorMock.Verify(x => x.ValidateDeleteAsync(It.IsAny<int>()));
			mock.MediatorMock.Verify(x => x.Publish(It.IsAny<BankAccountDeletedNotification>(), It.IsAny<CancellationToken>()), Times.Never());
		}

		[Fact]
		public async void ByArtistId_Exists()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			var records = new List<BankAccount>();
			records.Add(new BankAccount());
			mock.RepositoryMock.Setup(x => x.ByArtistId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(records));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			List<ApiBankAccountServerResponseModel> response = await service.ByArtistId(default(int));

			response.Should().NotBeEmpty();
			mock.RepositoryMock.Verify(x => x.ByArtistId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async void ByArtistId_Not_Exists()
		{
			var mock = new ServiceMockFacade<IBankAccountRepository>();
			mock.RepositoryMock.Setup(x => x.ByArtistId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult<List<BankAccount>>(new List<BankAccount>()));
			var service = new BankAccountService(mock.LoggerMock.Object,
			                                     mock.MediatorMock.Object,
			                                     mock.RepositoryMock.Object,
			                                     mock.ModelValidatorMockFactory.BankAccountModelValidatorMock.Object,
			                                     mock.DALMapperMockFactory.DALBankAccountMapperMock);

			List<ApiBankAccountServerResponseModel> response = await service.ByArtistId(default(int));

			response.Should().BeEmpty();
			mock.RepositoryMock.Verify(x => x.ByArtistId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
		}
	}
}

/*<Codenesium>
    <Hash>7ea93a436fc09fdfbc4964fd7fc0d77b</Hash>
</Codenesium>*/