-- Bảng Card
CREATE TABLE [dbo].[Card] (
    [Id] INT PRIMARY KEY,
    [CustomerID] INT,
    [CardTypeID] INT,
    [CardNumber] NVARCHAR(max),
    [CreatedAt] DATETIME,
    [CreateDate] DATETIME,
    [ExpiryDate] DATETIME,
    [Status] NVARCHAR(max)
);

-- Bảng Wallet
CREATE TABLE [dbo].[Wallet] (
    [Id] INT PRIMARY KEY,
    [WalletTypeId] INT,
    [Balance] DECIMAL(18, 2),
    [CreatedAt] DATETIME,
    [StoreId] INT,
    [CardId] INT,
    FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card]([Id])
);

-- Bảng Transaction
CREATE TABLE [dbo].[Transaction] (
    [Id] INT PRIMARY KEY,
    [WalletId] INT,
    [DepositId] INT,
    [OrderId] INT,
    [StoreWithDrawalId] INT,
    [Amount] DECIMAL(18, 2),
    [Status] NVARCHAR(max),
    [Description] NVARCHAR(max),
    FOREIGN KEY ([WalletId]) REFERENCES [dbo].[Wallet]([Id]),
    FOREIGN KEY ([StoreWithDrawalId]) REFERENCES [dbo].[StoreWithDrawal]([store_with_drawal_id])
);

-- Bảng WalletType
CREATE TABLE [dbo].[WalletType] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(max),
    [Description] NVARCHAR(max),
    [Mode] NVARCHAR(max)
);
