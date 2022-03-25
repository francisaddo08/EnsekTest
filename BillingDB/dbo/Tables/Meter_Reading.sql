CREATE TABLE [dbo].[Meter_Reading]
(
[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountId] NVARCHAR(50) NOT NULL, 
    [MeterReadingDateTime] DATETIME NOT NULL, 
    [MeterReadValue] NCHAR(10) NOT NULL,
     CONSTRAINT FK_Test_Accounts FOREIGN KEY (AccountId)
    REFERENCES Test_Accounts(AccountId)
)
