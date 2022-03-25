CREATE TABLE [dbo].[Test_Accounts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountId] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
     CONSTRAINT AK_AccountId UNIQUE(AccountId),
    CONSTRAINT FirstName CHECK (FirstName NOT LIKE '%[^A-Z]%'),
    CONSTRAINT LastName CHECK (LastName NOT LIKE '%[^A-Z]%')
)
