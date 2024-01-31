CREATE TABLE [dbo].[tbl_Store](
	[store_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[categoryid] [int] NULL,
	[location] [nvarchar](max) NULL,
	[phone] [int] NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[store_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[tbl_Order](
	[orderid] [int] IDENTITY(1,1) NOT NULL,
	[storeid] [int] NULL,
	[cardid] [int] NULL,
	[amount] [int] NULL,
	[date] [date] NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[tbl_StoreWithDrawal](
	[store_with_drawal_id] [int] IDENTITY(1,1) NOT NULL,
	[storeid] [int] NULL,
	[amount] [int] NULL,
	[date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[store_with_drawal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
