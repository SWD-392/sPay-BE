ALTER TABLE [USER]
ALTER COLUMN INS_DATE DATETIME NOT NULL

ALTER TABLE [ADMIN]
ALTER COLUMN CREATE_AT DATETIME NOT NULL

ALTER TABLE [ORDER]
ALTER COLUMN [DATE] DATETIME NOT NULL

ALTER TABLE [STORE_WITHDRAWAL]
ALTER COLUMN [DATE] DATETIME NOT NULL

DELETE
FROM STORE_CATEGORY WHERE STORE_CATEGORY_KEY = 'SC_5'

TRUNCATE TABLE CARD_STORE_CATEGORY;

INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_1_N', N'Ẩm thực Normal', N'Loại thẻ ẩm thực không cho phép rút tiền', 0)
INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_1_P', N'Ẩm thực Premium', N'Loại thẻ ẩm thực cho phép rút tiền', 1)

INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_2_N', N'Đồ gia dụng Normal', N'Loại thẻ đồ gia dụng không cho phép rút tiền', 0)
INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_2_P', N'Đồ gia dụng Premium', N'Loại thẻ đồ gia dụng cho phép rút tiền', 1)

INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_3_N', N'Phụ kiện điện tử Normal', N'Loại thẻ phụ kiện điện tử không cho phép rút tiền', 0)
INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_3_P', N'Phụ kiện điện tử Premium', N'Loại thẻ phụ kiện điện tử cho phép rút tiền', 1)

INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_4_N', N'Thời trang Normal', N'Loại thẻ thời trang không cho phép rút tiền', 0)
INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_4_P', N'Thời trang Premium', N'Loại thẻ thời trang cho phép rút tiền', 1)

INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_5_N', N'Trang sức Normal', N'Loại thẻ trang sức không cho phép rút tiền', 0)
INSERT [dbo].[CARD_TYPE] ([CARD_TYPE_KEY], [CARD_TYPE_NAME], [TYPE_DESCRIPTION], [WITHDRAWAL_ALLOWED]) VALUES (N'CT_SC_5_P', N'Trang sức Premium', N'Loại thẻ trang sức cho phép rút tiền', 1)