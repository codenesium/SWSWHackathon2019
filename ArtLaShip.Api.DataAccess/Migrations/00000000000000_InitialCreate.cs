using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using ArtLaShipNS.Api.DataAccess;

namespace ArtLaShipNS.Api.DataAccess.Migrations
{
	[DbContext(typeof(ApplicationDbContext))]
    [Migration("00000000000000_InitialCreate")]
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"IF NOT EXISTS(SELECT *
FROM sys.schemas
WHERE name = N'dbo')
EXEC('CREATE SCHEMA [dbo] AUTHORIZATION [dbo]');
GO

--IF (OBJECT_ID('dbo.fk_bankaccount_artistid_artist_id', 'F') IS NOT NULL)
--BEGIN
--ALTER TABLE [dbo].[BankAccount] DROP CONSTRAINT [fk_bankaccount_artistid_artist_id]
--END
--GO
--IF (OBJECT_ID('dbo.fk_email_artistid_artist_id', 'F') IS NOT NULL)
--BEGIN
--ALTER TABLE [dbo].[Email] DROP CONSTRAINT [fk_email_artistid_artist_id]
--END
--GO
--IF (OBJECT_ID('dbo.fk_transaction_artistid_artist_id', 'F') IS NOT NULL)
--BEGIN
--ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [fk_transaction_artistid_artist_id]
--END
--GO

--IF OBJECT_ID('dbo.Artist', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [dbo].[Artist]
--END
--GO
--IF OBJECT_ID('dbo.BankAccount', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [dbo].[BankAccount]
--END
--GO
--IF OBJECT_ID('dbo.Transaction', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [dbo].[Transaction]
--END
--GO
--IF OBJECT_ID('dbo.Email', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [dbo].[Email]
--END
--GO

CREATE TABLE [dbo].[Artist](
[id] [int]   IDENTITY(1,1)  NOT NULL,
[externalId] [uniqueidentifier]     NOT NULL,
[name] [varchar]  (128)   NOT NULL,
[bio] [varchar]  (8000)   NOT NULL,
[facebook] [varchar]  (128)   NOT NULL,
[twitter] [varchar]  (128)   NOT NULL,
[website] [varchar]  (128)   NOT NULL,
[soundCloud] [varchar]  (128)   NOT NULL,
[aspNetUserId] [nvarchar]  (450)   NOT NULL,
) ON[PRIMARY]
GO

CREATE TABLE [dbo].[BankAccount](
[id] [int]   IDENTITY(1,1)  NOT NULL,
[artistId] [int]     NOT NULL,
[routingNumber] [varchar]  (24)   NOT NULL,
[accountNumber] [varchar]  (24)   NOT NULL,
) ON[PRIMARY]
GO

CREATE TABLE [dbo].[Transaction](
[id] [int]   IDENTITY(1,1)  NOT NULL,
[artistId] [int]     NOT NULL,
[amount] [money]     NOT NULL,
[dateCreated] [datetime]     NOT NULL,
[stripeTransacitonId] [varchar]  (128)   NOT NULL,
) ON[PRIMARY]
GO

CREATE TABLE [dbo].[Email](
[id] [int]   IDENTITY(1,1)  NOT NULL,
[artistId] [int]     NOT NULL,
[email] [varchar]  (128)   NOT NULL,
[dateCreated] [datetime]     NOT NULL,
) ON[PRIMARY]
GO

ALTER TABLE[dbo].[Artist]
ADD CONSTRAINT[PK_Artist] PRIMARY KEY 
(
[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE[dbo].[BankAccount]
ADD CONSTRAINT[PK_BankAccount] PRIMARY KEY 
(
[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE  NONCLUSTERED INDEX[IX_BankAccount_artistId] ON[dbo].[BankAccount]
(
[artistId] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE[dbo].[Transaction]
ADD CONSTRAINT[PK_Transaction] PRIMARY KEY 
(
[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE  NONCLUSTERED INDEX[IX_Transaction_artistId] ON[dbo].[Transaction]
(
[artistId] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE[dbo].[Email]
ADD CONSTRAINT[PK_Email] PRIMARY KEY 
(
[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE  NONCLUSTERED INDEX[IX_Email_artistId] ON[dbo].[Email]
(
[artistId] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


ALTER TABLE[dbo].[BankAccount]  WITH CHECK ADD  CONSTRAINT[fk_bankaccount_artistid_artist_id] FOREIGN KEY([artistId])
REFERENCES[dbo].[Artist]([id]) on delete no action on update no action
GO
ALTER TABLE[dbo].[BankAccount] CHECK CONSTRAINT[fk_bankaccount_artistid_artist_id]
GO
ALTER TABLE[dbo].[Email]  WITH CHECK ADD  CONSTRAINT[fk_email_artistid_artist_id] FOREIGN KEY([artistId])
REFERENCES[dbo].[Artist]([id]) on delete no action on update no action
GO
ALTER TABLE[dbo].[Email] CHECK CONSTRAINT[fk_email_artistid_artist_id]
GO
ALTER TABLE[dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT[fk_transaction_artistid_artist_id] FOREIGN KEY([artistId])
REFERENCES[dbo].[Artist]([id]) on delete no action on update no action
GO
ALTER TABLE[dbo].[Transaction] CHECK CONSTRAINT[fk_transaction_artistid_artist_id]
GO

");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
		}
	}
}