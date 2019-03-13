using Codenesium.DataConversionExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public abstract class AbstractIntegrationTestMigration
	{
		protected ApplicationDbContext Context { get; private set; }

		public AbstractIntegrationTestMigration(ApplicationDbContext context)
		{
			this.Context = context;
		}

		public virtual async Task Migrate()
		{
			var artistItem1 = new Artist();
			artistItem1.SetProperties(1, "A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A");
			this.Context.Artists.Add(artistItem1);

			var bankAccountItem1 = new BankAccount();
			bankAccountItem1.SetProperties(1, "A", 1, "A");
			this.Context.BankAccounts.Add(bankAccountItem1);

			var transactionItem1 = new Transaction();
			transactionItem1.SetProperties(1, 1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			this.Context.Transactions.Add(transactionItem1);

			var emailItem1 = new Email();
			emailItem1.SetProperties(1, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			this.Context.Emails.Add(emailItem1);

			await this.Context.SaveChangesAsync();
		}
	}
}

/*<Codenesium>
    <Hash>7f3d8b5951fb9e7cdb023485a9a2cef8</Hash>
</Codenesium>*/