USE [master];
GO

BEGIN TRY
    BEGIN TRANSACTION;

    PRINT 'Create database';

    -- Kiểm tra nếu database chưa tồn tại
    IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'sPay_db')
    BEGIN
        -- Tạo mới database
        CREATE DATABASE sPay_db;
        PRINT 'Database sPay_db created.';
    END
    ELSE
        PRINT 'Database sPay_db already exists.';

    USE [sPay_db];

    -- Bảng ADMIN
    PRINT '1. Tạo Bảng ADMIN';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ADMIN')
    BEGIN
        CREATE TABLE [dbo].[ADMIN](
            [ADMIN_KEY] [int] IDENTITY(1,1) NOT NULL,
            [USER_ID] [int] NULL,
            [ADMIN_NAME] [varchar](50) NULL,
            PRIMARY KEY CLUSTERED ([ADMIN_KEY] ASC)
        );
        PRINT 'Table ADMIN created.';
    END
    ELSE
        PRINT 'Table ADMIN already exists.';

    -- Bảng CARD
    PRINT '2. Tạo Bảng CARD';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'CARD')
    BEGIN
        CREATE TABLE [dbo].[CARD](
            [CARD_KEY] [int] IDENTITY(1,1) NOT NULL,
            [CUSTOMER_ID] [int] NULL,
            [CARD_TYPE_ID] [int] NULL,
            [CARD_NUMBER] [nvarchar](max) NULL,
            [CREATED_AT] [datetime] NULL,
            [CREATE_DATE] [datetime] NULL,
            [EXPIRY_DATE] [datetime] NULL,
            [STATUS] [nvarchar](max) NULL,
            PRIMARY KEY CLUSTERED ([CARD_KEY] ASC)
        );
        PRINT 'Table CARD created.';
    END
    ELSE
        PRINT 'Table CARD already exists.';

    -- Bảng CARD_STORE_CATEGORY
    PRINT '3. Tạo Bảng CARD_STORE_CATEGORY';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'CARD_STORE_CATEGORY')
    BEGIN
        CREATE TABLE [dbo].[CARD_STORE_CATEGORY](
            [ID] [int] IDENTITY(1,1) NOT NULL,
            [CARD_TYPE_ID] [int] NULL,
            [STORE_CATEGORY_ID] [int] NULL,
            PRIMARY KEY CLUSTERED ([ID] ASC)
        );
        PRINT 'Table CARD_STORE_CATEGORY created.';
    END
    ELSE
        PRINT 'Table CARD_STORE_CATEGORY already exists.';

    -- Bảng CARD_TYPE
    PRINT '4. Tạo Bảng CARD_TYPE';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'CARD_TYPE')
    BEGIN
        CREATE TABLE [dbo].[CARD_TYPE](
            [CARD_TYPE_ID] [int] IDENTITY(1,1) NOT NULL,
            [NAME] [varchar](max) NULL,
            [DESCRIPTION] [varchar](max) NULL,
            PRIMARY KEY CLUSTERED ([CARD_TYPE_ID] ASC)
        );
        PRINT 'Table CARD_TYPE created.';
    END
    ELSE
        PRINT 'Table CARD_TYPE already exists.';

    -- Bảng CUSTOMER
    PRINT '5. Tạo Bảng CUSTOMER';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'CUSTOMER')
    BEGIN
        CREATE TABLE [dbo].[CUSTOMER](
            [CUSTOMER_ID] [int] NOT NULL,
            [USER_ID] [int] NULL,
            [FIRST_NAME] [varchar](50) NULL,
            [LAST_NAME] [varchar](50) NULL,
            [EMAIL] [varchar](100) NULL,
            [ADDRESS] [varchar](255) NULL,
            PRIMARY KEY CLUSTERED ([CUSTOMER_ID] ASC),
            UNIQUE NONCLUSTERED ([USER_ID] ASC)
        );
        PRINT 'Table CUSTOMER created.';
    END
    ELSE
        PRINT 'Table CUSTOMER already exists.';

    -- Bảng ORDER
    PRINT '6. Tạo Bảng [ORDER]';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'[ORDER]')
    BEGIN
        CREATE TABLE [dbo].[ORDER](
            [ORDER_ID] [int] IDENTITY(1,1) NOT NULL,
            [STORE_ID] [int] NULL,
            [CARD_ID] [int] NULL,
            [AMOUNT] [int] NULL,
            [DATE] [date] NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([ORDER_ID] ASC)
        );
        PRINT 'Table [ORDER] created.';
    END
    ELSE
        PRINT 'Table [ORDER] already exists.';

    -- Bảng STORE
    PRINT '7. Tạo Bảng STORE';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'STORE')
    BEGIN
        CREATE TABLE [dbo].[STORE](
            [STORE_ID] [int] IDENTITY(1,1) NOT NULL,
            [NAME] [nvarchar](max) NULL,
            [CATEGORY_ID] [int] NULL,
            [LOCATION] [nvarchar](max) NULL,
            [PHONE] [int] NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([STORE_ID] ASC)
        );
        PRINT 'Table STORE created.';
    END
    ELSE
        PRINT 'Table STORE already exists.';

    -- Bảng STORE_CATEGORY
    PRINT '8. Tạo Bảng STORE_CATEGORY';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'STORE_CATEGORY')
    BEGIN
        CREATE TABLE [dbo].[STORE_CATEGORY](
            [STORE_CATEGORY_ID] [int] IDENTITY(1,1) NOT NULL,
            [NAME] [varchar](max) NULL,
            [DESCRIPTION] [varchar](max) NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([STORE_CATEGORY_ID] ASC)
        );
        PRINT 'Table STORE_CATEGORY created.';
    END
    ELSE
        PRINT 'Table STORE_CATEGORY already exists.';

    -- Bảng STORE_OWNER
    PRINT '9. Tạo Bảng STORE_OWNER';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'STORE_OWNER')
    BEGIN
        CREATE TABLE [dbo].[STORE_OWNER](
            [STORE_OWNER_ID] [int] NOT NULL,
            [USER_ID] [int] NULL,
            [OWNER_NAME] [varchar](50) NULL,
            [STORE_ID] [int] NULL,
            PRIMARY KEY CLUSTERED ([STORE_OWNER_ID] ASC),
            UNIQUE NONCLUSTERED ([USER_ID] ASC)
        );
        PRINT 'Table STORE_OWNER created.';
    END
    ELSE
        PRINT 'Table STORE_OWNER already exists.';

    -- Bảng STORE_WITHDRAWAL
    PRINT '10. Tạo Bảng STORE_WITHDRAWAL';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'STORE_WITHDRAWAL')
    BEGIN
        CREATE TABLE [dbo].[STORE_WITHDRAWAL](
            [WITHDRAWAL_ID] [int] IDENTITY(1,1) NOT NULL,
            [STORE_ID] [int] NULL,
            [AMOUNT] [int] NULL,
            [DATE] [date] NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([WITHDRAWAL_ID] ASC)
        );
        PRINT 'Table STORE_WITHDRAWAL created.';
    END
    ELSE
        PRINT 'Table STORE_WITHDRAWAL already exists.';

    -- Bảng TOPUP_MEMBER
    PRINT '11. Tạo Bảng TOPUP_MEMBER';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TOPUP_MEMBER')
    BEGIN
        CREATE TABLE [dbo].[TOPUP_MEMBER](
            [TOPUP_ID] [int] IDENTITY(1,1) NOT NULL,
            [USER_ID] [int] NULL,
            [AMOUNT] [int] NULL,
            [DATE] [date] NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([TOPUP_ID] ASC)
        );
        PRINT 'Table TOPUP_MEMBER created.';
    END
    ELSE
        PRINT 'Table TOPUP_MEMBER already exists.';

    -- Bảng USER
    PRINT '12. Tạo Bảng USER';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'USER')
    BEGIN
        CREATE TABLE [dbo].[USER](
            [USER_ID] [int] IDENTITY(1,1) NOT NULL,
            [USERNAME] [varchar](50) NULL,
            [PASSWORD] [varchar](50) NULL,
            [ROLE] [int] NULL,
            [STATUS] [bit] NULL,
            PRIMARY KEY CLUSTERED ([USER_ID] ASC)
        );
        PRINT 'Table USER created.';
    END
    ELSE
        PRINT 'Table USER already exists.';

    -- Bảng WALLET
    PRINT '13. Tạo Bảng WALLET';
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'WALLET')
    BEGIN
        CREATE TABLE [dbo].[WALLET](
            [WALLET_ID] [int] IDENTITY(1,1) NOT NULL,
            [USER_ID] [int] NULL,
            [BALANCE] [int] NULL,
            PRIMARY KEY CLUSTERED ([WALLET_ID] ASC)
        );
        PRINT 'Table WALLET created.';
    END
    ELSE
        PRINT 'Table WALLET already exists.';

    -- Thêm khóa ngoại
    PRINT '14. Thêm khóa ngoại';

    -- Khóa ngoại cho bảng ADMIN
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ADMIN_USER')
    BEGIN
        ALTER TABLE [dbo].[ADMIN] WITH CHECK ADD FOREIGN KEY([USER_ID])
        REFERENCES [dbo].[USER] ([USER_ID]);
        PRINT 'Foreign key FK_ADMIN_USER added.';
    END
    ELSE
        PRINT 'Foreign key FK_ADMIN_USER already exists.';

    -- Khóa ngoại cho bảng CARD
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_CUSTOMER')
    BEGIN
        ALTER TABLE [dbo].[CARD] WITH CHECK ADD FOREIGN KEY([CUSTOMER_ID])
        REFERENCES [dbo].[CUSTOMER] ([CUSTOMER_ID]);
        PRINT 'Foreign key FK_CARD_CUSTOMER added.';
    END
    ELSE
        PRINT 'Foreign key FK_CARD_CUSTOMER already exists.';

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_CARD_TYPE')
    BEGIN
        ALTER TABLE [dbo].[CARD] WITH CHECK ADD FOREIGN KEY([CARD_TYPE_ID])
        REFERENCES [dbo].[CARD_TYPE] ([CARD_TYPE_ID]);
        PRINT 'Foreign key FK_CARD_CARD_TYPE added.';
    END
    ELSE
        PRINT 'Foreign key FK_CARD_CARD_TYPE already exists.';

    -- Khóa ngoại cho bảng CUSTOMER
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CUSTOMER_USER')
    BEGIN
        ALTER TABLE [dbo].[CUSTOMER] WITH CHECK ADD FOREIGN KEY([USER_ID])
        REFERENCES [dbo].[USER] ([USER_ID]);
        PRINT 'Foreign key FK_CUSTOMER_USER added.';
    END
    ELSE
        PRINT 'Foreign key FK_CUSTOMER_USER already exists.';

    PRINT '15. Thêm khóa ngoại cho bảng CARD_STORE_CATEGORY';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_STORE_CATEGORY_CARD_TYPE')
	BEGIN
		ALTER TABLE [dbo].[CARD_STORE_CATEGORY] WITH CHECK ADD FOREIGN KEY([CARD_TYPE_ID])
		REFERENCES [dbo].[CARD_TYPE] ([CARD_TYPE_ID]);
		PRINT 'Foreign key FK_CARD_STORE_CATEGORY_CARD_TYPE added.';
	END
	ELSE
		PRINT 'Foreign key FK_CARD_STORE_CATEGORY_CARD_TYPE already exists.';

	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_STORE_CATEGORY_STORE_CATEGORY')
	BEGIN
		ALTER TABLE [dbo].[CARD_STORE_CATEGORY] WITH CHECK ADD FOREIGN KEY([STORE_CATEGORY_ID])
		REFERENCES [dbo].[STORE_CATEGORY] ([STORE_CATEGORY_ID]);
		PRINT 'Foreign key FK_CARD_STORE_CATEGORY_STORE_CATEGORY added.';
	END
	ELSE
		PRINT 'Foreign key FK_CARD_STORE_CATEGORY_STORE_CATEGORY already exists.';

	-- Khóa ngoại cho bảng STORE
	PRINT '16. Thêm khóa ngoại cho bảng STORE';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_STORE_CATEGORY')
	BEGIN
		ALTER TABLE [dbo].[STORE] WITH CHECK ADD FOREIGN KEY([CATEGORY_ID])
		REFERENCES [dbo].[STORE_CATEGORY] ([STORE_CATEGORY_ID]);
		PRINT 'Foreign key FK_STORE_STORE_CATEGORY added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_STORE_CATEGORY already exists.';

	-- Khóa ngoại cho bảng STORE_OWNER
	PRINT '17. Thêm khóa ngoại cho bảng STORE_OWNER';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_OWNER_USER')
	BEGIN
		ALTER TABLE [dbo].[STORE_OWNER] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_STORE_OWNER_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_OWNER_USER already exists.';

	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_OWNER_STORE')
	BEGIN
		ALTER TABLE [dbo].[STORE_OWNER] WITH CHECK ADD FOREIGN KEY([STORE_ID])
		REFERENCES [dbo].[STORE] ([STORE_ID]);
		PRINT 'Foreign key FK_STORE_OWNER_STORE added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_OWNER_STORE already exists.';
	
	PRINT '18. Thêm khóa ngoại cho bảng [ORDER]';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ORDER_STORE')
	BEGIN
		ALTER TABLE [dbo].[ORDER] WITH CHECK ADD FOREIGN KEY([STORE_ID])
		REFERENCES [dbo].[STORE] ([STORE_ID]);
		PRINT 'Foreign key FK_ORDER_STORE added.';
	END
	ELSE
		PRINT 'Foreign key FK_ORDER_STORE already exists.';

	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ORDER_CARD')
	BEGIN
		ALTER TABLE [dbo].[ORDER] WITH CHECK ADD FOREIGN KEY([CARD_ID])
		REFERENCES [dbo].[CARD] ([CARD_ID]);
		PRINT 'Foreign key FK_ORDER_CARD added.';
	END
	ELSE
		PRINT 'Foreign key FK_ORDER_CARD already exists.';

	-- Khóa ngoại cho bảng STORE_WITHDRAWAL
	PRINT '19. Thêm khóa ngoại cho bảng STORE_WITHDRAWAL';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_WITHDRAWAL_STORE')
	BEGIN
		ALTER TABLE [dbo].[STORE_WITHDRAWAL] WITH CHECK ADD FOREIGN KEY([STORE_ID])
		REFERENCES [dbo].[STORE] ([STORE_ID]);
		PRINT 'Foreign key FK_STORE_WITHDRAWAL_STORE added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_WITHDRAWAL_STORE already exists.';
		COMMIT;
		PRINT 'Transaction committed.';
	
	PRINT '20. Thêm khóa ngoại cho bảng ADMIN';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ADMIN_USER')
	BEGIN
		ALTER TABLE [dbo].[ADMIN] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_ADMIN_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_ADMIN_USER already exists.';

	-- Khóa ngoại cho bảng CARD
	PRINT '21. Thêm khóa ngoại cho bảng CARD';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_CUSTOMER')
	BEGIN
		ALTER TABLE [dbo].[CARD] WITH CHECK ADD FOREIGN KEY([CUSTOMER_ID])
		REFERENCES [dbo].[CUSTOMER] ([CUSTOMER_ID]);
		PRINT 'Foreign key FK_CARD_CUSTOMER added.';
	END
	ELSE
		PRINT 'Foreign key FK_CARD_CUSTOMER already exists.';

	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CARD_CARD_TYPE')
	BEGIN
		ALTER TABLE [dbo].[CARD] WITH CHECK ADD FOREIGN KEY([CARD_TYPE_ID])
		REFERENCES [dbo].[CARD_TYPE] ([CARD_TYPE_ID]);
		PRINT 'Foreign key FK_CARD_CARD_TYPE added.';
	END
	ELSE
		PRINT 'Foreign key FK_CARD_CARD_TYPE already exists.';

	-- Khóa ngoại cho bảng CUSTOMER
	PRINT '22. Thêm khóa ngoại cho bảng CUSTOMER';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CUSTOMER_USER')
	BEGIN
		ALTER TABLE [dbo].[CUSTOMER] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_CUSTOMER_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_CUSTOMER_USER already exists.';
		
		PRINT '23. Thêm khóa ngoại cho bảng [ORDER]';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ORDER_STORE')
	BEGIN
		ALTER TABLE [dbo].[ORDER] WITH CHECK ADD FOREIGN KEY([STORE_ID])
		REFERENCES [dbo].[STORE] ([STORE_ID]);
		PRINT 'Foreign key FK_ORDER_STORE added.';
	END
	ELSE
		PRINT 'Foreign key FK_ORDER_STORE already exists.';

	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ORDER_CARD')
	BEGIN
		ALTER TABLE [dbo].[ORDER] WITH CHECK ADD FOREIGN KEY([CARD_ID])
		REFERENCES [dbo].[CARD] ([CARD_ID]);
		PRINT 'Foreign key FK_ORDER_CARD added.';
	END
	ELSE
		PRINT 'Foreign key FK_ORDER_CARD already exists.';

	-- Khóa ngoại cho bảng STORE
	PRINT '24. Thêm khóa ngoại cho bảng STORE';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_STORE_CATEGORY')
	BEGIN
		ALTER TABLE [dbo].[STORE] WITH CHECK ADD FOREIGN KEY([CATEGORY_ID])
		REFERENCES [dbo].[STORE_CATEGORY] ([STORE_CATEGORY_ID]);
		PRINT 'Foreign key FK_STORE_STORE_CATEGORY added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_STORE_CATEGORY already exists.';

	-- Khóa ngoại cho bảng STORE_OWNER
	PRINT '25. Thêm khóa ngoại cho bảng STORE_OWNER';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_OWNER_USER')
	BEGIN
		ALTER TABLE [dbo].[STORE_OWNER] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_STORE_OWNER_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_OWNER_USER already exists.';

	-- Khóa ngoại cho bảng STORE_WITHDRAWAL
	PRINT '26. Thêm khóa ngoại cho bảng STORE_WITHDRAWAL';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_STORE_WITHDRAWAL_STORE')
	BEGIN
		ALTER TABLE [dbo].[STORE_WITHDRAWAL] WITH CHECK ADD FOREIGN KEY([STORE_ID])
		REFERENCES [dbo].[STORE] ([STORE_ID]);
		PRINT 'Foreign key FK_STORE_WITHDRAWAL_STORE added.';
	END
	ELSE
		PRINT 'Foreign key FK_STORE_WITHDRAWAL_STORE already exists.';
		-- Khóa ngoại cho bảng TOPUP_MEMBER
	PRINT '28. Thêm khóa ngoại cho bảng TOPUP_MEMBER';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_TOPUP_MEMBER_USER')
	BEGIN
		ALTER TABLE [dbo].[TOPUP_MEMBER] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_TOPUP_MEMBER_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_TOPUP_MEMBER_USER already exists.';

	-- Khóa ngoại cho bảng ADMIN
	PRINT '29. Thêm khóa ngoại cho bảng ADMIN';
	IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_ADMIN_USER')
	BEGIN
		ALTER TABLE [dbo].[ADMIN] WITH CHECK ADD FOREIGN KEY([USER_ID])
		REFERENCES [dbo].[USER] ([USER_ID]);
		PRINT 'Foreign key FK_ADMIN_USER added.';
	END
	ELSE
		PRINT 'Foreign key FK_ADMIN_USER already exists.';
END TRY
BEGIN CATCH
    PRINT 'Error occurred. Rolling back transaction.';
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH;
